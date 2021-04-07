Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class ctrlReport
    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles btnCetak.Click
        Dim xForm As New DevExpress.Utils.WaitDialogForm
        xForm.LookAndFeel.UseDefaultLookAndFeel = False
        xForm.LookAndFeel.SkinName = "Blue"

        If cboJenisLaporan.Text = "Rekap Periodik" Then

            LoadReportSQL2Parameter("rekap_periodik.rpt", "{parkir.tgl keluar} In Datetime (" &
                                    dtAwal.DateTime.Year & ", " &
                                    dtAwal.DateTime.Month & ", " &
                                    dtAwal.DateTime.Day & "," &
                                    JamAwal.Time.Hour & "," &
                                    JamAwal.Time.Minute & "," &
                                    JamAwal.Time.Second & ") To Datetime (" &
                                    dtAkhir.DateTime.Year & "," &
                                    dtAkhir.DateTime.Month & "," &
                                    dtAkhir.DateTime.Day & "," &
                                    JamAkhir.Time.Hour & "," &
                                    JamAkhir.Time.Minute & "," &
                                    JamAkhir.Time.Second & ") and {parkir.status} = 'Y'",
                                    Date.Parse(dtAwal.DateTime),
                                    Date.Parse(dtAkhir.DateTime))

        ElseIf cboJenisLaporan.Text = "Rekap Periodik Petugas" Then
            LoadReportSQL3Parameter("rekap_parkir_petugas.rpt", "{parkir.tgl keluar} In Datetime (" &
                                    dtAwal.DateTime.Year & ", " &
                                    dtAwal.DateTime.Month & ", " &
                                    dtAwal.DateTime.Day & "," &
                                    JamAwal.Time.Hour & "," &
                                    JamAwal.Time.Minute & "," &
                                    JamAwal.Time.Second & ") To Datetime (" &
                                    dtAkhir.DateTime.Year & "," &
                                    dtAkhir.DateTime.Month & "," &
                                    dtAkhir.DateTime.Day & "," &
                                    JamAkhir.Time.Hour & "," &
                                    JamAkhir.Time.Minute & "," &
                                    JamAkhir.Time.Second & ") and {parkir.petugas keluar} = '" &
                                     cboUser.Text.Trim & "' and {parkir.status} = 'Y'",
                                     cboUser.Text.Trim,
                                     Date.Parse(dtAwal.DateTime),
                                     Date.Parse(dtAkhir.DateTime))

        ElseIf cboJenisLaporan.Text = "Rekap Periodik Jenis Kendaraan" Then
            LoadReportSQL3Parameter("rekap_parkir_jenis_kendaraan.rpt", "{parkir.tgl keluar} In Datetime (" &
                                    dtAwal.DateTime.Year & ", " &
                                    dtAwal.DateTime.Month & ", " &
                                    dtAwal.DateTime.Day & "," &
                                    JamAwal.Time.Hour & "," &
                                    JamAwal.Time.Minute & "," &
                                    JamAwal.Time.Second & ") To Datetime (" &
                                    dtAkhir.DateTime.Year & "," &
                                    dtAkhir.DateTime.Month & "," &
                                    dtAkhir.DateTime.Day & "," &
                                    JamAkhir.Time.Hour & "," &
                                    JamAkhir.Time.Minute & "," &
                                    JamAkhir.Time.Second & ") and {parkir.kd jenis} = '" &
                                     cboJenisKendaraan.Text.Trim & "' and {parkir.status} = 'Y'",
                                     txtJenisKendaraan.Text.Trim,
                                     Date.Parse(dtAwal.DateTime),
                                     Date.Parse(dtAkhir.DateTime))

        ElseIf cboJenisLaporan.Text = "Rekap Periodik Jenis Kendaraan Petugas" Then
            LoadReportSQL3Parameter("rekap_parkir_petugas.rpt", "{parkir.tgl keluar} In Datetime (" &
                                    dtAwal.DateTime.Year & ", " &
                                    dtAwal.DateTime.Month & ", " &
                                    dtAwal.DateTime.Day & "," &
                                    JamAwal.Time.Hour & "," &
                                    JamAwal.Time.Minute & "," &
                                    JamAwal.Time.Second & ") To Datetime (" &
                                    dtAkhir.DateTime.Year & "," &
                                    dtAkhir.DateTime.Month & "," &
                                    dtAkhir.DateTime.Day & "," &
                                    JamAkhir.Time.Hour & "," &
                                    JamAkhir.Time.Minute & "," &
                                    JamAkhir.Time.Second & ") and {parkir.kd jenis} = '" &
                                    cboJenisKendaraan.Text.Trim & "' and {parkir.status} = 'Y' " &
                                    "And {parkir.petugas keluar} = '" &
                                    cboUser.Text.Trim & "'",
                                    cboUser.Text.Trim,
                                    Date.Parse(dtAwal.DateTime),
                                    Date.Parse(dtAkhir.DateTime))
        ElseIf cboJenisLaporan.Text = "Rekap Periodik Khusus Member" Then
            LoadReportSQL2Parameter("rekap_periodik_member.rpt", "{parkir.tgl keluar} In Datetime (" &
                                        dtAwal.DateTime.Year & ", " &
                                        dtAwal.DateTime.Month & ", " &
                                        dtAwal.DateTime.Day & "," &
                                        JamAwal.Time.Hour & "," &
                                        JamAwal.Time.Minute & "," &
                                        JamAwal.Time.Second & ") To Datetime (" &
                                        dtAkhir.DateTime.Year & "," &
                                        dtAkhir.DateTime.Month & "," &
                                        dtAkhir.DateTime.Day & "," &
                                        JamAkhir.Time.Hour & "," &
                                        JamAkhir.Time.Minute & "," &
                                        JamAkhir.Time.Second & ") and {parkir.status} = 'Y' and " &
                                        "{parkir.jenis} = 'P'",
                                        Date.Parse(dtAwal.DateTime),
                                        Date.Parse(dtAkhir.DateTime))
        ElseIf cboJenisLaporan.Text = "Rekap Periodik Pendapatan Denda" Then
            LoadReportSQL2Parameter("rekap_periodik_denda.rpt", "{parkir_denda.tgl} In Datetime (" &
                                       dtAwal.DateTime.Year & ", " &
                                       dtAwal.DateTime.Month & ", " &
                                       dtAwal.DateTime.Day & "," &
                                       JamAwal.Time.Hour & "," &
                                       JamAwal.Time.Minute & "," &
                                       JamAwal.Time.Second & ") To Datetime (" &
                                       dtAkhir.DateTime.Year & "," &
                                       dtAkhir.DateTime.Month & "," &
                                       dtAkhir.DateTime.Day & "," &
                                       JamAkhir.Time.Hour & "," &
                                       JamAkhir.Time.Minute & "," &
                                       JamAkhir.Time.Second & ")",
                                       Date.Parse(dtAwal.DateTime),
                                       Date.Parse(dtAkhir.DateTime))
        End If
        xForm.Dispose()
    End Sub

    Private Sub ctrlReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Dock = DockStyle.Fill

        With cboJenisLaporan
            .Properties.Items.Add("Rekap Periodik")
            .Properties.Items.Add("Rekap Periodik Petugas")
            .Properties.Items.Add("Rekap Periodik Jenis Kendaraan")
            .Properties.Items.Add("Rekap Periodik Jenis Kendaraan Petugas")
            .Properties.Items.Add("Rekap Periodik Pendapatan Denda")
            .Properties.Items.Add("Rekap Periodik Khusus Member")
        End With

        dtAwal.DateTime = Now.Date
        dtAkhir.DateTime = Now.Date

        LoadData()
    End Sub

    Private Sub LoadData()

        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn

                    .CommandText = "Select [kd petugas] from petugas order by [kd petugas]"
                    Dim rdr As SqlDataReader = .ExecuteReader
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
        End Try
        Conn.Close()
    End Sub

    'report section
    'Private Function LoadReportLocal(ByVal cReportName As String, ByVal cFormula As String, ByVal cDBName As String) As Boolean
    '    Dim cryRpt As New ReportDocument
    '    Dim crtableLogoninfos As New TableLogOnInfos
    '    Dim crtableLogoninfo As New TableLogOnInfo
    '    Dim crConnectionInfo As New ConnectionInfo
    '    Dim CrTables As Tables
    '    Dim CrTable As Table

    '    cryRpt.Load("" & Application.StartupPath & "\Laporan\" & cReportName)

    '    With crConnectionInfo
    '        .Type = ConnectionInfoType.DBFile
    '        .ServerName = Application.StartupPath & "\" & Trim(cDBName)
    '        .DatabaseName = Application.StartupPath & "\" & Trim(cDBName)
    '        .UserID = ""
    '        .Password = ""
    '    End With

    '    CrTables = cryRpt.Database.Tables
    '    For Each CrTable In CrTables
    '        crtableLogoninfo = CrTable.LogOnInfo
    '        crtableLogoninfo.ConnectionInfo = crConnectionInfo
    '        CrTable.ApplyLogOnInfo(crtableLogoninfo)
    '    Next

    '    crViewer.ReportSource = cryRpt
    '    If cFormula.Trim <> "0" Then
    '        crViewer.SelectionFormula = cFormula.Trim
    '    End If
    '    crViewer.Refresh()
    '    Return True
    'End Function

    'Private Function LoadReportSQL(ByVal cReportName As String, ByVal cFormula As String) As Boolean
    '    Dim cryRpt As New ReportDocument
    '    Dim crtableLogoninfos As New TableLogOnInfos
    '    Dim crtableLogoninfo As New TableLogOnInfo
    '    Dim crConnectionInfo As New ConnectionInfo

    '    Dim CrTables As Tables
    '    Dim CrTable As Table

    '    cryRpt.Load("" & Application.StartupPath & "\Laporan\" & cReportName)
    '    If cFormula.Trim <> "0" Then
    '        cryRpt.RecordSelectionFormula = cFormula.Trim
    '    End If

    '    With crConnectionInfo
    '        .ServerName = My.Settings.server.Trim
    '        .DatabaseName = Simple3Des.Decrypt(My.Settings.dbname.Trim)
    '        .UserID = Simple3Des.Decrypt(My.Settings.user.Trim)
    '        .Password = Simple3Des.Decrypt(My.Settings.password.Trim)
    '    End With


    '    CrTables = cryRpt.Database.Tables
    '    For Each CrTable In CrTables
    '        crtableLogoninfo = CrTable.LogOnInfo
    '        crtableLogoninfo.ConnectionInfo = crConnectionInfo
    '        CrTable.ApplyLogOnInfo(crtableLogoninfo)
    '    Next

    '    crViewer.ReportSource = cryRpt
    '    If cFormula.Trim <> "0" Then
    '        crViewer.SelectionFormula = cFormula.Trim
    '    End If
    '    crViewer.RefreshReport()
    '    Return True
    'End Function

    Private Function LoadReportSQL2Parameter(ByVal cReportName As String, ByVal cFormula As String, dtAwal As Date, dtAkhir As Date) As Boolean
        Dim cryRpt As New ReportDocument
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        Dim CrTable As Table

        cryRpt.Load(String.Format("{0}\laporan\{1}", Application.StartupPath, cReportName))

        With crConnectionInfo
            .ServerName = My.Settings.server.Trim
            .DatabaseName = Simple3Des.Decrypt(My.Settings.dbname.Trim)
            .UserID = Simple3Des.Decrypt(My.Settings.user.Trim)
            .Password = Simple3Des.Decrypt(My.Settings.password.Trim)
        End With

        CrTables = cryRpt.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        cryRpt.SetParameterValue("dtAwal", Date.Parse(dtAwal))
        cryRpt.SetParameterValue("dtAkhir", Date.Parse(dtAkhir))

        crViewer.ReportSource = cryRpt
        If cFormula.Trim <> "0" Then
            crViewer.SelectionFormula = cFormula.Trim
        End If
        crViewer.Refresh()
        Return True
    End Function

    Private Function LoadReportSQL3Parameter(ByVal cReportName As String, ByVal cFormula As String, cString As String, dtAwal As Date, dtAkhir As Date) As Boolean
        Dim cryRpt As New ReportDocument
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        Dim CrTable As Table

        cryRpt.Load(String.Format("{0}\laporan\{1}", Application.StartupPath, cReportName))

        With crConnectionInfo
            .ServerName = My.Settings.server.Trim
            .DatabaseName = Simple3Des.Decrypt(My.Settings.dbname.Trim)
            .UserID = Simple3Des.Decrypt(My.Settings.user.Trim)
            .Password = Simple3Des.Decrypt(My.Settings.password.Trim)
        End With

        CrTables = cryRpt.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        cryRpt.SetParameterValue("cUser", cString.Trim)
        cryRpt.SetParameterValue("dtAwal", Date.Parse(dtAwal))
        cryRpt.SetParameterValue("dtAkhir", Date.Parse(dtAkhir))

        crViewer.ReportSource = cryRpt
        If cFormula.Trim <> "0" Then
            crViewer.SelectionFormula = cFormula.Trim
        End If
        crViewer.Refresh()
        Return True
    End Function
End Class
