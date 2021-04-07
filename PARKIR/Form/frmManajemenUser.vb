Imports System.Data.SqlClient
Public Class frmManajemenUser
    Dim lNew As Boolean

    Private Sub frmManajemenUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DefaultSetting()
        LoadData()
    End Sub

    Private Sub LoadData()
        cboGroupUser.Properties.Items.Clear()
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select [nama level] from level_petugas order by [nama level] asc"
                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        cboGroupUser.Properties.Items.Add(rdr.Item(0).ToString.Trim)
                    End While
                    .Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
        End Try
    End Sub

    Private Sub DefaultSetting()
        ClearText()
        DisableText()
        btnHapus.Enabled = False
        btnTambah.Text = "&Tambah"
        btnSimpan.Enabled = False
        btnRefresh.Enabled = True
        btnTutup.Enabled = True
    End Sub

    Private Sub ClearText()
        txtNamaPetugas.Text = ""
        txtUserId.Text = ""
        txtPassword1.Text = ""
        txtPassword2.Text = ""
        cboGroupUser.Text = ""
        txtKodeGroup.Text = ""
    End Sub

    Private Sub DisableText()
        txtNamaPetugas.Properties.ReadOnly = True
        txtUserId.Properties.ReadOnly = True
        txtPassword1.Properties.ReadOnly = True
        txtPassword2.Properties.ReadOnly = True
        cboGroupUser.Properties.ReadOnly = True
        txtKodeGroup.Properties.ReadOnly = True
    End Sub

    Private Sub EnableText()
        txtNamaPetugas.Properties.ReadOnly = False
        txtUserId.Properties.ReadOnly = False
        txtPassword1.Properties.ReadOnly = False
        txtPassword2.Properties.ReadOnly = False
        cboGroupUser.Properties.ReadOnly = False
    End Sub

    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        If btnTambah.Text = "&Tambah" Then
            lNew = True
            ClearText()
            EnableText()
            btnHapus.Enabled = False
            btnSimpan.Enabled = True
            btnTambah.Text = "&Batal"
            txtNamaPetugas.Focus()
        ElseIf btnTambah.Text = "&Ubah" Then
            lNew = False
            EnableText()
            btnSimpan.Enabled = True
            btnHapus.Enabled = False
            btnTambah.Text = "&Batal"
            txtNamaPetugas.Focus()
        ElseIf btnTambah.Text = "&Batal" Then
            DefaultSetting()
        End If
    End Sub


    Private Sub btnTutup_Click(sender As Object, e As EventArgs) Handles btnTutup.Click
        Me.Dispose()
    End Sub

    Private Sub cboGroupUser_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboGroupUser.SelectedIndexChanged
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select [kd level] from level_petugas where [nama level] = '" & cboGroupUser.Text.Trim & "'"
                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        txtKodeGroup.Text = rdr.Item(0).ToString.Trim
                    End While
                    .Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
        End Try
    End Sub

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        Dim cMsg As String = MsgBox("Anda yakin ingin menghapus data ini?", MsgBoxStyle.Exclamation + vbYesNo, "Konfirmasi")
        If cMsg = vbYes Then
            Try
                ExecuteQuery("delete from petugas where [kd petugas] = '" & _
                          txtUserId.Text.Trim & "'")

                ExecuteQuery("delete from sesi_login where [kd petugas] = '" & _
                          txtUserId.Text.Trim & "'")

                grdPetugas.Rows.RemoveAt(grdPetugas.CurrentCell.RowIndex)
            Catch ex As Exception
                MsgBox(ex.Message.Trim)
            End Try
        End If
        DefaultSetting()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click

        grdPetugas.Rows.Clear()

        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "SELECT petugas.[nama petugas],petugas.[kd petugas],level_petugas.[nama level],petugas.[kd level] " & _
                                   "FROM petugas petugas INNER JOIN level_petugas level_petugas ON petugas.[kd level] = level_petugas.[kd level] " & _
                                   "ORDER BY petugas.[nama petugas] ASC"

                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        grdPetugas.Rows.Add(New Object() {rdr.Item(0).ToString.Trim, _
                                                           rdr.Item(1).ToString.Trim, _
                                                          rdr.Item(2).ToString.Trim, _
                                                          rdr.Item(3).ToString.Trim})

                    End While
                    .Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
        End Try

    End Sub

    Private Sub grdPetugas_Click(sender As Object, e As EventArgs) Handles grdPetugas.Click
        ShowRecord()
        btnHapus.Enabled = True
        btnTambah.Text = "&Ubah"
        btnRefresh.Enabled = False
        btnTutup.Enabled = False
    End Sub

    Private Sub ShowRecord()
        Try

            txtNamaPetugas.Text = grdPetugas.CurrentRow.Cells.Item(0).Value.ToString.Trim
            txtUserId.Text = grdPetugas.CurrentRow.Cells.Item(1).Value.ToString.Trim
            cboGroupUser.Text = grdPetugas.CurrentRow.Cells.Item(2).Value.ToString.Trim
            txtKodeGroup.Text = grdPetugas.CurrentRow.Cells.Item(3).Value.ToString.Trim

        Catch ex As Exception
        End Try

        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select [password] from petugas where [kd petugas] = '" & txtUserId.Text.Trim & "'"

                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        txtPassword1.Text = rdr.Item(0).ToString.Trim
                        txtPassword2.Text = rdr.Item(0).ToString.Trim
                    End While
                    .Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
        End Try

    End Sub

    Private Sub grdPetugas_CurrentRowChanged(sender As Object, e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles grdPetugas.CurrentRowChanged
        ShowRecord()
        btnHapus.Enabled = True
        btnTambah.Text = "&Ubah"
        btnRefresh.Enabled = False
        btnTutup.Enabled = False
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If lNew Then
            SaveData()
        Else
            UpdateData()
        End If
    End Sub

    Private Sub SaveData()

        If txtUserId.Text.Trim = "" Then
            MsgBox("User Id tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtUserId.Focus()
            Exit Sub
        End If

        If txtPassword1.Text.Trim = "" Then
            MsgBox("Password tidak boleh kosong, silahkan pilih password anda", MsgBoxStyle.Critical, "Error")
            txtPassword1.Focus()
            Exit Sub
        End If

        If txtPassword1.Text.Trim <> txtPassword2.Text.Trim Then
            MsgBox("Password yang anda ketikkan tidak sama", MsgBoxStyle.Critical, "Error")
            txtPassword1.Text = ""
            txtPassword2.Text = ""
            txtPassword1.Focus()
            Exit Sub
        End If

        If ValidasiData(txtUserId.Text.Trim, _
                        "select [kd petugas] from petugas where [kd petugas] = '" & txtUserId.Text.Trim & "'", _
                        "user id yang anda masukkan sudah tersimpan dalam database") Then

            ExecuteQuery("insert into petugas ([kd petugas],[nama petugas],[kd level],[password]) values ('" &
                          txtUserId.Text.Trim & "','" &
                          txtNamaPetugas.Text.Trim & "','" &
                          txtKodeGroup.Text.Trim & "','" &
                          Simple3Des.GetMd5Hash(txtPassword1.Text.Trim) & "')")

            grdPetugas.Rows.Add(New Object() {txtNamaPetugas.Text.Trim, _
                                              txtUserId.Text.Trim, _
                                              cboGroupUser.Text.Trim, _
                                              txtKodeGroup.Text.Trim})
            DefaultSetting()
        End If
    End Sub

    Private Sub UpdateData()
        If txtUserId.Text.Trim = "" Then
            MsgBox("User Id tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtUserId.Focus()
            Exit Sub
        End If

        If txtPassword1.Text.Trim = "" Then
            MsgBox("Password tidak boleh kosong, silahkan pilih password anda", MsgBoxStyle.Critical, "Error")
            txtPassword1.Focus()
            Exit Sub
        End If

        If txtPassword1.Text.Trim <> txtPassword2.Text.Trim Then
            MsgBox("Password yang anda ketikkan tidak sama", MsgBoxStyle.Critical, "Error")
            txtPassword1.Text = ""
            txtPassword2.Text = ""
            txtPassword1.Focus()
            Exit Sub
        End If

        

        ExecuteQuery("update petugas set [kd petugas] = '" & _
                     txtUserId.Text.Trim & "',[nama petugas] = '" & _
                     txtNamaPetugas.Text.Trim & "',[kd level] = '" & _
                     txtKodeGroup.Text.Trim & "',[password] = '" & _
                     txtPassword1.Text.Trim & "' where [kd petugas] = '" & _
                     txtUserId.Text.Trim & "'")

        grdPetugas.CurrentRow.Cells.Item(0).Value = txtNamaPetugas.Text.Trim
        grdPetugas.CurrentRow.Cells.Item(1).Value = txtUserId.Text.Trim
        grdPetugas.CurrentRow.Cells.Item(2).Value = cboGroupUser.Text.Trim
        grdPetugas.CurrentRow.Cells.Item(3).Value = txtKodeGroup.Text.Trim

        DefaultSetting()
    End Sub
End Class