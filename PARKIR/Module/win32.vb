Imports System
Imports System.Runtime.InteropServices
Module win32
    <DllImport("CP210xManufacturing.dll")>
    Public Function CP210x_GetNumDevicess(numDevices As Integer) As String
    End Function


End Module
