Imports System.Data.SqlClient
Public Class frmMember
    Implements iEntryData
    Dim lNew As Boolean
    Dim lShowRecord As Boolean
    Friend cKodePetugas As String

    Private Sub btnTutup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTutup.Click
        Me.Dispose()
    End Sub

    Private Sub frmMember_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DefaultSetting()
        LoadDataDefault()
    End Sub


    Private Sub ClearScreen() Implements iEntryData.ClearScreen
        txtKodeMember.Text = ""
        txtNamaMember.Text = ""
        txtAlamatMember.Text = ""
        txtNoTelpon.Text = ""
        txtUnitKerja.Text = ""
        dtExp.Text = ""
    End Sub

    Private Sub ClearDetail()
        cboJenisKendaraan.Text = ""
        cboJenisKendaraan.Text = ""
        txtJenisKendaraan.Text = ""
        txtNomorPolisi.Text = ""
        txtDetail.Text = ""
    End Sub

    Private Sub DefaultSetting() Implements iEntryData.DefaultSetting
        ClearScreen()
        ClearDetail()
        DisableText()
        EnableNavBar()
        btnTambah.Text = "&Tambah"
        btnHapus.Enabled = False
        btnSimpan.Enabled = False
        grdKendaraan.Rows.Clear()
        lShowRecord = False
    End Sub

    Private Sub DisableNavBar() Implements iEntryData.DisableNavBar
        btnFirst.Enabled = False
        btnPrevious.Enabled = False
        btnNext.Enabled = False
        btnLast.Enabled = False
    End Sub

    Private Sub DisableText() Implements iEntryData.DisableText
        txtKodeMember.Properties.ReadOnly = True
        txtNamaMember.Properties.ReadOnly = True
        txtAlamatMember.Properties.ReadOnly = True
        txtNoTelpon.Properties.ReadOnly = True
        txtUnitKerja.Properties.ReadOnly = True
        cboJenisKendaraan.Properties.ReadOnly = True
        txtJenisKendaraan.Properties.ReadOnly = True
        txtNomorPolisi.Properties.ReadOnly = True
        txtDetail.Properties.ReadOnly = True
        dtExp.Properties.ReadOnly = True
    End Sub

    Private Sub EnableNavBar() Implements iEntryData.EnableNavBar
        btnFirst.Enabled = True
        btnPrevious.Enabled = True
        btnNext.Enabled = True
        btnLast.Enabled = True
    End Sub

    Private Sub EnableText() Implements iEntryData.EnableText
        txtKodeMember.Properties.ReadOnly = False
        txtNamaMember.Properties.ReadOnly = False
        txtAlamatMember.Properties.ReadOnly = False
        txtNoTelpon.Properties.ReadOnly = False
        txtUnitKerja.Properties.ReadOnly = False
        cboJenisKendaraan.Properties.ReadOnly = False
        'txtJenisKendaraan.Properties.ReadOnly = False
        txtNomorPolisi.Properties.ReadOnly = False
        txtDetail.Properties.ReadOnly = False
        dtExp.Properties.ReadOnly = False
    End Sub

    Private Sub NavBarMode() Implements iEntryData.NavBarMode
        btnTambah.Text = "&Ubah"
        btnHapus.Enabled = True
        btnSimpan.Enabled = False
        ShowRecord()
        LoadDataMemberDetail()
    End Sub

    Private Sub ShowRecord() Implements iEntryData.ShowRecord
        txtKodeMember.Text = grdMember.CurrentRow.Cells.Item(0).Value.ToString.Trim
        txtNamaMember.Text = grdMember.CurrentRow.Cells.Item(1).Value.ToString.Trim
        txtAlamatMember.Text = grdMember.CurrentRow.Cells.Item(2).Value.ToString.Trim
        txtNoTelpon.Text = grdMember.CurrentRow.Cells.Item(3).Value.ToString.Trim
        txtUnitKerja.Text = grdMember.CurrentRow.Cells.Item(4).Value.ToString.Trim
        dtExp.Text = grdMember.CurrentRow.Cells.Item(5).Value.ToString.Trim
    End Sub

    Private Sub LoadDataMemberDetail()
        grdKendaraan.Rows.Clear()
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "SELECT jenis_kendaraan.[keterangan],member_detail.[kd jenis],member_detail.[detail]," & _
                                   "member_detail.[nopol] " & _
                                   "FROM member_detail member_detail INNER JOIN  jenis_kendaraan " & _
                                   "jenis_kendaraan ON member_detail.[kd jenis] = jenis_kendaraan.[kd jenis] " & _
                                   "WHERE member_detail.[kode member] = '" & txtKodeMember.Text.Trim & "'"

                    Dim rdr As SqlDataReader = .ExecuteReader

                    While rdr.Read
                        grdKendaraan.Rows.Add(New Object() {
                                                         rdr.Item(0).ToString.Trim, _
                                                         rdr.Item(1).ToString.Trim, _
                                                         rdr.Item(2).ToString.Trim, _
                                                         rdr.Item(3).ToString.Trim
                                                         })
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

    Private Sub ShowRecordDetail()
        Try
            cboJenisKendaraan.Text = grdKendaraan.CurrentRow.Cells.Item(0).Value.ToString.Trim
            txtJenisKendaraan.Text = grdKendaraan.CurrentRow.Cells.Item(1).Value.ToString.Trim
            txtDetail.Text = grdKendaraan.CurrentRow.Cells.Item(2).Value.ToString.Trim
            txtNomorPolisi.Text = grdKendaraan.CurrentRow.Cells.Item(3).Value.ToString.Trim
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If lNew Then
            SaveData()
        Else
            UpdateData()
        End If
    End Sub

    Private Sub SaveData()
        If txtKodeMember.Text.Trim = "" Then
            MsgBox("Kode member tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtKodeMember.Focus()
            Exit Sub
        End If

        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn

                    'Simpan kedatabase

                    .CommandText = "Insert Into Member ([kode member],[nama member],[alamat member]," & _
                        "[no telpon],[unit kerja],[exp date],[kd petugas]) " & _
                        "Values ('" & _
                        txtKodeMember.Text.Trim & "','" & _
                        txtNamaMember.Text.Trim & "','" & _
                        txtAlamatMember.Text.Trim & "','" & _
                        txtNoTelpon.Text.Trim & "','" & _
                        txtUnitKerja.Text.Trim & "','" & _
                        ConvertTanggal(dtExp) & "','" & _
                        cKodePetugas.Trim & "')"
                    .ExecuteNonQuery()

                    'masukkan ke grid
                    grdMember.Rows.Add(New Object() {txtKodeMember.Text.Trim, _
                                         txtNamaMember.Text.Trim, _
                                         txtAlamatMember.Text.Trim, _
                                         txtNoTelpon.Text.Trim, _
                                         txtUnitKerja.Text.Trim, _
                                         ConvertTanggal(dtExp), _
                                         cKodePetugas.Trim})

                    'simpan kedatabase member_detail
                    For Each DataRow In grdKendaraan.Rows
                        .CommandText = "insert into member_detail ([kode member],[kd jenis],[detail],[nopol]) values ('" & _
                            txtKodeMember.Text.Trim & "','" & _
                            DataRow.Cells.Item(1).Value.ToString & "','" & _
                            DataRow.Cells.Item(2).Value.ToString & "','" & _
                            DataRow.Cells.Item(3).Value.ToString & "')"

                        .ExecuteNonQuery()
                    Next

                    .Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
        End Try
        DefaultSetting()
    End Sub

    Private Sub UpdateData()
        If txtKodeMember.Text.Trim = "" Then
            MsgBox("Kode member tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtKodeMember.Focus()
            Exit Sub
        End If

        If txtNomorPolisi.Text.Trim = "" Then
            MsgBox("Nomor polisi tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtNomorPolisi.Focus()
            Exit Sub
        End If

        
        grdMember.CurrentRow.Cells.Item(0).Value = txtKodeMember.Text.Trim
        grdMember.CurrentRow.Cells.Item(1).Value = txtNamaMember.Text.Trim
        grdMember.CurrentRow.Cells.Item(2).Value = txtAlamatMember.Text.Trim
        grdMember.CurrentRow.Cells.Item(4).Value = txtUnitKerja.Text.Trim
        grdMember.CurrentRow.Cells.Item(5).Value = ConvertTanggal(dtExp)
        grdMember.CurrentRow.Cells.Item(6).Value = cKodePetugas.Trim

        DefaultSetting()
    End Sub

    

    Private Sub LoadDataDefault()
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "Select [Keterangan] From Jenis_Kendaraan Order By [Keterangan] Asc"
                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        cboJenisKendaraan.Properties.Items.Add(rdr.Item(0).ToString.Trim)
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


    Private Sub btnTambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTambah.Click
        If btnTambah.Text = "&Tambah" Then
            If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "member", "add") Then
                MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "error")
                DefaultSetting()
                Exit Sub
            End If
            lNew = True
            ClearScreen()
            EnableText()
            btnHapus.Enabled = False
            btnSimpan.Enabled = True
            btnTambah.Text = "&Batal"
            txtKodeMember.Text = "M." & Now.Year & Now.Month & Now.Day & "." & Now.Hour & Now.Minute & Now.Second
            txtNamaMember.Focus()
        ElseIf btnTambah.Text = "&Ubah" Then
            If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "member", "update") Then
                MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "error")
                DefaultSetting()
                Exit Sub
            End If
            lNew = False
            EnableText()
            btnSimpan.Enabled = True
            btnHapus.Enabled = False
            btnTambah.Text = "&Batal"
            txtNamaMember.Focus()
        ElseIf btnTambah.Text = "&Batal" Then
            DefaultSetting()
        End If
    End Sub

    Private Sub btnFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirst.Click
        Try
            grdMember.GridNavigator.SelectFirstRow()
            NavBarMode()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLast.Click
        Try
            grdMember.GridNavigator.SelectLastRow()
            NavBarMode()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnPrevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevious.Click
        Try
            grdMember.GridNavigator.SelectPreviousRow(1)
            NavBarMode()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Try
            grdMember.GridNavigator.SelectNextRow(1)
            NavBarMode()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHapus.Click
        If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "member", "delete") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "error")
            DefaultSetting()
            Exit Sub
        End If
        Dim cMsg As String = MsgBox("Apakah anda yakin ingin menghapus data ini?", MsgBoxStyle.Exclamation + vbYesNo, "Konfirmasi")
        If cMsg = vbYes Then
            Try
                ExecuteQuery("Delete From Member Where [kode member] = '" &
                          txtKodeMember.Text.Trim & "'")

                ExecuteQuery("Delete From Member_Detail Where [kode member] = '" &
                          txtKodeMember.Text.Trim & "'")

                grdMember.Rows.RemoveAt(grdMember.CurrentCell.RowIndex)
                grdKendaraan.Rows.Clear()

            Catch ex As Exception
                MsgBox(ex.Message.Trim)
            End Try
        End If
        DefaultSetting()
    End Sub

    Private Sub cboJenisKendaraan_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboJenisKendaraan.SelectedIndexChanged
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "Select [kd jenis] From Jenis_Kendaraan where [keterangan] like '%" & _
                        cboJenisKendaraan.Text.Trim & "%'"
                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        txtJenisKendaraan.Text = rdr.Item(0).ToString.Trim
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

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "member", "view") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "error")
            DefaultSetting()
            Exit Sub
        End If
        lShowRecord = False
        grdMember.Rows.Clear()
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select * from member order by [nama member] asc"
                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        grdMember.Rows.Add(New Object() {rdr.Item(0).ToString.Trim,
                                                         rdr.Item(1).ToString.Trim,
                                                         rdr.Item(2).ToString.Trim,
                                                          rdr.Item(3).ToString.Trim,
                                                          rdr.Item(4).ToString.Trim,
                                                          rdr.Item(5).ToString.Trim,
                                                          rdr.Item(6).ToString.Trim
                                                         })
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


    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If txtNomorPolisi.Text.Trim = "" Then
            MsgBox("Nomor polisi tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtNomorPolisi.Focus()
            Exit Sub
        End If

        If txtJenisKendaraan.Text.Trim = "" Then
            MsgBox("kode jenis kendaraan tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtJenisKendaraan.Focus()
            Exit Sub
        End If

        For Each datarow In grdKendaraan.Rows
            If datarow.Cells.Item(3).Value.ToString.Trim = txtNomorPolisi.Text.Trim Then
                MsgBox("Kendaraan dengan nomor polisi tersebut sudah ada", MsgBoxStyle.Critical, "Error")
                txtNomorPolisi.Focus()
                Exit Sub
            End If
        Next

        If ValidasiData(txtNomorPolisi.Text.Trim, _
                        "select [nopol] from member_detail where [nopol] = '" & txtNomorPolisi.Text.Trim & "'", _
                        "Kendaraan dengan Nomor Polisi Tersebut sudah tersimpan didatabase") Then
            grdKendaraan.Rows.Add(New Object() {
                                  cboJenisKendaraan.Text.Trim, _
                                  txtJenisKendaraan.Text.Trim, _
                                  txtDetail.Text.Trim, _
                                  txtNomorPolisi.Text.Trim
                                })
            ClearDetail()
            cboJenisKendaraan.Focus()
        Else
            txtNomorPolisi.Focus()
        End If
    End Sub


    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        grdKendaraan.Rows.RemoveAt(grdKendaraan.CurrentCell.RowIndex)
        ClearDetail()
    End Sub

    Private Sub grdKendaraan_Click(sender As Object, e As EventArgs) Handles grdKendaraan.Click
        ShowRecordDetail()
    End Sub

    Private Sub grdKendaraan_CurrentRowChanged(sender As Object, e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles grdKendaraan.CurrentRowChanged
        ShowRecordDetail()
    End Sub

    Private Sub txtNomorPolisi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNomorPolisi.KeyPress
        e.KeyChar = UCase(e.KeyChar)
    End Sub

    Private Sub grdMember_Click(sender As Object, e As EventArgs) Handles grdMember.Click
        Try
            lShowRecord = True
            NavBarMode()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdMember_CurrentRowChanged(sender As Object, e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles grdMember.CurrentRowChanged
        If lShowRecord Then
            Try
                NavBarMode()
            Catch ex As Exception

            End Try
        End If
    End Sub


End Class