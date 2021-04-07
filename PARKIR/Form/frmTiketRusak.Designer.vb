<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTiketRusak
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.btnSimpan = New DevExpress.XtraEditors.SimpleButton()
        Me.txtAlamat = New DevExpress.XtraEditors.TextEdit()
        Me.txtNamaPemilik = New DevExpress.XtraEditors.TextEdit()
        Me.txtNomorSTNK = New DevExpress.XtraEditors.TextEdit()
        Me.txtJamMasuk = New DevExpress.XtraEditors.TextEdit()
        Me.txtTarif = New DevExpress.XtraEditors.TextEdit()
        Me.txtNoPol = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem5 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem6 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.txtLamaParkir = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.txtAlamat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNamaPemilik.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNomorSTNK.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtJamMasuk.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTarif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoPol.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLamaParkir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.txtLamaParkir)
        Me.LayoutControl1.Controls.Add(Me.btnSimpan)
        Me.LayoutControl1.Controls.Add(Me.txtAlamat)
        Me.LayoutControl1.Controls.Add(Me.txtNamaPemilik)
        Me.LayoutControl1.Controls.Add(Me.txtNomorSTNK)
        Me.LayoutControl1.Controls.Add(Me.txtJamMasuk)
        Me.LayoutControl1.Controls.Add(Me.txtTarif)
        Me.LayoutControl1.Controls.Add(Me.txtNoPol)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
        Me.LayoutControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(673, 263, 650, 400)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(458, 250)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'btnSimpan
        '
        Me.btnSimpan.Location = New System.Drawing.Point(368, 199)
        Me.btnSimpan.Name = "btnSimpan"
        Me.btnSimpan.Size = New System.Drawing.Size(85, 21)
        Me.btnSimpan.StyleController = Me.LayoutControl1
        Me.btnSimpan.TabIndex = 10
        Me.btnSimpan.Text = "&Simpan"
        '
        'txtAlamat
        '
        Me.txtAlamat.EnterMoveNextControl = True
        Me.txtAlamat.Location = New System.Drawing.Point(71, 169)
        Me.txtAlamat.Name = "txtAlamat"
        Me.txtAlamat.Size = New System.Drawing.Size(382, 20)
        Me.txtAlamat.StyleController = Me.LayoutControl1
        Me.txtAlamat.TabIndex = 9
        '
        'txtNamaPemilik
        '
        Me.txtNamaPemilik.EnterMoveNextControl = True
        Me.txtNamaPemilik.Location = New System.Drawing.Point(71, 139)
        Me.txtNamaPemilik.Name = "txtNamaPemilik"
        Me.txtNamaPemilik.Size = New System.Drawing.Size(382, 20)
        Me.txtNamaPemilik.StyleController = Me.LayoutControl1
        Me.txtNamaPemilik.TabIndex = 8
        '
        'txtNomorSTNK
        '
        Me.txtNomorSTNK.EnterMoveNextControl = True
        Me.txtNomorSTNK.Location = New System.Drawing.Point(71, 109)
        Me.txtNomorSTNK.Name = "txtNomorSTNK"
        Me.txtNomorSTNK.Size = New System.Drawing.Size(382, 20)
        Me.txtNomorSTNK.StyleController = Me.LayoutControl1
        Me.txtNomorSTNK.TabIndex = 7
        '
        'txtJamMasuk
        '
        Me.txtJamMasuk.EditValue = "12:00"
        Me.txtJamMasuk.EnterMoveNextControl = True
        Me.txtJamMasuk.Location = New System.Drawing.Point(71, 45)
        Me.txtJamMasuk.Name = "txtJamMasuk"
        Me.txtJamMasuk.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJamMasuk.Properties.Appearance.Options.UseFont = True
        Me.txtJamMasuk.Properties.Mask.EditMask = "(0?\d|1\d|2[0-3])\:[0-5]\d"
        Me.txtJamMasuk.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.txtJamMasuk.Size = New System.Drawing.Size(50, 22)
        Me.txtJamMasuk.StyleController = Me.LayoutControl1
        Me.txtJamMasuk.TabIndex = 6
        '
        'txtTarif
        '
        Me.txtTarif.EditValue = "1000000"
        Me.txtTarif.EnterMoveNextControl = True
        Me.txtTarif.Location = New System.Drawing.Point(71, 77)
        Me.txtTarif.Name = "txtTarif"
        Me.txtTarif.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTarif.Properties.Appearance.Options.UseFont = True
        Me.txtTarif.Properties.Appearance.Options.UseTextOptions = True
        Me.txtTarif.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtTarif.Properties.Mask.EditMask = "n0"
        Me.txtTarif.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtTarif.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtTarif.Size = New System.Drawing.Size(100, 22)
        Me.txtTarif.StyleController = Me.LayoutControl1
        Me.txtTarif.TabIndex = 5
        '
        'txtNoPol
        '
        Me.txtNoPol.EditValue = "DR1234AB"
        Me.txtNoPol.EnterMoveNextControl = True
        Me.txtNoPol.Location = New System.Drawing.Point(71, 5)
        Me.txtNoPol.Name = "txtNoPol"
        Me.txtNoPol.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoPol.Properties.Appearance.Options.UseFont = True
        Me.txtNoPol.Size = New System.Drawing.Size(100, 30)
        Me.txtNoPol.StyleController = Me.LayoutControl1
        Me.txtNoPol.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.EmptySpaceItem1, Me.EmptySpaceItem2, Me.LayoutControlItem2, Me.EmptySpaceItem3, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.LayoutControlItem6, Me.LayoutControlItem7, Me.EmptySpaceItem5, Me.LayoutControlItem3, Me.EmptySpaceItem6, Me.LayoutControlItem8})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.OptionsItemText.TextToControlDistance = 5
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(458, 250)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.txtNoPol
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(176, 40)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(176, 40)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(176, 40)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Nomor Polisi"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(61, 13)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 194)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(363, 31)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(176, 0)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(282, 40)
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.txtTarif
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(176, 32)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(176, 32)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(176, 32)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "Tarif"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(61, 13)
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.AllowHotTrack = False
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(176, 72)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(282, 32)
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.txtNomorSTNK
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 104)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(458, 30)
        Me.LayoutControlItem4.Text = "Nomor STNK"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(61, 13)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.txtNamaPemilik
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 134)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(458, 30)
        Me.LayoutControlItem5.Text = "Nama Pemilik"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(61, 13)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.txtAlamat
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 164)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(458, 30)
        Me.LayoutControlItem6.Text = "Alamat"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(61, 13)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.btnSimpan
        Me.LayoutControlItem7.Location = New System.Drawing.Point(363, 194)
        Me.LayoutControlItem7.MaxSize = New System.Drawing.Size(95, 31)
        Me.LayoutControlItem7.MinSize = New System.Drawing.Size(95, 31)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(95, 31)
        Me.LayoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextVisible = False
        '
        'EmptySpaceItem5
        '
        Me.EmptySpaceItem5.AllowHotTrack = False
        Me.EmptySpaceItem5.Location = New System.Drawing.Point(0, 225)
        Me.EmptySpaceItem5.Name = "EmptySpaceItem5"
        Me.EmptySpaceItem5.Size = New System.Drawing.Size(458, 25)
        Me.EmptySpaceItem5.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.txtJamMasuk
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 40)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(126, 32)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(126, 32)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(126, 32)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "Jam Masuk"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(61, 13)
        '
        'EmptySpaceItem6
        '
        Me.EmptySpaceItem6.AllowHotTrack = False
        Me.EmptySpaceItem6.Location = New System.Drawing.Point(438, 40)
        Me.EmptySpaceItem6.Name = "EmptySpaceItem6"
        Me.EmptySpaceItem6.Size = New System.Drawing.Size(20, 32)
        Me.EmptySpaceItem6.TextSize = New System.Drawing.Size(0, 0)
        '
        'txtLamaParkir
        '
        Me.txtLamaParkir.Location = New System.Drawing.Point(197, 45)
        Me.txtLamaParkir.Name = "txtLamaParkir"
        Me.txtLamaParkir.Size = New System.Drawing.Size(236, 20)
        Me.txtLamaParkir.StyleController = Me.LayoutControl1
        Me.txtLamaParkir.TabIndex = 11
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.txtLamaParkir
        Me.LayoutControlItem8.Location = New System.Drawing.Point(126, 40)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(312, 32)
        Me.LayoutControlItem8.Text = "Lama Parkir"
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(61, 13)
        '
        'frmTiketRusak
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(458, 250)
        Me.Controls.Add(Me.LayoutControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTiketRusak"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tiket Rusak"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.txtAlamat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNamaPemilik.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNomorSTNK.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtJamMasuk.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTarif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoPol.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLamaParkir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents txtNoPol As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents txtJamMasuk As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtTarif As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtNomorSTNK As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtAlamat As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNamaPemilik As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents btnSimpan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem5 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem6 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents txtLamaParkir As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
End Class
