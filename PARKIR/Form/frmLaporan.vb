Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms

Public Class frmLaporan
    Public Function LoadReportLocal(ByVal cReportName As String, ByVal cFormula As String, ByVal cDBName As String) As Boolean
        Dim cryRpt As New ReportDocument
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        Dim CrTable As Table

        cryRpt.Load("" & Application.StartupPath & "\Laporan\" & cReportName)

        With crConnectionInfo
            .Type = ConnectionInfoType.DBFile
            .ServerName = Application.StartupPath & "\" & Trim(cDBName)
            .DatabaseName = Application.StartupPath & "\" & Trim(cDBName)
            .UserID = ""
            .Password = ""
        End With

        CrTables = cryRpt.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        crViewer.ReportSource = cryRpt
        If cFormula.Trim <> "0" Then
            crViewer.SelectionFormula = cFormula.Trim
        End If
        crViewer.Refresh()
        Return True
    End Function

    Public Function LoadReportSQL(ByVal cReportName As String, ByVal cFormula As String) As Boolean
        Dim cryRpt As New ReportDocument
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo

        Dim CrTables As Tables
        Dim CrTable As Table

        cryRpt.Load("" & Application.StartupPath & "\Laporan\" & cReportName)
        If cFormula.Trim <> "0" Then
            cryRpt.RecordSelectionFormula = cFormula.Trim
        End If

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

        crViewer.ReportSource = cryRpt
        If cFormula.Trim <> "0" Then
            crViewer.SelectionFormula = cFormula.Trim
        End If
        crViewer.RefreshReport()
        Return True
    End Function

    Public Function LoadReportSQL2Parameter(ByVal cReportName As String, ByVal cFormula As String, dtAwal As Date, dtAkhir As Date) As Boolean
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

    Public Function LoadReportSQL3Parameter(ByVal cReportName As String, ByVal cFormula As String, cString As String, dtAwal As Date, dtAkhir As Date) As Boolean
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
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Me.Dispose()
    End Sub

    Private Sub FrmLaporan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class