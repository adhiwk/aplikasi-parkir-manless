Imports Emgu.CV
Imports Emgu.CV.Structure
Imports System.IO.Ports
Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing.Printing
Public Class frmManLessOut
    Dim lTransaction As Boolean
    Dim cKodeLokasi As String
    Dim cKodeAlamat As String
    Dim cPerusahaan As String
    Dim cAlamat As String

    Dim cMemberOverTime As String
    Dim cMemberTarifOvertime As String
    Dim cMemberByNopol As String

    Dim cKodeArea As String
    Dim cInfo As String
    Dim cKodeKunci As String
    Dim cJenis As String
    Dim nTarif As Integer
    Dim cDateTime As Date
    Dim cDateKeluar As Date
    Dim nJam, nMenit, nDetik As Integer

    Dim nDenda As Integer
    Dim cKodeShift As String
    Dim nTarifOvertime As Integer
    Dim nOvertime As Integer
    Dim lStatMember As Boolean
    Dim cKodeMember As String

    'ARDUINO 
    Private mySerialPort As New SerialPort
    Dim cInput As String = ""

    'PRINTER SECTION
    '*********************************************************
    ReadOnly smallfont As New Font("Tahoma", 9)
    ReadOnly boldfont As New Font("Tahoma", 14)
    ReadOnly barcodefont As New Font("IDAHC39M Code 39 Barcode", 16)

    Private PrintString As String
    Private PrintPageSettings As New PageSettings

    Private Sub PrintControler_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintControler.PrintPage

    End Sub

    Private Sub PrintCasual()
        Try
            AddHandler PrintControler.PrintPage, AddressOf Me.PrintTicket
            PrintControler.DefaultPageSettings = PrintPageSettings
            PrintString = " "
            PrintControler.Print()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function PrintTicket(ByVal sender As Object, ByVal e As PrintPageEventArgs) As Boolean
        Dim sf As New StringFormat() With {.FormatFlags = StringFormatFlags.NoWrap}

        e.Graphics.DrawString("KURA-KURA FAMILY ENTERTAIMENT", smallfont, Brushes.Black, New PointF(19, 30), sf)
        e.Graphics.DrawString("JLN. SRIWIJAYA No. 95 MATARAM", smallfont, Brushes.Black, New PointF(19, 45), sf)
        e.Graphics.DrawString("--------------------------------------------", smallfont, Brushes.Black, New PointF(19, 60), sf)
        e.Graphics.DrawString("*" & txtNomorTiket.Text.Trim & "*", barcodefont, Brushes.Black, New PointF(19, 75), sf)
        'e.Graphics.DrawString(txtNomorTiket.Text.Trim, smallfont, Brushes.Black, New PointF(19, 118), sf)
        e.Graphics.DrawString(cKodeKunci.Trim, boldfont, Brushes.Black, New PointF(19, 160), sf)
        e.Graphics.DrawString("Tanggal : ", smallfont, Brushes.Black, New PointF(19, 180), sf)
        e.Graphics.DrawString("Jam     : ", smallfont, Brushes.Black, New PointF(19, 200), sf)
        e.Graphics.DrawString("--------------------------------------------", smallfont, Brushes.Black, New PointF(19, 240), sf)
        e.Graphics.DrawString("Selamat Datang, simpan tiket anda           ", smallfont, Brushes.Black, New PointF(19, 280), sf)
        e.Graphics.DrawString("Kehilangan tiket akan dikenakan denda       ", smallfont, Brushes.Black, New PointF(19, 300), sf)

        e.HasMorePages = False
        Return True
    End Function
    '***************************** end print section ***********************************************

    Private Sub CommPortSetup()
        With mySerialPort
            .PortName = My.Settings.com_port_out.Trim
            .BaudRate = My.Settings.baud_rate.Trim
            .DataBits = 8
            .Parity = Parity.None
            .StopBits = StopBits.One
            .Handshake = Handshake.None
        End With

        Try
            mySerialPort.Open()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmParkirKeluarManLess_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        cDateTime = Date.Parse(GetDate)
        cDateKeluar = Date.Parse(GetDate)
        txtKodeJenisKendaraan.Text = My.Settings.kode_kendaraan.Trim
        LoadDataDefault()
        DefaultSetting()
        CommPortSetup()
        txtNomorPolisi.Focus()
    End Sub

    Private Sub UpdateParkir()
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "add_parkir_keluar"
                    .CommandType = Data.CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = Data.SqlDbType.NChar
                    .Parameters(0).Direction = Data.ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", Data.SqlDbType.NChar, 6)).Value = "UPDATE"
                    .Parameters.Add(New SqlParameter("@nomor_transaksi", Data.SqlDbType.NChar, 25)).Value = txtNomorTiket.Text.Trim
                    .Parameters.Add(New SqlParameter("@nopol", Data.SqlDbType.NChar, 10)).Value = txtNomorPolisi.Text.Trim
                    .Parameters.Add(New SqlParameter("@gerbang_keluar", Data.SqlDbType.NChar, 50)).Value = My.Settings.gerbang.Trim
                    .Parameters.Add(New SqlParameter("@tgl_keluar", Data.SqlDbType.Char, 10)).Value = Format(cDateKeluar, "yyyy-MM-dd")
                    .Parameters.Add(New SqlParameter("@jam_keluar", Data.SqlDbType.Char, 10)).Value = Format(cDateTime, "HH:mm:ss")
                    .Parameters.Add(New SqlParameter("@jam", Data.SqlDbType.Int)).Value = Integer.Parse(nJam)
                    .Parameters.Add(New SqlParameter("@menit", Data.SqlDbType.Int)).Value = Integer.Parse(nMenit)
                    .Parameters.Add(New SqlParameter("@detik", Data.SqlDbType.Int)).Value = Integer.Parse(nDetik)
                    .Parameters.Add(New SqlParameter("@tarif_keluar", Data.SqlDbType.Decimal, 18, 0)).Value = Decimal.Parse(nTarif)
                    .Parameters.Add(New SqlParameter("@overtime", Data.SqlDbType.Int)).Value = Integer.Parse(nOvertime)
                    .Parameters.Add(New SqlParameter("@tarif_overtime", Data.SqlDbType.Decimal, 18, 0)).Value = Decimal.Parse(nTarifOvertime)
                    .Parameters.Add(New SqlParameter("@denda", Data.SqlDbType.Decimal, 18, 0)).Value = Decimal.Parse(nDenda)
                    .Parameters.Add(New SqlParameter("@info", Data.SqlDbType.Char, 2)).Value = cInfo.Trim
                    .Parameters.Add(New SqlParameter("@jenis", Data.SqlDbType.Char, 1)).Value = cJenis.Trim
                    .Parameters.Add(New SqlParameter("@kode_member", Data.SqlDbType.Char, 25)).Value = cKodeMember.Trim
                    .Parameters.Add(New SqlParameter("@petugas_keluar", Data.SqlDbType.Char, 10)).Value = frmLogin.txtUser.Text.Trim
                    .Parameters.Add(New SqlParameter("@kd_shift_out", Data.SqlDbType.Char, 10)).Value = cKodeShift.Trim
                    .Parameters.Add(New SqlParameter("@img_out", Data.SqlDbType.Image)).Value = imgToByteArray(clsImageProcsesing.ResizeImage(peCaptureOut.Image, 400, 300))
                    .ExecuteNonQuery()

                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Conn.Close()
    End Sub
    Private Sub txtKey_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKey.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtNomorPolisi.Text.Trim = "" Then
                MsgBox("Nomor Polisi tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
                txtNomorPolisi.Text = ""
                txtNomorPolisi.Focus()
                Exit Sub
            End If

            If UCase(txtKey.Text.Trim) = "HL" Then

                'load image kendaraaan keluar
                Dim Capturez As VideoCapture
                If My.Settings.cam_type.Trim = "IP_CAM" Then
                    Capturez = New VideoCapture(My.Settings.cam_link.Trim)
                    Dim Images As Image(Of Bgr, Byte) = Capturez.QueryFrame.ToImage(Of Bgr, Byte)
                    peCaptureOut.Image = Images.ToBitmap
                    peCaptureOut.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
                ElseIf My.Settings.cam_type.Trim = "IMAGING_DEVICE" Then
                    Capturez = New VideoCapture()
                    Dim Images As Image(Of Bgr, Byte) = Capturez.QueryFrame.ToImage(Of Bgr, Byte)
                    peCaptureOut.Image = Images.ToBitmap
                    peCaptureOut.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
                Else
                    Capturez = Nothing
                    Dim fs As FileStream = New FileStream(Application.StartupPath & "\noimageavailable.png", FileMode.Open)
                    Dim img As Byte()
                    img = Nothing
                    img = New Byte(fs.Length) {}
                    fs.Read(img, 0, fs.Length)
                    fs.Close()
                    peCaptureOut.Image = byteArrayToImage(img)
                End If

                'load data denda berdasarkan jenis kendaraan
                Dim Conn As SqlConnection = KoneksiSQL()
                Conn.Open()
                Try
                    Using cmd As New SqlCommand
                        With cmd
                            .Connection = Conn
                            .CommandText = "select [tarif denda] from tarif where [kd jenis] = '" & txtKodeJenisKendaraan.Text.Trim & "'"

                            Dim rdr As SqlDataReader = .ExecuteReader
                            While rdr.Read
                                nDenda = Decimal.Parse(rdr.Item(0).ToString.Trim)
                            End While
                            rdr.Close()
                        End With
                    End Using
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                Conn.Close()

                Using cfrmDendaParkir As New frmDendaParkir
                    cfrmDendaParkir.img = imgToByteArray(peCaptureOut.Image)
                    cfrmDendaParkir.txtNopol.Text = txtNomorPolisi.Text.Trim
                    cfrmDendaParkir.txtDenda.Text = Decimal.Parse(nDenda)
                    cfrmDendaParkir.ShowDialog()
                    If cfrmDendaParkir.lStatDenda Then
                        If mySerialPort.IsOpen Then
                            mySerialPort.WriteLine("1")
                        End If
                    Else
                        MsgBox("Masukkan data denda terlebih dahulu")
                        DefaultSetting()
                        Exit Sub
                    End If
                End Using
            ElseIf Mid(cKodeKunci.Trim, 3, 6).ToString.Trim = txtKey.Text.Trim Then
                cInfo = ""
                nDenda = 0
                UpdateParkir()
                If mySerialPort.IsOpen Then
                    mySerialPort.WriteLine("1")
                End If
            Else
                MsgBox("Kode kunci tidak valid", MsgBoxStyle.Critical, "error")
                txtKey.Text = ""
                txtKey.Focus()
                Exit Sub
            End If

            DefaultSetting()
        End If
    End Sub


    Private Sub txtNomorPolisi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNomorPolisi.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If Asc(e.KeyChar) = 13 Then
            If txtNomorPolisi.Text.Trim <> "" Then
                If IsNumeric(Mid(txtNomorPolisi.Text.Trim, 1, 1)) Then
                    txtNomorPolisi.Text = cKodeArea.Trim & txtNomorPolisi.Text.Trim
                End If
            End If
        End If
    End Sub

    Private Sub HitungTarifParkirMember(ByVal ckdjmember As String)

        If cMemberByNopol.Trim = "Y" Then
            Dim cJenisTarif As String = ""
            Dim nTarifDasar As Decimal = 0
            Dim nPersenTarif As Integer = 0

            Dim Conn As SqlConnection = KoneksiSQL()
            Conn.Open()
            Try
                Using cmd As New SqlCommand
                    With cmd
                        .Connection = Conn

                        'query jenis member, ambil jenis tarif dan persen tarif
                        .CommandText = "select [jenis tarif],[persen tarif] from jenis_member " &
                                       "where [kdj member] = '" & ckdjmember.Trim & "'"

                        Dim rdr As SqlDataReader = .ExecuteReader
                        While rdr.Read
                            If rdr.Item(0).ToString.Trim = "J" Then
                                cJenisTarif = "J"
                            ElseIf rdr.Item(0).ToString.Trim = "P" Then
                                cJenisTarif = "P"
                            End If
                            nPersenTarif = Integer.Parse(rdr.Item(1).ToString.Trim)
                        End While
                        rdr.Close()

                        'jika jenis tarif berdasarkan jenis kendaraan
                        'query tarif pada tabel jenis member detail 
                        'sesuai dengan kode jenis kendaraan

                        If cJenisTarif.Trim = "J" Then
                            .CommandText = "select [tarif] from jenis_member_detail " &
                                           "where [kdj member] = '" & ckdjmember.Trim & "' and " &
                                           "[kd jenis] = '" & txtKodeJenisKendaraan.Text.Trim & "'"
                            rdr = .ExecuteReader
                            While rdr.Read
                                nTarifDasar = Decimal.Parse(rdr.Item(0).ToString.Trim)
                            End While
                            rdr.Close()

                            'jika jenis tarif berdasarkan persentase dari tarif dasar
                            'query tarif dasar sesuai jenis kendaraan dan kalikan dengan persentase 
                        ElseIf cJenisTarif.Trim = "P" Then
                            .CommandText = "select [tarif] from tarif where [kd jenis] = '" &
                            txtKodeJenisKendaraan.Text.Trim & "'"

                            rdr = .ExecuteReader
                            While rdr.Read
                                nTarifDasar = Decimal.Parse(rdr.Item(0).ToString.Trim) * nPersenTarif / 100
                            End While
                            rdr.Close()
                        End If

                        If cMemberOverTime.Trim = "Y" Then
                            'Hitung waktu overtime dan sesuaikan tarif sesuai pengaturan overtime pada tabel tarif
                            .CommandText = "select [overtime],[periode overtime],[tarif overtime]," &
                                           "[maksimal overtime] from tarif where [kd jenis] = '" &
                                txtKodeJenisKendaraan.Text.Trim & "'"
                            rdr = .ExecuteReader
                            nTarifOvertime = 0
                            nOvertime = 0
                            While rdr.Read
                                'Jika jam lebih besar dari batas awal overtime
                                If Val(nJam) > Val(rdr.Item(0).ToString.Trim) Then
                                    'Bandingkan kelebihan jam dengan periode overtime
                                    'Jika kelebihan jam lebih besar dari periode overtime
                                    If (Val(nJam) - Val(rdr.Item(0).ToString.Trim)) > Val(rdr.Item(1).ToString.Trim) Then
                                        'Bagikan kelebihan jam dengan periode overtime
                                        'untuk mendapatkan nilai pengali overtime
                                        nOvertime = (Val(nJam) - Val(rdr.Item(0).ToString.Trim)) / Val(rdr.Item(1).ToString.Trim)
                                        'Jika modulus (hasil bagi) kelebihan jam dengan periode overtime tidak sama dengan nol
                                        'tambahkan nilai pengali dengan satu
                                        If (Val(nJam) - Val(rdr.Item(0).ToString.Trim)) Mod Val(rdr.Item(1).ToString.Trim) <> 0 Then
                                            nOvertime = nOvertime + 1
                                        End If
                                        'Jika nilai pengali lebih besar dari maksimal overtime yg ditentukan
                                        'maka nilai pengali disesuaikan dengan maksimal overtime
                                        If Val(nOvertime) > Val(rdr.Item(1).ToString.Trim) Then
                                            nOvertime = Val(rdr.Item(1).ToString.Trim)
                                        End If
                                        'jika nilai pengali lebih kecil atau sama dengan periode overtime
                                        'maka nilai pengali sama dengan satu
                                    ElseIf (Val(nJam) - Val(rdr.Item(0).ToString.Trim)) <= Val(rdr.Item(1).ToString.Trim) Then
                                        nOvertime = 1
                                    End If
                                ElseIf Val(nJam) = Val(rdr.Item(0).ToString.Trim) Then 'Jika jam sama dengan periode overtime
                                    'cek apakah menit lebih besar dari 0
                                    If Val(nMenit) > 0 Then
                                        nOvertime = 1
                                    Else
                                        If Val(nDetik) > 0 Then
                                            nOvertime = 1
                                        End If
                                    End If
                                End If

                                If cMemberTarifOvertime.Trim = "STATIS" Then
                                    nTarifOvertime = Val(nOvertime) * Decimal.Parse(rdr.Item(2).ToString.Trim)
                                ElseIf cMemberTarifOvertime.Trim = "DINAMIS" Then
                                    nTarifOvertime = Val(nOvertime) * Decimal.Parse(nTarifDasar)
                                End If

                            End While
                            rdr.Close()

                        ElseIf cMemberOverTime.Trim = "T" Then
                            nOvertime = 0
                            nTarifOvertime = 0
                        End If

                    End With
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Conn.Close()

            cJenis = "P"
            nTarif = Decimal.Parse(nTarifDasar)
            txtTarif.Properties.ReadOnly = True
            txtKembali.Properties.ReadOnly = True
            txtBayar.Properties.ReadOnly = False
            txtKey.Properties.ReadOnly = False
            txtTarif.Text = Decimal.Parse(nTarif) + Decimal.Parse(nTarifOvertime)
            txtBayar.Focus()
        End If
    End Sub

    Private Sub HitungTarifParkirNonMember()
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select [tarif] from tarif where [kd jenis] ='" & txtKodeJenisKendaraan.Text.Trim & "'"
                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        nTarif = Val(rdr.Item(0).ToString.Trim)
                    End While
                    rdr.Close()

                    '*********************************************************************************************
                    '** Hitung waktu overtime dan sesuaikan tarif sesuai pengaturan overtime pada tabel tarif
                    '*********************************************************************************************
                    .CommandText = "select [overtime],[periode overtime],[tarif overtime]," &
                                   "[maksimal overtime] from tarif where [kd jenis] = '" &
                            txtKodeJenisKendaraan.Text.Trim & "'"
                    rdr = .ExecuteReader
                    While rdr.Read

                        'Jika jam lebih besar dari batas awal overtime
                        If Val(nJam) > Val(rdr.Item(0).ToString.Trim) Then

                            'Bandingkan kelebihan jam dengan periode overtime
                            'Jika kelebihan jam lebih besar dari periode overtime
                            If (Val(nJam) - Val(rdr.Item(0).ToString.Trim)) > Val(rdr.Item(1).ToString.Trim) Then

                                'Bagikan kelebihan jam dengan periode overtime
                                'untuk mendapatkan nilai pengali overtime
                                nOvertime = (Val(nJam) - Val(rdr.Item(0).ToString.Trim)) / Val(rdr.Item(1).ToString.Trim)

                                'Jika modulus (hasil bagi) kelebihan jam dengan periode overtime tidak sama dengan nol
                                'tambahkan nilai pengali dengan satu
                                If (Val(nJam) - Val(rdr.Item(0).ToString.Trim)) Mod Val(rdr.Item(1).ToString.Trim) <> 0 Then
                                    nOvertime = nOvertime + 1
                                End If

                                'Jika nilai pengali lebih besar dari maksimal overtime yg ditentukan
                                'maka nilai pengali disesuaikan dengan maksimal overtime
                                If Val(nOvertime) > Val(rdr.Item(1).ToString.Trim) Then
                                    nOvertime = Val(rdr.Item(1).ToString.Trim)
                                End If

                                'jika nilai pengali lebih kecil atau sama dengan periode overtime
                                'maka nilai pengali sama dengan satu
                            ElseIf (Val(nJam) - Val(rdr.Item(0).ToString.Trim)) <= Val(rdr.Item(1).ToString.Trim) Then
                                nOvertime = 1
                            End If

                        ElseIf Val(nJam) = Val(rdr.Item(0).ToString.Trim) Then 'Jika jam sama dengan periode overtime
                            'cek apakah menit lebih besar dari 0
                            If Val(nMenit) > 0 Then
                                nOvertime = 1
                            Else
                                If Val(nDetik) > 0 Then
                                    nOvertime = 1
                                End If
                            End If
                        End If
                        nTarifOvertime = Val(nOvertime) * Val(rdr.Item(2).ToString.Trim)
                    End While
                    rdr.Close()
                End With
            End Using
        Catch
        End Try
        Conn.Close()
        cJenis = "C"
        txtTarif.Properties.ReadOnly = True
        txtKembali.Properties.ReadOnly = True
        txtBayar.Properties.ReadOnly = False
        txtKey.Properties.ReadOnly = False
        txtTarif.Text = Decimal.Parse(nTarif) + Decimal.Parse(nTarifOvertime)
        txtBayar.Focus()
    End Sub

    Private Sub txtNomorTiket_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNomorTiket.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If Asc(e.KeyChar) = 13 Then
            'Try

            '**************
            'validasi nopol jika kosong abaikan
            If txtNomorPolisi.Text.Trim = "" Then
                MsgBox("Masukkan nomor polisi", MsgBoxStyle.Critical, "error")
                txtNomorPolisi.Focus()
                Exit Sub
            End If

            lTransaction = True
            lStatMember = False
            cKodeMember = ""

            'Deklarasi variabel
            Dim cDateMasuk As Date
            Dim cDateMember As Date
            Dim dTgl As Date
            Dim cJam, cMenit, cDetik As String
            Dim nGrassPeriode As Integer = 0
            Dim lGrassPeriode As Boolean = False
            Dim ckdjMember As String = ""


            nJam = 0
            nMenit = 0
            nDetik = 0

            nOvertime = 0
            nTarifOvertime = 0


            'ambil data parkir dan hitung waktu parkir

            Dim Conn As SqlConnection = KoneksiSQL()
            Conn.Open()

            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn


                    '*****************************************************************************************************************
                    '**** Cek avaibility data diarea parkir
                    '**** Hanya diperbolehkan nopol yg terdaftar diarea parkir
                    '*****************************************************************************************************************
                    .CommandText = "SELECT parkir.[nomor transaksi]," &
                                   "parkir.[kode kunci]," &
                                   "parkir.[tgl masuk]," &
                                   "parkir.[jam masuk]," &
                                   "parkir.[kd jenis]," &
                                   "tarif.[tarif] " &
                                   "FROM parkir parkir INNER JOIN tarif tarif ON parkir.[kd jenis] = tarif.[kd jenis] " &
                                   "Where parkir.[nomor transaksi] = '" & txtNomorTiket.Text.Trim & "' And [status] = 'T'"

                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        If txtNomorTiket.Text.Trim <> rdr.Item(0).ToString.Trim Then
                            MsgBox("tiket ini tidak terdaftar dalam database, silahkan hubungi administrator", MsgBoxStyle.Critical, "Error")
                            DefaultSetting()
                            Exit Sub
                        Else
                            txtKodeJenisKendaraan.Text = rdr.Item(4).ToString.Trim

                            dTgl = Date.Parse(rdr.Item(2).ToString.Trim)
                            cDateMasuk = Date.Parse(dTgl.Year.ToString.Trim & "-" & dTgl.Month.ToString.Trim & "-" & dTgl.Day.ToString.Trim & " " & rdr.Item(3).ToString.Trim)
                            cKodeKunci = rdr.Item(1).ToString.Trim

                            'Captute new image from cam
                            Dim Capturez As VideoCapture
                            If My.Settings.cam_type.Trim = "IP_CAM" Then
                                Capturez = New VideoCapture(My.Settings.cam_link.Trim)
                                Dim Images As Image(Of Bgr, Byte) = Capturez.QueryFrame.ToImage(Of Bgr, Byte)
                                peCaptureOut.Image = Images.ToBitmap
                                peCaptureOut.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
                            ElseIf My.Settings.cam_type.Trim = "IMAGING_DEVICE" Then
                                Capturez = New VideoCapture()
                                Dim Images As Image(Of Bgr, Byte) = Capturez.QueryFrame.ToImage(Of Bgr, Byte)
                                peCaptureOut.Image = Images.ToBitmap
                                peCaptureOut.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
                            Else
                                Capturez = Nothing
                                Dim fs As FileStream = New FileStream(Application.StartupPath & "\noimageavailable.png", FileMode.Open)
                                Dim img As Byte()
                                img = Nothing
                                img = New Byte(fs.Length) {}
                                fs.Read(img, 0, fs.Length)
                                fs.Close()
                                peCaptureOut.Image = byteArrayToImage(img)
                            End If

                        End If
                    End While

                    If rdr.HasRows = False Then
                        MsgBox("tiket ini tidak terdaftar dalam database, silahkan hubungi administrator", MsgBoxStyle.Critical, "Error")
                        DefaultSetting()
                        Exit Sub
                    End If
                    rdr.Close()

                    '*************************************************************************************
                    '** Load data image parkir masuk
                    '*************************************************************************************
                    .CommandText = "select [img_in] from parkir_image where [nomor transaksi] = '" & txtNomorTiket.Text.Trim & "'"
                    rdr = .ExecuteReader
                    While rdr.Read
                        'Load byte array and convert to image
                        peCaptureIn.Image = byteArrayToImage(rdr.Item(0))
                        peCaptureIn.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
                    End While
                    rdr.Close()

                    '*************************************************************************************
                    '** Ambil jam dari server utk menghitung lama parkir
                    '*************************************************************************************
                    .CommandText = "select getdate() as [Tanggal]"
                    rdr = .ExecuteReader
                    While rdr.Read
                        cDateTime = Date.Parse(rdr.Item(0).ToString.Trim)
                        cDateKeluar = Date.Parse(cDateTime.Year.ToString.Trim & "-" &
                                                 cDateTime.Month.ToString.Trim & "-" &
                                                 cDateTime.Day.ToString.Trim & " " &
                                                 cDateTime.Hour.ToString.Trim & ":" &
                                                 cDateTime.Minute.ToString.Trim & ":" &
                                                 cDateTime.Second.ToString.Trim)
                    End While
                    rdr.Close()

                    HitungJam2(cDateMasuk, cDateKeluar)

                    If Len(nJam.ToString.Trim) = 1 Then
                        cJam = "0" & nJam.ToString.Trim
                    Else
                        cJam = nJam.ToString.Trim
                    End If

                    If Len(nMenit.ToString.Trim) = 1 Then
                        cMenit = "0" & nMenit.ToString.Trim
                    Else
                        cMenit = nMenit.ToString.Trim
                    End If

                    If Len(nDetik.ToString.Trim) = 1 Then
                        cDetik = "0" & nDetik.ToString.Trim
                    Else
                        cDetik = nDetik.ToString.Trim
                    End If


                    txtLamaParkir.Text = cJam & " Jam " & cMenit & " Menit " & cDetik & " Detik"

                    '***************************************************************************************
                    '** Query shift kerja
                    '***************************************************************************************

                    .CommandText = "select [kd shift],[keterangan],[jam mulai],[jam akhir] from shift where datepart(HH,[jam mulai]) <= " &
                                               cDateTime.Hour.ToString.Trim & " and datepart(HH,[jam akhir]) >= " &
                                               cDateTime.Hour.ToString.Trim
                    rdr = .ExecuteReader

                    While rdr.Read
                        cKodeShift = rdr.Item(0).ToString.Trim
                    End While

                    '******* query kode shift diatas jam 12 malam
                    If rdr.HasRows = False Then
                        QueryDefaultShift()
                    End If
                    rdr.Close()


                    '***************************************************************************************
                    '** Query grass periode dari tabel tarif, jika waktu parkir diatas grass periode
                    '** tarif parkir sesuai tarif yang disimpan ditabel tarif, jika waktu parkir dibawah
                    '** grass parkir, tarif parkir = 0
                    '***************************************************************************************
                    .CommandText = "select [grass periode] from tarif where [kd jenis] = '" &
                                txtKodeJenisKendaraan.Text.Trim & "'"
                    rdr = .ExecuteReader
                    While rdr.Read
                        nGrassPeriode = Val(Integer.Parse(rdr.Item(0).ToString.Trim))
                    End While
                    rdr.Close()

                    'bandingkan waktu parkir dengan grass periode
                    If Val(nJam) <= 0 Then
                        If Val(nMenit) >= Val(nGrassPeriode) Then
                            lGrassPeriode = True
                        ElseIf Val(nMenit) < Val(nGrassPeriode) Then
                            lGrassPeriode = False
                        End If
                    Else
                        lGrassPeriode = True
                    End If

                    '********************************************************************************
                    'cek nopol apakah terdaftar sebagai member
                    'jika ya, cek expired date member.
                    'jika data member aktif proses tarif member
                    'pada proses grass period, jika tidak aktif
                    'pada grass priod proses tarif normal
                    '********************************************************************************
                    .CommandText = "SELECT member_detail.[nopol]," &
                                   "member.[exp date]," &
                                   "member.[kdj member],member.[kode member] " &
                                   "FROM member_detail member_detail INNER JOIN member " &
                                   "member ON member_detail.[kode member] = member.[kode member] " &
                                   "Where member_detail.[nopol] = '" & txtNomorPolisi.Text & "'"

                    rdr = .ExecuteReader
                    While rdr.Read
                        cDateMember = Date.Parse(rdr.Item(1).ToString.Trim)

                        'jika terdaftar dalam data member, cek apakah datanya belum expired
                        If txtNomorPolisi.Text.Trim = rdr.Item(0).ToString.Trim Then
                            'cek apakah tahun sesuai dengan tahun sekarang
                            'jika ya
                            If Val(cDateMember.Year.ToString.Trim) = Val(cDateTime.Year.ToString.Trim) Then
                                'cek apakah bulan sama dengan bulan skrg
                                'jika ya
                                If Val(cDateMember.Month.ToString.Trim) = Val(cDateTime.Month.ToString.Trim) Then
                                    'cek apakah tanggal lebih besar atau sama dengan tanggal sekarang
                                    'jika ya
                                    If Val(cDateMember.Day.ToString.Trim) >= Val(cDateTime.Day.ToString.Trim) Then
                                        lStatMember = True
                                    Else 'cek apakah tanggal lebih besar atau sama dengan tanggal sekarang, jika tidak
                                        lStatMember = False
                                    End If
                                ElseIf Val(cDateMember.Month.ToString.Trim) > Val(cDateTime.Month.ToString.Trim) Then
                                    lStatMember = True
                                ElseIf Val(cDateMember.Month.ToString.Trim) < Val(cDateTime.Month.ToString.Trim) Then
                                    lStatMember = False
                                End If
                            ElseIf Val(cDateMember.Year.ToString.Trim) > Val(cDateTime.Year.ToString.Trim) Then
                                lStatMember = True
                            ElseIf Val(cDateMember.Year.ToString.Trim) < Val(cDateTime.Year.ToString.Trim) Then
                                lStatMember = False
                            End If
                        Else
                            lStatMember = False
                        End If

                        ckdjMember = rdr.Item(2).ToString.Trim
                        cKodeMember = rdr.Item(3).ToString.Trim
                    End While
                    rdr.Close()

                    If lGrassPeriode Then
                        If lStatMember Then
                            HitungTarifParkirMember(ckdjMember.Trim)
                        Else
                            HitungTarifParkirNonMember()
                        End If
                    Else
                        nTarif = 0
                        nOvertime = 0
                        nTarifOvertime = 0
                        If lStatMember Then
                            cJenis = "P"
                            txtTarif.Properties.ReadOnly = True
                            txtKembali.Properties.ReadOnly = True
                            txtBayar.Properties.ReadOnly = False
                            txtKey.Properties.ReadOnly = False
                            txtTarif.Text = 0
                            txtBayar.Focus()
                        Else
                            cJenis = "C"
                            txtTarif.Properties.ReadOnly = True
                            txtKembali.Properties.ReadOnly = True
                            txtBayar.Properties.ReadOnly = False
                            txtKey.Properties.ReadOnly = False
                            txtTarif.Text = 0
                            txtBayar.Focus()
                        End If
                    End If
                End With
            End Using
            Conn.Close()
        End If
    End Sub

    Private Sub LoadDataDefault()
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select [keterangan] from jenis_kendaraan where [kd jenis] = '" & txtKodeJenisKendaraan.Text.Trim & "'"
                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        txtJenisKendaraan.Text = rDr.Item(0).ToString.Trim
                    End While
                    rDr.Close()


                    .CommandText = "select [member overtime],[member tarif overtime],[member by nopol] " &
                                   "from pengaturan"
                    rDr = .ExecuteReader
                    While rDr.Read
                        cMemberOverTime = rDr.Item(0).ToString.Trim
                        cMemberTarifOvertime = rDr.Item(1).ToString.Trim
                        cMemberByNopol = rDr.Item(2).ToString.Trim
                    End While
                    rDr.Close()

                    .CommandText = "select [area_nopol],[kode_lokasi],[perusahaan],[alamat] From info"
                    rDr = .ExecuteReader
                    While rDr.Read
                        cKodeArea = rDr.Item(0).ToString.Trim
                        cKodeLokasi = rDr.Item(1).ToString.Trim
                        cPerusahaan = rDr.Item(2).ToString.Trim
                        cAlamat = rDr.Item(3).ToString.Trim
                    End While
                    rDr.Close()

                    .CommandText = "select [kd shift],[keterangan],[jam mulai],[jam akhir] from shift where datepart(HH,[jam mulai]) <= " &
                                   cDateKeluar.Hour & " and datepart(HH,[jam akhir]) >= " &
                                   cDateKeluar.Hour
                    rDr = .ExecuteReader

                    While rDr.Read
                        cKodeShift = rDr.Item(0).ToString.Trim
                    End While

                    '******* query kode shift diatas jam 12 malam
                    If rDr.HasRows = False Then
                        QueryDefaultShift()
                    End If
                    rDr.Close()


                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally

        End Try
        Conn.Close()
    End Sub

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

                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Conn.Close()
    End Sub

    Private Sub txtNomorPolisi_LostFocus(sender As Object, e As EventArgs) Handles txtNomorPolisi.LostFocus
        If txtNomorPolisi.Text.Trim <> "" Then
            If IsNumeric(Mid(txtNomorPolisi.Text.Trim, 1, 1)) Then
                txtNomorPolisi.Text = cKodeArea.Trim & txtNomorPolisi.Text.Trim
            End If
        End If
    End Sub

    Private Sub txtKey_EditValueChanged(sender As Object, e As EventArgs) Handles txtKey.EditValueChanged
        If UCase(txtKey.Text.Trim) = "HL" Then
            txtTarif.Text = Decimal.Parse(txtTarif.Text.Trim) + Decimal.Parse(nDenda)
        End If
    End Sub

    Private Sub txtBayar_EditValueChanged(sender As Object, e As EventArgs) Handles txtBayar.EditValueChanged
        If lTransaction Then
            txtKembali.Text = Decimal.Parse(txtBayar.Text.Trim) - Decimal.Parse(txtTarif.Text.Trim)
        End If
    End Sub


    Private Sub DefaultSetting()
        lTransaction = False
        peCaptureIn.Image = Nothing
        peCaptureOut.Image = Nothing
        txtNomorPolisi.Text = ""
        txtNomorTiket.Text = ""
        txtLamaParkir.Text = ""
        txtTarif.Text = 0

        txtBayar.Text = 0
        txtKembali.Text = 0
        txtKey.Text = 0


        txtTarif.Properties.ReadOnly = True
        txtKembali.Properties.ReadOnly = True
        txtBayar.Properties.ReadOnly = True
        txtNomorPolisi.Focus()

        nJam = 0
        nMenit = 0
        nDetik = 0
        nTarif = 0
        nOvertime = 0
        nTarifOvertime = 0
        nDenda = 0

    End Sub


    Private Sub HitungJam2(ByVal cDtMasuk As Date, ByVal cDtKeluar As Date)
        Dim dd As Double = DateDiff(DateInterval.Second, cDtMasuk, cDtKeluar)

        nJam = (dd - (dd Mod 3600)) / 3600
        nMenit = (((dd - (dd Mod 60))) / 60) Mod 60
        nDetik = dd Mod 60
    End Sub

    Private Sub frmManLessOut_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        If mySerialPort.IsOpen Then
            mySerialPort.Close()
        End If
    End Sub

    Private Sub frmManLessOut_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        'DestroySession(frmLogin.txtUser.Text.Trim)
        frmLogin.txtUser.Text = ""
        frmLogin.txtPassword.Text = ""
        frmLogin.Show()
    End Sub

    Private Sub frmManLessOut_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F3 Then
            Using cfrmMemberOut As New frmMemberOut

                cfrmMemberOut.cKodeArea = cKodeArea.Trim
                cfrmMemberOut.cMemberOverTime = cMemberOverTime.Trim
                cfrmMemberOut.cMemberTarifOvertime = cMemberTarifOvertime.Trim
                cfrmMemberOut.txtNomorPolisi.Text = txtNomorPolisi.Text.Trim
                cfrmMemberOut.txtNomorTiket.Text = txtNomorTiket.Text.Trim
                cfrmMemberOut.txtKodeJenisKendaraan.Text = txtKodeJenisKendaraan.Text.Trim
                cfrmMemberOut.txtJenisKendaraan.Text = txtJenisKendaraan.Text.Trim
                cfrmMemberOut.ShowDialog()
                If cfrmMemberOut.lSuccessSave Then
                    If mySerialPort.IsOpen Then
                        mySerialPort.WriteLine("1")
                    End If
                Else
                    MsgBox("gagal proses data member, gunakan perhitungan tarif normal")
                End If
            End Using
            DefaultSetting()
        End If
    End Sub
End Class