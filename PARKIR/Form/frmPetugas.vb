Imports System.IO
Imports System.Data.SqlClient
Public Class frmPetugas

    Private mImageFile As Image
    Private mImageFilePath As String
    Public img As Byte()

    Private Sub btnTambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTambah.Click
        If btnTambah.Text = "&Tambah" Then
            btnTambah.Text = "&Batal"
        ElseIf btnTambah.Text = "&Ubah" Then
            btnTambah.Text = "&Batal"
        Else
            btnTambah.Text = "&Tambah"
        End If
    End Sub

    Private Sub frmPetugas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DefaultSetting()
    End Sub

    Private Sub DefaultSetting()
        btnTambah.Text = "&Tambah"
    End Sub

    Private Sub SimpleButton8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton8.Click
        Me.Close()
    End Sub

    Private Sub btnGambar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGambar.Click
        With OpenFileDialog_
            .Title = "Open Import File"
            .Filter = "Jpeg Files|*.jpg" & _
                      "|Gif Files|*.gif|JPEG Files|*.jpg"
            .ShowDialog()
            If .FileName.ToString <> "" Then
                Try
                    Dim fs As FileStream = New FileStream(.FileName, FileMode.Open)
                    img = Nothing
                    img = New Byte(fs.Length) {}
                    fs.Read(img, 0, fs.Length)
                    fs.Close()
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "File Test Error")
                End Try
                picboxPetugas.BackgroundImageLayout = ImageLayout.Stretch
                picboxPetugas.BackgroundImage = Image.FromFile(.FileName.ToString)
            Else
                MsgBox("No File Is Selected")
                Exit Sub
            End If
        End With
    End Sub
End Class