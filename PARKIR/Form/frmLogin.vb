'Imports System.Runtime.InteropServices
'Imports System.Reflection

Imports System.Data.SqlClient
Public Class frmLogin
    Dim nCount As Integer = 0

    Private Sub frmLogin_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        txtUser.Focus()
    End Sub
    Private Sub frmLogin_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        End
    End Sub

    Private Sub frmLogin_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyValue.ToString = "116" Then
            'F2 -> Mulai Transaksi
            If txtUser.Text.Trim = "" Then
                MsgBox("user name dalam keadaan kosong, ganti password gagal")
                Exit Sub
            Else
                frmGantiPassword.lStart = True
                frmGantiPassword.txtPasswordLama.Text = ""
                frmGantiPassword.txtPasswordBaru1.Text = ""
                frmGantiPassword.txtPasswordBaru2.Text = ""
                frmGantiPassword.txtUserName.Text = txtUser.Text.Trim
                frmGantiPassword.ShowDialog()
            End If
        End If
    End Sub

    'Private Sub frmLogin_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.HandleCreated
    '    KeyboardJammer.Jam()
    'End Sub

    'Private Sub frmLogin_HandleDestroyed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.HandleDestroyed
    '    KeyboardJammer.UnJam()
    'End Sub

    Private Sub frmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Lock Desktop when load the software
        txtPassword.Text = ""
        txtUser.Text = ""
        txtUser.Focus()
    End Sub

    Private Function CekSesionLogin(ByVal cUserId As String) As Boolean
        Dim cStatus As Boolean = True
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select [kd petugas],[status] " &
                                   "from sesi_login where [kd petugas] = '" &
                                   cUserId.Trim & "'"
                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        If rDr.HasRows Then
                            If rDr.Item(0).ToString.Trim = cUserId.Trim Then
                                If rDr.Item(1).ToString.Trim = "Y" Then
                                    cStatus = True
                                Else
                                    cStatus = False
                                End If
                            End If
                        End If
                    End While

                    If rDr.HasRows = False Then
                        rDr.Close()
                        .CommandText = "insert into sesi_login ([kd petugas],[status]) values ('" &
                            cUserId.Trim & "','Y')"
                        .ExecuteNonQuery()
                        cStatus = False
                    End If

                    rDr.Close()
                    .Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
        End Try
        Return cStatus
    End Function

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click

        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "Select [kd petugas],[nama petugas],[kd level],[password] " &
                                   "From Petugas Where [kd petugas] = '" &
                                   txtUser.Text.Trim & "' And [Password] = '" &
                                   Simple3Des.GetMd5Hash(txtPassword.Text.Trim) & "'"
                    Dim rDr As SqlDataReader = .ExecuteReader
                    While rDr.Read
                        If rDr.HasRows Then

                            If rDr.Item(0).ToString.Trim = txtUser.Text.Trim Then
                                If rDr.Item(3).ToString.Trim = Simple3Des.GetMd5Hash(txtPassword.Text.Trim) Then

                                    'If CekSesionLogin(txtUser.Text.Trim) Then
                                    '    MsgBox("Sesi login dengan user id ini sudah terdaftar, tidak diperbolehkan adanya duplikasi login", MsgBoxStyle.Critical, "Error")
                                    '    txtUser.Text = ""
                                    '    txtPassword.Text = ""
                                    '    rDr.Close()
                                    '    Exit Sub
                                    'End If

                                    'CreateSession(txtUser.Text.Trim)

                                    If rDr.Item(2).ToString.Trim = "OP" Then
                                        Me.Hide()

                                        If My.Settings.jenis_gerbang.Trim = "IN" Then
                                            frmManLessIn.Show()
                                        ElseIf My.Settings.jenis_gerbang.Trim = "OUT" Then
                                            frmManLessOut.Show()
                                        End If
                                    ElseIf rDr.Item(2).ToString.Trim = "ADMIN" Then
                                        Me.Hide()
                                        frmMain.cUserId = rDr.Item(0).ToString.Trim
                                        frmMain.Show()
                                    End If
                                Else
                                    MetroFramework.MetroMessageBox.Show(Me, "user name dan password tidak ditemukan")
                                    'MsgBox("user name dan password tidak ditemukan", MsgBoxStyle.Critical, "Error")
                                    Exit Sub
                                End If
                            Else
                                MetroFramework.MetroMessageBox.Show(Me, "Maaf anda belum terdaftar, silahkan hubungi administrasi",
                                                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                'MsgBox("Maaf anda belum terdaftar, silahkan hubungi administrasi", MsgBoxStyle.Critical, "Error!")
                                txtUser.Text = ""
                                txtPassword.Text = ""
                                txtUser.Focus()
                                Exit Sub
                            End If
                        Else
                            nCount = nCount + 1
                            MetroFramework.MetroMessageBox.Show(Me, "Maaf anda belum terdaftar, silahkan hubungi administrasi",
                                                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            'MsgBox("Maaf anda belum terdaftar, silahkan hubungi administrasi", MsgBoxStyle.Critical, "Error!")
                            txtUser.Text = ""
                            txtPassword.Text = ""
                            txtUser.Focus()
                        End If
                    End While

                    If rDr.HasRows = False Then
                        MetroFramework.MetroMessageBox.Show(Me, "Maaf anda belum terdaftar, silahkan hubungi administrasi",
                                                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        'MsgBox("Maaf anda belum terdaftar, silahkan hubungi administrasi", MsgBoxStyle.Critical, "Error!")
                        nCount = nCount + 1
                        txtUser.Text = ""
                        txtPassword.Text = ""
                        txtUser.Focus()
                        Exit Sub
                    End If

                    rDr.Close()
                    .Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Dispose()
    End Sub

    Private Sub lblGantiPassword_Click(sender As Object, e As EventArgs) Handles lblGantiPassword.Click
        If txtUser.Text.Trim = "" Then
            MsgBox("user name dalam keadaan kosong, ganti password gagal")
            Exit Sub
        Else
            frmGantiPassword.lStart = True
            frmGantiPassword.txtPasswordLama.Text = ""
            frmGantiPassword.txtPasswordBaru1.Text = ""
            frmGantiPassword.txtPasswordBaru2.Text = ""
            frmGantiPassword.txtUserName.Text = txtUser.Text.Trim
            frmGantiPassword.ShowDialog()
        End If
    End Sub
End Class