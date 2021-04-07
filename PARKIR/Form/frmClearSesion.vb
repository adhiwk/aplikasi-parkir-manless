Imports System.Data.SqlClient
Public Class frmClearSesion

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        grdSession.Rows.Clear()
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select [kd petugas],[status] from sesi_login where [status] = 'Y' order by [kd petugas] asc"
                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        grdSession.Rows.Add(New Object() {rdr.Item(0).ToString.Trim, _
                                                          String.Format(Now.Date, DateFormat.ShortDate),
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
        btnHapus.Enabled = False
    End Sub

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        Dim cMsg As String = MsgBox("Anda yakin ingin menghapus session ini?", MsgBoxStyle.Exclamation + vbYesNo, "konfirmasi")
        If cMsg = vbYes Then
            Dim Conn As SqlConnection = KoneksiSQL()
            Conn.Open()
            Try
                Using cmd As New SqlCommand
                    With cmd
                        .Connection = Conn
                        .CommandText = "delete from sesi_login where [kd petugas] = '" & _
                                 grdSession.CurrentRow.Cells.Item(0).Value.ToString.Trim & "'"
                        .ExecuteNonQuery()
                        grdSession.Rows.RemoveAt(grdSession.CurrentCell.RowIndex)
                        .Connection.Close()
                    End With
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                Conn.Close()
            End Try
        End If
        btnHapus.Enabled = False
    End Sub

    Private Sub btnTutup_Click(sender As Object, e As EventArgs) Handles btnTutup.Click
        Me.Dispose()
    End Sub

    Private Sub frmClearSesion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnHapus.Enabled = False
    End Sub

    Private Sub grdSession_Click(sender As Object, e As EventArgs) Handles grdSession.Click
        btnHapus.Enabled = True
    End Sub
End Class