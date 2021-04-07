Imports System.Data.SqlClient
Public Class frmTiketRusak
    Friend lStatTicket As Boolean
    Friend img As Byte()
    Friend cKodeJenis As String

    Dim nJam As Integer
    Dim nMenit As Integer
    Dim nDetik As Integer
    Dim nGrassPeriode As Integer
    Dim nTarif As Integer
    Dim nTarifOvertime As Integer
    Dim nOvertime As Integer

    Private Sub FrmTiketRusak_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lStatTicket = False
        ClearScreen()
    End Sub
    Private Sub ClearScreen()
        txtNomorSTNK.Text = ""
        txtNamaPemilik.Text = ""
        txtAlamat.Text = ""
        txtJamMasuk.Text = ""
    End Sub

    Private Sub DisableText()
        txtNoPol.Properties.ReadOnly = True
        txtTarif.Properties.ReadOnly = True
    End Sub

    Private Sub BtnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If txtNomorSTNK.Text.Trim = "" Then
            MsgBox("Nomor STNK tidak boleh kosong, silahkan input nomor STNK", MsgBoxStyle.Critical, "Error")
            txtNomorSTNK.Text = ""
            txtNomorSTNK.Focus()
            Exit Sub
        End If

        If txtNamaPemilik.Text.Trim = "" Then
            MsgBox("Nama pemilik kendaraan tidak boleh kosong, silahkan input nama pemilik kendaraan", MsgBoxStyle.Critical, "Error")
            txtNamaPemilik.Text = ""
            txtNamaPemilik.Focus()
            Exit Sub
        End If

        If txtAlamat.Text.Trim = "" Then
            MsgBox("Alamat pemilik kendaraan tidak boleh kosong, silahkan input alamat pemilik kendaraan", MsgBoxStyle.Critical, "Error")
            txtAlamat.Text = ""
            txtAlamat.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "add_tiket_rusak"
                    .CommandType = Data.CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = Data.SqlDbType.NChar
                    .Parameters(0).Direction = Data.ParameterDirection.Output
                    .Parameters.Add(New SqlParameter("@mPROCESS", Data.SqlDbType.NChar, 6)).Value = "ADD"
                    .Parameters.Add(New SqlParameter("@nopol", Data.SqlDbType.NChar, 10)).Value = txtNoPol.Text.Trim
                    .Parameters.Add(New SqlParameter("@tarif", Data.SqlDbType.Decimal, 18, 0)).Value = Decimal.Parse(txtTarif.Text.Trim)
                    .Parameters.Add(New SqlParameter("@overtime", Data.SqlDbType.Decimal, 18, 0)).Value = Decimal.Parse(txtTarif.Text.Trim)
                    .Parameters.Add(New SqlParameter("@tarif_overtime", Data.SqlDbType.Decimal, 18, 0)).Value = Decimal.Parse(txtTarif.Text.Trim)
                    .Parameters.Add(New SqlParameter("@nomor_stnk", Data.SqlDbType.Char, 35)).Value = txtNomorSTNK.Text.Trim
                    .Parameters.Add(New SqlParameter("@nama_pemilik", Data.SqlDbType.Char, 50)).Value = txtNamaPemilik.Text.Trim
                    .Parameters.Add(New SqlParameter("@alamat_pemilik", Data.SqlDbType.Char, 50)).Value = txtAlamat.Text.Trim
                    .Parameters.Add(New SqlParameter("@img", Data.SqlDbType.Image)).Value = img
                    .Parameters.Add(New SqlParameter("@user_id", Data.SqlDbType.Char, 10)).Value = frmLogin.txtUser.Text.Trim
                    .ExecuteNonQuery()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Conn.Close()

        lStatTicket = True
        Me.Dispose()
    End Sub

    Private Sub txtJamMasuk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtJamMasuk.KeyPress
        Dim cDt As Date
        Dim cDateMasuk As Date
        Dim cDateKeluar As Date
        If Asc(e.KeyChar) = 13 Then
            Dim Conn As SqlConnection = KoneksiSQL()
            Conn.Open()
            Try
                Using cmd As New SqlCommand
                    With cmd
                        .Connection = Conn
                        .CommandText = "select getdate()"

                        Dim rdr As SqlDataReader = .ExecuteReader
                        While rdr.Read
                            cDt = Date.Parse(rdr.Item(0).ToString.Trim)
                            cDateKeluar = Date.Parse(rdr.Item(1).ToString.Trim)
                        End While
                        rdr.Close()

                        .CommandText = "select [grass periode] from tarif where [kd jenis] = '" & cKodeJenis.Trim & "'"
                        rdr = .ExecuteReader
                        While rdr.Read
                            nGrassPeriode = Val(Integer.Parse(rdr.Item(0).ToString.Trim))
                        End While
                        rdr.Close()

                        'bandingkan waktu parkir dengan grass periode
                        If Val(nJam) <= 0 Then
                            If Val(nMenit) >= Val(nGrassPeriode) Then
                                .CommandText = "select [tarif] from tarif where [kd jenis] ='" & cKodeJenis.Trim & "'"
                                rdr = .ExecuteReader
                                While rdr.Read
                                    nTarif = Val(rdr.Item(0).ToString.Trim)
                                End While
                                rdr.Close()
                            ElseIf Val(nMenit) < Val(nGrassPeriode) Then
                                nTarif = 0
                            End If
                        Else
                            .CommandText = "select [tarif] from tarif where [kd jenis] ='" & cKodeJenis.Trim & "'"
                            rdr = .ExecuteReader
                            While rdr.Read
                                nTarif = Val(rdr.Item(0).ToString.Trim)
                            End While
                            rdr.Close()
                        End If


                        '*********************************************************************************************
                        '** Hitung waktu overtime dan sesuaikan tarif sesuai pengaturan overtime pada tabel tarif
                        '*********************************************************************************************
                        .CommandText = "select [overtime],[periode overtime],[tarif overtime]," &
                                "[maksimal overtime] from tarif where [kd jenis] = '" &
                                cKodeJenis.Trim & "'"
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

                            nTarifOvertime = Val(nOvertime) * Val(rdr.Item(2).ToString.Trim)


                        End While
                        rdr.Close()
                    End With
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Conn.Close()
            cDateMasuk = Date.Parse(cDt.Year.ToString.Trim & "-" &
                                    cDt.Month.ToString.Trim & "-" &
                                    cDt.Day.ToString.Trim & " " &
                                    txtJamMasuk.Text.Trim)
            HitungJam2(cDateMasuk, cDateKeluar)
            txtLamaParkir.Text = nJam & " Jam " & nMenit & " Menit " & nDetik & " Detik"
        End If
    End Sub

    Private Sub HitungJam2(ByVal cDtMasuk As Date, ByVal cDtKeluar As Date)
        Dim dd As Double = DateDiff(DateInterval.Second, cDtMasuk, cDtKeluar)

        nJam = (dd - (dd Mod 3600)) / 3600
        nMenit = (((dd - (dd Mod 60))) / 60) Mod 60
        nDetik = dd Mod 60
    End Sub
End Class