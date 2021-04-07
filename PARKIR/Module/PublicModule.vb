Imports System.IO
Imports System.Data.SqlClient
Imports System.Data.OleDb
Module PublicModule
    Public Function KoneksiSQL() As SqlConnection
        Dim strServer As String = My.Settings.server.Trim
        Dim strDBname As String = Simple3Des.Decrypt(My.Settings.dbname.Trim)
        Dim strUser As String = Simple3Des.Decrypt(My.Settings.user.Trim)
        Dim strPass As String = Simple3Des.Decrypt(My.Settings.password.Trim)

        Dim connectionString As String _
        = "Data Source='" &
        strServer.Trim & "';Initial Catalog='" &
        strDBname.Trim & "';User ID='" &
        strUser.Trim & "'; Password='" & strPass.Trim & "'"

        Return New SqlConnection(connectionString)
    End Function

    Public Function KoneksiOleDB() As OleDbConnection
        Dim connectionString As String = _
          "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & _
          Application.StartupPath & "\mydb.mdb"

        Return New OleDbConnection(connectionString)
    End Function

    Public Function ConvertTanggal(ByVal dtTanggal As DevExpress.XtraEditors.DateEdit) As String
        Dim cBulan As String = ""
        Dim cTanggal As String = ""

        If Len(dtTanggal.DateTime.Month.ToString.Trim) = 1 Then
            cBulan = "0" & dtTanggal.DateTime.Month.ToString.Trim
        Else
            cBulan = dtTanggal.DateTime.Month.ToString.Trim
        End If

        If Len(dtTanggal.DateTime.Day.ToString.Trim) = 1 Then
            cTanggal = "0" & dtTanggal.DateTime.Day.ToString.Trim
        Else
            cTanggal = dtTanggal.DateTime.Day.ToString.Trim
        End If
        ConvertTanggal = dtTanggal.DateTime.Year.ToString.Trim & "-" & cBulan.Trim & "-" & cTanggal.Trim
    End Function

    Public Function ConvertNumeric(ByVal cString As String) As Integer
        Dim nNominal As String = ""
        For x = 1 To Len(cString.Trim)
            If Mid(cString.Trim, x, 1) <> "," And Mid(cString.Trim, x, 1) <> "." Then
                nNominal = nNominal + Mid(cString.Trim, x, 1)
            End If
        Next
        Return Val(nNominal)
    End Function

    Public Function RolesBasedAccessControl(ByVal cUserId As String,
                                             ByVal cParent As String,
                                             ByVal cProcess As String) As Boolean
        Dim lRoles As Boolean = False

        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()

        Try
            Using Cmd As SqlCommand = New SqlCommand() With {.Connection = Conn}
                Cmd.CommandText = "SELECT access_control_rule.[access] " &
                                  "FROM access_control access_control " &
                                  "INNER JOIN access_control_rule " &
                                  "access_control_rule ON access_control.[ac_id] = access_control_rule.[ac_id] " &
                                  "WHERE access_control_rule.[kd petugas] = '" & cUserId.Trim & "' AND " &
                                  "access_control.[parent] = '" & cParent.Trim & "' AND " &
                                  "access_control.[process] = '" & cProcess.Trim & "'"

                Dim rDr As SqlDataReader = Cmd.ExecuteReader
                While rDr.Read
                    If rDr.Item(0).ToString.Trim = "Y" Then
                        lRoles = True
                    Else
                        lRoles = False
                    End If
                End While

                If rDr.HasRows = False Then
                    lRoles = False
                End If

                rDr.Close()
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Conn.Close()
        Return lRoles
    End Function

    Public Function GetDate() As Date
        Dim dDate As Date
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()

        Try
            Using Cmd As SqlCommand = New SqlCommand() With {.Connection = Conn}

                Cmd.CommandText = "select getdate()"
                Dim rDr As SqlDataReader = Cmd.ExecuteReader
                While rDr.Read
                    dDate = Date.Parse(rDr.Item(0).ToString.Trim)
                End While
                rDr.Close()
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Conn.Close()
        Return Date.Parse(dDate)
    End Function

    Public Function ExecuteQuery(ByVal strQuery As String) As Boolean
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()

        Try
            Using Cmd As SqlCommand = New SqlCommand() With {.Connection = Conn}

                Cmd.CommandText = strQuery
                Cmd.ExecuteNonQuery()
                'MsgBox("Query berhasil disimpan")
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Conn.Close()
        Return True
    End Function

    Public Function DestroySession(ByVal cUserId As String) As Boolean
        Dim cStatus As Boolean
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "update sesi_login set [status] = 'T' " &
                                   "Where [kd petugas] = '" &
                                   cUserId.Trim & "'"
                    .ExecuteNonQuery()
                    cStatus = True

                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Conn.Close()
        Return cStatus
    End Function

    Public Function CreateSession(ByVal cUserId As String) As Boolean
        Dim cStatus As Boolean
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = "update sesi_login set [status] = 'Y' " &
                                   "Where [kd petugas] = '" &
                                   cUserId.Trim & "'"
                    .ExecuteNonQuery()
                    cStatus = True

                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Conn.Close()
        Return cStatus
    End Function

    Public Function ValidasiData(ByVal cKode As String, ByVal cQuery As String, ByVal cMsg As String) As Boolean
        Dim xKode As String
        Dim cResult As Boolean
        Dim Conn As SqlConnection = KoneksiSQL()
        Conn.Open()
        Try
            Using cmd As New SqlCommand
                With cmd
                    .Connection = Conn
                    .CommandText = cQuery.Trim
                    xKode = Convert.ToString(.ExecuteScalar)
                    If cKode.Trim <> xKode.Trim Then
                        cResult = True
                    Else
                        MsgBox(cMsg.Trim, MsgBoxStyle.Critical, "error")
                        cResult = False
                    End If
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
        End Try
        Conn.Close()
        Return cResult
    End Function

    Public Function ValidasiKey(ByVal cKey As String) As Boolean
        Dim lStatus As Boolean
        If cKey.Trim = "A" Then
            lStatus = True
        ElseIf cKey.Trim = "B" Then
            lStatus = True
        ElseIf cKey.Trim = "C" Then
            lStatus = True
        ElseIf cKey.Trim = "C" Then
            lStatus = True
        ElseIf cKey.Trim = "D" Then
            lStatus = True
        ElseIf cKey.Trim = "E" Then
            lStatus = True
        ElseIf cKey.Trim = "F" Then
            lStatus = True
        ElseIf cKey.Trim = "G" Then
            lStatus = True
        ElseIf cKey.Trim = "H" Then
            lStatus = True
        ElseIf cKey.Trim = "I" Then
            lStatus = True
        ElseIf cKey.Trim = "J" Then
            lStatus = True
        ElseIf cKey.Trim = "K" Then
            lStatus = True
        ElseIf cKey.Trim = "L" Then
            lStatus = True
        ElseIf cKey.Trim = "M" Then
            lStatus = True
        ElseIf cKey.Trim = "N" Then
            lStatus = True
        ElseIf cKey.Trim = "O" Then
            lStatus = True
        ElseIf cKey.Trim = "P" Then
            lStatus = True
        ElseIf cKey.Trim = "Q" Then
            lStatus = True
        ElseIf cKey.Trim = "R" Then
            lStatus = True
        ElseIf cKey.Trim = "S" Then
            lStatus = True
        ElseIf cKey.Trim = "T" Then
            lStatus = True
        ElseIf cKey.Trim = "U" Then
            lStatus = True
        ElseIf cKey.Trim = "V" Then
            lStatus = True
        ElseIf cKey.Trim = "W" Then
            lStatus = True
        ElseIf cKey.Trim = "X" Then
            lStatus = True
        ElseIf cKey.Trim = "Y" Then
            lStatus = True
        ElseIf cKey.Trim = "Z" Then
            lStatus = True
        ElseIf cKey.Trim = "0" Then
            lStatus = True
        ElseIf cKey.Trim = "1" Then
            lStatus = True
        ElseIf cKey.Trim = "2" Then
            lStatus = True
        ElseIf cKey.Trim = "3" Then
            lStatus = True
        ElseIf cKey.Trim = "4" Then
            lStatus = True
        ElseIf cKey.Trim = "5" Then
            lStatus = True
        ElseIf cKey.Trim = "6" Then
            lStatus = True
        ElseIf cKey.Trim = "7" Then
            lStatus = True
        ElseIf cKey.Trim = "8" Then
            lStatus = True
        ElseIf cKey.Trim = "9" Then
            lStatus = True
        Else
            lStatus = False
        End If
        Return lStatus
    End Function

    Public Function imgToByteArray(ByVal img As Image) As Byte()
        Using mStream As New MemoryStream()
            img.Save(mStream, Imaging.ImageFormat.Bmp)
            Return mStream.ToArray()
        End Using
    End Function

    Public Function byteArrayToImage(ByVal byteArrayIn As Byte()) As Image
        Using mStream As New MemoryStream(byteArrayIn)
            Return Image.FromStream(mStream)
        End Using
    End Function

    Public Function imgToByteConverter(ByVal inImg As Image) As Byte()
        Dim imgCon As New ImageConverter()
        Return DirectCast(imgCon.ConvertTo(inImg, GetType(Byte())), Byte())
    End Function

    Public Sub Byte2Image(ByRef NewImage As Image, ByVal ByteArr() As Byte)
        Dim ImageStream As MemoryStream
        Try
            If ByteArr.GetUpperBound(0) > 0 Then
                ImageStream = New MemoryStream(ByteArr)
                NewImage = Image.FromStream(ImageStream)
            Else
                NewImage = Nothing
            End If
        Catch ex As Exception
            NewImage = Nothing
        End Try
    End Sub

    Public Sub Image2Byte(ByRef NewImage As Image, ByRef ByteArr() As Byte)
        Dim ImageStream As MemoryStream
        Try
            ReDim ByteArr(0)
            If NewImage IsNot Nothing Then
                ImageStream = New MemoryStream
                NewImage.Save(ImageStream, Imaging.ImageFormat.Jpeg)
                ReDim ByteArr(CInt(ImageStream.Length - 1))
                ImageStream.Position = 0
                ImageStream.Read(ByteArr, 0, CInt(ImageStream.Length))
            End If
        Catch ex As Exception

        End Try
    End Sub

End Module
