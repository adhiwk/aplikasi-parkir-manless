Imports System.Data.SqlClient
Imports Telerik.WinControls.UI

Public Class ctrlImageCaptureList
    Dim lShow As Boolean
    Private Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click

        Dim cQuery As String = ""
        Dim cTglMasuk As String = ""
        Dim cTglKeluar As String = ""

        Dim xForm As New DevExpress.Utils.WaitDialogForm
        xForm.LookAndFeel.UseDefaultLookAndFeel = False
        xForm.LookAndFeel.SkinName = "Blue"

        If cboFilter.Text.Trim = "TANGGAL MASUK" Then
            cQuery = "select [nomor transaksi],[nopol],[tgl masuk],[tgl keluar]," &
                     "[jam],[menit],[detik] from parkir where convert(datetime,[tgl masuk]) between '" &
                     ConvertTanggal(dtAwal) & " " &
                     dtJamAwal.Time.Hour.ToString.Trim & ":" &
                     dtJamAwal.Time.Minute.ToString.Trim & ":" &
                     dtJamAwal.Time.Second.ToString.Trim & "' and '" &
                     ConvertTanggal(dtAkhir) & " " &
                     dtJamAkhir.Time.Hour.ToString.Trim & ":" &
                     dtJamAkhir.Time.Minute.ToString.Trim & ":" &
                     dtJamAkhir.Time.Second.ToString.Trim & "'"

        ElseIf cboFilter.Text.Trim = "TANGGAL KELUAR" Then
            cQuery = "select [nomor transaksi],[nopol],[tgl masuk],[tgl keluar]," &
                     "[jam],[menit],[detik] from parkir where convert(datetime,[tgl keluar]) between '" &
                     ConvertTanggal(dtAwal) & " " &
                     dtJamAwal.Time.Hour.ToString.Trim & ":" &
                     dtJamAwal.Time.Minute.ToString.Trim & ":" &
                     dtJamAwal.Time.Second.ToString.Trim & "' and '" &
                     ConvertTanggal(dtAkhir) & " " &
                     dtJamAkhir.Time.Hour.ToString.Trim & ":" &
                     dtJamAkhir.Time.Minute.ToString.Trim & ":" &
                     dtJamAkhir.Time.Second.ToString.Trim & "'"

        ElseIf cboFilter.Text.Trim = "NOMOR TIKET" Then
            cQuery = "select [nomor transaksi],[nopol],[tgl masuk],[tgl keluar]," &
                    "[jam],[menit],[detik] from parkir where [nomor transaksi] = '" & txtNomorTiket.Text.Trim & "'"
        End If

        If cQuery.Trim = "" Then
            Exit Sub
        End If

        grdParkir.Rows.Clear()

        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        'Try
        Using cmd As New SqlCommand
            With cmd
                .Connection = Conn
                .CommandText = cQuery.Trim
                Dim rdr As SqlDataReader = .ExecuteReader
                While rdr.Read
                    If rdr.Item(2).ToString.Trim = "" Then
                        cTglMasuk = ""
                    Else
                        cTglMasuk = FormatDateTime(Date.Parse(rdr.Item(2).ToString.Trim), DateFormat.GeneralDate)
                    End If

                    If rdr.Item(3).ToString.Trim = "" Then
                        cTglKeluar = ""
                    Else
                        cTglKeluar = FormatDateTime(Date.Parse(rdr.Item(3).ToString.Trim), DateFormat.GeneralDate)
                    End If

                    grdParkir.Rows.Add(New Object() {rdr.Item(0).ToString.Trim,
                                           rdr.Item(1).ToString.Trim,
                                           cTglMasuk.Trim,
                                           cTglKeluar.Trim,
                                           rdr.Item(4).ToString.Trim,
                                           rdr.Item(5).ToString.Trim,
                                           rdr.Item(6).ToString.Trim})

                End While
                rdr.Close()
            End With
        End Using
        'Catch ex As Exception
        'MsgBox(ex.Message)
        'End Try
        Conn.Close()
        lShow = False
        xForm.Dispose()
    End Sub

    Private Sub ctrlImageCaptureList_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Dock = DockStyle.Fill
    End Sub

    Private Sub MasterTemplate_Click(sender As Object, e As EventArgs) Handles grdParkir.Click
        Try
            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            Dim Conn As SqlConnection = KoneksiSQL()
            Conn.Open()
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select [img_in] from parkir_image " &
                                   "where [nomor transaksi] = '" &
                                   grdParkir.CurrentRow.Cells.Item(0).Value.ToString.Trim & "'"
                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        peImage.Image = byteArrayToImage(rdr.Item(0))
                        peImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
                    End While
                    rdr.Close()
                End With
            End Using
            Conn.Close()

            xForm.Dispose()
        Catch ex As Exception

        End Try
        lShow = True
    End Sub

    Private Sub grdParkir_CurrentRowChanged(sender As Object, e As CurrentRowChangedEventArgs) Handles grdParkir.CurrentRowChanged
        If lShow Then
            Dim xForm As New DevExpress.Utils.WaitDialogForm
            xForm.LookAndFeel.UseDefaultLookAndFeel = False
            xForm.LookAndFeel.SkinName = "Blue"

            Dim Conn As SqlConnection = KoneksiSQL()
            Conn.Open()
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select [img_in] from parkir_image " &
                                   "where [nomor transaksi] = '" &
                                   grdParkir.CurrentRow.Cells.Item(0).Value.ToString.Trim & "'"
                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        peImage.Image = byteArrayToImage(rdr.Item(0))
                        peImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
                    End While
                    rdr.Close()
                End With
            End Using
            Conn.Close()

            xForm.Dispose()
        End If
    End Sub
End Class
