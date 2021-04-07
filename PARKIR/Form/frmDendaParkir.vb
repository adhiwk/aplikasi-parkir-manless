Imports System.Data.SqlClient
Public Class frmDendaParkir
    Friend lStatDenda As Boolean
    Friend img As Byte()
    Private Sub frmDendaParkir_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lStatDenda = False
        ClearScreen()
        DisableText()
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
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

        If txtAlamatPemilik.Text.Trim = "" Then
            MsgBox("Alamat pemilik kendaraan tidak boleh kosong, silahkan input alamat pemilik kendaraan", MsgBoxStyle.Critical, "Error")
            txtAlamatPemilik.Text = ""
            txtAlamatPemilik.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "add_parkir_denda"
                    .CommandType = Data.CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = Data.SqlDbType.NChar
                    .Parameters(0).Direction = Data.ParameterDirection.Output
                    .Parameters.Add(New SqlParameter("@mPROCESS", Data.SqlDbType.NChar, 6)).Value = "ADD"
                    .Parameters.Add(New SqlParameter("@nopol", Data.SqlDbType.NChar, 10)).Value = txtNopol.Text.Trim
                    .Parameters.Add(New SqlParameter("@kd_jenis", Data.SqlDbType.NChar, 10)).Value = My.Settings.kode_kendaraan.Trim
                    .Parameters.Add(New SqlParameter("@denda", Data.SqlDbType.Decimal, 18, 0)).Value = Decimal.Parse(txtDenda.Text.Trim)
                    .Parameters.Add(New SqlParameter("@nomor_stnk", Data.SqlDbType.Char, 35)).Value = txtNomorSTNK.Text.Trim
                    .Parameters.Add(New SqlParameter("@nama_pemilik", Data.SqlDbType.Char, 50)).Value = txtNamaPemilik.Text.Trim
                    .Parameters.Add(New SqlParameter("@alamat_pemilik", Data.SqlDbType.Char, 50)).Value = txtAlamatPemilik.Text.Trim
                    .Parameters.Add(New SqlParameter("@img", Data.SqlDbType.Image)).Value = img
                    .Parameters.Add(New SqlParameter("@user_id", Data.SqlDbType.Char, 10)).Value = frmLogin.txtUser.Text.Trim
                    .ExecuteNonQuery()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Conn.Close()

        lStatDenda = True
        Me.Dispose()
    End Sub

    Private Sub ClearScreen()
        txtNomorSTNK.Text = ""
        txtNamaPemilik.Text = ""
        txtAlamatPemilik.Text = ""
    End Sub

    Private Sub DisableText()
        txtNopol.Properties.ReadOnly = True
        txtDenda.Properties.ReadOnly = True
    End Sub
End Class