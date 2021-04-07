Imports System.Data.SqlClient
Public Class ctrlUsers
    Dim lNew As Boolean
    Dim lShow As Boolean
    Dim lGridEditable As Boolean

    Private Sub ClearScreen()
        txtUserName.Text = ""
        txtPassword.Text = ""
        txtRetype.Text = ""
        cboUserGroup.Text = ""
        chkCheckAll.CheckState = CheckState.Unchecked

        'uncheck all access
        grdAccess.GridNavigator.SelectLastRow()
        grdAccess.GridNavigator.SelectFirstRow()
        For Each datarows In grdAccess.Rows
            grdAccess.CurrentRow.Cells.Item(3).Value = False
            grdAccess.GridNavigator.SelectNextRow(1)
        Next
    End Sub

    Private Sub DisableText()
        txtUserName.Properties.ReadOnly = True
        txtPassword.Properties.ReadOnly = True
        txtRetype.Properties.ReadOnly = True
        cboUserGroup.Properties.ReadOnly = True
        chkCheckAll.Properties.ReadOnly = True
    End Sub

    Private Sub EnableText()
        txtUserName.Properties.ReadOnly = False
        txtPassword.Properties.ReadOnly = False
        txtRetype.Properties.ReadOnly = False
        cboUserGroup.Properties.ReadOnly = False
        chkCheckAll.Properties.ReadOnly = False
    End Sub

    Private Sub DefaultSetting()
        ClearScreen()
        DisableText()
        btnTambah.Text = "&Tambah"
        btnHapus.Enabled = False
        btnSimpan.Enabled = False
        lGridEditable = False
    End Sub
    Private Sub BtnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        If btnTambah.Text = "&Tambah" Then
            If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "users", "add") Then
                MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "error")
                DefaultSetting()
                Exit Sub
            End If
            ClearScreen()
            EnableText()

            lNew = True
            lShow = False
            lGridEditable = True
            btnTambah.Text = "&Batal"
            btnSimpan.Enabled = True
            btnHapus.Enabled = False
            btnSimpan.Enabled = True

            txtUserName.Focus()

        ElseIf btnTambah.Text = "&Ubah" Then
            If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "users", "update") Then
                MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "error")
                DefaultSetting()
                Exit Sub
            End If
            EnableText()
            lNew = False
            lShow = False
            lGridEditable = True
            btnTambah.Text = "&Batal"
            btnSimpan.Enabled = True
            btnHapus.Enabled = False
            btnSimpan.Enabled = True
            txtUserName.Focus()

        ElseIf btnTambah.Text = "&Batal" Then
            DefaultSetting()
        End If
    End Sub

    Private Sub ctrlUsers_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Dock = DockStyle.Fill
        LoadAccessRule()
        DefaultSetting()
    End Sub

    Private Sub LoadAccessRule()
        grdAccess.Rows.Clear()
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select [parent],[process],[ac_id] " &
                                   "from access_control order by [ac_id] asc"
                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        grdAccess.Rows.Add(New Object() {rdr.Item(0).ToString.Trim,
                                           rdr.Item(1).ToString.Trim,
                                           rdr.Item(2).ToString.Trim})
                    End While
                    rdr.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Conn.Close()
    End Sub
    Private Sub GrdAccess_CellBeginEdit(sender As Object, e As Telerik.WinControls.UI.GridViewCellCancelEventArgs) Handles grdAccess.CellBeginEdit
        If lGridEditable Then
            If e.ColumnIndex <> 3 Then
                e.Cancel = True
            End If
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub ChkCheckAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkCheckAll.CheckedChanged
        If chkCheckAll.Checked = True Then
            chkCheckAll.Text = "Uncheck All"
            grdAccess.GridNavigator.SelectLastRow()
            grdAccess.GridNavigator.SelectFirstRow()
            For Each datarows In grdAccess.Rows
                grdAccess.CurrentRow.Cells.Item(3).Value = True
                grdAccess.GridNavigator.SelectNextRow(1)
            Next
        ElseIf chkCheckAll.Checked = False Then
            chkCheckAll.Text = "Check All"
            grdAccess.GridNavigator.SelectLastRow()
            grdAccess.GridNavigator.SelectFirstRow()
            For Each datarows In grdAccess.Rows
                grdAccess.CurrentRow.Cells.Item(3).Value = False
                grdAccess.GridNavigator.SelectNextRow(1)
            Next
        End If
    End Sub

    Private Sub BtnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If lNew Then
            SaveData()
        Else
            UpdateData()
        End If
    End Sub

    Private Sub SaveData()
        Dim cAkses As String
        If txtUserName.Text.Trim = "" Then
            MsgBox("user name tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtUserName.Focus()
            Exit Sub
        End If

        If txtPassword.Text.Trim = "" Then
            MsgBox("password tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtPassword.Focus()
            Exit Sub
        End If

        If txtPassword.Text.Trim <> txtRetype.Text.Trim Then
            MsgBox("password yang dimasukkan tidak sama, silahkan ulangi masukkan password", MsgBoxStyle.Critical, "Error")
            txtPassword.Focus()
            Exit Sub
        End If

        If cboUserGroup.Text.Trim = "" Then
            MsgBox("group user tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            cboUserGroup.Focus()
            Exit Sub
        End If


        If ValidasiData(txtUserName.Text.Trim,
                        "select [kd petugas] from petugas where [kd petugas] = '" & txtUserName.Text.Trim & "'",
                        "user name yang anda masukkan sudah tersimpan dalam database") Then

            Dim Conn As SqlConnection = KoneksiSQL()
            Conn.Open()

            Using Cmd As New SqlCommand()
                Try
                    With Cmd
                        .Connection = Conn
                        .CommandText = "add_users"
                        .CommandType = Data.CommandType.StoredProcedure
                        .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                        .Parameters(0).SqlDbType = Data.SqlDbType.NChar
                        .Parameters(0).Direction = Data.ParameterDirection.Output

                        .Parameters.Add(New SqlParameter("@rowid", ""))
                        .Parameters(1).SqlDbType = Data.SqlDbType.Int
                        .Parameters(1).Direction = Data.ParameterDirection.Output

                        .Parameters.Add(New SqlParameter("@mPROCESS", Data.SqlDbType.NChar, 6)).Value = "ADD"
                        .Parameters.Add(New SqlParameter("@kd_petugas", Data.SqlDbType.NChar, 10)).Value = txtUserName.Text.Trim
                        .Parameters.Add(New SqlParameter("@kd_level", Data.SqlDbType.NChar, 10)).Value = cboUserGroup.Text.Trim
                        .Parameters.Add(New SqlParameter("@password", Data.SqlDbType.NChar, 32)).Value = Simple3Des.GetMd5Hash(txtPassword.Text.Trim)

                        Dim dtAccess As New Data.DataTable()
                        dtAccess.Columns.Add("kd_petugas", GetType([String])).MaxLength = 10
                        dtAccess.Columns.Add("ac_id", GetType(Integer))
                        dtAccess.Columns.Add("access", GetType([String])).MaxLength = 1

                        For Each DataRows In grdAccess.Rows
                            If DataRows.Cells.Item(3).Value = True Then
                                cAkses = "Y"
                            Else
                                cAkses = "T"
                            End If
                            dtAccess.Rows.Add(New Object() {txtUserName.Text.Trim,
                                                            DataRows.Cells.Item(2).Value.ToString.Trim,
                                                            cAkses.Trim})
                        Next

                        .Parameters.Add("@access_control_rule_TVP", Data.SqlDbType.Structured).Value = dtAccess
                        .ExecuteNonQuery()

                        If .Parameters("@mERROR_MESSAGE").Value.ToString.Trim = "Y" Then
                            grdUser.Rows.Add(New Object() {txtUserName.Text.Trim,
                                             cboUserGroup.Text.Trim,
                                             .Parameters("@rowid").Value})
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
        Else
            txtUserName.Focus()
        End If
    End Sub

    Private Sub UpdateData()
        Dim cAkses As String
        If txtUserName.Text.Trim = "" Then
            MsgBox("user name tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtUserName.Focus()
            Exit Sub
        End If

        If txtPassword.Text.Trim = "" Then
            MsgBox("password tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtPassword.Focus()
            Exit Sub
        End If

        If txtPassword.Text.Trim <> txtRetype.Text.Trim Then
            MsgBox("password yang dimasukkan tidak sama, silahkan ulangi masukkan password", MsgBoxStyle.Critical, "Error")
            txtPassword.Focus()
            Exit Sub
        End If

        If cboUserGroup.Text.Trim = "" Then
            MsgBox("group user tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            cboUserGroup.Focus()
            Exit Sub
        End If


        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd
                    .Connection = Conn
                    .CommandText = "update_users"
                    .CommandType = Data.CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = Data.SqlDbType.NChar
                    .Parameters(0).Direction = Data.ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", Data.SqlDbType.NChar, 6)).Value = "UPDATE"
                    .Parameters.Add(New SqlParameter("@kd_petugas", Data.SqlDbType.NChar, 10)).Value = txtUserName.Text.Trim
                    .Parameters.Add(New SqlParameter("@kd_level", Data.SqlDbType.NChar, 10)).Value = cboUserGroup.Text.Trim
                    .Parameters.Add(New SqlParameter("@password", Data.SqlDbType.NChar, 32)).Value = Simple3Des.GetMd5Hash(txtPassword.Text.Trim)

                    Dim dtAccess As New Data.DataTable()
                    dtAccess.Columns.Add("kd_petugas", GetType([String])).MaxLength = 10
                    dtAccess.Columns.Add("ac_id", GetType(Integer))
                    dtAccess.Columns.Add("access", GetType([String])).MaxLength = 1

                    For Each DataRows In grdAccess.Rows
                        If DataRows.Cells.Item(3).Value = True Then
                            cAkses = "Y"
                        Else
                            cAkses = "T"
                        End If
                        dtAccess.Rows.Add(New Object() {txtUserName.Text.Trim,
                                                        DataRows.Cells.Item(2).Value.ToString.Trim,
                                                        cAkses.Trim})
                    Next

                    .Parameters.Add("@access_control_rule_TVP", Data.SqlDbType.Structured).Value = dtAccess
                    .ExecuteNonQuery()

                    If .Parameters("@mERROR_MESSAGE").Value.ToString.Trim = "Y" Then
                        grdUser.CurrentRow.Cells.Item(0).Value = txtUserName.Text.Trim
                        grdUser.CurrentRow.Cells.Item(1).Value = cboUserGroup.Text.Trim
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
        If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "users", "delete") Then
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
                        .CommandText = "delete_users"
                        .CommandType = Data.CommandType.StoredProcedure
                        .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                        .Parameters(0).SqlDbType = Data.SqlDbType.NChar
                        .Parameters(0).Direction = Data.ParameterDirection.Output

                        .Parameters.Add(New SqlParameter("@mPROCESS", Data.SqlDbType.NChar, 6)).Value = "DELETE"
                        .Parameters.Add(New SqlParameter("@kd_petugas", Data.SqlDbType.NChar, 10)).Value = txtUserName.Text.Trim
                        .ExecuteNonQuery()

                        If .Parameters("@mERROR_MESSAGE").Value.ToString.Trim = "Y" Then
                            grdUser.Rows.RemoveAt(grdUser.CurrentCell.RowIndex)
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

    Private Sub BtnCariData_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnCariData.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "users", "view") Then
                MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "error")
                DefaultSetting()
                Exit Sub
            End If

            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            grdUser.Rows.Clear()

            Dim Conn As SqlConnection = KoneksiSQL()
            Conn.Open()
            Try
                Using cmd As New SqlCommand
                    With cmd
                        .Connection = Conn

                        .CommandText = "select [kd petugas],[kd level],[id] " &
                                       "from petugas where [kd petugas] like '%" &
                                       btnCariData.Text.Trim & "%'"

                        Dim rdr As SqlDataReader = .ExecuteReader
                        While rdr.Read
                            grdUser.Rows.Add(New Object() {rdr.Item(0).ToString.Trim,
                                               rdr.Item(1).ToString.Trim,
                                               rdr.Item(2).ToString.Trim})
                        End While
                    End With
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Conn.Close()
            xForm.Dispose()
        End If
    End Sub

    Private Sub GrdUser_Click(sender As Object, e As EventArgs) Handles grdUser.Click
        Try
            Dim lAccess As Boolean
            txtUserName.Text = grdUser.CurrentRow.Cells.Item(0).Value.ToString.Trim
            cboUserGroup.Text = grdUser.CurrentRow.Cells.Item(1).Value.ToString.Trim

            grdAccess.Rows.Clear()

            Dim Conn As SqlConnection = KoneksiSQL()
            Conn.Open()
            Try
                Using cmd As New SqlCommand
                    With cmd
                        .Connection = Conn

                        .CommandText = "SELECT access_control.[parent]," &
                                        "access_control.process," &
                                        "access_control_rule.[ac_id]," &
                                        "access_control_rule.[access] " &
                                        "FROM access_control access_control " &
                                        "INNER JOIN access_control_rule " &
                                        "access_control_rule ON access_control.[ac_id] = access_control_rule.[ac_id] " &
                                        "WHERE access_control_rule.[kd petugas] = '" & txtUserName.Text.Trim & "' " &
                                        "ORDER BY access_control_rule.[ac_id] ASC"

                        Dim rdr As SqlDataReader = .ExecuteReader
                        While rdr.Read
                            If rdr.Item(3).ToString.Trim = "Y" Then
                                lAccess = True
                            ElseIf rdr.Item(3).ToString.Trim = "T" Then
                                lAccess = False
                            End If
                            grdAccess.Rows.Add(New Object() {rdr.Item(0).ToString.Trim,
                                               rdr.Item(1).ToString.Trim,
                                               rdr.Item(2).ToString.Trim, lAccess})
                        End While
                    End With
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Conn.Close()
        Catch ex As Exception

        End Try
        btnTambah.Text = "&Ubah"
        btnHapus.Enabled = True
    End Sub
End Class
