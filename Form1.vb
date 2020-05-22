
Imports System.IO.Compression
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports System.Net
Imports IWshRuntimeLibrary

Public Class Form1
    Dim strSystemDir As String = Environment.GetEnvironmentVariable("SystemDrive")
    Dim novetusdir As String


    Dim novetusdir2 As String

    Dim IsMultipleNovetusDirs As Integer

    Private Declare Function GetDiskFreeSpaceEx _
        Lib "kernel32" _
        Alias "GetDiskFreeSpaceExA" _
        (ByVal lpDirectoryName As String, _
        ByRef lpFreeBytesAvailableToCaller As Long, ByRef lpTotalNumberOfBytes As Long, ByRef lpTotalNumberOfFreeBytes As Long) As Long


    Dim BruhFolder As String

    Dim ClientFolder As String
    'Declare the shell object
    Dim shObj As Object = Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application"))

    Public Sub ExtractArchive(ByVal zipFilename As String, ByVal ExtractDir As String)
        Dim Redo As Integer = 1
        Dim MyZipInputStream As ZipInputStream
        Dim MyFileStream As FileStream
        MyZipInputStream = New ZipInputStream(New FileStream(zipFilename, FileMode.Open, FileAccess.Read))
        Dim MyZipEntry As ZipEntry = MyZipInputStream.GetNextEntry
        Directory.CreateDirectory(ExtractDir)
        While Not MyZipEntry Is Nothing
            If (MyZipEntry.IsDirectory) Then
                Directory.CreateDirectory(ExtractDir & "\" & MyZipEntry.Name)
            Else
                If Not Directory.Exists(ExtractDir & "\" & _
                Path.GetDirectoryName(MyZipEntry.Name)) Then
                    Directory.CreateDirectory(ExtractDir & "\" & _
                    Path.GetDirectoryName(MyZipEntry.Name)) : Application.DoEvents()
                End If
                MyFileStream = New FileStream(ExtractDir & "\" & _
                  MyZipEntry.Name, FileMode.OpenOrCreate, FileAccess.Write) : Application.DoEvents()
                Dim count As Integer
                Dim buffer(4096) As Byte
                count = MyZipInputStream.Read(buffer, 0, 4096) : Application.DoEvents()
                While count > 0
                    MyFileStream.Write(buffer, 0, count)
                    count = MyZipInputStream.Read(buffer, 0, 4096) : Application.DoEvents()
                End While
                MyFileStream.Close()
            End If
            Try
                MyZipEntry = MyZipInputStream.GetNextEntry : Application.DoEvents()
            Catch ex As Exception
                MyZipEntry = Nothing
            End Try
        End While
        If Not (MyZipInputStream Is Nothing) Then MyZipInputStream.Close()
        If Not (MyFileStream Is Nothing) Then MyFileStream.Close()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        ProgressBar1.Visible = False
        Button3.Visible = False
        TextBox1.Visible = False
        Button7.Visible = False
        Button6.Visible = False
        Label11.Hide()


        Button8.Visible = True


        Button2.Visible = False
        Label3.Visible = False
        Label4.Visible = False
        Label5.Visible = False



        Label10.Hide()

        Label5.Text = "Installer Version: " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor

        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText(Environment.CurrentDirectory & "\PACKeula.txt")
        TextBox1.Text = fileReader
        Button1.Visible = False

        Label3.Text = "Novetus folder: " & novetusdir

        Label6.Visible = False
        Label7.Visible = False
        Label8.Visible = False
        Label9.Visible = False
    End Sub


    Public Sub ResponsiveSleep(ByRef iMilliSeconds As Integer)
        Dim i As Integer, iHalfSeconds As Integer = iMilliSeconds / 500
        For i = 1 To iHalfSeconds
            Threading.Thread.Sleep(500) : Application.DoEvents()
        Next i
    End Sub

    Public Function GetFreeSpace(ByVal Drive As String) As Long
        'returns free space in MB, formatted to two decimal places
        'e.g., msgbox("Free Space on C: "& GetFreeSpace("C:\") & "MB")

        Dim lBytesTotal, lFreeBytes, lFreeBytesAvailable As Long

        Dim iAns As Long

        iAns = GetDiskFreeSpaceEx(Drive, lFreeBytesAvailable, _
             lBytesTotal, lFreeBytes)
        If iAns > 0 Then

            Return BytesToMegabytes(lFreeBytes)
        Else
            Throw New Exception("Invalid or unreadable drive")
        End If


    End Function


    Public Function GetTotalSpace(ByVal Drive As String) As String
        'returns total space in MB, formatted to two decimal places
        'e.g., msgbox("Free Space on C: "& GetTotalSpace("C:\") & "MB")

        Dim lBytesTotal, lFreeBytes, lFreeBytesAvailable As Long

        Dim iAns As Long

        iAns = GetDiskFreeSpaceEx(Drive, lFreeBytesAvailable, lBytesTotal, lFreeBytes)
        If iAns > 0 Then

            Return BytesToMegabytes(lBytesTotal)
        Else
            Throw New Exception("Invalid or unreadable drive")
        End If
    End Function

    Private Function CreateShortCut(ByVal TargetName As String, ByVal ShortCutPath As String, ByVal ShortCutName As String, ByVal arg As String) As Boolean


        Dim WshShell As WshShellClass = New WshShellClass

        Dim MyShortcut As IWshRuntimeLibrary.IWshShortcut

        ' The shortcut will be created on the desktop


        MyShortcut = CType(WshShell.CreateShortcut(ShortCutPath & "\" & ShortCutName & ".lnk"), IWshRuntimeLibrary.IWshShortcut)

        MyShortcut.TargetPath = TargetName

        MyShortcut.Arguments = arg

        MyShortcut.Save()


    End Function
    Private Function BytesToMegabytes(ByVal Bytes As Long) _
    As Long


        Dim dblAns As Double
        dblAns = (Bytes / 1024) / 1024
        BytesToMegabytes = Format(dblAns, "###,###,##0.00")

    End Function


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim f As FileInfo = New FileInfo(novetusdir)
        Dim drive As String = Path.GetPathRoot(f.FullName)




        If My.Computer.FileSystem.FileExists(Environment.CurrentDirectory & "\Dir.txt") Then
            My.Computer.FileSystem.DeleteFile(Environment.CurrentDirectory & "\Dir.txt")
        End If





        If GetFreeSpace(drive) < 5000 Then
            MsgBox("Not enough drive space! 5GB Needed!", 0 + 16)

            Application.Exit()

        End If



        Label6.Visible = False
        Label7.Visible = False

        ProgressBar1.Visible = True
        Button1.Visible = False
        Button2.Visible = False
        Button4.Visible = False
        Button5.Visible = False
        Button6.Visible = False
        Label3.Visible = False
        Label4.Visible = False
        Label5.Visible = False
        TextBox1.Visible = False
        Button7.Visible = False
        Label8.Visible = True
        BruhFolder = novetusdir
        ClientFolder = novetusdir & "\clients"



        Label2.Text = "Checking If DjStuffs Expansion Pack Has already been installed..."
        ResponsiveSleep(1000)


        If My.Computer.FileSystem.FileExists(novetusdir & "\DJEP.TXT") Then
            Dim fileReader As String
            fileReader = My.Computer.FileSystem.ReadAllText(novetusdir & "\DJEP.TXT")

            Dim anas As String
            anas = MsgBox("DjStuffs Expansion Pack Has Already Been Installed (Version:" & fileReader & "). Would you like to overwrite?", 4 + 32)
            If anas = vbNo Then

                Application.Exit()


            End If


            My.Computer.FileSystem.DeleteFile(novetusdir & "\DJEP.TXT")
        End If





        Dim fileva As System.IO.StreamWriter
        fileva = My.Computer.FileSystem.OpenTextFileWriter(novetusdir & "\DJEP.TXT", True)
        fileva.WriteLine(My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor)
        fileva.Close()







        If My.Computer.FileSystem.FileExists(Environment.GetEnvironmentVariable("WINDIR") & "\NOVDIR.TXT") Then
            My.Computer.FileSystem.DeleteFile(Environment.GetEnvironmentVariable("WINDIR") & "\NOVDIR.TXT")
        End If


        Dim filevaE As System.IO.StreamWriter
        filevaE = My.Computer.FileSystem.OpenTextFileWriter(Environment.GetEnvironmentVariable("WINDIR") & "\NOVDIR.TXT", True)
        filevaE.WriteLine(novetusdir, System.Text.Encoding.Default)
        filevaE.Close()




        Dim ans As String
        ans = MsgBox("Would you like to add shortcuts?", 4 + 32)
        If ans = vbYes Then
            CreateShortCut(novetusdir & "\NEDESK.EXE", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Novetus DJEP 2.2", "")


        End If




     



        Label2.Text = "Installing."
        ResponsiveSleep(1000)




        ExtractArchive(Environment.CurrentDirectory & "\INSTALL.zip", novetusdir)









        Label8.Visible = False


        Label9.Visible = True

        Button3.Visible = True
        ProgressBar1.Visible = False
        Label2.Text = "Setup has been completed!"
        My.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Asterisk)
        Me.WindowState = FormWindowState.Normal
        Label10.Show()




    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
hmm2:
        FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer
        FolderBrowserDialog1.ShowNewFolderButton = False

        If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then


            If My.Computer.FileSystem.FileExists(FolderBrowserDialog1.SelectedPath & "\Novetus.exe") Then

                Dim info As System.Diagnostics.FileVersionInfo
                info = System.Diagnostics.FileVersionInfo.GetVersionInfo(FolderBrowserDialog1.SelectedPath & "\Novetus.exe")


                If info.FileVersion.ToString.Contains("1.0") Then
                    novetusdir = FolderBrowserDialog1.SelectedPath
                    Label4.Text = "Novetus Version: " & info.FileVersion.ToString
                    Button6.Enabled = True
                Else
                    MsgBox("Not a valid Novetus Version  Version of selected file: " & info.FileVersion.ToString, 0 + 16)
                    GoTo hmm2

                End If

            Else
                MsgBox("Not a valid Novetus folder", 0 + 16)
                GoTo hmm2

            End If

        Else

            MsgBox("Not a valid Novetus folder", 0 + 16)
            GoTo hmm2

        End If


        Label3.Text = "Novetus folder: " & novetusdir
    End Sub

    Private Sub ProgressBar1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProgressBar1.Click

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click


        Dim ans As String
        ans = MsgBox("Do you want to launch Novetus?", 4 + 32)
        If ans = vbYes Then
            Shell(novetusdir & "\novetus.exe")
        End If

        Application.Exit()

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Application.Exit()

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dialog1.Show()

    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Label7.Visible = True
        Label6.Visible = False
        Label11.Hide()

        TextBox1.Visible = True
        Button1.Visible = True
        Button7.Visible = True
        Button6.Visible = False
        Button2.Visible = False


        Button8.Visible = False
        Label2.Text = "Please accept these terms and conditions"
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Application.Exit()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Button6.Visible = True
        Button6.Enabled = False
        Button2.Visible = True
        Label3.Visible = True
        Label4.Visible = True
        Label5.Visible = True
        Button8.Hide()
        Label2.Text = "Please select your novetus folder."
        Label1.Visible = False
        Label6.Visible = True
        Label11.Visible = True

        Button5.Hide()

        Button9.Visible = False
        Dim f As FileInfo = New FileInfo(Environment.CurrentDirectory)
        Dim drive As String = Path.GetPathRoot(f.FullName)



        Label11.Text = "Free drive space: " & GetFreeSpace(drive) & "mb"
    End Sub

    Private Sub Form1_Load_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Environment.OSVersion.Version.Major < 6 Then

            MsgBox("Setup Cannot Start. Unsupported Windows Version", 0 + 16)
            Application.Exit()

        End If

    End Sub

    Private Sub RectangleShape1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RectangleShape1.Click

    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Dim webAddress As String = "https://discord.gg/WaVp6gq"
        Process.Start(webAddress)
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dialog2.Show()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class
