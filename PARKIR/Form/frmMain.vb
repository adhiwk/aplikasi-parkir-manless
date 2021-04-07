Public Class frmMain
    Friend cUserId As String
    Dim cctrlMember As New ctrlMember
    Dim cctrlJenisKendaraan As New ctrlJenisKendaraan
    Dim cctrlTarif As New ctrlTarif
    Dim cctrlUsers As New ctrlUsers
    Dim cctrlReport As New ctrlReport
    Dim cctrlShiftKerja As New ctrlShiftKerja
    Dim cctrlImageCaptureList As New ctrlImageCaptureList
    Private Sub BtnMember_Click(sender As Object, e As EventArgs) Handles btnMember.Click
        If CheckControl("ctrlMember") Then
            tmPanel.StartTransition(pnlApps)
            cctrlMember.Show()
            cctrlMember.BringToFront()
            tmPanel.EndTransition()
        Else
            tmPanel.StartTransition(pnlApps)
            pnlApps.Controls.Add(cctrlMember)
            cctrlMember.BringToFront()
            tmPanel.EndTransition()
        End If
        lblControl.Text = "ENTRY DATA MEMBER"
    End Sub

    Private Function CheckControl(ByVal cControlName As String) As Boolean
        Dim lFound As Boolean = False
        For Each ctrl As UserControl In pnlApps.Controls
            If ctrl.Name = cControlName.Trim Then
                lFound = True
            Else
                lFound = False
            End If
        Next
        Return lFound
    End Function

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        frmLogin.Show()
        Me.Dispose()
    End Sub

    Private Sub BtnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Me.Dispose()
    End Sub

    Private Sub frmMain_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        frmLogin.txtUser.Text = ""
        frmLogin.txtPassword.Text = ""
        frmLogin.Show()
    End Sub

    Private Sub BtnJenisKendaraan_Click(sender As Object, e As EventArgs) Handles btnJenisKendaraan.Click
        If CheckControl("ctrlJenisKendaraan") Then
            tmPanel.StartTransition(pnlApps)
            cctrlJenisKendaraan.Show()
            cctrlJenisKendaraan.BringToFront()
            tmPanel.EndTransition()
        Else
            tmPanel.StartTransition(pnlApps)
            pnlApps.Controls.Add(cctrlJenisKendaraan)
            cctrlJenisKendaraan.BringToFront()
            tmPanel.EndTransition()
        End If
        lblControl.Text = "ENTRY JENIS KENDARAAN"
    End Sub

    Private Sub BtnTarif_Click(sender As Object, e As EventArgs) Handles btnTarif.Click

        If CheckControl("ctrlTarif") Then
            tmPanel.StartTransition(pnlApps)
            cctrlTarif.Show()
            cctrlTarif.BringToFront()
            tmPanel.EndTransition()
        Else
            tmPanel.StartTransition(pnlApps)
            pnlApps.Controls.Add(cctrlTarif)
            cctrlTarif.BringToFront()
            tmPanel.EndTransition()
        End If
        lblControl.Text = "PENGATURAN TARIF PARKIR"
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub BtnEntryUser_Click(sender As Object, e As EventArgs) Handles btnEntryUser.Click
        If CheckControl("ctrlUsers") Then
            tmPanel.StartTransition(pnlApps)
            cctrlUsers.Show()
            cctrlUsers.BringToFront()
            tmPanel.EndTransition()
        Else
            tmPanel.StartTransition(pnlApps)
            pnlApps.Controls.Add(cctrlUsers)
            cctrlUsers.BringToFront()
            tmPanel.EndTransition()
        End If
        lblControl.Text = "ENTRY USERS"
    End Sub

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        If CheckControl("ctrlReport") Then
            tmPanel.StartTransition(pnlApps)
            cctrlReport.Show()
            cctrlReport.BringToFront()
            tmPanel.EndTransition()
        Else
            tmPanel.StartTransition(pnlApps)
            pnlApps.Controls.Add(cctrlReport)
            cctrlReport.BringToFront()
            tmPanel.EndTransition()
        End If
        lblControl.Text = "CETAK LAPORAN"
    End Sub

    Private Sub BtnShift_Click(sender As Object, e As EventArgs) Handles btnShift.Click
        If CheckControl("ctrlShiftKerja") Then
            tmPanel.StartTransition(pnlApps)
            cctrlShiftKerja.Show()
            cctrlShiftKerja.BringToFront()
            tmPanel.EndTransition()
        Else
            tmPanel.StartTransition(pnlApps)
            pnlApps.Controls.Add(cctrlShiftKerja)
            cctrlShiftKerja.BringToFront()
            tmPanel.EndTransition()
        End If
        lblControl.Text = "CETAK LAPORAN"
    End Sub

    Private Sub btnCapture_Click(sender As Object, e As EventArgs) Handles btnCapture.Click
        If CheckControl("ctrlShiftKerja") Then
            tmPanel.StartTransition(pnlApps)
            cctrlImageCaptureList.Show()
            cctrlImageCaptureList.BringToFront()
            tmPanel.EndTransition()
        Else
            tmPanel.StartTransition(pnlApps)
            pnlApps.Controls.Add(cctrlImageCaptureList)
            cctrlImageCaptureList.BringToFront()
            tmPanel.EndTransition()
        End If
        lblControl.Text = "VIEW IMAGE CAPTURE"
    End Sub
End Class