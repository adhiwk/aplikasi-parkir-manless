Imports System.Data.SqlClient
Public Class ctrlMember
    Dim lNew As Boolean
    Dim lShow As Boolean
    Dim cKodeArea As String
    Private Sub BtnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        If btnTambah.Text = "&Tambah" Then
            If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "member", "add") Then
                MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "error")
                DefaultSetting()
                Exit Sub
            End If
            ClearScreen()
            EnableText()

            lNew = True
            lShow = False
            btnTambah.Text = "&Batal"
            btnSimpan.Enabled = True
            btnHapus.Enabled = False
            btnSimpan.Enabled = True
            btnAdd.Enabled = True
            btnRemove.Enabled = False
            txtKodeMember.Text = "M." & Now.Year & Now.Month & Now.Day & "." & Now.Hour & Now.Minute & Now.Second
            txtNamaMember.Focus()

        ElseIf btnTambah.Text = "&Ubah" Then
            If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "member", "update") Then
                MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "error")
                DefaultSetting()
                Exit Sub
            End If
            ClearDetail()
            EnableText()
            lNew = False
            lShow = False
            btnTambah.Text = "&Batal"
            btnSimpan.Enabled = True
            btnHapus.Enabled = False
            btnSimpan.Enabled = True
            btnAdd.Enabled = True
            btnRemove.Enabled = False
            txtNamaMember.Focus()
        ElseIf btnTambah.Text = "&Batal" Then
            DefaultSetting()
        End If
    End Sub

    Private Sub ctrlMember_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Dock = DockStyle.Fill
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
                    rdr.Close()

                    .CommandText = "select [area_nopol],[kode_lokasi],[perusahaan],[alamat] From info"
                    rdr = .ExecuteReader
                    While rdr.Read
                        cKodeArea = rdr.Item(0).ToString.Trim
                    End While
                    rdr.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Conn.Close()
    End Sub

    Private Sub ClearScreen()
        txtKodeMember.Text = ""
        txtNamaMember.Text = ""
        txtJenisMember.Text = ""
        txtKodeJenisMember.Text = ""
        txtJenisPentarifan.Text = ""
        txtTarif.Text = ""
        txtAlamat.Text = ""
        txtTelpon.Text = ""
        dtExpDate.DateTime = Now.Date
        txtSecurityPIN.Text = ""
    End Sub

    Private Sub ClearDetail()
        cboJenisKendaraan.Text = ""
        txtKodeJenisKendaraaan.Text = ""
        txtDetail.Text = ""
        txtNomorPolisi.Text = ""
    End Sub

    Private Sub DefaultSetting()
        ClearScreen()
        ClearDetail()
        DisableText()
        btnTambah.Text = "&Tambah"
        btnHapus.Enabled = False
        btnSimpan.Enabled = False

        btnAdd.Enabled = False
        btnRemove.Enabled = False

        grdKendaraan.Rows.Clear()
        lShow = True
    End Sub

    Private Sub EnableText()
        txtKodeMember.Properties.ReadOnly = False
        txtNamaMember.Properties.ReadOnly = False
        txtJenisMember.Properties.ReadOnly = False
        'txtKodeJenisMember.Properties.ReadOnly = False
        'txtJenisPentarifan.Properties.ReadOnly = False
        'txtPersenTarif.Properties.ReadOnly = False
        txtAlamat.Properties.ReadOnly = False
        txtTelpon.Properties.ReadOnly = False
        dtExpDate.Properties.ReadOnly = False
        txtSecurityPIN.Properties.ReadOnly = False
        cboJenisKendaraan.Properties.ReadOnly = False
        'txtKodeJenisKendaraaan.Properties.ReadOnly = False
        txtDetail.Properties.ReadOnly = False
        txtNomorPolisi.Properties.ReadOnly = False
    End Sub

    Private Sub DisableText()
        txtKodeMember.Properties.ReadOnly = True
        txtNamaMember.Properties.ReadOnly = True
        txtJenisMember.Properties.ReadOnly = True
        txtKodeJenisMember.Properties.ReadOnly = True
        txtJenisPentarifan.Properties.ReadOnly = True
        txtTarif.Properties.ReadOnly = True
        txtAlamat.Properties.ReadOnly = True
        txtTelpon.Properties.ReadOnly = True
        dtExpDate.Properties.ReadOnly = True
        txtSecurityPIN.Properties.ReadOnly = True
        cboJenisKendaraan.Properties.ReadOnly = True
        txtKodeJenisKendaraaan.Properties.ReadOnly = True
        txtDetail.Properties.ReadOnly = True
        txtNomorPolisi.Properties.ReadOnly = True
    End Sub

    Private Sub BtnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If lNew Then
            SaveData()
        Else
            UpdateData()
        End If
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim xForm As New DevExpress.Utils.WaitDialogForm
        xForm.LookAndFeel.UseDefaultLookAndFeel = False
        xForm.LookAndFeel.SkinName = "Blue"

        grdMember.Rows.Clear()

        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select [kode member],[nama member],[alamat member]," &
                                   "[exp date] from member order by [nama member] asc"
                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        grdMember.Rows.Add(New Object() {rdr.Item(0).ToString.Trim,
                                           rdr.Item(1).ToString.Trim,
                                           rdr.Item(2).ToString.Trim,
                                           Format(Date.Parse(rdr.Item(3).ToString.Trim), "dd-MM-yyyy")})
                    End While
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Conn.Close()

        xForm.Dispose()
        lShow = True
    End Sub

    Private Sub TxtKodeJenisMember_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtKodeJenisMember.ButtonClick
        frmJenisMember.ShowDialog()
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
                        txtKodeJenisKendaraaan.Text = rdr.Item(0).ToString.Trim
                    End While
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Conn.Close()
    End Sub

    Private Sub SaveData()
        If txtKodeMember.Text.Trim = "" Then
            MsgBox("kode member boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtKodeMember.Focus()
            Exit Sub
        End If

        If txtNamaMember.Text.Trim = "" Then
            MsgBox("nama member boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtNamaMember.Focus()
            Exit Sub
        End If

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

        If txtSecurityPIN.Text.Trim = "" Then
            MsgBox("security PIN tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtSecurityPIN.Focus()
            Exit Sub
        End If

        If Not ValidasiData(txtKodeMember.Text.Trim,
                        "select [kode member] from member where [kode member] = '" & txtKodeMember.Text.Trim & "'",
                        "kode member yang anda masukkan sudah tersimpan dalam database") Then
            txtKodeMember.Focus()
            Exit Sub
        End If
        Dim Conn As SqlConnection = KoneksiSQL()
            Conn.Open()

            Using Cmd As New SqlCommand()
                Try
                    With Cmd
                    .Connection = Conn
                    .CommandText = "add_member"
                    .CommandType = Data.CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = Data.SqlDbType.NChar
                    .Parameters(0).Direction = Data.ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", Data.SqlDbType.NChar, 6)).Value = "ADD"
                    .Parameters.Add(New SqlParameter("@kode_member", Data.SqlDbType.NChar, 25)).Value = txtKodeMember.Text.Trim
                    .Parameters.Add(New SqlParameter("@kdj_member", Data.SqlDbType.NChar, 3)).Value = txtKodeJenisMember.Text.Trim
                    .Parameters.Add(New SqlParameter("@nama_member", Data.SqlDbType.NChar, 50)).Value = txtNamaMember.Text.Trim
                    .Parameters.Add(New SqlParameter("@alamat_member", Data.SqlDbType.NChar, 50)).Value = txtAlamat.Text.Trim
                    .Parameters.Add(New SqlParameter("@no_telpon", Data.SqlDbType.NChar, 15)).Value = txtTelpon.Text.Trim
                    .Parameters.Add(New SqlParameter("@exp_date", Data.SqlDbType.NChar, 10)).Value = ConvertTanggal(dtExpDate)
                    .Parameters.Add(New SqlParameter("@pin", Data.SqlDbType.NChar, 7)).Value = txtSecurityPIN.Text.Trim
                    .Parameters.Add(New SqlParameter("@kd_petugas", Data.SqlDbType.NChar, 10)).Value = frmLogin.txtUser.Text.Trim

                    Dim dtMemberDetail As New Data.DataTable()
                    dtMemberDetail.Columns.Add("kode_member", GetType([String])).MaxLength = 25
                    dtMemberDetail.Columns.Add("kd_jenis", GetType([String])).MaxLength = 10
                    dtMemberDetail.Columns.Add("detail", GetType([String])).MaxLength = 50
                    dtMemberDetail.Columns.Add("nopol", GetType([String])).MaxLength = 10

                    For Each DataRows In grdKendaraan.Rows
                        dtMemberDetail.Rows.Add(New Object() {
                                                txtKodeMember.Text.Trim,
                                                DataRows.Cells.Item(1).Value.ToString.Trim,
                                                DataRows.Cells.Item(2).Value.ToString.Trim,
                                                DataRows.Cells.Item(3).Value.ToString.Trim})
                    Next

                    .Parameters.Add("@member_detail_TVP", Data.SqlDbType.Structured).Value = dtMemberDetail
                        .ExecuteNonQuery()

                        If .Parameters("@mERROR_MESSAGE").Value.ToString.Trim = "Y" Then
                        grdMember.Rows.Add(New Object() {txtKodeMember.Text.Trim,
                                               txtNamaMember.Text.Trim,
                                               txtAlamat.Text.Trim,
                                               Format(Date.Parse(dtExpDate.DateTime), "dd-MM-yyyy")})
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
        If txtKodeMember.Text.Trim = "" Then
            MsgBox("kode member boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtKodeMember.Focus()
            Exit Sub
        End If

        If txtNamaMember.Text.Trim = "" Then
            MsgBox("nama member boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtNamaMember.Focus()
            Exit Sub
        End If

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

        If txtSecurityPIN.Text.Trim = "" Then
            MsgBox("security PIN tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtSecurityPIN.Focus()
            Exit Sub
        End If


        Dim Conn As SqlConnection = KoneksiSQL()
            Conn.Open()

            Using Cmd As New SqlCommand()
                Try
                    With Cmd
                    .Connection = Conn
                    .CommandText = "update_member"
                    .CommandType = Data.CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = Data.SqlDbType.NChar
                    .Parameters(0).Direction = Data.ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", Data.SqlDbType.NChar, 6)).Value = "UPDATE"
                    .Parameters.Add(New SqlParameter("@kode_member", Data.SqlDbType.NChar, 25)).Value = txtKodeMember.Text.Trim
                    .Parameters.Add(New SqlParameter("@kdj_member", Data.SqlDbType.NChar, 3)).Value = txtKodeJenisMember.Text.Trim
                    .Parameters.Add(New SqlParameter("@nama_member", Data.SqlDbType.NChar, 50)).Value = txtNamaMember.Text.Trim
                    .Parameters.Add(New SqlParameter("@alamat_member", Data.SqlDbType.NChar, 50)).Value = txtAlamat.Text.Trim
                    .Parameters.Add(New SqlParameter("@no_telpon", Data.SqlDbType.NChar, 15)).Value = txtTelpon.Text.Trim
                    .Parameters.Add(New SqlParameter("@exp_date", Data.SqlDbType.NChar, 10)).Value = ConvertTanggal(dtExpDate)
                    .Parameters.Add(New SqlParameter("@pin", Data.SqlDbType.NChar, 7)).Value = txtSecurityPIN.Text.Trim

                    Dim dtMemberDetail As New Data.DataTable()
                    dtMemberDetail.Columns.Add("kode_member", GetType([String])).MaxLength = 25
                    dtMemberDetail.Columns.Add("kd_jenis", GetType([String])).MaxLength = 10
                    dtMemberDetail.Columns.Add("detail", GetType([String])).MaxLength = 50
                    dtMemberDetail.Columns.Add("nopol", GetType([String])).MaxLength = 10

                    For Each DataRows In grdKendaraan.Rows
                        dtMemberDetail.Rows.Add(New Object() {
                                                  txtKodeMember.Text.Trim,
                                                  DataRows.Cells.Item(1).Value.ToString.Trim,
                                                  DataRows.Cells.Item(2).Value.ToString.Trim,
                                                  DataRows.Cells.Item(3).Value.ToString.Trim})
                    Next

                    .Parameters.Add("@member_detail_TVP", Data.SqlDbType.Structured).Value = dtMemberDetail
                    .ExecuteNonQuery()

                    If .Parameters("@mERROR_MESSAGE").Value.ToString.Trim = "Y" Then
                        grdMember.CurrentRow.Cells.Item(0).Value = txtKodeMember.Text.Trim
                        grdMember.CurrentRow.Cells.Item(1).Value = txtNamaMember.Text.Trim
                        grdMember.CurrentRow.Cells.Item(2).Value = txtAlamat.Text.Trim
                        grdMember.CurrentRow.Cells.Item(3).Value = ConvertTanggal(dtExpDate)
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
        If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "member", "delete") Then
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
                        .CommandText = "delete_member"
                        .CommandType = Data.CommandType.StoredProcedure
                        .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                        .Parameters(0).SqlDbType = Data.SqlDbType.NChar
                        .Parameters(0).Direction = Data.ParameterDirection.Output

                        .Parameters.Add(New SqlParameter("@mPROCESS", Data.SqlDbType.NChar, 6)).Value = "DELETE"
                        .Parameters.Add(New SqlParameter("@kode_member", Data.SqlDbType.NChar, 25)).Value = txtKodeMember.Text.Trim
                        .ExecuteNonQuery()

                        If .Parameters("@mERROR_MESSAGE").Value.ToString.Trim = "Y" Then
                            grdMember.Rows.RemoveAt(grdMember.CurrentCell.RowIndex)
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

    Private Sub TxtJenisMember_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtJenisMember.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Using cfrmCariJenisMember As New frmCariJenisMember
                cfrmCariJenisMember.ShowDialog()
                txtJenisMember.Text = cfrmCariJenisMember.grdMember.CurrentRow.Cells.Item(0).Value.ToString.Trim
                txtKodeJenisMember.Text = cfrmCariJenisMember.grdMember.CurrentRow.Cells.Item(1).Value.ToString.Trim
            End Using

            If txtJenisMember.Text.Trim <> "" Then
                Dim Conn As SqlConnection = KoneksiSQL()
                Conn.Open()
                Try
                    Using cmd As New SqlCommand
                        With cmd
                            .Connection = Conn
                            .CommandText = "select [jenis tarif],[persen tarif] from jenis_member " &
                                           "where [kdj member] = '" & txtKodeJenisMember.Text.Trim & "'"
                            Dim rdr As SqlDataReader = .ExecuteReader
                            While rdr.Read
                                txtJenisPentarifan.Text = rdr.Item(0).ToString.Trim
                                If txtJenisPentarifan.Text.Trim = "P" Then
                                    txtTarif.Text = Decimal.Parse(rdr.Item(1).ToString.Trim)
                                End If
                            End While
                            rdr.Close()

                            If txtJenisPentarifan.Text.Trim = "J" Then
                                .CommandText = "select top 1 [tarif] from jenis_member_detail " &
                                               "where [kdj member] = '" & txtKodeJenisMember.Text.Trim & "'"
                                rdr = .ExecuteReader
                                While rdr.Read
                                    txtTarif.Text = Decimal.Parse(rdr.Item(0).ToString.Trim)
                                End While
                                rdr.Close()
                            End If
                        End With
                    End Using
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                Conn.Close()
                txtAlamat.Focus()
            End If
        End If
    End Sub

    Private Sub TxtCariData_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCariData.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            grdMember.Rows.Clear()

            Dim Conn As SqlConnection = KoneksiSQL()
            Conn.Open()
            Try
                Using cmd As New SqlCommand
                    With cmd
                        .Connection = Conn
                        .CommandText = "select [kode member],[nama member],[alamat member]," &
                                       "[exp date] from member where [nama member] like '%" & txtCariData.Text.Trim & "%'"
                        Dim rdr As SqlDataReader = .ExecuteReader
                        grdMember.Rows.Clear()
                        While rdr.Read
                            grdMember.Rows.Add(New Object() {rdr.Item(0).ToString.Trim,
                                               rdr.Item(1).ToString.Trim,
                                               rdr.Item(2).ToString.Trim,
                                               Format(Date.Parse(rdr.Item(3).ToString.Trim), "dd-MM-yyyy")})
                        End While
                        rdr.Close()

                        .CommandText = "select [kode member],[nama member],[alamat member]," &
                                       "[exp date] from member where [kode member] = '" & txtCariData.Text.Trim & "'"
                        rdr = .ExecuteReader
                        While rdr.Read
                            grdMember.Rows.Add(New Object() {rdr.Item(0).ToString.Trim,
                                               rdr.Item(1).ToString.Trim,
                                               rdr.Item(2).ToString.Trim,
                                               Format(Date.Parse(rdr.Item(3).ToString.Trim), "dd-MM-yyyy")})
                        End While
                        rdr.Close()
                    End With
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Conn.Close()

            xForm.Dispose()
        End If
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If txtKodeJenisKendaraaan.Text.Trim = "" Then
                MsgBox("pilih jenis kendaraan terlebih dahulu", MsgBoxStyle.Critical, "error")
                cboJenisKendaraan.Focus()
                Exit Sub
            End If

            grdKendaraan.Rows.Add(New Object() {cboJenisKendaraan.Text.Trim,
                                  txtKodeJenisKendaraaan.Text.Trim,
                                  txtDetail.Text.Trim,
                                  txtNomorPolisi.Text.Trim})
            ClearDetail()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        Try
            grdKendaraan.Rows.RemoveAt(grdKendaraan.CurrentCell.RowIndex)
            ClearDetail()
            btnRemove.Enabled = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GrdKendaraan_Click(sender As Object, e As EventArgs) Handles grdKendaraan.Click
        Try
            cboJenisKendaraan.Text = grdKendaraan.CurrentRow.Cells.Item(0).Value.ToString.Trim
            txtKodeJenisKendaraaan.Text = grdKendaraan.CurrentRow.Cells.Item(1).Value.ToString.Trim
            txtDetail.Text = grdKendaraan.CurrentRow.Cells.Item(2).Value.ToString.Trim
            txtNomorPolisi.Text = grdKendaraan.CurrentRow.Cells.Item(3).Value.ToString.Trim
            btnRemove.Enabled = True
        Catch ex As Exception

        End Try
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

    Private Sub GrdMember_Click(sender As Object, e As EventArgs) Handles grdMember.Click
        Try
            If lShow Then
                btnTambah.Text = "&Ubah"
                btnHapus.Enabled = True
                txtKodeMember.Text = grdMember.CurrentRow.Cells.Item(0).Value.ToString.Trim
                LoadData(txtKodeMember.Text.Trim)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function LoadData(ByVal cKodeMember As String) As Integer
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn

                    '-------------------------------------
                    'load data member dari tabel member
                    '-------------------------------------
                    .CommandText = "select [kdj member],[nama member],[alamat member],[no telpon]," &
                                   "[exp date],[pin] from member where [kode member] = '" & cKodeMember.Trim & "'"

                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        txtKodeJenisMember.Text = rdr.Item(0).ToString.Trim
                        txtNamaMember.Text = rdr.Item(1).ToString.Trim
                        txtAlamat.Text = rdr.Item(2).ToString.Trim
                        txtTelpon.Text = rdr.Item(3).ToString.Trim
                        dtExpDate.DateTime = Date.Parse(rdr.Item(4).ToString.Trim)
                        txtSecurityPIN.Text = rdr.Item(5).ToString.Trim
                    End While
                    rdr.Close()

                    '------------------------------------------------
                    'load data jenis tarif dari tabel jenis_member
                    '------------------------------------------------
                    .CommandText = "select [jenis member],[jenis tarif],[persen tarif] from jenis_member " &
                                   "where [kdj member] = '" & txtKodeJenisMember.Text.Trim & "'"
                    rdr = .ExecuteReader
                    While rdr.Read
                        txtJenisMember.Text = rdr.Item(0).ToString.Trim
                        txtJenisPentarifan.Text = rdr.Item(1).ToString.Trim
                        If txtJenisPentarifan.Text.Trim = "P" Then
                            txtTarif.Text = Decimal.Parse(rdr.Item(2).ToString.Trim)
                        End If
                    End While
                    rdr.Close()

                    '-----------------------------------------------------
                    'jika jenis tarif by jenis kendaraan
                    'load data tarif dari tabel jenis_member_detail
                    '-----------------------------------------------------
                    If txtJenisPentarifan.Text.Trim = "J" Then
                        .CommandText = "select top 1 [tarif] from jenis_member_detail " &
                                       "where [kdj member] = '" & txtKodeJenisMember.Text.Trim & "'"
                        rdr = .ExecuteReader
                        While rdr.Read
                            txtTarif.Text = Decimal.Parse(rdr.Item(0).ToString.Trim)
                        End While
                        rdr.Close()
                    End If

                    '-----------------------------------------------------
                    'load data dari tabel member_detail
                    '-----------------------------------------------------
                    grdKendaraan.Rows.Clear()
                    .CommandText = "select jenis_kendaraan.[keterangan]," &
                                    "member_detail.[kd jenis]," &
                                    "member_detail.[detail]," &
                                    "member_detail.[nopol] " &
                                    "from member_detail " &
                                    "member_detail inner join jenis_kendaraan " &
                                    "jenis_kendaraan on member_detail.[kd jenis] = jenis_kendaraan.[kd jenis] " &
                                    "where member_detail.[kode member] = '" & cKodeMember.Trim & "'"

                    rdr = .ExecuteReader
                    While rdr.Read
                        grdKendaraan.Rows.Add(New Object() {rdr.Item(0).ToString.Trim,
                                              rdr.Item(1).ToString.Trim,
                                              rdr.Item(2).ToString.Trim,
                                              rdr.Item(3).ToString.Trim})
                    End While
                    rdr.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Conn.Close()

        Return 0
    End Function
End Class
