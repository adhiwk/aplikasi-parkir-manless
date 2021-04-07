Imports Emgu.CV
Imports Emgu.CV.Structure
Imports System.IO
Imports System.Data.SqlClient

Public Class frmMemberOut
    Friend cKodeArea As String
    Friend cMemberOverTime As String
    Friend cMemberTarifOvertime As String
    Friend lSuccessSave As Boolean

    Dim lTransaction As Boolean
    Dim lMember As Boolean
    Dim cDateTime As Date
    Dim cDateKeluar As Date
    Dim nDenda As Integer
    Dim cKodeShift As String

    Dim nGrassPeriode As Integer
    Dim nTarifOvertime As Integer
    Dim nOvertime As Integer

    Dim nJam As Integer
    Dim nMenit As Integer
    Dim nDetik As Integer
    Dim cKodeKunci As String

    Dim nTarif As Decimal
    Dim nTarifDasar As Decimal
    Dim ckode As String = ""

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub frmMemberOut_Load(sender As Object, e As EventArgs) Handles Me.Load
        ClearScreen()
        lSuccessSave = False
        lMember = False
        lTransaction = False
    End Sub

    Private Sub ClearScreen()
        txtLamaParkir.Text = ""
        txtKodeMember.Text = ""
        txtPinMember.Text = ""
        txtTarif.Text = 0
        txtBayar.Text = 0
        txtKembali.Text = 0
        txtKey.Text = 0

        peCaptureIn.Image = Nothing
    End Sub

    Private Sub TxtNomorPolisi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNomorPolisi.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If Asc(e.KeyChar) = 13 Then
            If txtNomorPolisi.Text.Trim <> "" Then
                If IsNumeric(Mid(txtNomorPolisi.Text.Trim, 1, 1)) Then
                    txtNomorPolisi.Text = cKodeArea.Trim & txtNomorPolisi.Text.Trim
                End If
            End If
        End If
    End Sub

    Private Sub frmMemberOut_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        txtNomorPolisi.Focus()
    End Sub

    Private Sub TxtNomorTiket_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNomorTiket.KeyPress
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


                'Deklarasi variabel
                Dim cDateMasuk As Date
                Dim dTgl As Date
                Dim cJam, cMenit, cDetik As String
                Dim nGrassPeriode As Integer = 0
                Dim lStatusTarif As Boolean = False

                lTransaction = True

                nJam = 0
                nMenit = 0
                nDetik = 0

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

                                Exit Sub
                            Else
                                txtKodeJenisKendaraan.Text = rdr.Item(4).ToString.Trim

                                dTgl = Date.Parse(rdr.Item(2).ToString.Trim)
                                cDateMasuk = Date.Parse(dTgl.Year.ToString.Trim & "-" & dTgl.Month.ToString.Trim & "-" & dTgl.Day.ToString.Trim & " " & rdr.Item(3).ToString.Trim)
                                cKodeKunci = rdr.Item(1).ToString.Trim


                            'Capture new image from cam
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
                            Exit Sub
                        End If

                        rdr.Close()

                        .CommandText = "select [img_in] from parkir_image where [nomor transaksi] = '" &
                            txtNomorTiket.Text.Trim & "'"
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

                    End With
                End Using
                Conn.Close()
            'Catch ex As Exception

            'End Try
        End If
    End Sub

    Private Sub HitungJam2(ByVal cDtMasuk As Date, ByVal cDtKeluar As Date)
        Dim dd As Double = DateDiff(DateInterval.Second, cDtMasuk, cDtKeluar)

        nJam = (dd - (dd Mod 3600)) / 3600
        nMenit = (((dd - (dd Mod 60))) / 60) Mod 60
        nDetik = dd Mod 60
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

    Private Sub TxtKey_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKey.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtKodeMember.Text.Trim = "" Then
                MsgBox("masukkan kode member terlebih dahulu", MsgBoxStyle.Critical, "error")
                txtKodeMember.Focus()
                Exit Sub
            End If

            If txtPinMember.Text.Trim = "" Then
                MsgBox("masukkan pin member terlebih dahulu", MsgBoxStyle.Critical, "error")
                txtPinMember.Focus()
                Exit Sub
            End If
            If lMember Then
                If Mid(cKodeKunci.Trim, 3, 6).ToString.Trim = txtKey.Text.Trim Then
                    'Update Parkir
                    'jika settingan aplikasi, member tidak dikenakan overtime
                    nDenda = 0
                    UpdateParkir()
                    lSuccessSave = True
                    Me.Dispose()
                Else
                    MsgBox("Kode kunci tidak valid", MsgBoxStyle.Critical, "error")
                    txtKey.Text = ""
                    txtKey.Focus()
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Private Sub UpdateParkir()
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "add_parkir_keluar_member"
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
                    .Parameters.Add(New SqlParameter("@info", Data.SqlDbType.Char, 2)).Value = ""
                    .Parameters.Add(New SqlParameter("@jenis", Data.SqlDbType.Char, 1)).Value = "P"
                    .Parameters.Add(New SqlParameter("@kode_member", Data.SqlDbType.Char, 25)).Value = txtKodeMember.Text.Trim
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

    Private Sub TxtPinMember_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPinMember.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtPinMember.Text = ckode.Substring(0, 7)
            ckode = ""
            Dim lGrassPeriode As Boolean
            Dim cKdJenisMember As String = ""
            Dim cJenisTarif As String = ""
            Dim nPersenTarif As Integer = 0

            nOvertime = 0
            nTarifOvertime = 0
            nTarifDasar = 0
            nTarif = 0

            Dim Conn As SqlConnection = KoneksiSQL()
            Conn.Open()
            Try
                Using cmd As New SqlCommand
                    With cmd
                        .Connection = Conn
                        .CommandText = "select [kode member],[pin],[exp date],[kdj member] from member where [kode member] = '" &
                                       txtKodeMember.Text.Trim & "' and [pin] = '" & txtPinMember.Text.Trim & "'"
                        Dim rdr As SqlDataReader = .ExecuteReader
                        While rdr.Read

                            'cek kecocokan kode member yang diinput
                            If rdr.Item(0).ToString.Trim = txtKodeMember.Text.Trim Then
                                'cek kecocokan pin member
                                If rdr.Item(1).ToString.Trim = txtPinMember.Text.Trim Then
                                    lMember = True
                                    cKdJenisMember = rdr.Item(3).ToString.Trim
                                Else
                                    lMember = False
                                End If
                            Else
                                lMember = False
                            End If

                            'cek apakah tahun sesuai dengan tahun sekarang
                            'jika ya
                            If Convert.ToDateTime(rdr.Item(2).ToString.Trim).Year = cDateKeluar.Year Then
                                'cek apakah bulan sama dengan bulan skrg
                                'jika ya
                                If Convert.ToDateTime(rdr.Item(2).ToString.Trim).Month = cDateKeluar.Month Then
                                    'cek apakah tanggal lebih besar atau sama dengan tanggal sekarang
                                    'jika ya
                                    If Convert.ToDateTime(rdr.Item(2).ToString.Trim).Day >= cDateKeluar.Day Then
                                        lMember = True
                                        'If Convert.ToDateTime(rdr.Item(2).ToString.Trim).Hour >= cDateKeluar.Hour Then
                                        '    lMember = True
                                        'Else
                                        '    lMember = False
                                        'End If
                                    Else 'cek apakah tanggal lebih besar atau sama dengan tanggal sekarang, jika tidak
                                        lMember = False
                                    End If
                                ElseIf Convert.ToDateTime(rdr.Item(2).ToString.Trim).Month > cDateKeluar.Month Then
                                    lMember = True
                                ElseIf Convert.ToDateTime(rdr.Item(2).ToString.Trim).Month < cDateKeluar.Month Then
                                    lMember = False
                                End If
                            ElseIf Convert.ToDateTime(rdr.Item(2).ToString.Trim).Year > cDateKeluar.Year Then
                                lMember = True
                            ElseIf Convert.ToDateTime(rdr.Item(2).ToString.Trim).Year < cDateKeluar.Year Then
                                lMember = False
                            End If
                        End While

                        If rdr.HasRows = False Then
                            lMember = False
                        End If
                        rdr.Close()

                        'jika data member valid
                        'proses penentuan tarif dasar parkir
                        If lMember Then
                            'query jenis member, ambil jenis tarif dan persen tarif
                            .CommandText = "select [jenis tarif],[persen tarif] from jenis_member " &
                                           "where [kdj member] = '" & cKdJenisMember.Trim & "'"

                            rdr = .ExecuteReader
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
                                               "where [kdj member] = '" & cKdJenisMember.Trim & "' and " &
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


                            'Query grass periode dari tabel tarif, jika waktu parkir diatas grass periode
                            'tarif parkir sesuai tarif yang disimpan ditabel tarif, jika waktu parkir dibawah
                            'grass parkir, tarif parkir = 0
                            .CommandText = "select [grass periode] from tarif where [kd jenis] = '" &
                                        txtKodeJenisKendaraan.Text.Trim & "'"
                            rdr = .ExecuteReader
                            While rdr.Read
                                nGrassPeriode = Integer.Parse(rdr.Item(0).ToString.Trim)
                            End While
                            rdr.Close()

                            'bandingkan waktu parkir dengan grass periode
                            'jika lama parkir dibawah satu jam
                            If Val(nJam) <= 0 Then
                                'apakah parkir sudah melewati batas minimum parkir
                                If Val(nMenit) >= Val(nGrassPeriode) Then
                                    nTarif = Decimal.Parse(nTarifDasar)
                                    lGrassPeriode = True
                                ElseIf Val(nMenit) < Val(nGrassPeriode) Then
                                    nTarif = 0
                                    lGrassPeriode = False
                                End If
                            Else
                                nTarif = Decimal.Parse(nTarifDasar)
                                lGrassPeriode = True
                            End If

                            If lGrassPeriode Then
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
                            End If
                            txtTarif.Text = Decimal.Parse(nTarif) + Decimal.Parse(nTarifOvertime)

                        Else
                            MsgBox("Data member tidak valid")
                            Conn.Close()
                            txtKodeMember.Text = ""
                            txtPinMember.Text = ""
                            txtKodeMember.Focus()
                        End If
                    End With
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Conn.Close()
        Else
            ckode = ckode.Trim & e.KeyChar
        End If
    End Sub

    Private Sub TxtBayar_EditValueChanged(sender As Object, e As EventArgs) Handles txtBayar.EditValueChanged
        If lTransaction Then
            txtKembali.Text = Decimal.Parse(txtBayar.Text.Trim) - Decimal.Parse(txtTarif.Text.Trim)
        End If
    End Sub

    Private Sub txtKodeMember_Properties_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKodeMember.Properties.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtKodeMember.Text = ckode.Trim
            ckode = ""
        Else
            ckode = ckode.Trim & e.KeyChar
        End If
    End Sub
End Class