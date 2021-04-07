<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctrlReport
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.btnCetak = New DevExpress.XtraEditors.SimpleButton()
        Me.txtJenisKendaraan = New DevExpress.XtraEditors.TextEdit()
        Me.cboJenisKendaraan = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cboUser = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.JamAkhir = New DevExpress.XtraEditors.TimeEdit()
        Me.dtAkhir = New DevExpress.XtraEditors.DateEdit()
        Me.JamAwal = New DevExpress.XtraEditors.TimeEdit()
        Me.dtAwal = New DevExpress.XtraEditors.DateEdit()
        Me.cboJenisLaporan = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem5 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.crViewer = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.txtJenisKendaraan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboJenisKendaraan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboUser.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.JamAkhir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtAkhir.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtAkhir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.JamAwal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtAwal.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtAwal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboJenisLaporan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.crViewer)
        Me.LayoutControl1.Controls.Add(Me.btnCetak)
        Me.LayoutControl1.Controls.Add(Me.txtJenisKendaraan)
        Me.LayoutControl1.Controls.Add(Me.cboJenisKendaraan)
        Me.LayoutControl1.Controls.Add(Me.cboUser)
        Me.LayoutControl1.Controls.Add(Me.JamAkhir)
        Me.LayoutControl1.Controls.Add(Me.dtAkhir)
        Me.LayoutControl1.Controls.Add(Me.JamAwal)
        Me.LayoutControl1.Controls.Add(Me.dtAwal)
        Me.LayoutControl1.Controls.Add(Me.cboJenisLaporan)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
        Me.LayoutControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(481, 329, 650, 400)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(961, 540)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'btnCetak
        '
        Me.btnCetak.ImageOptions.Image = Global.PARKIR.My.Resources.Resources.bill24
        Me.btnCetak.Location = New System.Drawing.Point(730, 35)
        Me.btnCetak.Name = "btnCetak"
        Me.btnCetak.Size = New System.Drawing.Size(92, 30)
        Me.btnCetak.StyleController = Me.LayoutControl1
        Me.btnCetak.TabIndex = 12
        Me.btnCetak.Text = "&Cetak"
        '
        'txtJenisKendaraan
        '
        Me.txtJenisKendaraan.Location = New System.Drawing.Point(472, 35)
        Me.txtJenisKendaraan.Name = "txtJenisKendaraan"
        Me.txtJenisKendaraan.Properties.ReadOnly = True
        Me.txtJenisKendaraan.Size = New System.Drawing.Size(248, 20)
        Me.txtJenisKendaraan.StyleController = Me.LayoutControl1
        Me.txtJenisKendaraan.TabIndex = 11
        '
        'cboJenisKendaraan
        '
        Me.cboJenisKendaraan.Location = New System.Drawing.Point(412, 35)
        Me.cboJenisKendaraan.Name = "cboJenisKendaraan"
        Me.cboJenisKendaraan.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboJenisKendaraan.Size = New System.Drawing.Size(50, 20)
        Me.cboJenisKendaraan.StyleController = Me.LayoutControl1
        Me.cboJenisKendaraan.TabIndex = 10
        '
        'cboUser
        '
        Me.cboUser.Location = New System.Drawing.Point(89, 35)
        Me.cboUser.Name = "cboUser"
        Me.cboUser.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboUser.Size = New System.Drawing.Size(229, 20)
        Me.cboUser.StyleController = Me.LayoutControl1
        Me.cboUser.TabIndex = 9
        '
        'JamAkhir
        '
        Me.JamAkhir.EditValue = New Date(2019, 9, 2, 0, 0, 0, 0)
        Me.JamAkhir.Location = New System.Drawing.Point(730, 5)
        Me.JamAkhir.Name = "JamAkhir"
        Me.JamAkhir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.JamAkhir.Size = New System.Drawing.Size(92, 20)
        Me.JamAkhir.StyleController = Me.LayoutControl1
        Me.JamAkhir.TabIndex = 8
        '
        'dtAkhir
        '
        Me.dtAkhir.EditValue = New Date(2019, 9, 30, 0, 0, 0, 0)
        Me.dtAkhir.EnterMoveNextControl = True
        Me.dtAkhir.Location = New System.Drawing.Point(639, 5)
        Me.dtAkhir.Name = "dtAkhir"
        Me.dtAkhir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtAkhir.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtAkhir.Size = New System.Drawing.Size(81, 20)
        Me.dtAkhir.StyleController = Me.LayoutControl1
        Me.dtAkhir.TabIndex = 7
        '
        'JamAwal
        '
        Me.JamAwal.EditValue = New Date(2019, 9, 2, 0, 0, 0, 0)
        Me.JamAwal.EnterMoveNextControl = True
        Me.JamAwal.Location = New System.Drawing.Point(472, 5)
        Me.JamAwal.Name = "JamAwal"
        Me.JamAwal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.JamAwal.Size = New System.Drawing.Size(96, 20)
        Me.JamAwal.StyleController = Me.LayoutControl1
        Me.JamAwal.TabIndex = 6
        '
        'dtAwal
        '
        Me.dtAwal.EditValue = New Date(2019, 9, 30, 0, 0, 0, 0)
        Me.dtAwal.EnterMoveNextControl = True
        Me.dtAwal.Location = New System.Drawing.Point(371, 5)
        Me.dtAwal.Name = "dtAwal"
        Me.dtAwal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtAwal.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtAwal.Size = New System.Drawing.Size(91, 20)
        Me.dtAwal.StyleController = Me.LayoutControl1
        Me.dtAwal.TabIndex = 5
        '
        'cboJenisLaporan
        '
        Me.cboJenisLaporan.EditValue = ""
        Me.cboJenisLaporan.EnterMoveNextControl = True
        Me.cboJenisLaporan.Location = New System.Drawing.Point(89, 5)
        Me.cboJenisLaporan.Name = "cboJenisLaporan"
        Me.cboJenisLaporan.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboJenisLaporan.Size = New System.Drawing.Size(229, 20)
        Me.cboJenisLaporan.StyleController = Me.LayoutControl1
        Me.cboJenisLaporan.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.EmptySpaceItem3, Me.LayoutControlItem7, Me.LayoutControlItem8, Me.EmptySpaceItem5, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.LayoutControlItem6, Me.LayoutControlItem9, Me.LayoutControlItem10})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.OptionsItemText.TextToControlDistance = 5
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(961, 540)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.cboJenisLaporan
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(323, 30)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(323, 30)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(323, 30)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Jenis Laporan"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(79, 13)
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.AllowHotTrack = False
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(827, 0)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(134, 30)
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.cboJenisKendaraan
        Me.LayoutControlItem7.Location = New System.Drawing.Point(323, 30)
        Me.LayoutControlItem7.MaxSize = New System.Drawing.Size(144, 40)
        Me.LayoutControlItem7.MinSize = New System.Drawing.Size(144, 40)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(144, 40)
        Me.LayoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem7.Text = "Jenis Kendaraan"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(79, 13)
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.txtJenisKendaraan
        Me.LayoutControlItem8.Location = New System.Drawing.Point(467, 30)
        Me.LayoutControlItem8.MaxSize = New System.Drawing.Size(258, 40)
        Me.LayoutControlItem8.MinSize = New System.Drawing.Size(258, 40)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(258, 40)
        Me.LayoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem8.TextVisible = False
        '
        'EmptySpaceItem5
        '
        Me.EmptySpaceItem5.AllowHotTrack = False
        Me.EmptySpaceItem5.Location = New System.Drawing.Point(827, 30)
        Me.EmptySpaceItem5.Name = "EmptySpaceItem5"
        Me.EmptySpaceItem5.Size = New System.Drawing.Size(134, 40)
        Me.EmptySpaceItem5.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.dtAwal
        Me.LayoutControlItem2.Location = New System.Drawing.Point(323, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(144, 30)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(144, 30)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(144, 30)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "Tanggal"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(38, 13)
        Me.LayoutControlItem2.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.JamAwal
        Me.LayoutControlItem3.Location = New System.Drawing.Point(467, 0)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(106, 30)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(106, 30)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(106, 30)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.dtAkhir
        Me.LayoutControlItem4.Location = New System.Drawing.Point(573, 0)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(152, 30)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(152, 30)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(152, 30)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.Text = "s/d Tanggal"
        Me.LayoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(56, 13)
        Me.LayoutControlItem4.TextToControlDistance = 5
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.JamAkhir
        Me.LayoutControlItem5.Location = New System.Drawing.Point(725, 0)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(102, 30)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(102, 30)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(102, 30)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.cboUser
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 30)
        Me.LayoutControlItem6.MaxSize = New System.Drawing.Size(323, 40)
        Me.LayoutControlItem6.MinSize = New System.Drawing.Size(323, 40)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(323, 40)
        Me.LayoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem6.Text = "User"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(79, 13)
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.btnCetak
        Me.LayoutControlItem9.Location = New System.Drawing.Point(725, 30)
        Me.LayoutControlItem9.MaxSize = New System.Drawing.Size(102, 40)
        Me.LayoutControlItem9.MinSize = New System.Drawing.Size(102, 40)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(102, 40)
        Me.LayoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem9.TextVisible = False
        '
        'crViewer
        '
        Me.crViewer.ActiveViewIndex = -1
        Me.crViewer.Cursor = System.Windows.Forms.Cursors.Default
        Me.crViewer.Location = New System.Drawing.Point(5, 75)
        Me.crViewer.Name = "crViewer"
        Me.crViewer.Size = New System.Drawing.Size(951, 460)
        Me.crViewer.TabIndex = 13
        Me.crViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.crViewer
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 70)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(961, 470)
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem10.TextVisible = False
        '
        'ctrlReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "ctrlReport"
        Me.Size = New System.Drawing.Size(961, 540)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.txtJenisKendaraan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboJenisKendaraan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboUser.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.JamAkhir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtAkhir.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtAkhir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.JamAwal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtAwal.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtAwal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboJenisLaporan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents cboJenisLaporan As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents JamAkhir As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents dtAkhir As DevExpress.XtraEditors.DateEdit
    Friend WithEvents JamAwal As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents dtAwal As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cboUser As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents btnCetak As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtJenisKendaraan As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cboJenisKendaraan As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem5 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents crViewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
End Class
