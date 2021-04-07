Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Class frmDailyReport

    '**********************************************************************************
    '** Deklarasikan variabel Shift
    '**********************************************************************************
    Dim cShift As String = ""
    Dim cJenisKendaraan As String = ""
    Dim nCasual_In As Integer = 0
    Dim nCasual_Out As Integer = 0
    Dim nPass_In As Integer = 0
    Dim nPass_Out As Integer = 0
    Dim nLost_Out As Integer = 0
    Dim nCash_In As Integer = 0
    Dim nCash_Out As Integer = 0
    Dim nVoucer As Integer = 0
    Dim nValet As Integer = 0
    Dim nDenda As Integer = 0
    Dim nDiscount As Integer = 0

    '**********************************************************************************
    '** Deklarasikan variabel petugas
    '**********************************************************************************
    Dim cJenisGerbang As String = ""
    Dim cPetugas As String = ""
    Dim nQty_In As Integer = 0
    Dim nQty_Out As Integer = 0
    Dim nQty_Loss As Integer = 0
    Dim nValue_Loss As Integer = 0
    Dim nCash As Integer = 0

    Private Sub RedimVariablePetugas()
        cJenisGerbang = ""
        cPetugas = ""
        nQty_In = 0
        nQty_Out = 0
        nQty_Loss = 0
        nValue_Loss = 0
        nCash = 0
        nVoucer = 0
        nValet = 0
        nDenda = 0
        nDiscount = 0
    End Sub

    Private Sub RedimVariableShift()
        cShift = ""
        cJenisKendaraan = ""
        nCasual_In = 0
        nCasual_Out = 0
        nPass_In = 0
        nPass_Out = 0
        nLost_Out = 0
        nCash_In = 0
        nCash_Out = 0
        nVoucer = 0
        nValet = 0
        nDenda = 0
        nDiscount = 0
    End Sub

    Private Sub btnTutup_Click(sender As Object, e As EventArgs) Handles btnTutup.Click
        Me.Dispose()
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        'Using cFormLaporan As New frmLaporan()
        Dim xForm As New DevExpress.Utils.WaitDialogForm
        xForm.LookAndFeel.UseDefaultLookAndFeel = False
        xForm.LookAndFeel.SkinName = "Blue"

        'select * from parkir where [tgl masuk] = '2014-05-04' and [jam masuk] between '00:00:00' and '01:00:00' 

        '*******************************************************************************
        '** Dasar logika
        '** query jenis kendaraan
        '** query data parkir berdasarkan jenis kendaraan dan kode shift
        '*******************************************************************************
       
        If cboJenisLaporan.Text.Trim = "Rekap Per Shift" Then

            SetupReportShift()
            Dim Conn As SqlConnection = KoneksiSQL()
            Conn.Open()
            Try
                Using cmd As New SqlCommand
                    With cmd
                        .Connection = Conn
                        .CommandText = "select [kd jenis],[keterangan] from jenis_kendaraan order by [kd jenis] asc"
                        Dim rdr As SqlDataReader = .ExecuteReader
                        pBar.EditValue = 0

                        While rdr.Read
                            If cboKdShift.Text <> "All" Then
                                RedimVariableShift()
                                QueryDataParkir(rdr.Item(0).ToString.Trim, rdr.Item(1).ToString.Trim, ConvertTanggal(dtTanggal), ConvertTanggal(dtAkhir))
                            End If
                            pBar.EditValue = pBar.EditValue + 10
                        End While
                        rdr.Close()
                        .Connection.Close()
                    End With
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                Conn.Close()
            End Try
        ElseIf cboJenisLaporan.Text = "Rekap Per Petugas" Then
            SetupReportPetugas()
            'select [petugas keluar], COUNT([nomor transaksi]) as [qty_out],
            'SUM([tarif_keluar]) as [Cash Out], SUM([tarif overtime]) as [Overtime],
            'SUM([denda]) as [denda] 
            'from parkir where [tgl keluar]  between '2014-05-17' and '2014-05-17' 
            'and [kd shift out] = '001' and [status] = 'Y' group by [petugas keluar] 
            'go

            '------ Query Parkir Masuk
            'select [petugas masuk], COUNT([nomor transaksi]) as [qty_in],
            'SUM([tarif_masuk]) as [Cash In]
            'from parkir where [tgl masuk]  between '2014-05-17' and '2014-05-17' 
            'and [kd shift in] = '001' group by [petugas masuk]
            'go
            Dim Conn As SqlConnection = KoneksiSQL()
            Conn.Open()
            Try
                Using cmd As New SqlCommand
                    With cmd
                        .Connection = Conn
                        '************************************************************************************
                        '** Query data pos keluar
                        '************************************************************************************
                        .CommandText = "select [petugas keluar], COUNT([nomor transaksi]) as [qty_out]," & _
                                      "SUM([tarif_keluar]) as [Cash Out], SUM([tarif overtime]) as [Overtime]," & _
                                      "SUM([denda]) as [denda] " & _
                                      "from parkir where [tgl keluar]  between '" & ConvertTanggal(dtTanggal) & "' and '" & _
                                      ConvertTanggal(dtAkhir) & "' and [kd shift out] = '" & _
                                      cboKdShift.Text.Trim & "' and [status] = 'Y' group by [petugas keluar] "
                        Dim rdr As SqlDataReader = .ExecuteReader
                        pBar.EditValue = 0

                        While rdr.Read
                            If cboKdShift.Text <> "All" Then
                                RedimVariablePetugas()
                                cJenisGerbang = "POS KELUAR"
                                cPetugas = rdr.Item(0).ToString.Trim
                                QueryQtyLoss(rdr.Item(0).ToString.Trim)
                                nQty_Out = Val(rdr.Item(1).ToString.Trim)
                                nValue_Loss = Val(rdr.Item(4).ToString.Trim)
                                nDenda = Val(rdr.Item(4).ToString.Trim)
                                nCash = Val(rdr.Item(2).ToString.Trim) + Val(rdr.Item(3).ToString.Trim)
                                SavePendapatanPetugas()
                            End If
                            pBar.EditValue = pBar.EditValue + 10
                        End While
                        rdr.Close()

                        '************************************************************************************
                        '** Query data pas masuk
                        '************************************************************************************
                        .CommandText = "select [petugas masuk], COUNT([nomor transaksi]) as [qty_in]," & _
                                       "SUM([tarif_masuk]) as [Cash In] " & _
                                      "from parkir where [tgl masuk]  between '" & ConvertTanggal(dtTanggal) & "' and '" & _
                                      ConvertTanggal(dtAkhir) & "' and [kd shift in] = '" & _
                                      cboKdShift.Text.Trim & "' group by [petugas masuk] "
                        rdr = .ExecuteReader


                        While rdr.Read
                            If cboKdShift.Text <> "All" Then
                                RedimVariablePetugas()
                                cJenisGerbang = "POS MASUK"
                                cPetugas = rdr.Item(0).ToString.Trim
                                nQty_In = Val(rdr.Item(1).ToString.Trim)
                                nCash = Val(rdr.Item(2).ToString.Trim)
                                SavePendapatanPetugas()
                            End If
                            pBar.EditValue = pBar.EditValue + 10
                        End While
                        rdr.Close()

                        .Connection.Close()
                    End With
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                Conn.Close()
            End Try
        End If
        xForm.Dispose()
        MsgBox("Proses filter data berhasil...", MsgBoxStyle.Information, "Informasi")
    End Sub

    Private Sub QueryQtyLoss(ByVal cKodePetugas As String)
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select COUNT([info]) as [qty_loss] " & _
                                  "from parkir where [tgl keluar]  between '" & ConvertTanggal(dtTanggal) & "' and '" & _
                                  ConvertTanggal(dtAkhir) & "' and [kd shift out] = '" & _
                                  cboKdShift.Text.Trim & "' and [status] = 'Y' and [info] = 'HL' and [petugas keluar] = '" & _
                                  cKodePetugas.Trim & "'"

                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        If cboKdShift.Text <> "All" Then
                            nQty_Loss = Val(rdr.Item(0).ToString.Trim)
                        End If
                    End While
                    rdr.Close()
                    .Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
        End Try
    End Sub

    Private Sub QueryDataParkir(ByVal cKodeJenis As String, ByVal cKendaraan As String, cTglAwal As String, cTglAkhir As String)

        cJenisKendaraan = cKodeJenis.Trim & " " & cKendaraan.Trim

        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn

                    '***********************************************************
                    'Query Data parkir masuk ( Casual )
                    '***********************************************************
                    'If cboKdShift.Text = "All" Then
                    '    .CommandText = "select COUNT([nomor transaksi]) as [casual in] from parkir " & _
                    '                   "where [tgl masuk] between '" & cTglAwal.Trim & "' and '" & _
                    '                   cTglAkhir.Trim & "' and [jenis] = 'C' and [kd jenis] = '" & _
                    '                   cKodeJenis.Trim & "' group by [kd shift in]"
                    'End If

                    If cboKdShift.Text <> "All" Then
                        .CommandText = "select COUNT([nomor transaksi]) as [casual in] from parkir " & _
                                       "where [tgl masuk] between '" & cTglAwal.Trim & "' and '" & _
                                       cTglAkhir.Trim & "' and [jenis] = 'C' and [kd jenis] = '" & _
                                       cKodeJenis.Trim & "' and [kd shift in] = '" & cboKdShift.Text.Trim & "'"
                    End If

                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        nCasual_In = Val(rdr.Item(0).ToString.Trim)
                    End While
                    rdr.Close()

                    '************************************************************
                    'Query Data parkir masuk ( Pass )
                    '************************************************************
                    If cboKdShift.Text <> "All" Then
                        .CommandText = "select COUNT([nomor transaksi]) as [pass in] from parkir " & _
                                       "where [tgl masuk] between '" & cTglAwal.Trim & "' and '" & _
                                       cTglAkhir.Trim & "' and [jenis] = 'P' and [kd jenis] = '" & _
                                       cKodeJenis.Trim & "' and [kd shift in] = '" & cboKdShift.Text.Trim & "'"
                    End If

                    rdr = .ExecuteReader
                    While rdr.Read
                        nPass_In = Val(rdr.Item(0).ToString.Trim)
                    End While
                    rdr.Close()

                    'Query Data Parkir Keluar ( Casual )
                    If cboKdShift.Text <> "All" Then
                        .CommandText = "select COUNT([nomor transaksi]) as [casual out] from parkir " & _
                                       "where [tgl keluar] between '" & cTglAwal.Trim & "' and '" & _
                                       cTglAkhir.Trim & "' and [jenis] = 'C' and [kd jenis] = '" & _
                                       cKodeJenis.Trim & "' and [kd shift out] = '" & cboKdShift.Text.Trim & "'"
                    End If

                    rdr = .ExecuteReader
                    While rdr.Read
                        nCasual_Out = Val(rdr.Item(0).ToString.Trim)
                    End While
                    rdr.Close()

                    'Query Data Parkir Keluar ( Pass )
                    If cboKdShift.Text <> "All" Then
                        .CommandText = "select COUNT([nomor transaksi]) as [pass out] from parkir " & _
                                       "where [tgl keluar] between '" & cTglAwal.Trim & "' and '" & _
                                       cTglAkhir.Trim & "' and [jenis] = 'P' and [kd jenis] = '" & _
                                       cKodeJenis.Trim & "' and [kd shift out] = '" & cboKdShift.Text.Trim & "'"
                    End If

                    rdr = .ExecuteReader
                    While rdr.Read
                        nPass_Out = Val(rdr.Item(0).ToString.Trim)
                    End While
                    rdr.Close()

                    'Query Data Parkir Keluar ( Loss )
                    If cboKdShift.Text <> "All" Then
                        .CommandText = "select COUNT([nomor transaksi]) as [loss out] from parkir " & _
                                       "where [tgl keluar] between '" & cTglAwal.Trim & "' and '" & _
                                       cTglAkhir.Trim & "' and [info] = 'HL' and [kd jenis] = '" & _
                                       cKodeJenis.Trim & "' and [kd shift out] = '" & cboKdShift.Text.Trim & "'"
                    End If

                    rdr = .ExecuteReader
                    While rdr.Read
                        nLost_Out = Val(rdr.Item(0).ToString.Trim)
                    End While
                    rdr.Close()

                    'Query Data transaksi parkir ( Cash In)
                    If cboKdShift.Text <> "All" Then
                        .CommandText = "select sum([tarif_masuk]) as [cash in] from parkir " & _
                                       "where [tgl masuk] between '" & cTglAwal.Trim & "' and '" & _
                                       cTglAkhir.Trim & "' and [kd jenis] = '" & _
                                       cKodeJenis.Trim & "' and [kd shift in] = '" & cboKdShift.Text.Trim & "'"
                    End If

                    rdr = .ExecuteReader
                    While rdr.Read
                        nCash_In = Val(rdr.Item(0).ToString.Trim)
                    End While
                    rdr.Close()

                    'Query Data transaksi parkir ( Cash Out)
                    If cboKdShift.Text <> "All" Then
                        .CommandText = "select sum([tarif_keluar]) as [cash out] from parkir " & _
                                       "where [tgl keluar] between '" & cTglAwal.Trim & "' and '" & _
                                       cTglAkhir.Trim & "' and [kd jenis] = '" & _
                                       cKodeJenis.Trim & "' and [kd shift out] = '" & cboKdShift.Text.Trim & "'"
                    End If

                    rdr = .ExecuteReader
                    While rdr.Read
                        nCash_Out = Val(rdr.Item(0).ToString.Trim)
                    End While
                    rdr.Close()

                    'Query Data transaksi parkir ( Overtime)
                    If cboKdShift.Text <> "All" Then
                        .CommandText = "select sum([tarif overtime]) as [overtime] from parkir " & _
                                       "where [tgl keluar] between '" & cTglAwal.Trim & "' and '" & _
                                       cTglAkhir.Trim & "' and [kd jenis] = '" & _
                                       cKodeJenis.Trim & "' and [kd shift out] = '" & cboKdShift.Text.Trim & "'"
                    End If

                    rdr = .ExecuteReader
                    While rdr.Read
                        nCash_Out = Val(nCash_Out) + Val(rdr.Item(0).ToString.Trim)
                    End While
                    rdr.Close()

                    'Query Data transaksi parkir (denda)
                    If cboKdShift.Text <> "All" Then
                        .CommandText = "select sum([denda]) as [denda] from parkir " & _
                                       "where [tgl keluar] between '" & cTglAwal.Trim & "' and '" & _
                                       cTglAkhir.Trim & "' and [kd jenis] = '" & _
                                       cKodeJenis.Trim & "' and [kd shift out] = '" & cboKdShift.Text.Trim & "'"
                    End If

                    rdr = .ExecuteReader
                    While rdr.Read
                        nDenda = Val(rdr.Item(0).ToString.Trim)
                    End While
                    rdr.Close()

                    .Connection.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
        End Try

        SaveDataLocal()
    End Sub


    Private Sub SetupReportShift()
        Dim Conn As OleDbConnection = KoneksiOleDB()
        Conn.Open()
        Try
            Using cmd As New OleDbCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "delete from rekap_harian_shift"
                    .ExecuteNonQuery()

                    .CommandText = "update header_report set [page_header_1] = 'Rekap Parkir Tanggal " & _
                        dtTanggal.Text.Trim & " s/d " & dtAkhir.Text.Trim & "'" & _
                        ",[page_header_2] = '" & txtShift.Text.Trim & "'"
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

    Private Sub SetupReportPetugas()
        Dim Conn As OleDbConnection = KoneksiOleDB()
        Conn.Open()
        Try
            Using cmd As New OleDbCommand
                With cmd
                    .Connection = Conn

                    .CommandText = "delete from rekap_harian_petugas"
                    .ExecuteNonQuery()

                    .CommandText = "update header_report set [page_header_1] = 'Rekap Pendapatan Petugas Pos Tanggal " & _
                        dtTanggal.Text.Trim & " s/d " & dtAkhir.Text.Trim & "'" & _
                        ",[page_header_2] = '" & txtShift.Text.Trim & "'"
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


    Private Sub SaveDataLocal()
        Dim Conn As OleDbConnection = KoneksiOleDB()
        Conn.Open()
        Try
            Using cmd As New OleDbCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "insert into rekap_harian_shift ([shift],[jenis_kendaraan],[casual_in]," & _
                                   "[pass_in],[casual_out],[pass_out],[loss_out],[cash_in],[cash_out]," & _
                                   "[voucher],[valet],[denda],[discount]) values ('" & _
                                   cShift.Trim & "','" & _
                                   cJenisKendaraan.Trim & "'," & _
                                   Val(nCasual_In) & "," & _
                                   Val(nPass_In) & "," & _
                                   Val(nCasual_Out) & "," & _
                                   Val(nPass_Out) & "," & _
                                   Val(nLost_Out) & "," & _
                                   Val(nCash_In) & "," & _
                                   Val(nCash_Out) & "," & _
                                   Val(nVoucer) & "," & _
                                   Val(nValet) & "," & _
                                   Val(nDenda) & "," & _
                                   Val(nDiscount) & ")"
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

    Private Sub SavePendapatanPetugas()
        Dim Conn As OleDbConnection = KoneksiOleDB()
        Conn.Open()
        Try
            Using cmd As New OleDbCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "insert into rekap_harian_petugas ([masuk_keluar],[petugas],[qty_in]," & _
                                   "[qty_out],[qty_loss],[values_loss],[cash],[voucher],[valet]," & _
                                   "[denda],[discount]) values ('" & _
                                   cJenisGerbang.Trim & "','" & _
                                   cPetugas.Trim & "'," & _
                                   Val(nQty_In) & "," & _
                                   Val(nQty_Out) & "," & _
                                   Val(nQty_Loss) & "," & _
                                   Val(nValue_Loss) & "," & _
                                   Val(nCash) & "," & _
                                   Val(nVoucer) & "," & _
                                   Val(nValet) & "," & _
                                   Val(nDenda) & "," & _
                                   Val(nDiscount) & ")"
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

    Private Sub btnCetak_Click(sender As Object, e As EventArgs) Handles btnCetak.Click
        Dim xForm As New DevExpress.Utils.WaitDialogForm
        xForm.LookAndFeel.UseDefaultLookAndFeel = False
        xForm.LookAndFeel.SkinName = "Blue"

        If cboJenisLaporan.Text = "Rekap Periodik" Then
            frmLaporan.LoadReportSQL2Parameter("rekap_periodik.rpt", "{parkir.tgl keluar} In Date(" &
                                    dtTanggal.DateTime.Year & ", " &
                                    dtTanggal.DateTime.Month & ", " &
                                    dtTanggal.DateTime.Day & ") To Date (" &
                                    dtAkhir.DateTime.Year & "," &
                                    dtAkhir.DateTime.Month & "," &
                                    dtAkhir.DateTime.Day & ") and {parkir.status} = 'Y'",
                                    Date.Parse(dtTanggal.DateTime),
                                    Date.Parse(dtAkhir.DateTime))
            frmLaporan.MdiParent = frmMenuUtama
        ElseIf cboJenisLaporan.Text = "Rekap Periodik Petugas" Then
            frmLaporan.LoadReportSQL3Parameter("rekap_parkir_petugas.rpt", "{parkir.tgl keluar} In Date(" &
                                     dtTanggal.DateTime.Year & ", " &
                                     dtTanggal.DateTime.Month & ", " &
                                     dtTanggal.DateTime.Day & ") To Date (" &
                                     dtAkhir.DateTime.Year & "," &
                                     dtAkhir.DateTime.Month & "," &
                                     dtAkhir.DateTime.Day & ") and {parkir.petugas keluar} = '" &
                                     cboUser.Text.Trim & "' and {parkir.status} = 'Y'",
                                     cboUser.Text.Trim,
                                     Date.Parse(dtTanggal.DateTime),
                                     Date.Parse(dtAkhir.DateTime))
            frmLaporan.MdiParent = frmMenuUtama
        ElseIf cboJenisLaporan.Text = "Rekap Periodik Jenis Kendaraan" Then
            frmLaporan.LoadReportSQL3Parameter("rekap_parkir_jenis_kendaraan.rpt", "{parkir.tgl keluar} In Date(" &
                                     dtTanggal.DateTime.Year & ", " &
                                     dtTanggal.DateTime.Month & ", " &
                                     dtTanggal.DateTime.Day & ") To Date (" &
                                     dtAkhir.DateTime.Year & "," &
                                     dtAkhir.DateTime.Month & "," &
                                     dtAkhir.DateTime.Day & ") and {parkir.kd jenis} = '" &
                                     cboJenisKendaraan.Text.Trim & "' and {parkir.status} = 'Y'",
                                     txtJenisKendaraan.Text.Trim,
                                     Date.Parse(dtTanggal.DateTime),
                                     Date.Parse(dtAkhir.DateTime))
            frmLaporan.MdiParent = frmMenuUtama
        ElseIf cboJenisLaporan.Text = "Rekap Periodik Jenis Kendaraan Petugas" Then
            frmLaporan.LoadReportSQL3Parameter("rekap_parkir_petugas.rpt", "{parkir.tgl keluar} In Date(" &
                                    dtTanggal.DateTime.Year & ", " &
                                    dtTanggal.DateTime.Month & ", " &
                                    dtTanggal.DateTime.Day & ") To Date (" &
                                    dtAkhir.DateTime.Year & "," &
                                    dtAkhir.DateTime.Month & "," &
                                    dtAkhir.DateTime.Day & ") and {parkir.kd jenis} = '" &
                                    cboJenisKendaraan.Text.Trim & "' and {parkir.status} = 'Y' " &
                                    "And {parkir.petugas keluar} = '" &
                                    cboUser.Text.Trim & "'",
                                    cboUser.Text.Trim,
                                    Date.Parse(dtTanggal.DateTime),
                                    Date.Parse(dtAkhir.DateTime))
            frmLaporan.MdiParent = frmMenuUtama
        End If

        xForm.Dispose()
        frmLaporan.Show()
        frmLaporan.Focus()
    End Sub

    Private Sub frmDailyReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDataShift()
        With cboJenisLaporan
            .Properties.Items.Add("Rekap Periodik")
            .Properties.Items.Add("Rekap Periodik Petugas")
            .Properties.Items.Add("Rekap Periodik Jenis Kendaraan")
            .Properties.Items.Add("Rekap Periodik Jenis Kendaraan Petugas")
        End With
        dtTanggal.DateTime = Now.Date
        dtAkhir.DateTime = Now.Date

        cboUser.Properties.ReadOnly = True
        cboKdShift.Properties.ReadOnly = True
        cboJenisKendaraan.Properties.ReadOnly = True
        btnLoad.Enabled = False
    End Sub

    Private Sub LoadDataShift()
        cboKdShift.Properties.Items.Clear()
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "Select [kd shift] from shift order by [kd shift] asc"

                    Dim rdr As SqlDataReader = .ExecuteReader

                    While rdr.Read
                        cboKdShift.Properties.Items.Add(rdr.Item(0).ToString.Trim)
                    End While
                    cboKdShift.Properties.Items.Add("All")
                    rdr.Close()

                    .CommandText = "Select [kd petugas] from petugas order by [kd petugas]"
                    rdr = .ExecuteReader
                    While rdr.Read
                        cboUser.Properties.Items.Add(rdr.Item(0).ToString.Trim)
                    End While
                    rdr.Close()

                    .CommandText = "Select [kd jenis] from jenis_kendaraan order by [kd jenis] asc"
                    rdr = .ExecuteReader
                    While rdr.Read
                        cboJenisKendaraan.Properties.Items.Add(rdr.Item(0).ToString.Trim)
                    End While
                    rdr.Close()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Conn.Close()
    End Sub

    Private Sub cboKdShift_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboKdShift.SelectedIndexChanged
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "Select [keterangan],[jam mulai],[jam akhir] from shift where [kd shift] = '" &
                        cboKdShift.Text.Trim & "'"

                    Dim rdr As SqlDataReader = .ExecuteReader

                    While rdr.Read
                        txtShift.Text = rdr.Item(0).ToString.Trim & " -> " &
                         rdr.Item(1).ToString.Trim & " s/d " &
                         rdr.Item(2).ToString.Trim
                    End While

                    If cboKdShift.Text = "All" Then
                        txtShift.Text = "All -> 07:00:00 s/d 06:00:00"
                    End If

                    rdr.Close()

                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
        Conn.Close()
    End Sub

    Private Sub CboJenisKendaraan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboJenisKendaraan.SelectedIndexChanged
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "select [keterangan] from jenis_kendaraan where [kd jenis] = '" & cboJenisKendaraan.Text.Trim & "'"
                    Dim rdr As SqlDataReader = .ExecuteReader
                    While rdr.Read
                        txtJenisKendaraan.Text = rdr.Item(0).ToString.Trim
                    End While
                    rdr.Close()

                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
        End Try
    End Sub

    Private Sub CboJenisLaporan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboJenisLaporan.SelectedIndexChanged
        If cboJenisLaporan.Text = "Rekap Periodik" Then
            cboUser.Properties.ReadOnly = True
            cboKdShift.Properties.ReadOnly = True
            cboJenisKendaraan.Properties.ReadOnly = True
        ElseIf cboJenisLaporan.Text = "Rekap Periodik Petugas" Then
            cboUser.Properties.ReadOnly = False
            cboKdShift.Properties.ReadOnly = True
            cboJenisKendaraan.Properties.ReadOnly = True
        ElseIf cboJenisLaporan.Text = "Rekap Periodik Jenis Kendaraan" Then
            cboUser.Properties.ReadOnly = True
            cboKdShift.Properties.ReadOnly = True
            cboJenisKendaraan.Properties.ReadOnly = False
        ElseIf cboJenisLaporan.Text = "Rekap Periodik Jenis Kendaraan Petugas" Then
            cboUser.Properties.ReadOnly = False
            cboKdShift.Properties.ReadOnly = True
            cboJenisKendaraan.Properties.ReadOnly = False
        End If
    End Sub
End Class