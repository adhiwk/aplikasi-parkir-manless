Imports System.Data.SqlClient
Public Class frmCariJenisMember
    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub frmCariJenisMember_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub TxtCariData_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCariData.KeyPress
        If Asc(e.KeyChar) = 13 Then
            grdMember.Rows.Clear()

            Dim Conn As SqlConnection = KoneksiSQL()
            Conn.Open()
            Try
                Using cmd As New SqlCommand
                    With cmd
                        .Connection = Conn
                        .CommandText = "select [jenis member],[kdj member] " &
                                       "from jenis_member where [jenis member] like '%" & txtCariData.Text.Trim & "%'"
                        Dim rdr As SqlDataReader = .ExecuteReader
                        While rdr.Read
                            grdMember.Rows.Add(New Object() {rdr.Item(0).ToString.Trim,
                                               rdr.Item(1).ToString.Trim})
                        End While
                    End With
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Conn.Close()
        End If
    End Sub

    Private Sub GrdMember_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdMember.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Me.Dispose()
        End If
    End Sub

    Private Sub frmCariJenisMember_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        txtCariData.Focus()
    End Sub
End Class