Imports System.Drawing.Printing
Public Class frmPrinting
    ReadOnly myfont As New Font("Verdana", 14)
    Private PrintString As String
    Private PrintPageSettings As New PageSettings

    Private Sub PrintControler_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintControler.PrintPage

        PrintControler.DocumentName = "Test Document"

        Dim PrintFont As New Font("Verdana", 14)
        Dim numChars As Integer
        Dim numLines As Integer
        Dim stringForPage As String
        Dim strFormat As New StringFormat

        Dim rectDraw As New RectangleF( _
          e.MarginBounds.Left, e.MarginBounds.Top, _
          e.MarginBounds.Width, e.MarginBounds.Height)
        'Determine maximum text ammount and spaces lines
        Dim sizeMeasure As New SizeF(e.MarginBounds.Width, _
          e.MarginBounds.Height - PrintFont.GetHeight(e.Graphics))

        'Break in between words
        strFormat.Trimming = StringTrimming.Word
        'Determines ammount of wordss and lines that can fit on a page
        e.Graphics.MeasureString(PrintString, PrintFont, _
          sizeMeasure, strFormat, numChars, numLines)

        stringForPage = PrintString.Substring(0, numChars)
        'Print strings to page
        e.Graphics.DrawString(stringForPage, PrintFont, _
          Brushes.Black, rectDraw, strFormat)
        'Determine whether or not there are more pages to print
        If numChars < PrintString.Length Then
            'Remove printed text from string
            PrintString = PrintString.Substring(numChars)
            e.HasMorePages = True
        Else
            e.HasMorePages = False
            'Restore string after printing
            PrintString = " "
        End If

    End Sub

    Friend Sub Print(ByVal cText As String)
        If PrintWin.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
                AddHandler PrintControler.PrintPage, AddressOf Me.TextPrint
                Dim sf As New StringFormat() With {.FormatFlags = StringFormatFlags.NoWrap}
                PrintControler.Print()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub


    Private Sub TextPrint(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        Dim sf As New StringFormat() With {.FormatFlags = StringFormatFlags.NoWrap}
        e.Graphics.DrawString(cText.Trim, myfont, Brushes.Black, New PointF(175, 165), sf)
        e.HasMorePages = False
    End Sub

    'Friend Sub SendPrint(ByVal cText As String)
    '    Try
    '        AddHandler PrintControler.PrintPage, AddressOf Me.TextPrint
    '        PrintControler.DefaultPageSettings = PrintPageSettings
    '        PrintString = cText.Trim
    '        PreviewPrint.Document = PrintControler
    '        PreviewPrint.ShowDialog()
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub frmPrinting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class