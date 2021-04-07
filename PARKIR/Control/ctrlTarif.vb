Imports System.Data.SqlClient
Public Class ctrlTarif
    Dim lNew As Boolean
    Dim lShow As Boolean
    Private Sub BtnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        If btnTambah.Text = "&Tambah" Then
            If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "tarif_parkir", "add") Then
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
            txtKodeTarif.Focus()
        ElseIf btnTambah.Text = "&Ubah" Then
            If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "tarif_parkir", "update") Then
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
            txtKodeTarif.Focus()
        Else
            DefaultSetting()
        End If
    End Sub

    Private Sub ctrlTarif_Load(sender As Object, e As EventArgs) Handles Me.Load
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
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Conn.Close()
    End Sub

    Private Sub DefaultSetting()
        ClearScreen()
        DisableText()
        btnTambah.Text = "&Tambah"
        btnHapus.Enabled = False
        btnSimpan.Enabled = False
        btnRefresh.Enabled = True
        lShow = True
    End Sub

    Private Sub ClearScreen()
        txtKodeTarif.Text = ""
        cboJenisKendaraan.Text = ""
        txtKodeJenisKendaraan.Text = ""
        txtTarif.Text = 0
        txtOverTime.Text = 0
        txtPeriodeOverTime.Text = 0
        txtMaksimalOverTime.Text = 0
        txtTarifOverTime.Text = 0
        txtTarifDenda.Text = 0
        txtGrassPeriode.Text = 0
    End Sub

    Private Sub DisableText()
        txtKodeTarif.Properties.ReadOnly = True
        cboJenisKendaraan.Properties.ReadOnly = True
        txtKodeJenisKendaraan.Properties.ReadOnly = True
        txtTarif.Properties.ReadOnly = True
        txtOverTime.Properties.ReadOnly = True
        txtPeriodeOverTime.Properties.ReadOnly = True
        txtMaksimalOverTime.Properties.ReadOnly = True
        txtTarifOverTime.Properties.ReadOnly = True
        txtTarifDenda.Properties.ReadOnly = True
        txtGrassPeriode.Properties.ReadOnly = True
    End Sub

    Private Sub EnableText()
        txtKodeTarif.Properties.ReadOnly = False
        cboJenisKendaraan.Properties.ReadOnly = False
        'txtKodeJenisKendaraan.Properties.ReadOnly = True
        txtTarif.Properties.ReadOnly = False
        txtOverTime.Properties.ReadOnly = False
        txtPeriodeOverTime.Properties.ReadOnly = False
        txtMaksimalOverTime.Properties.ReadOnly = False
        txtTarifOverTime.Properties.ReadOnly = False
        txtTarifDenda.Properties.ReadOnly = False
        txtGrassPeriode.Properties.ReadOnly = False
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

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "tarif_parkir", "view") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "error")
            DefaultSetting()
            Exit Sub
        End If

        grdTarif.Rows.Clear()

        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "SELECT tarif.[kode tarif], jenis_kendaraan.[keterangan],tarif.[kd jenis]," &
                                   "tarif.[tarif],tarif.[overtime],tarif.[periode overtime], " &
                                   "tarif.[maksimal overtime],tarif.[tarif overtime]," &
                                   "tarif.[tarif denda],tarif.[grass periode] " &
                                   "FROM jenis_kendaraan jenis_kendaraan " &
                                   "INNER JOIN tarif tarif ON jenis_kendaraan.[kd jenis] = tarif.[kd jenis] " &
                                   "ORDER BY jenis_kendaraan.[keterangan] ASC"


                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        grdTarif.Rows.Add(New Object() {rdr.Item(0).ToString.Trim,
                                                        rdr.Item(1).ToString.Trim,
                                                        rdr.Item(2).ToString.Trim,
                                                        rdr.Item(3).ToString.Trim,
                                                        rdr.Item(4).ToString.Trim,
                                                        rdr.Item(5).ToString.Trim,
                                                        rdr.Item(6).ToString.Trim,
                                                        rdr.Item(7).ToString.Trim,
                                                        rdr.Item(8).ToString.Trim,
                                                        rdr.Item(9).ToString.Trim})

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
            txtKodeTarif.Text = grdTarif.CurrentRow.Cells.Item(0).Value.ToString.Trim
            cboJenisKendaraan.Text = grdTarif.CurrentRow.Cells.Item(1).Value.ToString.Trim
            txtKodeJenisKendaraan.Text = grdTarif.CurrentRow.Cells.Item(2).Value.ToString.Trim
            txtTarif.Text = grdTarif.CurrentRow.Cells.Item(3).Value.ToString.Trim
            txtOverTime.Text = grdTarif.CurrentRow.Cells.Item(4).Value.ToString.Trim
            txtPeriodeOverTime.Text = grdTarif.CurrentRow.Cells.Item(5).Value.ToString.Trim
            txtMaksimalOverTime.Text = grdTarif.CurrentRow.Cells.Item(6).Value.ToString.Trim
            txtTarifOverTime.Text = grdTarif.CurrentRow.Cells.Item(7).Value.ToString.Trim
            txtTarifDenda.Text = grdTarif.CurrentRow.Cells.Item(8).Value.ToString.Trim
            txtGrassPeriode.Text = grdTarif.CurrentRow.Cells.Item(9).Value.ToString.Trim
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GrdTarif_Click(sender As Object, e As EventArgs) Handles grdTarif.Click
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
    End Sub

    Private Sub SaveData()
        If txtKodeTarif.Text.Trim = "" Then
            MsgBox("kode tarif tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtKodeTarif.Focus()
            Exit Sub
        End If

        If txtKodeJenisKendaraan.Text.Trim = "" Then
            MsgBox("kode jenis boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtKodeJenisKendaraan.Focus()
            Exit Sub
        End If

        If ValidasiData(txtKodeTarif.Text.Trim,
                        "select [kode tarif] from tarif where [kode tarif] = '" & txtKodeTarif.Text.Trim & "'",
                        "kode tarif yang anda masukkan sudah tersimpan dalam database") Then
            If ValidasiData(txtKodeJenisKendaraan.Text.Trim,
                            "select [kd jenis] from tarif where [kd jenis] = '" & txtKodeJenisKendaraan.Text.Trim & "'",
                            "kode jenis yang anda masukkan sudah tersimpan dalam database") Then

                Dim Conn As SqlConnection = KoneksiSQL()
                Conn.Open()

                Using Cmd As New SqlCommand()
                    Try
                        With Cmd
                            .Connection = Conn
                            .CommandText = "add_tarif"
                            .CommandType = Data.CommandType.StoredProcedure
                            .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                            .Parameters(0).SqlDbType = Data.SqlDbType.NChar
                            .Parameters(0).Direction = Data.ParameterDirection.Output

                            .Parameters.Add(New SqlParameter("@mPROCESS", Data.SqlDbType.NChar, 6)).Value = "ADD"
                            .Parameters.Add(New SqlParameter("@kode_tarif", Data.SqlDbType.NChar, 10)).Value = txtKodeTarif.Text.Trim
                            .Parameters.Add(New SqlParameter("@kd_jenis", Data.SqlDbType.NChar, 10)).Value = txtKodeJenisKendaraan.Text.Trim
                            .Parameters.Add(New SqlParameter("@tarif", Data.SqlDbType.Decimal, 18, 0)).Value = Decimal.Parse(txtTarif.Text.Trim)
                            .Parameters.Add(New SqlParameter("@overtime", Data.SqlDbType.Int)).Value = Integer.Parse(txtOverTime.Text.Trim)
                            .Parameters.Add(New SqlParameter("@periode_overtime", Data.SqlDbType.Int)).Value = Integer.Parse(txtPeriodeOverTime.Text.Trim)
                            .Parameters.Add(New SqlParameter("@maksimal_overtime", Data.SqlDbType.Int)).Value = Integer.Parse(txtMaksimalOverTime.Text.Trim)
                            .Parameters.Add(New SqlParameter("@tarif_overtime", Data.SqlDbType.Decimal, 18, 0)).Value = Decimal.Parse(txtTarifOverTime.Text.Trim)
                            .Parameters.Add(New SqlParameter("@tarif_denda", Data.SqlDbType.Decimal, 18, 0)).Value = Decimal.Parse(txtTarifDenda.Text.Trim)
                            .Parameters.Add(New SqlParameter("@grass_periode", Data.SqlDbType.Int)).Value = Integer.Parse(txtGrassPeriode.Text.Trim)
                            .ExecuteNonQuery()

                            If .Parameters("@mERROR_MESSAGE").Value.ToString.Trim = "Y" Then
                                grdTarif.Rows.Add(New Object() {txtKodeTarif.Text.Trim,
                                                cboJenisKendaraan.Text.Trim,
                                                txtKodeJenisKendaraan.Text.Trim,
                                                Decimal.Parse(txtTarif.Text.Trim),
                                                Integer.Parse(txtOverTime.Text.Trim),
                                                Integer.Parse(txtPeriodeOverTime.Text.Trim),
                                                Integer.Parse(txtMaksimalOverTime.Text.Trim),
                                                Decimal.Parse(txtTarifOverTime.Text.Trim),
                                                Decimal.Parse(txtTarifDenda.Text.Trim),
                                                Integer.Parse(txtGrassPeriode.Text.Trim)})
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
            End If
        End If
    End Sub

    Private Sub UpdateData()
        If txtKodeTarif.Text.Trim = "" Then
            MsgBox("kode tarif tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtKodeTarif.Focus()
            Exit Sub
        End If

        If txtKodeJenisKendaraan.Text.Trim = "" Then
            MsgBox("kode jenis boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtKodeJenisKendaraan.Focus()
            Exit Sub
        End If


        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()

        Using Cmd As New SqlCommand()
            Try
                With Cmd
                    .Connection = Conn
                    .CommandText = "update_tarif"
                    .CommandType = Data.CommandType.StoredProcedure
                    .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                    .Parameters(0).SqlDbType = Data.SqlDbType.NChar
                    .Parameters(0).Direction = Data.ParameterDirection.Output

                    .Parameters.Add(New SqlParameter("@mPROCESS", Data.SqlDbType.NChar, 6)).Value = "UPDATE"
                    .Parameters.Add(New SqlParameter("@kode_tarif", Data.SqlDbType.NChar, 10)).Value = txtKodeTarif.Text.Trim
                    .Parameters.Add(New SqlParameter("@kd_jenis", Data.SqlDbType.NChar, 10)).Value = txtKodeJenisKendaraan.Text.Trim
                    .Parameters.Add(New SqlParameter("@tarif", Data.SqlDbType.Decimal, 18, 0)).Value = Decimal.Parse(txtTarif.Text.Trim)
                    .Parameters.Add(New SqlParameter("@overtime", Data.SqlDbType.Int)).Value = Integer.Parse(txtOverTime.Text.Trim)
                    .Parameters.Add(New SqlParameter("@periode_overtime", Data.SqlDbType.Int)).Value = Integer.Parse(txtPeriodeOverTime.Text.Trim)
                    .Parameters.Add(New SqlParameter("@maksimal_overtime", Data.SqlDbType.Int)).Value = Integer.Parse(txtMaksimalOverTime.Text.Trim)
                    .Parameters.Add(New SqlParameter("@tarif_overtime", Data.SqlDbType.Decimal, 18, 0)).Value = Decimal.Parse(txtTarifOverTime.Text.Trim)
                    .Parameters.Add(New SqlParameter("@tarif_denda", Data.SqlDbType.Decimal, 18, 0)).Value = Decimal.Parse(txtTarifDenda.Text.Trim)
                    .Parameters.Add(New SqlParameter("@grass_periode", Data.SqlDbType.Int)).Value = Integer.Parse(txtGrassPeriode.Text.Trim)
                    .ExecuteNonQuery()

                    If .Parameters("@mERROR_MESSAGE").Value.ToString.Trim = "Y" Then
                        grdTarif.CurrentRow.Cells.Item(0).Value = txtKodeTarif.Text.Trim
                        grdTarif.CurrentRow.Cells.Item(1).Value = cboJenisKendaraan.Text.Trim
                        grdTarif.CurrentRow.Cells.Item(2).Value = txtKodeJenisKendaraan.Text.Trim
                        grdTarif.CurrentRow.Cells.Item(3).Value = Decimal.Parse(txtTarif.Text.Trim)
                        grdTarif.CurrentRow.Cells.Item(4).Value = Integer.Parse(txtOverTime.Text.Trim)
                        grdTarif.CurrentRow.Cells.Item(5).Value = Integer.Parse(txtPeriodeOverTime.Text.Trim)
                        grdTarif.CurrentRow.Cells.Item(6).Value = Integer.Parse(txtMaksimalOverTime.Text.Trim)
                        grdTarif.CurrentRow.Cells.Item(7).Value = Decimal.Parse(txtTarifOverTime.Text.Trim)
                        grdTarif.CurrentRow.Cells.Item(8).Value = Decimal.Parse(txtTarifDenda.Text.Trim)
                        grdTarif.CurrentRow.Cells.Item(9).Value = Integer.Parse(txtGrassPeriode.Text.Trim)
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
        If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "tarif_parkir", "delete") Then
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
                        .CommandText = "delete_tarif"
                        .CommandType = Data.CommandType.StoredProcedure
                        .Parameters.Add(New SqlParameter("@mERROR_MESSAGE", ""))
                        .Parameters(0).SqlDbType = Data.SqlDbType.NChar
                        .Parameters(0).Direction = Data.ParameterDirection.Output

                        .Parameters.Add(New SqlParameter("@mPROCESS", Data.SqlDbType.NChar, 6)).Value = "DELETE"
                        .Parameters.Add(New SqlParameter("@kode_tarif", Data.SqlDbType.NChar, 10)).Value = txtKodeTarif.Text.Trim
                        .ExecuteNonQuery()

                        If .Parameters("@mERROR_MESSAGE").Value.ToString.Trim = "Y" Then
                            grdTarif.Rows.RemoveAt(grdTarif.CurrentCell.RowIndex)
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
