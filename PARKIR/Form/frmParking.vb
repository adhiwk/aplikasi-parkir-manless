Imports System.Data.SqlClient
Imports System.Drawing.Printing

Public Class frmParking
    Dim cKodeArea As String = ""
    Dim cKodeLokasi As String = ""
    Dim cPerusahaan As String = ""
    Dim cALamat As String = ""
    Dim cNomorTransaksi As String = ""
    Dim cKodeKunci As String = ""
    Dim strQuery As String = ""
    Dim lStatMember As Boolean = False
    Dim nTarif As Integer = 0
    Dim cDtTime As Date
    Dim cKodeShift As String = ""

    Friend cKodeUser As String = ""


    '************ Print Section *****************************
    ReadOnly smallfont As New Font("Verdana", 8)
    ReadOnly boldfont As New Font("Verdana", 14)

    Private PrintString As String
    Private PrintPageSettings As New PageSettings

    Private Sub PrintControler_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintControler.PrintPage

        PrintControler.DocumentName = "Nusarindo Parking Solution"

        Dim PrintFont As New Font("Verdana", 8)
        Dim numChars As Integer
        Dim numLines As Integer
        Dim stringForPage As String
        Dim strFormat As New StringFormat

        Dim rectDraw As New RectangleF( _
          e.MarginBounds.Left, e.MarginBounds.Top, _
          e.MarginBounds.Width, e.MarginBounds.Height)
        'Determine maximum text ammount and spaces lines
        Dim sizeMeasure As New SizeF(e.MarginBounds.Width, _
          e.MarginBounds.Height - PrintFont.GetHeight(e.Graphics))

        'Break in between words
        strFormat.Trimming = StringTrimming.Word
        'Determines ammount of wordss and lines that can fit on a page
        e.Graphics.MeasureString(PrintString, PrintFont, _
          sizeMeasure, strFormat, numChars, numLines)

        stringForPage = PrintString.Substring(0, numChars)
        'Print strings to page
        e.Graphics.DrawString(stringForPage, PrintFont, _
          Brushes.Black, rectDraw, strFormat)
        'Determine whether or not there are more pages to print
        If numChars < PrintString.Length Then
            'Remove printed text from string
            PrintString = PrintString.Substring(numChars)
            e.HasMorePages = True
        Else
            e.HasMorePages = False
            'Restore string after printing
            PrintString = " "
        End If

    End Sub

    Private Sub PrintMember()
        Try
            AddHandler PrintControler.PrintPage, AddressOf Me.CetakNotaMember
            PrintControler.DefaultPageSettings = PrintPageSettings
            PrintString = " "
            PrintControler.Print()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub PrintCasual()
        Try
            AddHandler PrintControler.PrintPage, AddressOf Me.CetakNotaCasual
            PrintControler.DefaultPageSettings = PrintPageSettings
            PrintString = " "
            PrintControler.Print()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CetakNotaMember(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        Dim sf As New StringFormat() With {.FormatFlags = StringFormatFlags.NoWrap}


        'RawPrinterHelper.SendStringToPrinter(My.Settings.printer_name.Trim, cPerusahaan.Trim & vbCrLf)
        'RawPrinterHelper.SendStringToPrinter(My.Settings.printer_name.Trim, cALamat.Trim & vbCrLf)
        'RawPrinterHelper.SendStringToPrinter(My.Settings.printer_name.Trim, "--------------------------------" & vbCrLf)
        'RawPrinterHelper.SendStringToPrinter(My.Settings.printer_name.Trim, txtNomorPolisi.Text.Trim & "/" & cboJenisKendaraan.Text.Trim & vbCrLf)
        'RawPrinterHelper.SendStringToPrinter(My.Settings.printer_name.Trim, dtTgl.DateTime.Day & "/" & dtTgl.DateTime.Month & "/" & _
        '                                     dtTgl.DateTime.Year & " " & dtTgl.DateTime.Hour & ":" & dtTgl.DateTime.Minute & _
        '                                     ":" & dtTgl.DateTime.Second & vbCrLf)
        'RawPrinterHelper.SendStringToPrinter(My.Settings.printer_name.Trim, "[" & txtNamaGerbang.Text.Trim & "]" & _
        '                                     "[" & txtPetugas.Text.Trim & "]" & vbCrLf)
        'RawPrinterHelper.SendStringToPrinter(My.Settings.printer_name.Trim, "Rp. 0" & vbCrLf)
        'RawPrinterHelper.SendStringToPrinter(My.Settings.printer_name.Trim, cNomorTransaksi.Trim & vbCrLf)
        'RawPrinterHelper.SendStringToPrinter(My.Settings.printer_name.Trim, cKodeKunci.Trim & vbCrLf)
        'RawPrinterHelper.SendStringToPrinter(My.Settings.printer_name.Trim, "--------------------------------" & vbCrLf)
        'RawPrinterHelper.SendStringToPrinter(My.Settings.printer_name.Trim, "Selamat Datang" & vbCrLf)

        e.Graphics.DrawString(cPerusahaan.Trim, smallfont, Brushes.Black, New PointF(19, 30), sf)
        e.Graphics.DrawString(cALamat.Trim, smallfont, Brushes.Black, New PointF(19, 45), sf)
        e.Graphics.DrawString("-------------------------------------", smallfont, Brushes.Black, New PointF(19, 60), sf)
        e.Graphics.DrawString(txtNomorPolisi.Text.Trim, boldfont, Brushes.Black, New PointF(19, 75), sf)
        e.Graphics.DrawString("/" & cboJenisKendaraan.Text.Trim, smallfont, Brushes.Black, New PointF(130, 82), sf)
        'e.Graphics.DrawString(dtTgl.DateTime.Day & "/" & dtTgl.DateTime.Month & "/" & _
        '                     dtTgl.DateTime.Year & " " & cDtTime.Hour.ToString.Trim & ":" & cDtTime.Minute.ToString.Trim & _
        '                     ":" & cDtTime.Second.ToString.Trim, smallfont, Brushes.Black, New PointF(19, 97), sf)
        e.Graphics.DrawString(Format(cDtTime, "dd/MM/yyyy HH:mm:ss"), smallfont, Brushes.Black, New PointF(19, 97), sf)
        e.Graphics.DrawString("[" & txtNamaGerbang.Text.Trim & "]" & _
                              "[" & txtPetugas.Text.Trim & "]", smallfont, Brushes.Black, New PointF(19, 112), sf)
        e.Graphics.DrawString("Rp. 0", boldfont, Brushes.Black, New PointF(19, 127), sf)
        e.Graphics.DrawString(cNomorTransaksi.Trim, smallfont, Brushes.Black, New PointF(19, 150), sf)
        e.Graphics.DrawString(".", smallfont, Brushes.Black, New PointF(19, 165), sf)
        e.Graphics.DrawString(".", smallfont, Brushes.Black, New PointF(19, 180), sf)
        e.Graphics.DrawString(cKodeKunci.Trim, boldfont, Brushes.Black, New PointF(19, 195), sf)
        e.Graphics.DrawString("-------------------------------------", smallfont, Brushes.Black, New PointF(19, 210), sf)
        e.Graphics.DrawString("Selamat Datang", smallfont, Brushes.Black, New PointF(19, 225), sf)

        e.HasMorePages = False
    End Sub

    Private Sub CetakNotaCasual(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        Dim sf As New StringFormat() With {.FormatFlags = StringFormatFlags.NoWrap}

        e.Graphics.DrawString(cPerusahaan.Trim, smallfont, Brushes.Black, New PointF(19, 30), sf)
        e.Graphics.DrawString(cALamat.Trim, smallfont, Brushes.Black, New PointF(19, 45), sf)
        e.Graphics.DrawString("-------------------------------------", smallfont, Brushes.Black, New PointF(19, 60), sf)
        e.Graphics.DrawString(txtNomorPolisi.Text.Trim, boldfont, Brushes.Black, New PointF(19, 75), sf)
        e.Graphics.DrawString("/" & cboJenisKendaraan.Text.Trim, smallfont, Brushes.Black, New PointF(130, 82), sf)
        'e.Graphics.DrawString(dtTgl.DateTime.Day & "/" & dtTgl.DateTime.Month & "/" & _
        '                     dtTgl.DateTime.Year & " " & cDtTime.Hour.ToString.Trim & ":" & cDtTime.Minute.ToString.Trim & _
        '                     ":" & cDtTime.Second.ToString.Trim, smallfont, Brushes.Black, New PointF(19, 97), sf)
        e.Graphics.DrawString(Format(cDtTime, "dd/MM/yyyy HH:mm:ss"), smallfont, Brushes.Black, New PointF(19, 97), sf)
        e.Graphics.DrawString("[" & txtNamaGerbang.Text.Trim & "]" & _
                              "[" & txtPetugas.Text.Trim & "]", smallfont, Brushes.Black, New PointF(19, 112), sf)
        'e.Graphics.DrawString("Rp. " & nTarif.ToString.Trim, boldfont, Brushes.Black, New PointF(19, 127), sf)
        e.Graphics.DrawString(cNomorTransaksi.Trim, smallfont, Brushes.Black, New PointF(19, 150), sf)
        e.Graphics.DrawString(cKodeKunci.Trim, boldfont, Brushes.Black, New PointF(19, 165), sf)
        e.Graphics.DrawString("-------------------------------------", smallfont, Brushes.Black, New PointF(19, 182), sf)
        e.Graphics.DrawString("Selamat Datang", smallfont, Brushes.Black, New PointF(19, 197), sf)

        e.HasMorePages = False
    End Sub

    '*********** EOF Print Section ***********************

    Private Sub frmParking_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        'Cetak Rekap dan kembali ke form login
        DestroySession(cKodeUser)
        frmLogin.txtUser.Text = ""
        frmLogin.txtPassword.Text = ""
        frmLogin.Focus()
        frmLogin.Show()

    End Sub


    Private Sub frmParking_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        LoadDataDefault()
        txtJenisKendaraan.Text = My.Settings.kode_kendaraan.Trim
        LoadJenisKendaraan()
        ctlrTimer.Enabled = True
    End Sub

    Private Sub DefaultSetting()
        txtNomorPolisi.Text = ""
        txtNamaMember.Text = ""
        dtExpMember.DateTime = Date.Now
        txtKeterangan.Text = ""
        txtNomorPolisi.Focus()
    End Sub

    Private Sub txtNomorPolisi_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNomorPolisi.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If ValidasiKey(UCase(e.KeyChar.ToString.Trim)) Then
                e.KeyChar = UCase(e.KeyChar)
            Else
                e.KeyChar = ""
            End If
        End If
    End Sub

    Private Sub LoadDataDefault()
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "Select [kd jenis],[Keterangan] From Jenis_Kendaraan Order By [Keterangan] Asc"
                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        txtJenisKendaraan.Properties.Items.Add(rDr.Item(0).ToString.Trim)
                        cboJenisKendaraan.Properties.Items.Add(rDr.Item(1).ToString.Trim)
                    End While
                    rDr.Close()

                    .CommandText = "Select [area_nopol],[kode_lokasi],[perusahaan],[alamat] From info"
                    rDr = .ExecuteReader
                    While rDr.Read
                        cKodeArea = rDr.Item(0).ToString.Trim
                        cKodeLokasi = rDr.Item(1).ToString.Trim
                        cPerusahaan = rDr.Item(2).ToString.Trim
                        cALamat = rDr.Item(3).ToString.Trim
                    End While
                    rDr.Close()

                    .CommandText = "select getdate() as [Tanggal]"
                    rDr = .ExecuteReader
                    While rDr.Read
                        dtTgl.DateTime = Convert.ToDateTime(rDr.Item(0).ToString.Trim)
                        lbTanggal.Text = "TANGGAL : " & Convert.ToDateTime(rDr.Item(0).ToString.Trim)
                    End While
                    rDr.Close()

                    .CommandText = "select [kd shift],[keterangan],[jam mulai],[jam akhir] from shift where datepart(HH,[jam mulai]) <= " & _
                       dtTgl.DateTime.Hour & " and datepart(HH,[jam akhir]) >= " & _
                       dtTgl.DateTime.Hour
                    rDr = .ExecuteReader

                    While rdr.Read
                        cKodeShift = rdr.Item(0).ToString.Trim
                        txtShift.Text = rDr.Item(1).ToString.Trim & "  [ " & _
                            rDr.Item(2).ToString.Trim & " - " & _
                            rDr.Item(3).ToString.Trim & " ]"
                    End While

                    '******* query kode shift diatas jam 12 malam
                    If rdr.HasRows = False Then
                        QueryDefaultShift()
                    End If
                    rDr.Close()

                    txtNamaGerbang.Text = My.Settings.gerbang.Trim
                    .Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
        End Try
    End Sub

    Private Sub LoadJenisKendaraan()
        grdKendaraan.Rows.Clear()
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select [kd jenis],[keterangan] from jenis_kendaraan order by [keterangan] asc"
                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        QueryCountKendaraan(rDr.Item(0).ToString.Trim, _
                                             rDr.Item(1).ToString.Trim)
                    End While
                    rDr.Close()
                    .Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
        End Try
    End Sub

    Private Sub QueryCountKendaraan(ByVal cKdJenis As String, ByVal cJenisKendaraan As String)
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd

                    .Connection = Conn
                    .CommandText = "select count([kd jenis]) from parkir where [kd jenis] = '" & _
                        cKdJenis.Trim & "' and [status] = 'T'"

                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        If rDr.HasRows = True Then
                            grdKendaraan.Rows.Add(New Object() {cJenisKendaraan.Trim, _
                                                                Val(rDr.Item(0).ToString.Trim)})
                        End If
                    End While

                    rDr.Close()
                    .Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
        End Try
    End Sub

    Private Function GetDateTime() As Date
        Dim cDateTime As Date
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select getdate() as [Tanggal]"
                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        cDateTime = Convert.ToDateTime(rDr.Item(0).ToString.Trim)
                    End While
                    rDr.Close()
                    .Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
        End Try
        Return cDateTime
    End Function

    'Private Sub cboJenisKendaraan_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboJenisKendaraan.SelectedIndexChanged
    '    Dim Conn As SqlConnection = KoneksiSQL()
    '    Conn.Open()
    '    Try
    '        Using cmd As New SqlCommand
    '            With cmd
    '                .Connection = Conn
    '                .CommandText = "Select [kd jenis] From Jenis_Kendaraan where [keterangan] like '%" & _
    '                    cboJenisKendaraan.Text.Trim & "%'"
    '                Dim rdr As SqlDataReader = .ExecuteReader
    '                While rdr.Read
    '                    txtJenisKendaraan.Text = rdr.Item(0).ToString.Trim
    '                End While
    '                .Connection.Close()
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    Finally
    '        Conn.Close()
    '    End Try
    'End Sub

    Private Function ValidNopol(cNopol As String) As Boolean
        Dim lStatus = False

        If IsNumeric(Mid(cNopol.Trim, 1, 1)) Then
            lStatus = True
        Else
            lStatus = False
        End If

        If Len(cNopol) > 1 Then
            For x = 2 To Len(cNopol.Trim)
                If IsNumeric(Mid(cNopol.Trim, x, 1)) Then
                    lStatus = True
                End If
            Next
        End If
        Return lStatus
    End Function

    Private Sub txtNomorPolisi_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNomorPolisi.LostFocus
        If txtNomorPolisi.Text.Trim <> "" Then
            If IsNumeric(Mid(txtNomorPolisi.Text.Trim, 1, 1)) Then
                txtNomorPolisi.Text = cKodeArea.Trim & txtNomorPolisi.Text.Trim
            End If
            'ElseIf txtNomorPolisi.Text.Trim = "" Then
            '    MsgBox("Nomor Polisi tidak boleh kosong", MsgBoxStyle.Critical, "Error")
            '    Exit Sub
        End If

        lStatMember = False
        txtKeterangan.Text = ""
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn

                    '**** Cek avaibility diarea parkir
                    '**** Hanya diperbolehkan satu nopol yg sama diarea parkir
                    .CommandText = "Select [nopol] From Parkir Where [nopol] = '" & _
                        txtNomorPolisi.Text.Trim & "' And [status] = 'T'"
                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        If txtNomorPolisi.Text.Trim = rdr.Item(0).ToString.Trim Then
                            MsgBox("Nomor Polisi ini sudah ada diarea parkir, duplikasi nomor polisi tidak diperbolehkan", MsgBoxStyle.Critical, "Error")
                            DefaultSetting()
                            Exit Sub
                        End If
                    End While
                    rdr.Close()

                    .CommandText = "Select [keterangan] From Info_Kendaraan where [nopol] = '" & _
                        txtNomorPolisi.Text.Trim & "'"
                    rdr = .ExecuteReader
                    While rdr.Read
                        txtKeterangan.Text = rdr.Item(0).ToString.Trim
                    End While
                    rdr.Close()

                    'cek apakah nopol ini masuk dalam list member
                    'jika ya, cek expired date member.
                    'jika data member aktir tarif 0
                    'jika tidak aktif sesuai dengan jenis kendaraan

                    .CommandText = "SELECT member_detail.[nopol],member.[exp date],member.[nama member] " & _
                                   "FROM member_detail member_detail INNER JOIN member " & _
                                   "member ON member_detail.[kode member] = member.[kode member] " & _
                                   "Where member_detail.[nopol] = '" & txtNomorPolisi.Text & "'"

                    rdr = .ExecuteReader
                    While rdr.Read
                        'jika terdaftar dalam data member, cek apakah datanya belum expired
                        If txtNomorPolisi.Text.Trim = rdr.Item(0).ToString.Trim Then
                            'cek apakah tahun sesuai dengan tahun sekarang
                            'jika ya
                            If Convert.ToDateTime(rdr.Item(1).ToString.Trim).Year = dtTgl.DateTime.Year Then
                                'cek apakah bulan sama dengan bulan skrg
                                'jika ya
                                If Convert.ToDateTime(rdr.Item(1).ToString.Trim).Month = dtTgl.DateTime.Month Then
                                    'cek apakah tanggal lebih besar atau sama dengan tanggal sekarang
                                    'jika ya
                                    If Convert.ToDateTime(rdr.Item(1).ToString.Trim).Day >= dtTgl.DateTime.Day Then
                                        If Convert.ToDateTime(rdr.Item(1).ToString.Trim).Hour >= dtTgl.DateTime.Hour Then
                                            lStatMember = True
                                        Else
                                            lStatMember = False
                                        End If
                                    Else 'cek apakah tanggal lebih besar atau sama dengan tanggal sekarang, jika tidak
                                        lStatMember = False
                                    End If
                                End If

                                'cek apakah bulan expired lebih besar dari bulan sekarang
                                'jika ya 
                                If Convert.ToDateTime(rdr.Item(1).ToString.Trim).Month > dtTgl.DateTime.Month Then
                                    lStatMember = True
                                End If

                                'cek apakah bulan expired lebih kecil dari bulan sekarang
                                'jika ya, member tidak aktif
                                If Convert.ToDateTime(rdr.Item(1).ToString.Trim).Month < dtTgl.DateTime.Month Then
                                    lStatMember = False
                                End If
                            End If
                            'cek apakah tahun expired lebih besar dari tahun sekarang
                            'jika ya member masih aktif
                            If Convert.ToDateTime(rdr.Item(1).ToString.Trim).Year > dtTgl.DateTime.Year Then
                                lStatMember = True
                            End If

                            'cek apakah tahun member lebih kecil dari tahun sekarang
                            'jika ya member tidak aktif
                            If Convert.ToDateTime(rdr.Item(1).ToString.Trim).Year < dtTgl.DateTime.Year Then
                                lStatMember = False
                            End If
                        End If

                        'jika plat nomor tidak cocok dengan data database
                        'kendaraan ini bukan member
                        If txtNomorPolisi.Text.Trim <> rdr.Item(0).ToString.Trim Then
                            lStatMember = False
                        End If

                        If lStatMember Then
                            txtNamaMember.Text = rdr.Item(2).ToString.Trim
                            dtExpMember.DateTime = rdr.Item(1).ToString.Trim
                        End If
                    End While

                    rdr.Close()

                    .Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
        End Try

    End Sub

    Private Function ValidasiJenisKendaraan(ByVal cKode As String) As Boolean
        Dim cStatus As Boolean = False

        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select [kd jenis] from jenis_kendaraan where [kd jenis] = '" & _
                        cKode.Trim & "'"
                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        If rdr.HasRows = True Then
                            If cKode.Trim = rdr.Item(0).ToString.Trim Then
                                cStatus = True
                            Else
                                cStatus = False
                            End If
                        End If
                    End While

                    If rdr.HasRows = False Then
                        cStatus = False
                    End If
                    rdr.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Conn.Close()
        Return cStatus
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
                        txtShift.Text = rdr.Item(1).ToString.Trim & " " & _
                            rdr.Item(2).ToString.Trim & " - " & _
                            rdr.Item(3).ToString.Trim
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

    Private Sub btnCetak_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCetak.Click

        If txtNomorPolisi.Text.Trim = "" Then
            MsgBox("Nomor polisi tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "error")
            txtNomorPolisi.Text = ""
            txtNomorPolisi.Focus()
            Exit Sub
        End If

        If txtJenisKendaraan.Text.Trim = "" Then
            MsgBox("Jenis kendaraan harus diisi", MsgBoxStyle.Exclamation, "error")
            txtJenisKendaraan.Focus()
            Exit Sub
        End If


        If Not ValidasiJenisKendaraan(txtJenisKendaraan.Text.Trim) Then
            MsgBox("Kode Jenis kendaraan tidak valid", MsgBoxStyle.Critical, "error")
            txtJenisKendaraan.Focus()
            Exit Sub
        End If

        If Not ValidNopol(txtNomorPolisi.Text.Trim) Then
            MsgBox("Format Nomor Polisi Tidak Valid", MsgBoxStyle.Critical, "Error")
            txtNomorPolisi.Text = ""
            txtNomorPolisi.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select getdate() as [Tanggal]"
                    cDtTime = Convert.ToDateTime(.ExecuteScalar)

                    .CommandText = "select [kd shift],[keterangan],[jam mulai],[jam akhir] from shift where datepart(HH,[jam mulai]) <= " & _
                        cDtTime.Hour & " and datepart(HH,[jam akhir]) >= " & _
                        cDtTime.Hour
                    Dim rdr As SqlDataReader = .ExecuteReader

                    While rdr.Read
                        cKodeShift = rdr.Item(0).ToString.Trim
                        txtShift.Text = rdr.Item(1).ToString.Trim & "  [ " & _
                            rdr.Item(2).ToString.Trim & " - " & _
                            rdr.Item(3).ToString.Trim & " ]"
                    End While

                    '******* query kode shift diatas jam 12 malam
                    If rdr.HasRows = False Then
                        QueryDefaultShift()
                    End If

                    rdr.Close()
                    cmd.Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Conn.Close()

        Dim nKodeKunci As Double

        '************************************
        'jika status member masih aktif
        'tarif menjadi 0

        cNomorTransaksi = cKodeLokasi.Trim & cDtTime.Year.ToString.Trim & cDtTime.Month.ToString.Trim & _
                         cDtTime.Day.ToString.Trim & cDtTime.Hour.ToString.Trim & _
                         cDtTime.Minute.ToString.Trim & cDtTime.Second.ToString.Trim


        '**** Generate Random Number
        Dim cString As String = ""
        For x = 1 To Len(txtNomorPolisi.Text.Trim)
            If IsNumeric(Mid(txtNomorPolisi.Text.Trim, x, 1)) Then
                cString = cString.Trim & Mid(txtNomorPolisi.Text.Trim, x, 1)
            End If
        Next

        cKodeKunci = (Rnd() + 2) * Val(cString) + 3 + Rnd() + 5
        cKodeKunci = (Mid(cKodeKunci.Trim, 3, 6))
        nKodeKunci = Val(cKodeKunci) * Val(Mid(Rnd(), 3, 4))
        cKodeKunci = (Mid(nKodeKunci, 1, 6))

        If Len(cKodeKunci) < 6 Then
            cKodeKunci = (cString.Trim) & Now.Hour & Now.Minute & Now.Second
            cKodeKunci = (Mid(cKodeKunci, 1, 6))
        End If

        cString = ""

        For x = 1 To Len(cKodeKunci.Trim)
            If Mid(cKodeKunci.Trim, x, 1) = "." Then
                cString = cString.Trim & "0"
            Else
                cString = cString & Mid(cKodeKunci.Trim, x, 1)
            End If
        Next
        cKodeKunci = cString.Trim


        '****** EOF Kode Kunci *************************
        'P = Pass
        'C = Casual

        If lStatMember Then
            'Simpan data
            ExecuteQuery("Insert Into Parkir ([nomor transaksi],[nopol],[kode kunci],[gerbang masuk]," & _
                       "[tgl masuk],[jam masuk],[tarif_masuk],[jenis],[petugas masuk],[kd jenis],[kd shift in]) values ('" & _
                       cNomorTransaksi.Trim & "','" & _
                       txtNomorPolisi.Text.Trim & "','" & _
                       cKodeKunci.Trim & "','" & _
                       txtNamaGerbang.Text.Trim & "', '" & _
                       cDtTime.Year.ToString.Trim & "/" & cDtTime.Month.ToString.Trim & "/" & cDtTime.Day.ToString.Trim & "','" & _
                       cDtTime.Hour.ToString.Trim & ":" & cDtTime.Minute.ToString.Trim & ":" & cDtTime.Second.ToString.Trim & "',0,'P','" & _
                       cKodeUser.Trim & "','" & _
                       txtJenisKendaraan.Text.Trim & "','" & _
                       cKodeShift.Trim & "')")
            PrintMember()
        Else
            'simpan tarif berdasarkan jenis kendaraan
            ExecuteQuery("Insert Into Parkir ([nomor transaksi],[nopol],[kode kunci],[gerbang masuk]," & _
                      "[tgl masuk],[jam masuk],[tarif_masuk],[jenis],[petugas masuk],[kd jenis],[kd shift in]) values ('" & _
                      cNomorTransaksi.Trim & "','" & _
                      txtNomorPolisi.Text.Trim & "','" & _
                      cKodeKunci.Trim & "','" & _
                      txtNamaGerbang.Text.Trim & "', '" & _
                      cDtTime.Year.ToString.Trim & "/" & cDtTime.Month.ToString.Trim & "/" & cDtTime.Day.ToString.Trim & "','" & _
                      cDtTime.Hour.ToString.Trim & ":" & cDtTime.Minute.ToString.Trim & ":" & cDtTime.Second.ToString.Trim & "'," & _
                      Val(0) & ",'C','" & _
                      cKodeUser.Trim & "','" & _
                      txtJenisKendaraan.Text.Trim & "','" & _
                      cKodeShift.Trim & "')")
            PrintCasual()
        End If
        LoadJenisKendaraan()
        DefaultSetting()
        txtNomorPolisi.Focus()
    End Sub

    
    Private Sub txtJenisKendaraan_SelectedValueChanged(sender As Object, e As EventArgs) Handles txtJenisKendaraan.SelectedValueChanged
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    'cari tarif berdasarkan jenis kendaraan
                    .CommandText = "Select [keterangan] From Jenis_Kendaraan where [kd jenis] = '" & _
                        txtJenisKendaraan.Text.Trim & "'"
                    cboJenisKendaraan.Text = ""
                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        cboJenisKendaraan.Text = rdr.Item(0).ToString.Trim
                    End While
                    rdr.Close()

                    .CommandText = "Select [Tarif] From Tarif Where [kd jenis] = '" & _
                        txtJenisKendaraan.Text.Trim & "'"
                    rdr = .ExecuteReader
                    While rdr.Read
                        nTarif = Val(rdr.Item(0).ToString.Trim)
                    End While
                    rdr.Close()
                    .Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Conn.Close()
    End Sub

    Private Sub txtNomorPolisi_EditValueChanged(sender As Object, e As EventArgs) Handles txtNomorPolisi.EditValueChanged

    End Sub

    Private Sub ctlrTimer_Tick(sender As Object, e As EventArgs) Handles ctlrTimer.Tick
        lblTimer.Text = Now.TimeOfDay.Hours & ":" & Now.TimeOfDay.Minutes & ":" & Now.TimeOfDay.Seconds
    End Sub
End Class