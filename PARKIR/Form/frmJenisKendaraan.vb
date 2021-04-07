Imports System.Data.SqlClient
Public Class frmJenisKendaraan

    Dim lNew As Boolean

    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        If btnTambah.Text = "&Tambah" Then
            If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "jenis_kendaraan", "add") Then
                MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "error")
                DefaultSetting()
                Exit Sub
            End If
            lNew = True
            ClearText()
            EnableText()
            btnHapus.Enabled = False
            btnSimpan.Enabled = True
            btnTambah.Text = "&Batal"
            txtKodeJenis.Focus()
        ElseIf btnTambah.Text = "&Ubah" Then
            If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "jenis_kendaraan", "update") Then
                MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "error")
                DefaultSetting()
                Exit Sub
            End If
            lNew = False
            EnableText()
            btnSimpan.Enabled = True
            btnHapus.Enabled = False
            btnTambah.Text = "&Batal"
            txtJenisKendaraan.Focus()
        ElseIf btnTambah.Text = "&Batal" Then
            DefaultSetting()
        End If
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
        txtKodeJenis.Text = ""
        txtJenisKendaraan.Text = ""
    End Sub

    Private Sub EnableText()
        txtKodeJenis.Properties.ReadOnly = False
        txtJenisKendaraan.Properties.ReadOnly = False
    End Sub

    Private Sub DisableText()
        txtKodeJenis.Properties.ReadOnly = True
        txtJenisKendaraan.Properties.ReadOnly = True
    End Sub

    Private Sub frmJenisKendaraan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DefaultSetting()
    End Sub

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "jenis_kendaraan", "delete") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "error")
            DefaultSetting()
            Exit Sub
        End If
        Dim cMsg As String = MsgBox("Anda yakin ingin menghapus data ini?", MsgBoxStyle.Exclamation + vbYesNo, "Konfirmasi")
        If cMsg = vbYes Then
            Try
                ExecuteQuery("delete from jenis_kendaraan where [kd jenis] = '" &
                          txtKodeJenis.Text.Trim & "'")

                grdJenisKendaraan.Rows.RemoveAt(grdJenisKendaraan.CurrentCell.RowIndex)
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
        If txtKodeJenis.Text.Trim = "" Then
            MsgBox("kode jenis boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtKodeJenis.Focus()
            Exit Sub
        End If

        If ValidasiData(txtKodeJenis.Text.Trim, _
                        "select [kd jenis] from jenis_kendaraan where [kd jenis] = '" & txtKodeJenis.Text.Trim & "'", _
                        "kode jenis yang anda masukkan sudah tersimpan dalam database") Then

            ExecuteQuery("insert into jenis_kendaraan ([kd jenis],[keterangan]) values ('" & _
                          txtKodeJenis.Text.Trim & "','" & _
                          txtJenisKendaraan.Text.Trim & "')")

            grdJenisKendaraan.Rows.Add(New Object() {txtKodeJenis.Text.Trim, _
                                              txtJenisKendaraan.Text.Trim
                                                    })

            DefaultSetting()
        End If
    End Sub

    Private Sub UpdateData()
        If txtKodeJenis.Text.Trim = "" Then
            MsgBox("kode jenis tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtKodeJenis.Focus()
            Exit Sub
        End If

        ExecuteQuery("update jenis_kendaraan set [keterangan] = '" & _
                     txtJenisKendaraan.Text.Trim & "' where [kd jenis] = '" & _
                     txtKodeJenis.Text.Trim & "'")

        grdJenisKendaraan.CurrentRow.Cells.Item(0).Value = txtKodeJenis.Text.Trim
        grdJenisKendaraan.CurrentRow.Cells.Item(1).Value = txtJenisKendaraan.Text.Trim

        DefaultSetting()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
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
                    .Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
        End Try
    End Sub

    Private Sub btnTutup_Click(sender As Object, e As EventArgs) Handles btnTutup.Click
        Me.Dispose()
    End Sub

    Private Sub grdJenisKendaraan_Click(sender As Object, e As EventArgs) Handles grdJenisKendaraan.Click
        ShowRecord()
        btnHapus.Enabled = True
        btnTambah.Text = "&Ubah"
        btnRefresh.Enabled = False
        btnTutup.Enabled = False
    End Sub

    Private Sub ShowRecord()
        Try
            txtKodeJenis.Text = grdJenisKendaraan.CurrentRow.Cells.Item(0).Value.ToString.Trim
            txtJenisKendaraan.Text = grdJenisKendaraan.CurrentRow.Cells.Item(1).Value.ToString.Trim
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdJenisKendaraan_CurrentRowChanged(sender As Object, e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles grdJenisKendaraan.CurrentRowChanged
        ShowRecord()
        btnHapus.Enabled = True
        btnTambah.Text = "&Ubah"
        btnRefresh.Enabled = False
        btnTutup.Enabled = False
    End Sub
End Class