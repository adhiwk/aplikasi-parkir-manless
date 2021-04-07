Imports Emgu.CV
Imports Emgu.CV.Structure
Imports System.Drawing.Printing
Imports System.Data.SqlClient
Imports System.IO.Ports
Imports System.IO
Imports DevExpress.XtraEditors.Controls


Public Class frmManLessIn
    'Device port
    Private mySerialPort As New SerialPort
    Dim cOutput As String = ""

    'VARIABEL UMUM
    Dim cKodeShift As String = ""
    Dim cKodeKunci As String = ""
    Dim cDtime As Date = Now.Date

    'PRINTER SECTION
    '*********************************************************
    ReadOnly smallfont As New Font("Tahoma", 9)
    ReadOnly boldfont As New Font("Tahoma", 12)
    ReadOnly barcodefont As New Font("IDAHC39M Code 39 Barcode", 16)
    Private prn As PrintDocument

    Private Sub PrintCasual()
        Try
            prn = New PrintDocument
            AddHandler prn.PrintPage, AddressOf Me.PrintTicket

            prn.Print()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function PrintTicket(ByVal sender As Object, ByVal e As PrintPageEventArgs) As Boolean
        Dim sf As New StringFormat() With {.FormatFlags = StringFormatFlags.NoWrap}

        e.Graphics.DrawString("KURA-KURA FAMILY ENTERTAINMENT", smallfont, Brushes.Black, New PointF(19, 30), sf)
        e.Graphics.DrawString("JLN. SRIWIJAYA No. 95 MATARAM", smallfont, Brushes.Black, New PointF(19, 45), sf)
        e.Graphics.DrawString("--------------------------------------------", smallfont, Brushes.Black, New PointF(19, 60), sf)
        e.Graphics.DrawString("*" & txtNomorTiket.Text.Trim & "*", barcodefont, Brushes.Black, New PointF(19, 75), sf)
        e.Graphics.DrawString(txtKodeJenisKendaraan.Text.Trim & "-" & cKodeKunci.Trim, boldfont, Brushes.Black, New PointF(19, 160), sf)
        e.Graphics.DrawString("Tanggal : " & txtTanggal.Text.Trim, smallfont, Brushes.Black, New PointF(19, 180), sf)
        e.Graphics.DrawString("Jam     : " & txtJam.Text.Trim, smallfont, Brushes.Black, New PointF(19, 200), sf)
        e.Graphics.DrawString("--------------------------------------------", smallfont, Brushes.Black, New PointF(19, 240), sf)
        e.Graphics.DrawString("Selamat datang, simpan tiket anda           ", smallfont, Brushes.Black, New PointF(19, 280), sf)
        e.Graphics.DrawString("Kehilangan tiket akan dikenakan denda       ", smallfont, Brushes.Black, New PointF(19, 300), sf)

        e.HasMorePages = False
        Return True
    End Function
    '***************************** end print section ***********************************************

    Private Sub frmManLessIn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler mySerialPort.DataReceived, AddressOf DataReceived
        Me.WindowState = FormWindowState.Maximized

        txtNomorTiket.Text = ""
        txtJenisKendaraan.Text = ""
        txtKodeJenisKendaraan.Text = My.Settings.kode_kendaraan.Trim
        txtTanggal.Text = ""
        txtJam.Text = ""
        CommPortSetup()
        LoadDefaultData()
    End Sub

    Private Sub LoadDefaultData()
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select getdate() as [Tanggal]"
                    cDtime = Date.Parse(.ExecuteScalar)
                    txtTanggal.Text = Format(cDtime, "dd-MM-yyyy")
                    txtJam.Text = Format(cDtime, "HH:mm:ss")
                End With
            End Using
        Catch
        End Try
        Conn.Close()
    End Sub


    'Private Sub ctlrTimer_Tick(sender As Object, e As EventArgs) Handles ctlrTimer.Tick
    '    Try
    '        txtJam.Text = Now.TimeOfDay.Hours.ToString.PadLeft(2, "0") &
    '        ":" & Now.TimeOfDay.Minutes.ToString.PadLeft(2, "0") &
    '        ":" & Now.TimeOfDay.Seconds.ToString.PadLeft(2, "0")


    '        'cOutput = mySerialPort.ReadExisting.ToString.Trim
    '        'If cOutput = "ON" Then
    '        '    ProsesParkir()
    '        'End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Function GenerateOTP() As String
        'Dim alphabets As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        'Dim small_alphabets As String = "abcdefghijklmnopqrstuvwxyz"
        'Dim numbers As String = "1234567890"

        Dim characters As String = "1234567890"
        Dim length As Integer = 6
        Dim otp As String = String.Empty
        For i As Integer = 0 To length - 1
            Dim character As String = String.Empty
            Do
                Dim index As Integer = New Random().Next(0, characters.Length)
                character = characters.ToCharArray()(index).ToString()
            Loop While otp.IndexOf(character) <> -1
            otp += character
        Next
        Return otp.Trim
    End Function

    Private Sub QueryDefaultShift()
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn

                    .CommandText = "select [kd shift],[keterangan],[jam mulai],[jam akhir] from shift where [status] = 'Y'"
                    Dim rdr As SqlDataReader = .ExecuteReader

                    While rdr.Read
                        cKodeShift = rdr.Item(0).ToString.Trim
                    End While

                    rdr.Close()
                    cmd.Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Conn.Close()
    End Sub

    Private Sub CommPortSetup()
        If mySerialPort.IsOpen = True Then
            mySerialPort.Close()
        End If
        With mySerialPort
            .PortName = My.Settings.com_port_in.Trim
            .BaudRate = My.Settings.baud_rate.Trim
            .DataBits = 8
            .Parity = Parity.None
            .StopBits = StopBits.One
            .Handshake = Handshake.RequestToSend
            .ReadTimeout = 500
        End With

        Try
            mySerialPort.Open()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs)
        If e.EventType = IO.Ports.SerialData.Chars Then ShowReceived()
    End Sub

    Private Sub ShowReceived()
        If Me.InvokeRequired Then
            'invoke the thread
            Me.Invoke(New MethodInvoker(AddressOf ShowReceived))
        Else
            cOutput = mySerialPort.ReadExisting.ToString.Trim
            If cOutput.Trim = "S" Then
                My.Computer.Audio.Play(Application.StartupPath & "\sounds\welcome.wav")
            ElseIf cOutput.Trim = "P" Then
                ProsesParkir()
            End If
        End If
    End Sub

    Private Function GenerateTiket() As String
        Dim alphabets As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim numbers As String = "1234567890"

        Dim characters As String = ""
        characters += Convert.ToString(alphabets) & numbers & UCase(Simple3Des.GetMd5Hash(txtTanggal.Text.Trim & txtJam.Text.Trim))

        Dim length As Integer = 6
        Dim otp As String = String.Empty
        For i As Integer = 0 To length - 1
            Dim character As String = String.Empty
            Do
                Dim index As Integer = New Random().Next(0, characters.Length)
                character = characters.ToCharArray()(index).ToString()
            Loop While otp.IndexOf(character) <> -1
            otp += character
        Next

        'validasi nomor tiket
        'jika sudah tersimpan didatabase
        'generate ulang nomor tiket
        If ValidasiTiket(otp.Trim) Then
            GenerateTiket()
        Else
            Return otp.Trim
        End If
        Return otp.Trim
    End Function

    Private Sub ProsesParkir()
        If cOutput.Trim = "P" Then

            txtNomorTiket.Text = GenerateTiket.Trim
            cKodeKunci = GenerateOTP.Trim

            'IMAGE CAPTURE
            Dim Capturez As VideoCapture
            If My.Settings.cam_type.Trim = "IP_CAM" Then
                Capturez = New VideoCapture(My.Settings.cam_link.Trim)
                Dim Images As Image(Of Bgr, Byte) = Capturez.QueryFrame.ToImage(Of Bgr, Byte)
                peCapture.Image = Images.ToBitmap
                peCapture.Properties.SizeMode = PictureSizeMode.Stretch
                Capturez.Dispose()
            ElseIf My.Settings.cam_type.Trim = "IMAGING_DEVICE" Then
                Capturez = New VideoCapture()
                Dim Images As Image(Of Bgr, Byte) = Capturez.QueryFrame.ToImage(Of Bgr, Byte)
                peCapture.Image = Images.ToBitmap
                peCapture.Properties.SizeMode = PictureSizeMode.Stretch
                Capturez.Dispose()
            Else
                Capturez = Nothing
                Dim fs As FileStream = New FileStream(Application.StartupPath & "\noimageavailable.png", FileMode.Open)
                Dim img As Byte()
                img = Nothing
                img = New Byte(fs.Length) {}
                fs.Read(img, 0, fs.Length)
                fs.Close()
                peCapture.Image = byteArrayToImage(img)
            End If

            'SAVE DATA
            Dim Conn As SqlConnection = KoneksiSQL()
            Conn.Open()
            Try
                Using cmd As New SqlCommand
                    With cmd
                        .Connection = Conn
                        .CommandText = "select getdate() as [Tanggal]"
                        cDtime = Date.Parse(.ExecuteScalar)

                        .CommandText = "select [kd shift],[keterangan],[jam mulai],[jam akhir] " &
                                       "from shift where datepart(HH,[jam mulai]) <= " &
                                       cDtime.Hour & " and datepart(HH,[jam akhir]) >= " &
                                       cDtime.Hour

                        Dim rdr As SqlDataReader = .ExecuteReader
                        While rdr.Read
                            cKodeShift = rdr.Item(0).ToString.Trim
                        End While

                        '******* query kode shift diatas jam 12 malam
                        If rdr.HasRows = False Then
                            QueryDefaultShift()
                        End If
                        rdr.Close()

                        '****************************************************************
                        'save data parkir masuk
                        '****************************************************************
                        .CommandText = "add_parkir_masuk"
                        .CommandType = Data.CommandType.StoredProcedure
                        .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                        .Parameters(0).SqlDbType = Data.SqlDbType.NChar
                        .Parameters(0).Direction = Data.ParameterDirection.Output

                        .Parameters.Add(New SqlParameter("@mPROCESS", Data.SqlDbType.NChar, 6)).Value = "ADD"
                        .Parameters.Add(New SqlParameter("@nomor_transaksi", Data.SqlDbType.NChar, 25)).Value = txtNomorTiket.Text.Trim
                        .Parameters.Add(New SqlParameter("@kode_kunci", Data.SqlDbType.NChar, 6)).Value = cKodeKunci.Trim
                        .Parameters.Add(New SqlParameter("@gerbang_masuk", Data.SqlDbType.NChar, 50)).Value = My.Settings.gerbang.Trim
                        .Parameters.Add(New SqlParameter("@tgl_masuk", Data.SqlDbType.Char, 20)).Value = Format(cDtime, "yyyy-MM-dd HH:mm:ss")
                        .Parameters.Add(New SqlParameter("@jam_masuk", Data.SqlDbType.Char, 10)).Value = Format(cDtime, "HH:mm:ss")
                        .Parameters.Add(New SqlParameter("@kd_jenis", Data.SqlDbType.Char, 10)).Value = txtKodeJenisKendaraan.Text.Trim
                        .Parameters.Add(New SqlParameter("@petugas_masuk", Data.SqlDbType.Char, 10)).Value = "manless"
                        .Parameters.Add(New SqlParameter("@kd_shift_in", Data.SqlDbType.Char, 10)).Value = cKodeShift.Trim
                        .Parameters.Add(New SqlParameter("@img_in", Data.SqlDbType.Image)).Value = imgToByteArray(clsImageProcsesing.ResizeImage(peCapture.Image, 400, 300))
                        .ExecuteNonQuery()

                        txtTanggal.Text = Format(cDtime, "dd-MM-yyyy")
                        txtJam.Text = Format(cDtime, "HH:mm:ss")

                        PrintCasual()
                    End With
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Conn.Close()
            cOutput = ""
        End If
        AddHandler mySerialPort.DataReceived, AddressOf DataReceived
    End Sub

    Private Sub txtKodeJenisKendaraan_EditValueChanged(sender As Object, e As EventArgs) Handles txtKodeJenisKendaraan.EditValueChanged
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select [keterangan] " &
                                   "from jenis_kendaraan where [kd jenis] = '" & txtKodeJenisKendaraan.Text.Trim & "'"
                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        txtJenisKendaraan.Text = rdr.Item(0).ToString.Trim
                    End While
                    rdr.Close()
                End With
            End Using
        Catch
        End Try
        Conn.Close()
    End Sub

    Private Sub frmManLessIn_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        If mySerialPort.IsOpen Then
            RemoveHandler mySerialPort.DataReceived, AddressOf DataReceived
            mySerialPort.Close()
        End If
    End Sub

    Private Sub frmManLessIn_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        AddHandler mySerialPort.DataReceived, AddressOf DataReceived
    End Sub

    Private Sub frmManLessIn_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            cOutput = "P"
            ProsesParkir()
        End If
    End Sub

    Private Function ValidasiTiket(ByVal cNomorTiket As String) As Boolean
        Dim lFound As Boolean
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select [nomor transaksi] " &
                                   "from parkir where [nomor transaksi] = '" & cNomorTiket.Trim & "'"
                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        If rdr.Item(0).ToString.Trim = cNomorTiket.Trim Then
                            lFound = True
                        End If
                    End While

                    If rdr.HasRows = False Then
                        lFound = False
                    End If
                    rdr.Close()
                End With
            End Using
        Catch
        End Try
        Conn.Close()
        Return lFound
    End Function

End Class