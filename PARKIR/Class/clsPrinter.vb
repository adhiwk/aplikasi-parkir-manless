Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.Drawing

'ReceiptItem is used to hold line item data for our receipt
Public Class ReceiptItem
    Public Property Item As String
    Public Property Price As Decimal
    Public Property Quantity As Integer

    Public Sub New(_Item As String, _Price As Decimal, _Quantity As Integer)
        Item = _Item
        Price = _Price
        Quantity = _Quantity
    End Sub
End Class

Public Class Receipt
    'Declare our PrintDocument
    Private WithEvents Document As PrintDocument

    'Declare variables to track our position in the document
    Private x As Integer
    Private y As Integer

    'The value LineOffset will be used to step further down the page. 
    'Needs to be slightly taller than the font size
    Private lineOffset As Integer

    'Declare some fonts with various sizes and styles
    Private printFont As New Font("Microsoft Sans Serif", 10, FontStyle.Regular, GraphicsUnit.Point)
    Private titleFont As New Font("Microsoft Sans Serif", 14, FontStyle.Bold, GraphicsUnit.Point)
    Private totalFont As New Font("Microsoft Sans Serif", 14, FontStyle.Bold, GraphicsUnit.Point)
    Private printFontLarge = New Font("Microsoft Sans Serif", 20, FontStyle.Regular, GraphicsUnit.Point)

    'Declare a font to use for barcodes. The font used here will need to be substituted to a Barcode 
    'font in the TM-88V printer settings in windows, or it will just print the text
    Private barcodeFont As New Font("Courier New", 14) 'Substituted to Barcode1 Font

    'Create a container that will hold our line items
    Private Lines As List(Of ReceiptItem)

    'Various Private variables
    Private ReceiptID As Long
    Private mPrinterName As String
    Private mLastException As Exception
    Private disposedValue As Boolean

    'Provide a way to retrieve error data
    Public ReadOnly Property LastException As Exception
        Get
            Return mLastException
        End Get
    End Property

    'Used to set/get the Printer name that the PrintDocument will use.
    Public Property PrinterName As String
        Get
            Return mPrinterName
        End Get
        Set(value As String)
            mPrinterName = value
            Document.PrinterSettings.PrinterName = mPrinterName
            If Not Document.PrinterSettings.IsValid Then
                MsgBox("The specified printer, " + mPrinterName + " does not exist", vbOKOnly)
            End If
        End Set
    End Property

    'Basic Constructor
    Public Sub New()
        mPrinterName = Nothing
        Lines = New List(Of ReceiptItem)
        ReceiptID = 0

        mLastException = New Exception
        Document = New PrintDocument
    End Sub

    'Call this to print the receipt
    Public Sub PrintReceipt(_Lines As List(Of ReceiptItem), _ReceiptID As Long)
        Try
            'Receipt #
            ReceiptID = _ReceiptID
            'Line Items
            Lines = _Lines
            Document.Print()
        Catch ex As Exception
            mLastException = ex
        End Try
    End Sub

    Private Sub Receipt_Print(ByVal sender As System.Object,
                              ByVal Page As PrintPageEventArgs) Handles Document.PrintPage
        'Declare some temporary variables
        Dim Total As Decimal = 0
        Dim SubTotal As Decimal

        'Set our start location
        x = 10
        y = 4

        'Configure the page units to Point
        Page.Graphics.PageUnit = GraphicsUnit.Point

        'Set our LineOffset to 2 points less that the graphics size for printFont
        lineOffset = printFont.GetHeight(Page.Graphics) - 2

        'Print some basic info
        AddText("Company Name", printFont, Page)

        'Print two blank lines
        AddBlankLine(2)

        'More basic info. 
        AddText("CompanyAddress1", printFont, Page)
        AddText("CompanyCity, CompanyState CompanyZip", printFont, Page)
        AddText("CompanyPhone", printFont, Page)
        AddText("CompanyWebSite", printFont, Page)

        'More blank lines
        AddBlankLine(2)

        'Print the current date and time
        AddText(Now.ToShortDateString & " " & Now.ToShortTimeString, printFont, Page)

        AddBlankLine(2)

        'Print customer name, if we actually had one
        AddText("Customer: " & "Customer.Name", printFont, Page)

        'Iterate through all of the lines in Lines
        For Each Line As ReceiptItem In Lines
            'Calculate a sub total for this line
            SubTotal = Line.Price * Line.Quantity
            'Add subtotal to total
            Total += SubTotal

            'Print a dividing line
            AddDivider(printFont, Page)
            'Print line item details along with sub total
            AddText("Item: " & Line.Item, printFont, Page)
            AddText("Price: " & Line.Price, printFont, Page)
            AddText("Quantity: " & Line.Quantity, printFont, Page)
            AddText("Sub-Total: " & SubTotal.ToString("0.00"), printFont, Page)
        Next

        'Print another dividing line
        AddDivider(printFont, Page)


        'print ticket total
        AddText("Total: " & Total.ToString("0.00"), printFontLarge, Page)

        'Yep. More blanks.
        AddBlankLine(2)

        'print out our barcode
        AddText(ReceiptID, barcodeFont, Page)

        'If we are past the end of the page, extend the page past our last content
        If Page.PageSettings.PaperSize.Height < y Then
            Page.PageSettings.PaperSize.Height = y + 40
        End If

        'The following tells the PrintDocument that we are done adding content.
        'Once this is set to false, the document is sent to the windows print queue
        Page.HasMorePages = False
    End Sub

    'Helper function. Call it with the parameters and it will add a line of text to 
    'the document using the font provided in _Font. This function tracks our position 
    'in the document by adding the LineOffset to the Y value
    Private Sub AddText(ByVal _Text As String, ByRef _Font As Font, ByRef _Page As PrintPageEventArgs)
        _Page.Graphics.DrawString(_Text, _Font, Brushes.Black, x, y)
        lineOffset = _Font.GetHeight(_Page.Graphics)
        y += lineOffset
    End Sub

    'Helper function adds _Lines * LineOffset to the Y value
    Private Sub AddBlankLine(ByVal _Lines As Integer)
        y += lineOffset * _Lines
    End Sub

    'Helper function to add an ugly (but cheap) divider
    Private Sub AddDivider(ByRef _Font As Font, ByRef _Page As PrintPageEventArgs)
        _Page.Graphics.DrawString("___________________________________", _Font, Brushes.Black, x, y)
        lineOffset = _Font.GetHeight(_Page.Graphics)
        y += lineOffset
    End Sub

    'Adds an Image to document at the current position. I use this to add a captured 
    'signature to the receipt just call it with an Image and the Page reference as parameters
    Private Sub AddImage(ByRef _Image As Image, ByRef _Page As PrintPageEventArgs)
        _Page.Graphics.DrawImage(_Image, x, y)
        y += _Image.Height
    End Sub

    'Added a fancy line divider
    Private Sub AddLine(ByRef _Page As System.Drawing.Printing.PrintPageEventArgs)
        _Page.Graphics.DrawLine(Pens.Black, x, y - 4, x + 220, y - 4)
        y += 10
    End Sub
End Class