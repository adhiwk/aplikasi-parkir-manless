Imports System.Data.SqlClient
Public Class frmParkirKeluarManLess
    Dim cKodeArea As String = ""
    Dim cKodeLokasi As String = ""
    Dim cPerusahaan As String = ""
    Dim cALamat As String = ""
    Dim cNomorTransaksi As String = ""
    Dim cKodeKunci As String = ""
    Dim strQuery As String = ""

    'Dim lStatMember As Boolean = False


    'Variabel untuk cetak nota
    Dim cNoPol, cJenis, cPosMasuk, cPetugasMasuk, cTglMasuk, cTglKeluar, cLamaParkir, cTarifKeluar As String

    Dim nTarif As Integer = 0

    'Variabel untuk penyimpanan ke database
    Dim cDateTime As Date
    Dim cDateKeluar As Date
    Dim nJam, nMenit, nDetik As Integer

    Dim nDenda As Integer

    Private Sub frmParkirKeluarManLess_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Dim nTarifOvertime As Integer
    Dim nOvertime As Integer

    Dim cKodeShift As String = ""

    Friend cKodeUser As String = ""
    Private Sub txtNomorPolisi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNomorPolisi.KeyPress
        e.KeyChar = UCase(e.KeyChar)
    End Sub

    Private Sub txtNomorTiket_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNomorTiket.KeyPress
        If Asc(e.KeyChar) = 13 Then
            '**************
            'validasi nopol jika kosong abaikan
            If txtNomorPolisi.Text.Trim = "" Then
                Exit Sub
            End If


            'Deklarasi variabel
            Dim cNomorPolisi As String = ""
            Dim cDateMasuk As Date
            Dim cJam, cMenit, cDetik As String
            Dim lStatMember As Boolean = False
            Dim nGrassPeriode As Integer = 0
            Dim lStatusTarif As Boolean = False

            nTarifOvertime = 0

            nJam = 0
            nMenit = 0
            nDetik = 0

            'ambil data parkir dan hitung waktu parkir
            If txtNomorPolisi.Text.Trim <> "" Then
                If IsNumeric(Mid(txtNomorPolisi.Text.Trim, 1, 1)) Then
                    txtNomorPolisi.Text = cKodeArea.Trim & txtNomorPolisi.Text.Trim
                End If
            End If


            Dim Conn As SqlConnection = KoneksiSQL()
            Conn.Open()
            Try
                Using cmd As New SqlCommand
                    With cmd
                        .Connection = Conn

                        '********************************************************************************
                        'cek status member
                        'cek apakah nopol ini masuk dalam list member
                        'jika ya, cek expired date member.
                        'jika data member aktir tarif 0
                        'jika tidak aktif sesuai dengan jenis kendaraan
                        '********************************************************************************
                        .CommandText = "SELECT member_detail.[nopol],member.[exp date],member.[nama member] " &
                                       "FROM member_detail member_detail INNER JOIN member " &
                                       "member ON member_detail.[kode member] = member.[kode member] " &
                                       "Where member_detail.[nopol] = '" & txtNomorPolisi.Text & "'"

                        Dim rdr As SqlDataReader = .ExecuteReader
                        While rdr.Read
                            'jika terdaftar dalam data member, cek apakah datanya belum expired
                            If txtNomorPolisi.Text.Trim = rdr.Item(0).ToString.Trim Then
                                'cek apakah tahun sesuai dengan tahun sekarang
                                'jika ya
                                If Convert.ToDateTime(rdr.Item(1).ToString.Trim).Year = dtKeluar.DateTime.Year Then
                                    'cek apakah bulan sama dengan bulan skrg
                                    'jika ya
                                    If Convert.ToDateTime(rdr.Item(1).ToString.Trim).Month = dtKeluar.DateTime.Month Then
                                        'cek apakah tanggal lebih besar atau sama dengan tanggal sekarang
                                        'jika ya
                                        If Convert.ToDateTime(rdr.Item(1).ToString.Trim).Day >= dtKeluar.DateTime.Day Then
                                            If Convert.ToDateTime(rdr.Item(1).ToString.Trim).Hour >= dtKeluar.DateTime.Hour Then
                                                lStatMember = True
                                            Else
                                                lStatMember = False
                                            End If
                                        Else 'cek apakah tanggal lebih besar atau sama dengan tanggal sekarang, jika tidak
                                            lStatMember = False
                                        End If
                                    ElseIf Convert.ToDateTime(rdr.Item(1).ToString.Trim).Month > dtKeluar.DateTime.Month Then
                                        lStatMember = True
                                    ElseIf Convert.ToDateTime(rdr.Item(1).ToString.Trim).Month < dtKeluar.DateTime.Month Then
                                        lStatMember = False
                                    End If
                                ElseIf Convert.ToDateTime(rdr.Item(1).ToString.Trim).Year > dtKeluar.DateTime.Year Then
                                    lStatMember = True
                                ElseIf Convert.ToDateTime(rdr.Item(1).ToString.Trim).Year < dtKeluar.DateTime.Year Then
                                    lStatMember = False
                                End If
                            Else
                                lStatMember = False
                            End If
                        End While
                        rdr.Close()

                        '*****************************************************************************************************************
                        '**** Cek avaibility data diarea parkir
                        '**** Hanya diperbolehkan nopol yg terdaftar diarea parkir
                        '.CommandText = "Select [nopol],[kode kunci],[tgl masuk],[jam masuk],[kd jenis]," & _
                        '    "[tarif_keluar],[gerbang masuk],[nomor transaksi],[petugas masuk] From Parkir Where [nopol] = '" & _
                        '    txtNomorPolisi.Text.Trim & "' And [status] = 'T'"
                        '*****************************************************************************************************************
                        .CommandText = "SELECT parkir.[nopol],parkir.[kode kunci],parkir.[tgl masuk],parkir.[jam masuk],parkir.[kd jenis]," &
                                       "tarif.[tarif],parkir.[gerbang masuk],parkir.[nomor transaksi],parkir.[petugas masuk] " &
                                       "FROM parkir parkir INNER JOIN tarif tarif ON parkir.[kd jenis] = tarif.[kd jenis] " &
                                       "Where parkir.[nopol] = '" & txtNomorPolisi.Text.Trim & "' And [status] = 'T'"

                        rdr = .ExecuteReader
                        While rdr.Read
                            If txtNomorPolisi.Text.Trim <> rdr.Item(0).ToString.Trim Then
                                MsgBox("Nomor Polisi ini tidak terdaftar dalam database, silahkan hubungi administrator", MsgBoxStyle.Critical, "Error")
                                DefaultSetting()
                                Exit Sub
                            Else
                                cNomorPolisi = txtNomorPolisi.Text.Trim
                                txtJenisKendaraan.Text = rdr.Item(4).ToString.Trim
                                cJenis = cboJenisKendaraan.Text.Trim

                                txtPosMasuk.Text = rdr.Item(6).ToString.Trim
                                cPosMasuk = txtPosMasuk.Text.Trim

                                txtMasuk.Text = Format(Convert.ToDateTime(rdr.Item(2).ToString.Trim), "dd/MM/yyyy") & " " &
                                  Format(Convert.ToDateTime(rdr.Item(3).ToString.Trim), "HH:mm:ss")
                                cTglMasuk = txtMasuk.Text.Trim

                                cDateMasuk = Format(Convert.ToDateTime(rdr.Item(2).ToString.Trim), "yyyy/MM/dd") &
                                    " " & Format(Convert.ToDateTime(rdr.Item(3).ToString.Trim), "HH:mm:ss")


                                cKodeKunci = rdr.Item(1).ToString.Trim


                                cNomorTransaksi = rdr.Item(7).ToString.Trim
                                cPetugasMasuk = rdr.Item(8).ToString.Trim

                                'txtTanggal.Text = Format(cDateTime, "dd/MM/yyyy HH:mm:ss")
                            End If
                        End While

                        '**********************************************************************************
                        'validasi untuk data yang tidak tersimpan didatabase
                        'jika nopol yang dimasukkan tidak terdapat dalam tabel parkir, tampilkan pesan
                        '**********************************************************************************
                        If txtNomorPolisi.Text.Trim <> "" Then
                            If rdr.HasRows = False Then
                                MsgBox("Nomor Polisi ini tidak terdaftar dalam database, silahkan hubungi administrator", MsgBoxStyle.Critical, "Error")
                                DefaultSetting()
                                Exit Sub
                            End If
                        End If
                        rdr.Close()


                        '*************************************************************************************
                        '** Ambil jam dari server utk menghitung lama parkir
                        '*************************************************************************************
                        .CommandText = "select getdate() as [Tanggal]"
                        rdr = .ExecuteReader
                        While rdr.Read
                            cDateTime = Convert.ToDateTime(rdr.Item(0).ToString.Trim)
                            txtKeluar.Text = Format(cDateTime, "dd/MM/yyyy HH:mm:ss")
                            cTglKeluar = txtKeluar.Text.Trim
                            cDateKeluar = rdr.Item(0).ToString.Trim 'Format(cDateTime, "yyyy/MM/dd HH:mm:ss")
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


                        txtLamaParkir.Text = cJam & ":" & cMenit & ":" & cDetik
                        cLamaParkir = cJam & " Jam" & cMenit & " Menit" & cDetik & " Detik"

                        '***************************************************************************************
                        '** Query shift kerja
                        '***************************************************************************************
                        .CommandText = "select [kd shift],[keterangan],[jam mulai],[jam akhir] from shift where datepart(HH,[jam mulai]) <= " &
                                       cDateTime.Hour & " and datepart(HH,[jam akhir]) >= " &
                                       cDateTime.Hour
                        rdr = .ExecuteReader

                        While rdr.Read
                            cKodeShift = rdr.Item(0).ToString.Trim
                            txtShift.Text = rdr.Item(1).ToString.Trim & "  [ " &
                                rdr.Item(2).ToString.Trim & " - " &
                                rdr.Item(3).ToString.Trim & " ]"
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
                            txtJenisKendaraan.Text.Trim & "'"
                        rdr = .ExecuteReader
                        While rdr.Read
                            nGrassPeriode = Val(rdr.Item(0).ToString.Trim)
                        End While
                        rdr.Close()

                        'bandingkan waktu parkir dengan grass periode
                        If Val(nJam) <= 0 Then
                            If Val(nMenit) >= Val(nGrassPeriode) Then
                                .CommandText = "select [tarif] from tarif where [kd jenis] ='" & txtJenisKendaraan.Text.Trim & "'"
                                rdr = .ExecuteReader
                                While rdr.Read
                                    nTarif = Val(rdr.Item(0).ToString.Trim)
                                End While
                                rdr.Close()
                            ElseIf Val(nMenit) < Val(nGrassPeriode) Then
                                nTarif = 0
                            End If
                        Else
                            .CommandText = "select [tarif] from tarif where [kd jenis] ='" & txtJenisKendaraan.Text.Trim & "'"
                            rdr = .ExecuteReader
                            While rdr.Read
                                nTarif = Val(rdr.Item(0).ToString.Trim)
                            End While
                            rdr.Close()
                        End If

                        If lStatMember Then
                            nTarif = 0
                            lblTarif.Text = "Rp. 0"
                        Else
                            lblTarif.Text = "Rp. " & Val(nTarif)
                        End If


                        '*********************************************************************************************
                        '** Hitung waktu overtime dan sesuaikan tarif sesuai pengaturan overtime pada tabel tarif
                        '*********************************************************************************************
                        .CommandText = "Select [overtime],[periode overtime],[tarif overtime]," &
                            "[maksimal overtime],[tarif denda] From Tarif Where [kd jenis] = '" &
                            txtJenisKendaraan.Text.Trim & "'"
                        rdr = .ExecuteReader

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

                            If Not lStatMember Then
                                nTarifOvertime = Val(nOvertime) * Val(rdr.Item(2).ToString.Trim)
                                nDenda = Val(rdr.Item(4).ToString.Trim)
                            End If

                        End While
                        rdr.Close()

                        If Not lStatMember Then
                            'lblTarifTambahan.Text = "Rp. " & Val(nTarifOvertime)
                            lblTarif.Text = "Rp. " & Val(nTarif) + Val(nTarifOvertime)
                        End If


                    End With
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
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
                    .CommandText = "Select [Keterangan] From Jenis_Kendaraan Order By [Keterangan] Asc"
                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        cboJenisKendaraan.Properties.Items.Add(rDr.Item(0).ToString.Trim)
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

                    .CommandText = "Select GetDate() As Waktu"
                    rDr = .ExecuteReader
                    While rDr.Read
                        dtKeluar.DateTime = Convert.ToDateTime(rDr.Item(0).ToString.Trim)
                    End While
                    txtNamaGerbang.Text = My.Settings.gerbang.Trim
                    rDr.Close()

                    .CommandText = "select [kd shift],[keterangan],[jam mulai],[jam akhir] from shift where datepart(HH,[jam mulai]) <= " &
                                   dtKeluar.DateTime.Hour & " and datepart(HH,[jam akhir]) >= " &
                                   dtKeluar.DateTime.Hour
                    rDr = .ExecuteReader

                    While rDr.Read
                        cKodeShift = rDr.Item(0).ToString.Trim
                        txtShift.Text = rDr.Item(1).ToString.Trim & "  [ " &
                            rDr.Item(2).ToString.Trim & " - " &
                            rDr.Item(3).ToString.Trim & " ]"
                    End While

                    '******* query kode shift diatas jam 12 malam
                    If rDr.HasRows = False Then
                        QueryDefaultShift()
                    End If
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
                        txtShift.Text = rdr.Item(1).ToString.Trim & " " &
                            rdr.Item(2).ToString.Trim & " - " &
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

    Private Sub txtNomorPolisi_LostFocus(sender As Object, e As EventArgs) Handles txtNomorPolisi.LostFocus
        If txtNomorPolisi.Text.Trim <> "" Then
            If IsNumeric(Mid(txtNomorPolisi.Text.Trim, 1, 1)) Then
                txtNomorPolisi.Text = cKodeArea.Trim & txtNomorPolisi.Text.Trim
            End If
        End If
    End Sub
End Class