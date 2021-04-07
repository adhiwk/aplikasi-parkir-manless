<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrinting
    Inherits DevExpress.XtraEditors.XtraForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrinting))
        Me.PrintControler = New System.Drawing.Printing.PrintDocument()
        Me.PrintWin = New System.Windows.Forms.PrintDialog()
        Me.PreviewPrint = New System.Windows.Forms.PrintPreviewDialog()
        Me.SuspendLayout()
        '
        'PrintControler
        '
        '
        'PrintWin
        '
        Me.PrintWin.UseEXDialog = True
        '
        'PreviewPrint
        '
        Me.PreviewPrint.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PreviewPrint.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PreviewPrint.ClientSize = New System.Drawing.Size(400, 300)
        Me.PreviewPrint.Enabled = True
        Me.PreviewPrint.Icon = CType(resources.GetObject("PreviewPrint.Icon"), System.Drawing.Icon)
        Me.PreviewPrint.Name = "PreviewPrint"
        Me.PreviewPrint.Visible = False
        '
        'frmPrinting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Name = "frmPrinting"
        Me.Text = "PRINTING"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PrintControler As System.Drawing.Printing.PrintDocument
    Friend WithEvents PrintWin As System.Windows.Forms.PrintDialog
    Friend WithEvents PreviewPrint As System.Windows.Forms.PrintPreviewDialog
End Class
