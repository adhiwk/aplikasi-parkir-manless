<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.components = New System.ComponentModel.Container()
        Dim Transition1 As DevExpress.Utils.Animation.Transition = New DevExpress.Utils.Animation.Transition()
        Dim SlideFadeTransition1 As DevExpress.Utils.Animation.SlideFadeTransition = New DevExpress.Utils.Animation.SlideFadeTransition()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnCapture = New System.Windows.Forms.Button()
        Me.btnShift = New System.Windows.Forms.Button()
        Me.btnLogout = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnReport = New System.Windows.Forms.Button()
        Me.btnEntryUser = New System.Windows.Forms.Button()
        Me.btnTarif = New System.Windows.Forms.Button()
        Me.btnJenisKendaraan = New System.Windows.Forms.Button()
        Me.btnMember = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblControl = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.pnlApps = New System.Windows.Forms.Panel()
        Me.tmPanel = New DevExpress.Utils.Animation.TransitionManager(Me.components)
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(111, Byte), Integer))
        Me.Panel1.Controls.Add(Me.btnCapture)
        Me.Panel1.Controls.Add(Me.btnShift)
        Me.Panel1.Controls.Add(Me.btnLogout)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.btnReport)
        Me.Panel1.Controls.Add(Me.btnEntryUser)
        Me.Panel1.Controls.Add(Me.btnTarif)
        Me.Panel1.Controls.Add(Me.btnJenisKendaraan)
        Me.Panel1.Controls.Add(Me.btnMember)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(194, 686)
        Me.Panel1.TabIndex = 0
        '
        'btnCapture
        '
        Me.btnCapture.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(111, Byte), Integer))
        Me.btnCapture.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(91, Byte), Integer))
        Me.btnCapture.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCapture.Font = New System.Drawing.Font("Lucida Console", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCapture.ForeColor = System.Drawing.Color.White
        Me.btnCapture.Image = Global.PARKIR.My.Resources.Resources.camera48
        Me.btnCapture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCapture.Location = New System.Drawing.Point(0, 556)
        Me.btnCapture.Name = "btnCapture"
        Me.btnCapture.Size = New System.Drawing.Size(204, 60)
        Me.btnCapture.TabIndex = 8
        Me.btnCapture.Text = "  Capture List"
        Me.btnCapture.UseVisualStyleBackColor = True
        '
        'btnShift
        '
        Me.btnShift.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(111, Byte), Integer))
        Me.btnShift.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(91, Byte), Integer))
        Me.btnShift.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShift.Font = New System.Drawing.Font("Lucida Console", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShift.ForeColor = System.Drawing.Color.White
        Me.btnShift.Image = Global.PARKIR.My.Resources.Resources.shift48
        Me.btnShift.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnShift.Location = New System.Drawing.Point(0, 358)
        Me.btnShift.Name = "btnShift"
        Me.btnShift.Size = New System.Drawing.Size(215, 60)
        Me.btnShift.TabIndex = 7
        Me.btnShift.Text = "Shift Kerja"
        Me.btnShift.UseVisualStyleBackColor = True
        '
        'btnLogout
        '
        Me.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnLogout.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(111, Byte), Integer))
        Me.btnLogout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(91, Byte), Integer))
        Me.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogout.Font = New System.Drawing.Font("Lucida Console", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogout.ForeColor = System.Drawing.Color.White
        Me.btnLogout.Image = Global.PARKIR.My.Resources.Resources.logout48
        Me.btnLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLogout.Location = New System.Drawing.Point(0, 626)
        Me.btnLogout.Name = "btnLogout"
        Me.btnLogout.Size = New System.Drawing.Size(194, 60)
        Me.btnLogout.TabIndex = 6
        Me.btnLogout.Text = "Logout"
        Me.btnLogout.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.PARKIR.My.Resources.Resources.parking_solution_2nd
        Me.PictureBox1.Location = New System.Drawing.Point(21, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(138, 133)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 5
        Me.PictureBox1.TabStop = False
        '
        'btnReport
        '
        Me.btnReport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(111, Byte), Integer))
        Me.btnReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(91, Byte), Integer))
        Me.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReport.Font = New System.Drawing.Font("Lucida Console", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReport.ForeColor = System.Drawing.Color.White
        Me.btnReport.Image = Global.PARKIR.My.Resources.Resources.sale_report48
        Me.btnReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReport.Location = New System.Drawing.Point(0, 490)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(204, 60)
        Me.btnReport.TabIndex = 4
        Me.btnReport.Text = "Pelaporan"
        Me.btnReport.UseVisualStyleBackColor = True
        '
        'btnEntryUser
        '
        Me.btnEntryUser.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(111, Byte), Integer))
        Me.btnEntryUser.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(91, Byte), Integer))
        Me.btnEntryUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEntryUser.Font = New System.Drawing.Font("Lucida Console", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEntryUser.ForeColor = System.Drawing.Color.White
        Me.btnEntryUser.Image = Global.PARKIR.My.Resources.Resources.user_entry48
        Me.btnEntryUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEntryUser.Location = New System.Drawing.Point(0, 424)
        Me.btnEntryUser.Name = "btnEntryUser"
        Me.btnEntryUser.Size = New System.Drawing.Size(215, 60)
        Me.btnEntryUser.TabIndex = 3
        Me.btnEntryUser.Text = "Entry Users"
        Me.btnEntryUser.UseVisualStyleBackColor = True
        '
        'btnTarif
        '
        Me.btnTarif.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(111, Byte), Integer))
        Me.btnTarif.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(91, Byte), Integer))
        Me.btnTarif.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTarif.Font = New System.Drawing.Font("Lucida Console", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTarif.ForeColor = System.Drawing.Color.White
        Me.btnTarif.Image = Global.PARKIR.My.Resources.Resources.tarif48
        Me.btnTarif.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTarif.Location = New System.Drawing.Point(0, 292)
        Me.btnTarif.Name = "btnTarif"
        Me.btnTarif.Size = New System.Drawing.Size(215, 60)
        Me.btnTarif.TabIndex = 2
        Me.btnTarif.Text = "Entry Tarif"
        Me.btnTarif.UseVisualStyleBackColor = True
        '
        'btnJenisKendaraan
        '
        Me.btnJenisKendaraan.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(111, Byte), Integer))
        Me.btnJenisKendaraan.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(91, Byte), Integer))
        Me.btnJenisKendaraan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnJenisKendaraan.Font = New System.Drawing.Font("Lucida Console", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnJenisKendaraan.ForeColor = System.Drawing.Color.White
        Me.btnJenisKendaraan.Image = Global.PARKIR.My.Resources.Resources.jeniskendaraan48
        Me.btnJenisKendaraan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnJenisKendaraan.Location = New System.Drawing.Point(0, 226)
        Me.btnJenisKendaraan.Name = "btnJenisKendaraan"
        Me.btnJenisKendaraan.Size = New System.Drawing.Size(241, 60)
        Me.btnJenisKendaraan.TabIndex = 1
        Me.btnJenisKendaraan.Text = "Jenis Kendaraan"
        Me.btnJenisKendaraan.UseVisualStyleBackColor = True
        '
        'btnMember
        '
        Me.btnMember.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(111, Byte), Integer))
        Me.btnMember.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(91, Byte), Integer))
        Me.btnMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMember.Font = New System.Drawing.Font("Lucida Console", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMember.ForeColor = System.Drawing.Color.White
        Me.btnMember.Image = Global.PARKIR.My.Resources.Resources.member48
        Me.btnMember.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMember.Location = New System.Drawing.Point(0, 160)
        Me.btnMember.Name = "btnMember"
        Me.btnMember.Size = New System.Drawing.Size(215, 60)
        Me.btnMember.TabIndex = 0
        Me.btnMember.Text = "Entry Member"
        Me.btnMember.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(91, Byte), Integer))
        Me.Panel2.Controls.Add(Me.lblControl)
        Me.Panel2.Controls.Add(Me.btnClose)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(194, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1006, 35)
        Me.Panel2.TabIndex = 1
        '
        'lblControl
        '
        Me.lblControl.AutoSize = True
        Me.lblControl.Font = New System.Drawing.Font("Lucida Console", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblControl.ForeColor = System.Drawing.Color.White
        Me.lblControl.Location = New System.Drawing.Point(7, 13)
        Me.lblControl.Name = "lblControl"
        Me.lblControl.Size = New System.Drawing.Size(183, 13)
        Me.lblControl.TabIndex = 1
        Me.lblControl.Text = "MANAJEMEN PARKIR V.5.0"
        '
        'btnClose
        '
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(91, Byte), Integer))
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Image = Global.PARKIR.My.Resources.Resources.window_close24
        Me.btnClose.Location = New System.Drawing.Point(976, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(30, 35)
        Me.btnClose.TabIndex = 0
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'pnlApps
        '
        Me.pnlApps.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlApps.Location = New System.Drawing.Point(194, 35)
        Me.pnlApps.Name = "pnlApps"
        Me.pnlApps.Size = New System.Drawing.Size(1006, 651)
        Me.pnlApps.TabIndex = 2
        '
        'tmPanel
        '
        Transition1.BarWaitingIndicatorProperties.Caption = ""
        Transition1.BarWaitingIndicatorProperties.Description = ""
        Transition1.Control = Me.pnlApps
        Transition1.LineWaitingIndicatorProperties.AnimationElementCount = 5
        Transition1.LineWaitingIndicatorProperties.Caption = ""
        Transition1.LineWaitingIndicatorProperties.Description = ""
        Transition1.RingWaitingIndicatorProperties.AnimationElementCount = 5
        Transition1.RingWaitingIndicatorProperties.Caption = ""
        Transition1.RingWaitingIndicatorProperties.Description = ""
        Transition1.TransitionType = SlideFadeTransition1
        Transition1.WaitingIndicatorProperties.Caption = "Please Wait..."
        Transition1.WaitingIndicatorProperties.Description = ""
        Me.tmPanel.Transitions.Add(Transition1)
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1200, 686)
        Me.Controls.Add(Me.pnlApps)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmMain"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents pnlApps As Panel
    Friend WithEvents btnMember As Button
    Friend WithEvents btnJenisKendaraan As Button
    Friend WithEvents btnTarif As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents btnEntryUser As Button
    Friend WithEvents btnReport As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents btnLogout As Button
    Friend WithEvents lblControl As Label
    Friend WithEvents btnShift As Button
    Friend WithEvents btnCapture As Button
    Friend WithEvents tmPanel As DevExpress.Utils.Animation.TransitionManager
End Class
