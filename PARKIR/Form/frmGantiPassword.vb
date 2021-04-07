Imports System.Data.SqlClient
Public Class frmGantiPassword
    Friend lStart As Boolean
    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        txtUserName.Text = ""
        txtPasswordLama.Text = ""
        txtPasswordBaru1.Text = ""
        txtPasswordBaru2.Text = ""
        Me.Dispose()
    End Sub

    Private Sub frmGantiPassword_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        'txtPasswordLama.Text = ""
        'txtPasswordBaru1.Text = ""
        'txtPasswordBaru2.Text = ""
        If lStart Then
            txtPasswordLama.Focus()
        Else
            txtPasswordBaru1.Focus()
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If CekPassword(txtUserName.Text.Trim) Then
            'update password
            UpdatePassword(txtUserName.Text.Trim)
            MsgBox("Update Password Berhasil")
            Me.Dispose()
        Else
            txtPasswordLama.Focus()
        End If
    End Sub

    Private Sub UpdatePassword(ByVal cUser As String)
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "update petugas set [password] = '" &
                                   Simple3Des.GetMd5Hash(txtPasswordBaru2.Text.Trim) & "' " &
                                   "where [kd petugas] = '" &
                                   cUser.Trim & "'"
                    .ExecuteNonQuery()
                    .Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
        End Try
    End Sub

    Private Function CekPassword(cUser As String) As Boolean
        Dim lStatus As Boolean
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select [password] " &
                                   "from petugas where [kd petugas] = '" &
                                   cUser.Trim & "'"
                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        If rDr.HasRows Then
                            If rDr.Item(0).ToString.Trim = Simple3Des.GetMd5Hash(txtPasswordLama.Text.Trim) Then
                                lStatus = True
                            Else
                                MsgBox("password lama tidak cocok", MsgBoxStyle.Critical, "Error")
                                lStatus = False
                            End If
                        End If
                    End While
                    rDr.Close()
                    .Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
        End Try

        Return lStatus
    End Function

    Private Sub txtPasswordBaru2_LostFocus(sender As Object, e As EventArgs) Handles txtPasswordBaru2.LostFocus
        If txtPasswordBaru1.Text.Trim <> txtPasswordBaru2.Text.Trim Then
            MsgBox("Password tidak cocok, ketik ulang password baru anda", MsgBoxStyle.Critical, "Error")
            txtPasswordBaru1.Text = ""
            txtPasswordBaru2.Text = ""
            lStart = False
        End If
    End Sub
End Class