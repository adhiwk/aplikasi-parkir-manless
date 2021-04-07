<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMenuUtama
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMenuUtama))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.btnParkirMasuk = New DevExpress.XtraBars.BarButtonItem()
        Me.btnParkirKeluar = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.btnMember = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem5 = New DevExpress.XtraBars.BarButtonItem()
        Me.btnManageSesion = New DevExpress.XtraBars.BarButtonItem()
        Me.btnJenisKendaraan = New DevExpress.XtraBars.BarButtonItem()
        Me.btnAturShift = New DevExpress.XtraBars.BarButtonItem()
        Me.btnRekapHarian = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPage2 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup4 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.DefaultLookAndFeel1 = New DevExpress.LookAndFeel.DefaultLookAndFeel(Me.components)
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.btnParkirMasuk, Me.btnParkirKeluar, Me.BarButtonItem3, Me.btnMember, Me.BarButtonItem5, Me.btnManageSesion, Me.btnJenisKendaraan, Me.btnAturShift, Me.btnRekapHarian})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 12
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1, Me.RibbonPage2})
        Me.RibbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.MacOffice
        Me.RibbonControl.Size = New System.Drawing.Size(677, 133)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'btnParkirMasuk
        '
        Me.btnParkirMasuk.Caption = "PARKIR MASUK"
        Me.btnParkirMasuk.Id = 3
        Me.btnParkirMasuk.ImageOptions.Image = Global.PARKIR.My.Resources.Resources.arrow_down_blue
        Me.btnParkirMasuk.Name = "btnParkirMasuk"
        Me.btnParkirMasuk.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.btnParkirMasuk.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'btnParkirKeluar
        '
        Me.btnParkirKeluar.Caption = "PARKIR KELUAR"
        Me.btnParkirKeluar.Id = 4
        Me.btnParkirKeluar.ImageOptions.Image = Global.PARKIR.My.Resources.Resources.arrow_up_green
        Me.btnParkirKeluar.Name = "btnParkirKeluar"
        Me.btnParkirKeluar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.btnParkirKeluar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "PETUGAS"
        Me.BarButtonItem3.Id = 5
        Me.BarButtonItem3.ImageOptions.Image = Global.PARKIR.My.Resources.Resources.user2
        Me.BarButtonItem3.Name = "BarButtonItem3"
        Me.BarButtonItem3.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.BarButtonItem3.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'btnMember
        '
        Me.btnMember.Caption = "MEMBER"
        Me.btnMember.Id = 6
        Me.btnMember.ImageOptions.Image = Global.PARKIR.My.Resources.Resources.users_family
        Me.btnMember.Name = "btnMember"
        Me.btnMember.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.btnMember.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'BarButtonItem5
        '
        Me.BarButtonItem5.Caption = "TARIF"
        Me.BarButtonItem5.Id = 7
        Me.BarButtonItem5.ImageOptions.Image = Global.PARKIR.My.Resources.Resources.money_envelope
        Me.BarButtonItem5.Name = "BarButtonItem5"
        Me.BarButtonItem5.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.BarButtonItem5.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'btnManageSesion
        '
        Me.btnManageSesion.Caption = "MANAGE SESSION"
        Me.btnManageSesion.Id = 8
        Me.btnManageSesion.ImageOptions.Image = Global.PARKIR.My.Resources.Resources.cookies
        Me.btnManageSesion.Name = "btnManageSesion"
        Me.btnManageSesion.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'btnJenisKendaraan
        '
        Me.btnJenisKendaraan.Caption = "JENIS KENDARAAN"
        Me.btnJenisKendaraan.Id = 9
        Me.btnJenisKendaraan.ImageOptions.Image = Global.PARKIR.My.Resources.Resources.document_ok
        Me.btnJenisKendaraan.Name = "btnJenisKendaraan"
        Me.btnJenisKendaraan.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'btnAturShift
        '
        Me.btnAturShift.Caption = "PENGATURAN SHIFT"
        Me.btnAturShift.Id = 10
        Me.btnAturShift.ImageOptions.Image = Global.PARKIR.My.Resources.Resources.gears
        Me.btnAturShift.Name = "btnAturShift"
        Me.btnAturShift.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'btnRekapHarian
        '
        Me.btnRekapHarian.Caption = "REKAP HARIAN"
        Me.btnRekapHarian.Id = 11
        Me.btnRekapHarian.ImageOptions.Image = Global.PARKIR.My.Resources.Resources.text_view
        Me.btnRekapHarian.Name = "btnRekapHarian"
        Me.btnRekapHarian.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "FRONT OFFICE"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnParkirMasuk)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnParkirKeluar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        Me.RibbonPageGroup1.Text = "TRANSAKSI"
        '
        'RibbonPage2
        '
        Me.RibbonPage2.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup2, Me.RibbonPageGroup3, Me.RibbonPageGroup4})
        Me.RibbonPage2.Name = "RibbonPage2"
        Me.RibbonPage2.Text = "BACK OFFICE"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.BarButtonItem3)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.btnMember)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.BarButtonItem5)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.btnJenisKendaraan)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.btnAturShift)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        Me.RibbonPageGroup2.Text = "SETTING"
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.ItemLinks.Add(Me.btnManageSesion)
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        Me.RibbonPageGroup3.Text = "TOOLS"
        '
        'RibbonPageGroup4
        '
        Me.RibbonPageGroup4.ItemLinks.Add(Me.btnRekapHarian)
        Me.RibbonPageGroup4.Name = "RibbonPageGroup4"
        Me.RibbonPageGroup4.Text = "LAPORAN"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 428)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(677, 21)
        '
        'DefaultLookAndFeel1
        '
        Me.DefaultLookAndFeel1.LookAndFeel.SkinName = "Office 2016 Colorful"
        '
        'frmMenuUtama
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(677, 449)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Name = "frmMenuUtama"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "KURA-KURA WATERPARK"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents btnParkirMasuk As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnParkirKeluar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage2 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnMember As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem5 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnManageSesion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents btnJenisKendaraan As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnAturShift As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnRekapHarian As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup4 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents DefaultLookAndFeel1 As DevExpress.LookAndFeel.DefaultLookAndFeel

End Class
