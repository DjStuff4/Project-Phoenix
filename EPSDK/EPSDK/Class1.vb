Imports System.Text
Imports System.Runtime.InteropServices
Public Class INI

    <DllImport("kernel32")> _
    Private Shared Function GetPrivateProfileString(ByVal Section As String, ByVal Key As String, ByVal Value As String, ByVal Result As StringBuilder, ByVal Size As Integer, ByVal FileName As String) As Integer
    End Function


    <DllImport("kernel32")> _
    Private Shared Function GetPrivateProfileString(ByVal Section As String, ByVal Key As Integer, ByVal Value As String, <MarshalAs(UnmanagedType.LPArray)> ByVal Result As Byte(), ByVal Size As Integer, ByVal FileName As String) As Integer
    End Function

    <DllImport("kernel32")> _
    Private Shared Function GetPrivateProfileString(ByVal Section As Integer, ByVal Key As String, ByVal Value As String, <MarshalAs(UnmanagedType.LPArray)> ByVal Result As Byte(), ByVal Size As Integer, ByVal FileName As String) As Integer
    End Function

    Public path As String
    Public Sub New(ByVal INIPath As String)
        path = INIPath
    End Sub

    Public Function GetSectionNames() As String()
        Dim maxsize As Integer = 500
        While True
            Dim bytes As Byte() = New Byte(maxsize - 1) {}
            Dim size As Integer = GetPrivateProfileString(0, "", "", bytes, maxsize, path)
            If size < maxsize - 2 Then
                Dim Selected As String = Encoding.ASCII.GetString(bytes, 0, size - (If(size > 0, 1, 0)))
                Return Selected.Split(New Char() {ControlChars.NullChar})
            End If
            maxsize *= 2
        End While
    End Function
    Public Function GetEntryNames(ByVal section As String) As String()
        Dim maxsize As Integer = 500
        While True
            Dim bytes As Byte() = New Byte(maxsize - 1) {}
            Dim size As Integer = GetPrivateProfileString(section, 0, "", bytes, maxsize, path)
            If size < maxsize - 2 Then
                Dim entries As String = Encoding.ASCII.GetString(bytes, 0, size - (If(size > 0, 1, 0)))
                Return entries.Split(New Char() {ControlChars.NullChar})
            End If
            maxsize *= 2
        End While
    End Function
    Public Function GetEntryValue(ByVal section As String, ByVal entry As String) As Object
        Dim maxsize As Integer = 250
        While True
            Dim result As New StringBuilder(maxsize)
            Dim size As Integer = GetPrivateProfileString(section, entry, "", result, maxsize, path)
            If size < maxsize - 1 Then
                Return result.ToString()
            End If
            maxsize *= 2
        End While
    End Function
End Class