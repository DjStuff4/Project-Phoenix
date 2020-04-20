Imports Dropbox
Imports System.IO.Compression
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports System.Net

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

    Dim Bruh As String = Environment.CurrentDirectory & "\bigbruh.zip"
    Dim Client As String = Environment.CurrentDirectory & "\clients.zip"

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
        fileReader = My.Computer.FileSystem.ReadAllText(Environment.CurrentDirectory & "\eula.txt")
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
        ans = MsgBox("Would you like to add shortcuts and install extras or configure setup to install specfic things?", 4 + 32)
        If ans = vbYes Then
            Form2.ShowDialog()

        End If


        Label2.Text = "Copying Splashes, EULA, Changelog, Error List"
        ResponsiveSleep(1000)
        If Form2.CheckBox12.Checked = True Then
            If Form2.CheckBox13.Checked = True Then

                File.Copy(Environment.CurrentDirectory & "\splashes.txt", novetusdir & "\config\splashes.txt", True)

                File.Copy(Environment.CurrentDirectory & "\err.txt", novetusdir & "\err.txt", True)
                File.Copy(Environment.CurrentDirectory & "\changeloge.txt", novetusdir & "\changeloge.txt", True)
                File.Copy(Environment.CurrentDirectory & "\eula.txt", novetusdir & "\eula.txt", True)


                File.Copy(Environment.CurrentDirectory & "\DJSTUFFREADME.txt", novetusdir & "\DJSTUFFREADME.txt", True)

            End If
        Else


            File.Copy(Environment.CurrentDirectory & "\splashes.txt", novetusdir & "\config\splashes.txt", True)

            File.Copy(Environment.CurrentDirectory & "\err.txt", novetusdir & "\err.txt", True)
            File.Copy(Environment.CurrentDirectory & "\changeloge.txt", novetusdir & "\changeloge.txt", True)
            File.Copy(Environment.CurrentDirectory & "\eula.txt", novetusdir & "\eula.txt", True)


            File.Copy(Environment.CurrentDirectory & "\DJSTUFFREADME.txt", novetusdir & "\DJSTUFFREADME.txt", True)

        End If

        If Form2.CheckBox12.Checked = True Then
            If Form2.CheckBox18.Checked = True Then




                Label2.Text = "Installing ESZ Mappack"
                ResponsiveSleep(1000)






                If (Not System.IO.Directory.Exists(novetusdir & "\maps\ESZ MapPack")) Then
                    System.IO.Directory.CreateDirectory(novetusdir & "\maps\ESZ MapPack")
                End If





                ExtractArchive(Environment.CurrentDirectory & "\esz.zip", novetusdir & "\maps\ESZ MapPack")


            End If
        Else
          


            Label2.Text = "Installing Extra Clients"
            ResponsiveSleep(1000)









            ExtractArchive(Environment.CurrentDirectory & "\cl.zip", ClientFolder)


        End If

        If Form2.CheckBox12.Checked = True Then
            If Form2.CheckBox7.Checked = True Then
                Label2.Text = "Installing P4RIS Mappack"
                ResponsiveSleep(1000)






                If (Not System.IO.Directory.Exists(novetusdir & "\maps\P4RIS Mappack")) Then
                    System.IO.Directory.CreateDirectory(novetusdir & "\maps\P4RIS Mappack")
                End If





                ExtractArchive(Environment.CurrentDirectory & "\P.zip", novetusdir & "\maps\P4RIS Mappack")


            End If
        Else

    Label2.Text = "Installing P4RIS Mappack"
            ResponsiveSleep(1000)






            If (Not System.IO.Directory.Exists(novetusdir & "\maps\P4RIS Mappack")) Then
                System.IO.Directory.CreateDirectory(novetusdir & "\maps\P4RIS Mappack")
            End If





            ExtractArchive(Environment.CurrentDirectory & "\P.zip", novetusdir & "\maps\P4RIS Mappack")


        End If




        If Form2.CheckBox12.Checked = True Then
            If Form2.CheckBox23.Checked = True Then
                Label2.Text = "Installing ESZ Mappack"
                ResponsiveSleep(1000)






                If (Not System.IO.Directory.Exists(novetusdir & "\maps\ESZ MapPack")) Then
                    System.IO.Directory.CreateDirectory(novetusdir & "\maps\ESZ MapPack")
                End If





                ExtractArchive(Environment.CurrentDirectory & "\esz.zip", novetusdir & "\maps\ESZ MapPack")


            End If
        Else
       

            Label2.Text = "Installing ESZ Mappack"
            ResponsiveSleep(1000)






            If (Not System.IO.Directory.Exists(novetusdir & "\maps\ESZ MapPack")) Then
                System.IO.Directory.CreateDirectory(novetusdir & "\maps\ESZ MapPack")
            End If





            ExtractArchive(Environment.CurrentDirectory & "\esz.zip", novetusdir & "\maps\ESZ MapPack")


        End If





        If Form2.CheckBox12.Checked = True Then
            If Form2.CheckBox11.Checked = True Then
                Label2.Text = "Unpacking Clients"
                ResponsiveSleep(1000)
                ExtractArchive(Client, ClientFolder)
                Label2.Text = "Unpacking 2007ES"
                ResponsiveSleep(1000)
                ExtractArchive(Environment.CurrentDirectory & "\2007MM.zip", ClientFolder)
            End If

        Else

            Label2.Text = "Unpacking Clients"
            ResponsiveSleep(1000)
            ExtractArchive(Client, ClientFolder)
            Label2.Text = "Unpacking 2007MM"
            ResponsiveSleep(1000)
            ExtractArchive(Environment.CurrentDirectory & "\2007MM.zip", ClientFolder)
            Label2.Text = "Patching 2011E+ Textures"
            ResponsiveSleep(1000)

            My.Computer.FileSystem.DeleteDirectory(novetusdir & "\clients\2011E+\content\textures", FileIO.DeleteDirectoryOption.DeleteAllContents)


            If (System.IO.Directory.Exists(novetusdir & "\clients\ui")) Then

                My.Computer.FileSystem.DeleteDirectory(novetusdir & "\clients\ui", FileIO.DeleteDirectoryOption.DeleteAllContents)

            End If


            ExtractArchive(Environment.CurrentDirectory & "\2011ETexture.zip", novetusdir & "\clients\2011E+\content\textures")
        End If



        ExtractArchive(Environment.CurrentDirectory & "\oof.zip", novetusdir)


        If Form2.CheckBox12.Checked = True Then
            If Form2.CheckBox22.Checked = True Then
                If My.Computer.FileSystem.FileExists(Environment.CurrentDirectory & "\rbxms.zip") Then


                    Label2.Text = "Unpacking Roblox Models"
                    ResponsiveSleep(1000)
                    ExtractArchive(Environment.CurrentDirectory & "\rbxms.zip", novetusdir & "\models")

                Else

                    Label2.Text = "Skipping Roblox Models."
                    ResponsiveSleep(1000)

                End If
            End If
        Else

            If My.Computer.FileSystem.FileExists(Environment.CurrentDirectory & "\rbxms.zip") Then


                Label2.Text = "Unpacking Roblox Models"
                ResponsiveSleep(1000)
                ExtractArchive(Environment.CurrentDirectory & "\rbxms.zip", novetusdir & "\models")

            Else

                Label2.Text = "Skipping Roblox Models."
                ResponsiveSleep(1000)

            End If
        End If




        If Form2.CheckBox12.Checked = True Then
            If Form2.CheckBox17.Checked = True Then
                Label2.Text = "Unpacking Hats,Faces"
                ResponsiveSleep(1000)
                ExtractArchive(Bruh, BruhFolder)

            End If
        Else
            Label2.Text = "Unpacking Hats,Faces"
            ResponsiveSleep(1000)
            ExtractArchive(Bruh, BruhFolder)


        End If




   





        Label2.Text = "Creating Mappack Folder"
        ResponsiveSleep(1000)

        If Form2.CheckBox12.Checked = True Then
            If Form2.CheckBox14.Checked = True Then
                If (Not System.IO.Directory.Exists(novetusdir & "\maps\MAO Mappack")) Then
                    System.IO.Directory.CreateDirectory(novetusdir & "\maps\MAO Mappack")
                End If
                Label2.Text = "Unpacking Maps (MAO)"
                ResponsiveSleep(1000)
                ExtractArchive(Environment.CurrentDirectory & "\Mao.zip", novetusdir & "\maps\MAO Mappack")


            End If

        Else
            If (Not System.IO.Directory.Exists(novetusdir & "\maps\MAO Mappack")) Then
                System.IO.Directory.CreateDirectory(novetusdir & "\maps\MAO Mappack")
            End If
            Label2.Text = "Unpacking Maps (MAO)"
            ResponsiveSleep(1000)
            ExtractArchive(Environment.CurrentDirectory & "\Mao.zip", novetusdir & "\maps\MAO Mappack")



        End If


        If Form2.CheckBox12.Checked = True Then
            If Form2.CheckBox16.Checked = True Then
                Label2.Text = "Unpacking Maps (djstuff)"
                ResponsiveSleep(1000)
                If (Not System.IO.Directory.Exists(novetusdir & "\maps\DjStuff MapPack")) Then
                    System.IO.Directory.CreateDirectory(novetusdir & "\maps\DjStuff MapPack")
                End If





                ExtractArchive(Environment.CurrentDirectory & "\maps.zip", novetusdir & "\maps\DjStuff MapPack")

            End If
        Else
            Label2.Text = "Unpacking Maps (djstuff)"
            ResponsiveSleep(1000)
            If (Not System.IO.Directory.Exists(novetusdir & "\maps\DjStuff MapPack")) Then
                System.IO.Directory.CreateDirectory(novetusdir & "\maps\DjStuff MapPack")
            End If





            ExtractArchive(Environment.CurrentDirectory & "\maps.zip", novetusdir & "\maps\DjStuff MapPack")


        End If





        If Form2.CheckBox21.Checked = True Then

            Dim directoryName As String = Environment.CurrentDirectory
            For Each deleteFile In Directory.GetFiles(directoryName, "*.zip", SearchOption.TopDirectoryOnly)
                File.Delete(deleteFile)
            Next

        End If




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
        Button10.Hide()
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
        Dim webAddress As String = "https://discord.gg/mzKnXdf"
        Process.Start(webAddress)
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dialog2.Show()
    End Sub
End Class
