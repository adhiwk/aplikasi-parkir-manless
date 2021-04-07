Imports System.Data.SqlClient

Public Class frmShift
    Dim lNew As Boolean

    Private Sub btnTutup_Click(sender As Object, e As EventArgs) Handles btnTutup.Click
        Me.Dispose()
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
        txtKodeShift.Text = ""
        txtNamaShift.Text = ""
        txtMulai.Text = ""
        txtAkhir.Text = ""
        txtStatus.Text = ""
    End Sub

    Private Sub DisableText()
        txtKodeShift.Properties.ReadOnly = True
        txtNamaShift.Properties.ReadOnly = True
        txtMulai.Properties.ReadOnly = True
        txtAkhir.Properties.ReadOnly = True
        txtStatus.Properties.ReadOnly = True
    End Sub

    Private Sub EnableText()
        txtKodeShift.Properties.ReadOnly = False
        txtNamaShift.Properties.ReadOnly = False
        txtMulai.Properties.ReadOnly = False
        txtAkhir.Properties.ReadOnly = False
        txtStatus.Properties.ReadOnly = False
    End Sub


    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        If btnTambah.Text = "&Tambah" Then
            lNew = True
            ClearText()
            EnableText()
            btnHapus.Enabled = False
            btnSimpan.Enabled = True
            btnTambah.Text = "&Batal"
            txtKodeShift.Focus()
        ElseIf btnTambah.Text = "&Ubah" Then
            lNew = False
            EnableText()
            btnSimpan.Enabled = True
            btnHapus.Enabled = False
            btnTambah.Text = "&Batal"
            txtNamaShift.Focus()
        ElseIf btnTambah.Text = "&Batal" Then
            DefaultSetting()
        End If
    End Sub

    Private Sub grdShift_Click(sender As Object, e As EventArgs) Handles grdShift.Click
        ShowRecord()
        btnHapus.Enabled = True
        btnTambah.Text = "&Ubah"
        btnRefresh.Enabled = False
        btnTutup.Enabled = False
    End Sub

    Private Sub ShowRecord()
        Try
            txtKodeShift.Text = grdShift.CurrentRow.Cells.Item(0).Value.ToString.Trim
            txtNamaShift.Text = grdShift.CurrentRow.Cells.Item(1).Value.ToString.Trim
            txtMulai.Text = grdShift.CurrentRow.Cells.Item(2).Value.ToString.Trim
            txtAkhir.Text = grdShift.CurrentRow.Cells.Item(3).Value.ToString.Trim
            txtStatus.Text = grdShift.CurrentRow.Cells.Item(4).Value.ToString.Trim
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdShift_CurrentRowChanged(sender As Object, e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles grdShift.CurrentRowChanged
        ShowRecord()
        btnHapus.Enabled = True
        btnTambah.Text = "&Ubah"
        btnRefresh.Enabled = False
        btnTutup.Enabled = False
    End Sub

    Private Sub frmShift_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DefaultSetting()
    End Sub

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        Dim cMsg As String = MsgBox("Anda yakin ingin menghapus data ini?", MsgBoxStyle.Exclamation + vbYesNo, "Konfirmasi")
        If cMsg = vbYes Then
            Try

                ExecuteQuery("delete from shift where [kd shift] = '" & _
                          txtKodeShift.Text.Trim & "'")

                grdShift.Rows.RemoveAt(grdShift.CurrentCell.RowIndex)

            Catch ex As Exception
                MsgBox(ex.Message.Trim)
            End Try
        End If
        DefaultSetting()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        grdShift.Rows.Clear()

        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select [kd shift],[keterangan],[jam mulai],[jam akhir],[status] from shift order by [keterangan] asc"

                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        grdShift.Rows.Add(New Object() {rdr.Item(0).ToString.Trim, _
                                                        rdr.Item(1).ToString.Trim, _
                                                        rdr.Item(2).ToString.Trim, _
                                                        rdr.Item(3).ToString.Trim, _
                                                        rdr.Item(4).ToString.Trim})

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

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If lNew Then
            SaveData()
        Else
            UpdateData()
        End If
    End Sub

    Private Sub SaveData()
        If txtKodeShift.Text.Trim = "" Then
            MsgBox("kode shift tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtKodeShift.Focus()
            Exit Sub
        End If

        If txtMulai.Text.Trim = "" Then
            MsgBox("jam mulai tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtMulai.Focus()
            Exit Sub
        End If

        If txtAkhir.Text.Trim = "" Then
            MsgBox("jam selesai tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtAkhir.Focus()
            Exit Sub
        End If


        If ValidasiData(txtKodeShift.Text.Trim, _
                        "select [kd shift] from shift where [kd shift] = '" & txtKodeShift.Text.Trim & "'", _
                        "kode shift yang anda masukkan sudah tersimpan dalam database") Then

            ExecuteQuery("insert into shift ([kd shift],[keterangan],[jam mulai],[jam akhir]) values ('" & _
                          txtKodeShift.Text.Trim & "','" & _
                          txtNamaShift.Text.Trim & "','" & _
                          txtMulai.Text.Trim & "','" & _
                          txtAkhir.Text.Trim & "')")

            grdShift.Rows.Add(New Object() {txtKodeShift.Text.Trim, _
                                            txtNamaShift.Text.Trim, _
                                            txtMulai.Text.Trim, _
                                            txtAkhir.Text.Trim
                                           })

            DefaultSetting()
        End If
    End Sub

    Private Sub UpdateData()
        If txtKodeShift.Text.Trim = "" Then
            MsgBox("kode shift tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtKodeShift.Focus()
            Exit Sub
        End If

        If txtMulai.Text.Trim = "" Then
            MsgBox("jam mulai tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtMulai.Focus()
            Exit Sub
        End If

        If txtAkhir.Text.Trim = "" Then
            MsgBox("jam selesai tidak boleh dalam keadaan kosong", MsgBoxStyle.Critical, "Error")
            txtAkhir.Focus()
            Exit Sub
        End If

        ExecuteQuery("update shift set [kd shift] = '" & _
                     txtKodeShift.Text.Trim & "',[keterangan] = '" & _
                     txtNamaShift.Text.Trim & "',[jam mulai] = '" & _
                     txtMulai.Text.Trim & "',[jam akhir] = '" & _
                     txtAkhir.Text.Trim & "',[status] = '" & _
                     txtStatus.Text.Trim & "'")
                        

        grdShift.CurrentRow.Cells.Item(0).Value = txtKodeShift.Text.Trim
        grdShift.CurrentRow.Cells.Item(1).Value = txtNamaShift.Text.Trim
        grdShift.CurrentRow.Cells.Item(2).Value = txtMulai.Text.Trim
        grdShift.CurrentRow.Cells.Item(3).Value = txtAkhir.Text.Trim
        grdShift.CurrentRow.Cells.Item(4).Value = txtStatus.Text.Trim
       
        DefaultSetting()
    End Sub
End Class