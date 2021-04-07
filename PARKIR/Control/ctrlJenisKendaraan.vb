Imports System.Data.SqlClient
Public Class ctrlJenisKendaraan
    Dim lNew As Boolean
    Dim lShow As Boolean
    Private Sub BtnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        If btnTambah.Text = "&Tambah" Then
            If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "jenis_kendaraan", "add") Then
                MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "error")
                DefaultSetting()
                Exit Sub
            End If

            ClearScreen()
            EnableText()
            btnTambah.Text = "&Batal"
            btnHapus.Enabled = False
            btnSimpan.Enabled = True
            btnRefresh.Enabled = False
            lNew = True
            lShow = False
            txtKodeJenis.Focus()
        ElseIf btnTambah.Text = "&Ubah" Then
            If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "jenis_kendaraan", "update") Then
                MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "error")
                DefaultSetting()
                Exit Sub
            End If

            EnableText()
            btnTambah.Text = "&Batal"
            btnHapus.Enabled = False
            btnSimpan.Enabled = True
            btnRefresh.Enabled = False
            lNew = False
            lShow = False
            txtKodeJenis.Focus()
        Else
            DefaultSetting()
            lShow = True
        End If
    End Sub

    Private Sub ctrlJenisKendaraan_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Dock = DockStyle.Fill
        DefaultSetting()
    End Sub

    Private Sub ClearScreen()
        txtKodeJenis.Text = ""
        txtJenisKendaraan.Text = ""
    End Sub

    Private Sub DisableText()
        txtKodeJenis.Properties.ReadOnly = True
        txtJenisKendaraan.Properties.ReadOnly = True
    End Sub

    Private Sub EnableText()
        txtKodeJenis.Properties.ReadOnly = False
        txtJenisKendaraan.Properties.ReadOnly = False
    End Sub

    Private Sub DefaultSetting()
        ClearScreen()
        DisableText()
        btnTambah.Text = "&Tambah"
        btnHapus.Enabled = False
        btnSimpan.Enabled = False
        btnRefresh.Enabled = True
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "jenis_kendaraan", "view") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "error")
            DefaultSetting()
            Exit Sub
        End If

        grdJenisKendaraan.Rows.Clear()
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select [kd jenis],[keterangan] from jenis_kendaraan order by [keterangan] asc"

                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        grdJenisKendaraan.Rows.Add(New Object() {rdr.Item(0).ToString.Trim,
                                                           rdr.Item(1).ToString.Trim})

                    End While
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Conn.Close()
        lShow = True
    End Sub

    Private Sub ShowRecord()
        Try
            txtKodeJenis.Text = grdJenisKendaraan.CurrentRow.Cells.Item(0).Value.ToString.Trim
            txtJenisKendaraan.Text = grdJenisKendaraan.CurrentRow.Cells.Item(1).Value.ToString.Trim
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GrdJenisKendaraan_Click(sender As Object, e As EventArgs) Handles grdJenisKendaraan.Click
        If lShow Then
            ShowRecord()
            btnHapus.Enabled = True
            btnTambah.Text = "&Ubah"
            btnRefresh.Enabled = False
        End If
    End Sub

    Private Sub GrdJenisKendaraan_CurrentRowChanged(sender As Object, e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles grdJenisKendaraan.CurrentRowChanged
        If lShow Then
            ShowRecord()
            btnHapus.Enabled = True
            btnTambah.Text = "&Ubah"
            btnRefresh.Enabled = False
        End If
    End Sub

    Private Sub BtnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If lNew Then
            SaveData()
        Else
            UpdateData()
        End If
        lShow = True
    End Sub

    Private Sub SaveData()
        If txtKodeJenis.Text.Trim = "" Then
            MsgBox("kode jenis boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtKodeJenis.Focus()
            Exit Sub
        End If

        If Not ValidasiData(txtKodeJenis.Text.Trim,
                        "select [kd jenis] from jenis_kendaraan where [kd jenis] = '" & txtKodeJenis.Text.Trim & "'",
                        "kode jenis yang anda masukkan sudah tersimpan dalam database") Then

            txtKodeJenis.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = KoneksiSQL()
            Conn.Open()
            Using Cmd As New SqlCommand()
                Try
                    With Cmd
                        .Connection = Conn
                        .CommandText = "add_jenis_kendaraan"
                        .CommandType = Data.CommandType.StoredProcedure
                        .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                        .Parameters(0).SqlDbType = Data.SqlDbType.NChar
                        .Parameters(0).Direction = Data.ParameterDirection.Output

                        .Parameters.Add(New SqlParameter("@mPROCESS", Data.SqlDbType.NChar, 6)).Value = "ADD"
                        .Parameters.Add(New SqlParameter("@kd_jenis", Data.SqlDbType.NChar, 10)).Value = txtKodeJenis.Text.Trim
                        .Parameters.Add(New SqlParameter("@keterangan", Data.SqlDbType.NChar, 50)).Value = txtJenisKendaraan.Text.Trim
                        .ExecuteNonQuery()

                        If .Parameters("@mERROR_MESSAGE").Value.ToString.Trim = "Y" Then
                            grdJenisKendaraan.Rows.Add(New Object() {txtKodeJenis.Text.Trim, txtJenisKendaraan.Text.Trim})
                        Else
                            MsgBox(.Parameters("@mERROR_MESSAGE").Value.ToString.Trim)
                            Exit Sub
                        End If

                    End With
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End Using
            Conn.Close()
        DefaultSetting()
    End Sub

    Private Sub UpdateData()
        If txtKodeJenis.Text.Trim = "" Then
            MsgBox("kode jenis boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtKodeJenis.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd
                    .Connection = Conn
                    .CommandText = "update_jenis_kendaraan"
                    .CommandType = Data.CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = Data.SqlDbType.NChar
                    .Parameters(0).Direction = Data.ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", Data.SqlDbType.NChar, 6)).Value = "UPDATE"
                    .Parameters.Add(New SqlParameter("@kd_jenis", Data.SqlDbType.NChar, 10)).Value = txtKodeJenis.Text.Trim
                    .Parameters.Add(New SqlParameter("@keterangan", Data.SqlDbType.NChar, 50)).Value = txtJenisKendaraan.Text.Trim
                    .ExecuteNonQuery()

                    If .Parameters("@mERROR_MESSAGE").Value.ToString.Trim = "Y" Then
                        grdJenisKendaraan.CurrentRow.Cells.Item(0).Value = txtKodeJenis.Text.Trim
                        grdJenisKendaraan.CurrentRow.Cells.Item(1).Value = txtJenisKendaraan.Text.Trim
                    Else
                        MsgBox(.Parameters("@mERROR_MESSAGE").Value.ToString.Trim)
                        Exit Sub
                    End If
                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Conn.Close()
        DefaultSetting()
    End Sub

    Private Sub BtnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "jenis_kendaraan", "delete") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "error")
            DefaultSetting()
            Exit Sub
        End If
        Dim cMsg As String = MsgBox("Anda yakin ingin menghapus data ini?", MsgBoxStyle.Exclamation + vbYesNo, "Konfirmasi")
        If cMsg = vbYes Then
            Dim Conn As SqlConnection = KoneksiSQL()
            Conn.Open()

            Using Cmd As New SqlCommand()
                Try
                    With Cmd
                        .Connection = Conn
                        .CommandText = "delete_jenis_kendaraan"
                        .CommandType = Data.CommandType.StoredProcedure
                        .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                        .Parameters(0).SqlDbType = Data.SqlDbType.NChar
                        .Parameters(0).Direction = Data.ParameterDirection.Output

                        .Parameters.Add(New SqlParameter("@mPROCESS", Data.SqlDbType.NChar, 6)).Value = "DELETE"
                        .Parameters.Add(New SqlParameter("@kd_jenis", Data.SqlDbType.NChar, 10)).Value = txtKodeJenis.Text.Trim
                        .ExecuteNonQuery()

                        If .Parameters("@mERROR_MESSAGE").Value.ToString.Trim = "Y" Then
                            grdJenisKendaraan.Rows.RemoveAt(grdJenisKendaraan.CurrentCell.RowIndex)
                        Else
                            MsgBox(.Parameters("@mERROR_MESSAGE").Value.ToString.Trim)
                            Exit Sub
                        End If
                    End With
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End Using
            Conn.Close()
        End If
        DefaultSetting()
    End Sub
End Class
