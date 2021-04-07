Imports System.IO.Ports
Public Class frmReadSerialPort
    Dim comm As New CommunicationManager
    Private mySerialPort As New SerialPort

    Private Sub CommPortSetup()
        With mySerialPort
            .PortName = "COM4"
            .BaudRate = 34800
            .DataBits = 8
            .Parity = Parity.None
            .StopBits = StopBits.One
            .Handshake = Handshake.None
        End With

        Try
            mySerialPort.Open()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmReadSerialPort_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        comm.Parity = "None"
        comm.StopBits = "One"
        comm.DataBits = "8"
        comm.BaudRate = "38400"
        comm.PortName = "COM4"
        comm.OpenPort()
    End Sub

    Private Sub mySerialPort_DataReceived(ByVal sender As Object, ByVal e As SerialDataReceivedEventArgs)
        'Handles serial port data received events    
        Dim n As Integer = mySerialPort.BytesToRead
        Dim comBuffer As Byte() = New Byte(n - 1) {}
        txtOutput.Text = (comBuffer(0))
    End Sub
End Class