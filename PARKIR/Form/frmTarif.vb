Imports System.Data.SqlClient
Public Class frmTarif
    Dim lNew As Boolean
    Private Sub btnTutup_Click(sender As Object, e As EventArgs) Handles btnTutup.Click
        Me.Dispose()
    End Sub

    Private Sub frmTarif_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DefaultSetting()
        LoadDataDefault()
    End Sub

    Private Sub LoadDataDefault()
        cboJenisKendaraan.Properties.Items.Clear()
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

                    .Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
        End Try
    End Sub

    Private Sub ClearScreen()
        txtKodeTarif.Text = ""
        cboJenisKendaraan.Text = ""
        txtKodeJenis.Text = ""
        txtTarif.Text = ""
        txtOvertime.Text = ""
        txtPeriodeOvertime.Text = ""
        txtMaksimalOvertime.Text = ""
        txtTarifOvertime.Text = ""
        txtTarifDenda.Text = ""
        txtGrassPeriode.Text = ""
    End Sub

    Private Sub DisableText()
        txtKodeTarif.Properties.ReadOnly = True
        cboJenisKendaraan.Properties.ReadOnly = True
        txtKodeJenis.Properties.ReadOnly = True
        txtTarif.Properties.ReadOnly = True
        txtOvertime.Properties.ReadOnly = True
        txtPeriodeOvertime.Properties.ReadOnly = True
        txtMaksimalOvertime.Properties.ReadOnly = True
        txtTarifOvertime.Properties.ReadOnly = True
        txtTarifDenda.Properties.ReadOnly = True
        txtGrassPeriode.Properties.ReadOnly = True
    End Sub

    Private Sub EnableText()
        txtKodeTarif.Properties.ReadOnly = False
        cboJenisKendaraan.Properties.ReadOnly = False
        txtKodeJenis.Properties.ReadOnly = False
        txtTarif.Properties.ReadOnly = False
        txtOvertime.Properties.ReadOnly = False
        txtPeriodeOvertime.Properties.ReadOnly = False
        txtMaksimalOvertime.Properties.ReadOnly = False
        txtTarifOvertime.Properties.ReadOnly = False
        txtTarifDenda.Properties.ReadOnly = False
        txtGrassPeriode.Properties.ReadOnly = False
    End Sub


    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        If btnTambah.Text = "&Tambah" Then
            If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "tarif_parkir", "add") Then
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
            txtKodeTarif.Focus()
        ElseIf btnTambah.Text = "&Ubah" Then
            If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "tarif_parkir", "add") Then
                MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "error")
                DefaultSetting()
                Exit Sub
            End If
            lNew = False
            EnableText()
            btnSimpan.Enabled = True
            btnHapus.Enabled = False
            btnTambah.Text = "&Batal"
            cboJenisKendaraan.Focus()
        ElseIf btnTambah.Text = "&Batal" Then
            DefaultSetting()
        End If
    End Sub

    Private Sub DefaultSetting()
        ClearScreen()
        DisableText()
        btnHapus.Enabled = False
        btnTambah.Text = "&Tambah"
        btnSimpan.Enabled = False
        btnRefresh.Enabled = True
        btnTutup.Enabled = True
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
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
        If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "tarif_parkir", "delete") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "error")
            DefaultSetting()
            Exit Sub
        End If
        Dim cMsg As String = MsgBox("Anda yakin ingin menghapus data ini?", MsgBoxStyle.Exclamation + vbYesNo, "Konfirmasi")
        If cMsg = vbYes Then
            Try
                ExecuteQuery("delete from tarif where [kd tarif] = '" &
                          txtKodeTarif.Text.Trim & "'")

                grdTarif.Rows.RemoveAt(grdTarif.CurrentCell.RowIndex)
            Catch ex As Exception
                MsgBox(ex.Message.Trim)
            End Try
        End If
        DefaultSetting()
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
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

        If txtKodeJenis.Text.Trim = "" Then
            MsgBox("kode jenis boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtKodeJenis.Focus()
            Exit Sub
        End If

        If ValidasiData(txtKodeTarif.Text.Trim, _
                        "select [kode tarif] from tarif where [kode tarif] = '" & txtKodeTarif.Text.Trim & "'", _
                        "kode tarif yang anda masukkan sudah tersimpan dalam database") Then
            If ValidasiData(txtKodeJenis.Text.Trim, _
                            "select [kd jenis] from tarif where [kd jenis] = '" & txtKodeJenis.Text.Trim & "'", _
                            "kode jenis yang anda masukkan sudah tersimpan dalam database") Then

                ExecuteQuery("insert into tarif ([kode tarif],[kd jenis],[tarif]," &
                             "[overtime],[periode overtime],[maksimal overtime]," &
                             "[tarif overtime],[tarif denda],[grass periode]) values ('" &
                             txtKodeTarif.Text.Trim & "','" &
                             txtKodeJenis.Text.Trim & "'," &
                             Val(Decimal.Parse(txtTarif.Text.Trim)) & "," &
                             Val(Integer.Parse(txtOvertime.Text.Trim)) & "," &
                             Val(Integer.Parse(txtPeriodeOvertime.Text.Trim)) & "," &
                             Val(Integer.Parse(txtMaksimalOvertime.Text.Trim)) & "," &
                             Val(Decimal.Parse(txtTarifOvertime.Text.Trim)) & "," &
                             Val(Decimal.Parse(txtTarifDenda.Text.Trim)) & "," &
                             Val(Integer.Parse(txtGrassPeriode.Text.Trim)) & ")")

                grdTarif.Rows.Add(New Object() {txtKodeTarif.Text.Trim, _
                                                cboJenisKendaraan.Text.Trim, _
                                                txtKodeJenis.Text.Trim, _
                                                Val(txtTarif.Text.Trim), _
                                                Val(txtOvertime.Text.Trim), _
                                                Val(txtPeriodeOvertime.Text.Trim), _
                                                Val(txtMaksimalOvertime.Text.Trim), _
                                                Val(txtTarifOvertime.Text.Trim), _
                                                Val(txtTarifDenda.Text.Trim), _
                                                Val(txtGrassPeriode.Text.Trim)})
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

        If txtKodeJenis.Text.Trim = "" Then
            MsgBox("kode jenis boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtKodeJenis.Focus()
            Exit Sub
        End If

        ExecuteQuery("update tarif set [kd jenis] = '" &
                      txtKodeJenis.Text.Trim & "',[tarif] = " &
                      Val(Decimal.Parse(txtTarif.Text.Trim)) & ",[overtime] = " &
                      Val(Integer.Parse(txtOvertime.Text.Trim)) & ",[periode overtime] = " &
                      Val(Integer.Parse(txtPeriodeOvertime.Text.Trim)) & ",[maksimal overtime] = " &
                      Val(Integer.Parse(txtMaksimalOvertime.Text.Trim)) & ",[tarif overtime] = " &
                      Val(Decimal.Parse(txtTarifOvertime.Text.Trim)) & ",[tarif denda] = " &
                      Val(Decimal.Parse(txtTarifDenda.Text.Trim)) & ",[grass periode] = " &
                      Val(Integer.Parse(txtGrassPeriode.Text.Trim)) & " where [kode tarif] = '" &
                      txtKodeTarif.Text.Trim & "'")


        grdTarif.CurrentRow.Cells.Item(0).Value = txtKodeTarif.Text.Trim
        grdTarif.CurrentRow.Cells.Item(1).Value = cboJenisKendaraan.Text.Trim
        grdTarif.CurrentRow.Cells.Item(2).Value = txtKodeJenis.Text.Trim
        grdTarif.CurrentRow.Cells.Item(3).Value = Val(txtTarif.Text.Trim)
        grdTarif.CurrentRow.Cells.Item(4).Value = Val(txtOvertime.Text.Trim)
        grdTarif.CurrentRow.Cells.Item(5).Value = Val(txtPeriodeOvertime.Text.Trim)
        grdTarif.CurrentRow.Cells.Item(6).Value = Val(txtMaksimalOvertime.Text.Trim)
        grdTarif.CurrentRow.Cells.Item(7).Value = Val(txtTarifOvertime.Text.Trim)
        grdTarif.CurrentRow.Cells.Item(8).Value = Val(txtTarifDenda.Text.Trim)
        grdTarif.CurrentRow.Cells.Item(9).Value = Val(txtGrassPeriode.Text.Trim)
        DefaultSetting()
    End Sub

    Private Sub cboJenisKendaraan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboJenisKendaraan.SelectedIndexChanged
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select [kd jenis] from jenis_kendaraan where [keterangan] = '" & _
                        cboJenisKendaraan.Text.Trim & "'"

                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        txtKodeJenis.Text = rdr.Item(0).ToString.Trim
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

    Private Sub grdTarif_Click(sender As Object, e As EventArgs) Handles grdTarif.Click
        ShowRecord()
        btnHapus.Enabled = True
        btnTambah.Text = "&Ubah"
        btnRefresh.Enabled = False
        btnTutup.Enabled = False
    End Sub

    Private Sub ShowRecord()
        Try
            txtKodeTarif.Text = grdTarif.CurrentRow.Cells.Item(0).Value.ToString.Trim
            cboJenisKendaraan.Text = grdTarif.CurrentRow.Cells.Item(1).Value.ToString.Trim
            txtKodeJenis.Text = grdTarif.CurrentRow.Cells.Item(2).Value.ToString.Trim
            txtTarif.Text = grdTarif.CurrentRow.Cells.Item(3).Value.ToString.Trim
            txtOvertime.Text = grdTarif.CurrentRow.Cells.Item(4).Value.ToString.Trim
            txtPeriodeOvertime.Text = grdTarif.CurrentRow.Cells.Item(5).Value.ToString.Trim
            txtMaksimalOvertime.Text = grdTarif.CurrentRow.Cells.Item(6).Value.ToString.Trim
            txtTarifOvertime.Text = grdTarif.CurrentRow.Cells.Item(7).Value.ToString.Trim
            txtTarifDenda.Text = grdTarif.CurrentRow.Cells.Item(8).Value.ToString.Trim
            txtGrassPeriode.Text = grdTarif.CurrentRow.Cells.Item(9).Value.ToString.Trim
        Catch ex As Exception

        End Try
    End Sub
End Class