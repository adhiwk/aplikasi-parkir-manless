Public Class frmMenuUtama
    Friend cUserId As String
    Private Sub btnParkirMasuk_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnParkirMasuk.ItemClick
        If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "parkir_masuk", "add") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "error")
            Exit Sub
        End If
        frmManLessIn.ShowDialog()
        frmManLessIn.BringToFront()
    End Sub

    Private Sub btnParkirKeluar_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnParkirKeluar.ItemClick
        If Not RolesBasedAccessControl(frmLogin.txtUser.Text.Trim, "parkir_keluar", "add") Then
            MsgBox("anda tidak memilik akses untuk melakukan proses ini", MsgBoxStyle.Critical, "error")
            Exit Sub
        End If
        frmManLessOut.ShowDialog()
        frmManLessOut.BringToFront()
    End Sub


    Private Sub btnMember_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnMember.ItemClick
        frmMember.cKodePetugas = cUserId.Trim
        frmMember.MdiParent = Me
        frmMember.Show()
        frmMember.Focus()
    End Sub

    Private Sub frmMenuUtama_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        'DestroySession(cUserId.Trim)
        frmLogin.txtUser.Text = ""
        frmLogin.txtPassword.Text = ""
        frmLogin.Show()
    End Sub


    Private Sub btnManageSesion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnManageSesion.ItemClick
        frmClearSesion.ShowDialog()
        frmClearSesion.Focus()
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        frmManajemenUser.MdiParent = Me
        frmManajemenUser.Show()
        frmManajemenUser.Focus()
    End Sub

    Private Sub btnJenisKendaraan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnJenisKendaraan.ItemClick
        frmJenisKendaraan.MdiParent = Me
        frmJenisKendaraan.Show()
        frmJenisKendaraan.Focus()
    End Sub

    Private Sub BarButtonItem5_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem5.ItemClick
        frmTarif.MdiParent = Me
        frmTarif.Show()
        frmTarif.Focus()
    End Sub

    Private Sub btnAturShift_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnAturShift.ItemClick
        frmShift.MdiParent = Me
        frmShift.Show()
        frmShift.Focus()
    End Sub

    Private Sub btnRekapHarian_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnRekapHarian.ItemClick
        frmDailyReport.MdiParent = Me
        frmDailyReport.Show()
        frmDailyReport.Focus()
    End Sub

    Private Sub frmMenuUtama_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub RibbonControl_Click(sender As Object, e As EventArgs) Handles RibbonControl.Click

    End Sub
End Class