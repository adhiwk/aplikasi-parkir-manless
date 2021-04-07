Imports System.Data.SqlClient
Public Class frmJenisMember
    Dim lNew As Boolean
    Dim lShow As Boolean
    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub
    Private Sub BtnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        If btnTambah.Text = "&Tambah" Then
            If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "jenis_member", "add") Then
                MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "error")
                DefaultSetting()
                Exit Sub
            End If
            ClearHeader()
            ClearDetail()
            EnableText()
            txtKodeJenisMember.Focus()
            lNew = True
            lShow = False
            btnTambah.Text = "&Batal"
            btnAdd.Enabled = True
            btnRemove.Enabled = False
            btnSimpan.Enabled = True
        ElseIf btnTambah.Text = "&Ubah" Then
            If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "jenis_member", "update") Then
                MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "error")
                DefaultSetting()
                Exit Sub
            End If
            EnableText()
            txtKodeJenisMember.Focus()
            lNew = False
            lShow = False
            btnTambah.Text = "&Batal"
            btnAdd.Enabled = True
            btnRemove.Enabled = False
            btnSimpan.Enabled = True
        Else
            DefaultSetting()
        End If
    End Sub

    Private Sub ClearHeader()
        txtKodeJenisMember.Text = ""
        txtJenisMember.Text = ""
        cboJenisTarif.Text = ""
        txtPersenTarif.Text = 0
    End Sub

    Private Sub ClearDetail()
        cboJenisKendaraan.Text = ""
        txtKodeJenisKendaraan.Text = ""
        txtTarif.Text = 0
    End Sub

    Private Sub DisableText()
        txtKodeJenisMember.Properties.ReadOnly = True
        txtJenisMember.Properties.ReadOnly = True
        cboJenisTarif.Properties.ReadOnly = True
        txtPersenTarif.Properties.ReadOnly = True
        cboJenisKendaraan.Properties.ReadOnly = True
        txtKodeJenisKendaraan.Properties.ReadOnly = True
        txtTarif.Properties.ReadOnly = True
    End Sub

    Private Sub EnableText()
        txtKodeJenisMember.Properties.ReadOnly = False
        txtJenisMember.Properties.ReadOnly = False
        cboJenisTarif.Properties.ReadOnly = False
        txtPersenTarif.Properties.ReadOnly = False
        cboJenisKendaraan.Properties.ReadOnly = False
        'txtKodeJenisKendaraan.Properties.ReadOnly = True
        txtTarif.Properties.ReadOnly = False
    End Sub

    Private Sub DefaultSetting()
        ClearHeader()
        ClearDetail()
        DisableText()
        grdTarif.Rows.Clear()

        btnTambah.Text = "&Tambah"
        btnHapus.Enabled = False
        btnSimpan.Enabled = False
        btnRefresh.Enabled = True

        btnAdd.Enabled = False
        btnRemove.Enabled = False
    End Sub

    Private Sub frmJenisMember_Load(sender As Object, e As EventArgs) Handles Me.Load
        DefaultSetting()

        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select [keterangan] from jenis_kendaraan order by [keterangan] asc"
                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        cboJenisKendaraan.Properties.Items.Add(rdr.Item(0).ToString.Trim)
                    End While
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Conn.Close()
    End Sub

    Private Sub CboJenisKendaraan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboJenisKendaraan.SelectedIndexChanged
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select [kd jenis] from jenis_kendaraan where [keterangan] = '" & cboJenisKendaraan.Text.Trim & "'"
                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        txtKodeJenisKendaraan.Text = rdr.Item(0).ToString.Trim
                    End While
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Conn.Close()
    End Sub

    Private Sub BtnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If lNew Then
            SaveData
        Else
            UpdateData
        End If
    End Sub

    Private Sub SaveData()
        Dim cJenisTarif As String = ""

        If txtKodeJenisMember.Text.Trim = "" Then
            MsgBox("kode jenis boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtKodeJenisMember.Focus()
            Exit Sub
        End If

        If txtJenisMember.Text.Trim = "" Then
            MsgBox("jenis member boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtJenisMember.Focus()
            Exit Sub
        End If

        If cboJenisTarif.Text.Trim = "" Then
            MsgBox("jenis tarif boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            cboJenisTarif.Focus()
            Exit Sub
        End If

        If ValidasiData(txtKodeJenisMember.Text.Trim,
                        "select [kdj member] from jenis_member where [kdj member] = '" & txtKodeJenisMember.Text.Trim & "'",
                        "kode jenis yang anda masukkan sudah tersimpan dalam database") Then
            If cboJenisTarif.Text.Trim = "PERSENTASE (P)" Then
                cJenisTarif = "P"
            ElseIf cboJenisTarif.Text.Trim = "JENIS KENDARAAN (J)" Then
                cJenisTarif = "J"
            End If

            Dim Conn As SqlConnection = KoneksiSQL()
            Conn.Open()

            Using Cmd As New SqlCommand()
                Try
                    With Cmd
                        .Connection = Conn
                        .CommandText = "add_jenis_member"
                        .CommandType = Data.CommandType.StoredProcedure
                        .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                        .Parameters(0).SqlDbType = Data.SqlDbType.NChar
                        .Parameters(0).Direction = Data.ParameterDirection.Output

                        .Parameters.Add(New SqlParameter("@mPROCESS", Data.SqlDbType.NChar, 6)).Value = "ADD"
                        .Parameters.Add(New SqlParameter("@kdj_member", Data.SqlDbType.NChar, 3)).Value = txtKodeJenisMember.Text.Trim
                        .Parameters.Add(New SqlParameter("@jenis_member", Data.SqlDbType.NChar, 50)).Value = txtJenisMember.Text.Trim
                        .Parameters.Add(New SqlParameter("@jenis_tarif", Data.SqlDbType.NChar, 1)).Value = cJenisTarif.Trim
                        .Parameters.Add(New SqlParameter("@persen_tarif", Data.SqlDbType.Int)).Value = Integer.Parse(txtPersenTarif.Text.Trim)

                        Dim dtJenisMemberDetail As New Data.DataTable()
                        dtJenisMemberDetail.Columns.Add("kdj_member", GetType([String])).MaxLength = 3
                        dtJenisMemberDetail.Columns.Add("kd_jenis", GetType([String])).MaxLength = 10
                        dtJenisMemberDetail.Columns.Add("tarif", GetType(Decimal))

                        For Each DataRows In grdTarif.Rows
                            dtJenisMemberDetail.Rows.Add(New Object() {
                                                  txtKodeJenisMember.Text.Trim,
                                                  DataRows.Cells.Item(1).Value.ToString.Trim,
                                                  Decimal.Parse(DataRows.Cells.Item(2).Value.ToString.Trim)})
                        Next

                        .Parameters.Add("@jenis_member_detail_TVP", Data.SqlDbType.Structured).Value = dtJenisMemberDetail
                        .ExecuteNonQuery()

                        If .Parameters("@mERROR_MESSAGE").Value.ToString.Trim = "Y" Then
                            grdJenisMember.Rows.Add(New Object() {txtKodeJenisMember.Text.Trim, txtJenisMember.Text.Trim, cJenisTarif.Trim})
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

    Private Sub UpdateData()
        Dim cJenisTarif As String = ""
        If txtKodeJenisMember.Text.Trim = "" Then
            MsgBox("kode jenis boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtKodeJenisMember.Focus()
            Exit Sub
        End If

        If txtJenisMember.Text.Trim = "" Then
            MsgBox("jenis member boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtJenisMember.Focus()
            Exit Sub
        End If

        If cboJenisTarif.Text.Trim = "" Then
            MsgBox("jenis tarif boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            cboJenisTarif.Focus()
            Exit Sub
        End If

        If cboJenisTarif.Text.Trim = "PERSENTASE (P)" Then
            cJenisTarif = "P"
        ElseIf cboJenisTarif.Text.Trim = "JENIS KENDARAAN (J)" Then
            cJenisTarif = "J"
        End If

        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd
                    .Connection = Conn
                    .CommandText = "update_jenis_member"
                    .CommandType = Data.CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = Data.SqlDbType.NChar
                    .Parameters(0).Direction = Data.ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", Data.SqlDbType.NChar, 6)).Value = "UPDATE"
                    .Parameters.Add(New SqlParameter("@kdj_member", Data.SqlDbType.NChar, 3)).Value = txtKodeJenisMember.Text.Trim
                    .Parameters.Add(New SqlParameter("@jenis_member", Data.SqlDbType.NChar, 50)).Value = txtJenisMember.Text.Trim
                    .Parameters.Add(New SqlParameter("@jenis_tarif", Data.SqlDbType.NChar, 1)).Value = cJenisTarif.Trim
                    .Parameters.Add(New SqlParameter("@persen_tarif", Data.SqlDbType.Int)).Value = Integer.Parse(txtPersenTarif.Text.Trim)

                    Dim dtJenisMemberDetail As New Data.DataTable()
                    dtJenisMemberDetail.Columns.Add("kdj_member", GetType([String])).MaxLength = 3
                    dtJenisMemberDetail.Columns.Add("kd_jenis", GetType([String])).MaxLength = 10
                    dtJenisMemberDetail.Columns.Add("tarif", GetType(Decimal))

                    For Each DataRows In grdTarif.Rows
                        dtJenisMemberDetail.Rows.Add(New Object() {
                                                  txtKodeJenisMember.Text.Trim,
                                                  DataRows.Cells.Item(1).Value.ToString.Trim,
                                                  Decimal.Parse(DataRows.Cells.Item(2).Value.ToString.Trim)})
                    Next

                    .Parameters.Add("@jenis_member_detail_TVP", Data.SqlDbType.Structured).Value = dtJenisMemberDetail
                    .ExecuteNonQuery()

                    If .Parameters("@mERROR_MESSAGE").Value.ToString.Trim = "Y" Then
                        grdJenisMember.CurrentRow.Cells.Item(0).Value = txtKodeJenisMember.Text.Trim
                        grdJenisMember.CurrentRow.Cells.Item(1).Value = txtJenisMember.Text.Trim
                        grdJenisMember.CurrentRow.Cells.Item(2).Value = cJenisTarif.Trim
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
        If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "jenis_member", "delete") Then
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
                        .CommandText = "delete_jenis_member"
                        .CommandType = Data.CommandType.StoredProcedure
                        .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                        .Parameters(0).SqlDbType = Data.SqlDbType.NChar
                        .Parameters(0).Direction = Data.ParameterDirection.Output

                        .Parameters.Add(New SqlParameter("@mPROCESS", Data.SqlDbType.NChar, 6)).Value = "DELETE"
                        .Parameters.Add(New SqlParameter("@kdj_member", Data.SqlDbType.NChar, 10)).Value = txtKodeJenisMember.Text.Trim
                        .ExecuteNonQuery()

                        If .Parameters("@mERROR_MESSAGE").Value.ToString.Trim = "Y" Then
                            grdJenisMember.Rows.RemoveAt(grdJenisMember.CurrentCell.RowIndex)
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

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If txtKodeJenisKendaraan.Text.Trim = "" Then
            MsgBox("jenis kendaraan tidak boleh kosong", MsgBoxStyle.Critical, "error")
            Exit Sub
        End If

        grdTarif.Rows.Add(New Object() {cboJenisKendaraan.Text.Trim,
                          txtKodeJenisKendaraan.Text.Trim,
                          txtTarif.Text.Trim})
        ClearDetail()
    End Sub

    Private Sub CboJenisTarif_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboJenisTarif.SelectedIndexChanged
        If Not lShow Then
            If cboJenisTarif.Text.Trim = "PERSENTASE (P)" Then
                txtPersenTarif.Properties.ReadOnly = False
                cboJenisKendaraan.Properties.ReadOnly = True
                txtTarif.Properties.ReadOnly = True
                btnAdd.Enabled = False
                btnRemove.Enabled = False
                txtPersenTarif.Focus()
            ElseIf cboJenisTarif.Text.Trim = "JENIS KENDARAAN (J)" Then
                txtPersenTarif.Text = 0
                txtPersenTarif.Properties.ReadOnly = True
                cboJenisKendaraan.Properties.ReadOnly = False
                txtTarif.Properties.ReadOnly = False
                btnAdd.Enabled = True
                btnRemove.Enabled = False
                cboJenisKendaraan.Focus()
            End If
        End If
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "jenis_member", "view") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "error")
            DefaultSetting()
            Exit Sub
        End If

        grdJenisMember.Rows.Clear()
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select [kdj member],[jenis member],[jenis tarif] " &
                    "from jenis_member order by [kdj member] asc"

                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        grdJenisMember.Rows.Add(New Object() {rdr.Item(0).ToString.Trim,
                                                           rdr.Item(1).ToString.Trim,
                                                           rdr.Item(2).ToString.Trim})

                    End While
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Conn.Close()
    End Sub

    Private Sub GrdJenisMember_Click(sender As Object, e As EventArgs) Handles grdJenisMember.Click
        Try
            DefaultSetting()
            ShowRecord()
            btnTambah.Text = "&Ubah"
            btnSimpan.Enabled = False
            btnHapus.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ShowRecord()
        lShow = True
        txtKodeJenisMember.Text = grdJenisMember.CurrentRow.Cells.Item(0).Value.ToString.Trim
        txtJenisMember.Text = grdJenisMember.CurrentRow.Cells.Item(1).Value.ToString.Trim
        If grdJenisMember.CurrentRow.Cells.Item(2).Value.ToString.Trim = "P" Then
            cboJenisTarif.Text = "PERSENTASE (P)"
        Else
            cboJenisTarif.Text = "JENIS KENDARAAN (J)"
        End If

        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select [persen tarif] " &
                    "from jenis_member where [kdj member] = '" & txtKodeJenisMember.Text.Trim & "'"

                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        txtPersenTarif.Text = FormatNumber(rdr.Item(0).ToString.Trim, 0)
                    End While
                    rdr.Close()

                    .CommandText = "SELECT jenis_kendaraan.[keterangan]," &
                                   "jenis_member_detail.[kd jenis],jenis_member_detail.[tarif] " &
                                   "FROM jenis_kendaraan jenis_kendaraan " &
                                   "INNER JOIN jenis_member_detail jenis_member_detail " &
                                   "ON jenis_kendaraan.[kd jenis] = jenis_member_detail.[kd jenis] " &
                                   "WHERE [kdj member] = '" & txtKodeJenisMember.Text.Trim & "'"

                    grdTarif.Rows.Clear()
                    rdr = .ExecuteReader
                    While rdr.Read
                        grdTarif.Rows.Add(New Object() {rdr.Item(0).ToString.Trim,
                                          rdr.Item(1).ToString.Trim,
                                          FormatNumber(rdr.Item(2).ToString.Trim, 0)})
                    End While
                    rdr.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Conn.Close()

    End Sub

    Private Sub GrdTarif_Click(sender As Object, e As EventArgs) Handles grdTarif.Click
        If lShow = False Then
            cboJenisKendaraan.Text = grdTarif.CurrentRow.Cells.Item(0).Value.ToString.Trim
            txtKodeJenisKendaraan.Text = grdTarif.CurrentRow.Cells.Item(1).Value.ToString.Trim
            txtTarif.Text = Decimal.Parse(grdTarif.CurrentRow.Cells.Item(2).Value.ToString.Trim)
            btnRemove.Enabled = True
        End If
    End Sub

    Private Sub BtnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        Try
            grdTarif.Rows.RemoveAt(grdTarif.CurrentCell.RowIndex)
            ClearDetail()
            btnRemove.Enabled = False
        Catch ex As Exception

        End Try
    End Sub
End Class