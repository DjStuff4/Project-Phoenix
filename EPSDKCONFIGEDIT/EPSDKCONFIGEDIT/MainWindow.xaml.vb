Imports System.Text
Imports System.Runtime.InteropServices

Class MainWindow
    Private Declare Ansi Function DeletePrivateProfileSection Lib "kernel32" Alias "WritePrivateProfileStringA" ( _
    ByVal Section As String, _
    ByVal NoKey As Integer, _
    ByVal NoSetting As Integer, _
    ByVal FileName As String) _
As Integer

    Public Sub EraseINISection(ByVal INIFile As String, ByVal Section As String)
        DeletePrivateProfileSection(Section, 0, 0, INIFile)
    End Sub
    <DllImport("kernel32")>
    Private Shared Function WritePrivateProfileString(ByVal lpSectionName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Long
    End Function
    Dim ini As String = Environment.CurrentDirectory & "\config.ini"


    Private Function SetIniValue(ByVal section As String, ByVal key As String, ByVal filename As String, Optional ByVal defaultValue As String = "") As String
        Dim sb As New StringBuilder(500)
        If WritePrivateProfileString(section, key, defaultValue, filename) > 0 Then
            Return sb.ToString
        Else
            Return defaultValue
        End If
    End Function
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button1.Click
        Dim SECTION As String = "[" & TextBox1.Text & "]"
        Dim LOCATION As String = "Location=" & TextBox3.Text
        Dim EXE As String = "EXE=" & TextBox2.Text
        Dim ARGS As String = "ARGS=" & TextBox5.Text
        Dim desc As String = "DESC=" & TextBox6.Text
        My.Computer.FileSystem.WriteAllText(ini, SECTION & vbCrLf, True)
        My.Computer.FileSystem.WriteAllText(ini, LOCATION & vbCrLf, True)
        My.Computer.FileSystem.WriteAllText(ini, EXE & vbCrLf, True)
        My.Computer.FileSystem.WriteAllText(ini, ARGS & vbCrLf, True)
        My.Computer.FileSystem.WriteAllText(ini, desc & vbCrLf, True)

    End Sub

    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        Dim _ini As New INI(ini)
        For Each lol In _ini.GetSectionNames()
            ListBox1.Items.Add(lol)
        Next
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button2.Click
        If Not ListBox1.SelectedValue = "" Then
            EraseINISection(ini, ListBox1.SelectedValue)
            ListBox1.Items.Clear()
            Dim _ini As New INI(ini)
            For Each lol In _ini.GetSectionNames()
                ListBox1.Items.Add(lol)
            Next
        End If
    End Sub
End Class
