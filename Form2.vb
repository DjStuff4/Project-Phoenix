Imports System.Runtime.InteropServices
Imports IWshRuntimeLibrary
Imports ICSharpCode.SharpZipLib.Zip
Imports System.IO
Imports Microsoft.Win32

Public Class Form2


    Dim novetusdirs1 As String


    Dim novetusdirs As String
    Dim allUsers As String

    Dim StartMenuFolder As String
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

    Private Function CreateShortCut(ByVal TargetName As String, ByVal ShortCutPath As String, ByVal ShortCutName As String, ByVal arg As String) As Boolean


        Dim WshShell As WshShellClass = New WshShellClass

        Dim MyShortcut As IWshRuntimeLibrary.IWshShortcut

        ' The shortcut will be created on the desktop


        MyShortcut = CType(WshShell.CreateShortcut(ShortCutPath & "\" & ShortCutName & ".lnk"), IWshRuntimeLibrary.IWshShortcut)

        MyShortcut.TargetPath = TargetName

        MyShortcut.Arguments = arg

        MyShortcut.Save()


    End Function
    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        novetusdirs1 = Form1.Label3.Text

        CheckBox23.Enabled = False

        CheckBox23.Checked = False




        CheckBox18.Enabled = False

        CheckBox18.Checked = False

        CheckBox22.Enabled = False
        CheckBox22.Checked = False

        CheckBox7.Enabled = False


        CheckBox7.Checked = False
        novetusdirs = novetusdirs1.Replace("Novetus folder: ", "")
        StartMenuFolder = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu)
        CheckBox9.Enabled = False
        CheckBox10.Enabled = False
        CheckBox9.Checked = False
        CheckBox10.Checked = False
        CheckBox13.Enabled = False
        CheckBox11.Enabled = False
        CheckBox14.Enabled = False
        CheckBox16.Enabled = False
        CheckBox17.Enabled = False
        CheckBox17.Enabled = False
        CheckBox19.Enabled = False
        CheckBox20.Enabled = False

        CheckBox19.Checked = False

        CheckBox20.Checked = False
        CheckBox13.Checked = False
        CheckBox11.Checked = False
        CheckBox14.Checked = False
        CheckBox16.Checked = False
        CheckBox17.Checked = False
        CheckBox17.Checked = False

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If CheckBox1.Checked = True Then
            CreateShortCut(novetusdirs & "\novetus.exe", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Novetus Overhaul", "")

        End If
        If CheckBox2.Checked = True Then
            CreateShortCut(novetusdirs & "\Novetus.exe", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Novetus SDK", "-sdk")
        End If
        If CheckBox3.Checked = True Then
            CreateShortCut(novetusdirs & "\err.txt", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Novetus Overhaul Errors", "")
            CreateShortCut(novetusdirs & "\CHANGELOGe.txt", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Novetus Overhaul Changelog", "")
            CreateShortCut(novetusdirs & "\eula.txt", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Novetus Overhaul EULA", "")

        End If









        If CheckBox6.Checked = True Then
            CreateShortCut(novetusdirs & "\novetus.exe", StartMenuFolder, "Novetus Overhaul", "")
        End If
        If CheckBox5.Checked = True Then
            CreateShortCut(novetusdirs & "\Novetus.exe", StartMenuFolder, "Novetus SDK", "-sdk")
        End If
        If CheckBox4.Checked = True Then
            CreateShortCut(novetusdirs & "\err.txt", StartMenuFolder, "Novetus Overhaul Errors", "")
            CreateShortCut(novetusdirs & "\CHANGELOGe.txt", StartMenuFolder, "Novetus Overhaul Changelog", "")
            CreateShortCut(novetusdirs & "\eula.txt", StartMenuFolder, "Novetus Overhaul EULA", "")
        End If




        If CheckBox8.Checked = True Then
            ExtractArchive(Environment.CurrentDirectory & "\necins.zip", novetusdirs & "\necins")


        End If
        If CheckBox9.Checked = True Then
            CreateShortCut(novetusdirs & "\necins\necins.exe", StartMenuFolder, "NEC Installer", "")
        End If
        If CheckBox10.Checked = True Then
            CreateShortCut(novetusdirs & "\necins\necins.exe", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "NEC Installer", "")
        End If

        If CheckBox15.Checked = True Then
            ExtractArchive(Environment.CurrentDirectory & "\auto.zip", novetusdirs)
            Dim regKey As Microsoft.Win32.RegistryKey
            regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
            regKey.SetValue("Autoupdatedjstuff", novetusdirs & "\auto.exe")
            regKey.Close()

        End If


        If CheckBox20.Checked = True Then
            CreateShortCut(novetusdirs & "\auto.exe", StartMenuFolder, "Check for Djstuff Novetus expansion pack updates", "")
        End If

        If CheckBox19.Checked = True Then
            CreateShortCut(novetusdirs & "\auto.exe", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Check for Djstuff Novetus expansion pack updates", "")
        End If
        Me.Hide()


    End Sub

    Private Sub CheckBox7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub CheckBox7_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged

    End Sub

    Private Sub CheckBox8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox8.CheckedChanged
        If CheckBox8.Checked = True Then
            CheckBox9.Enabled = True
            CheckBox10.Enabled = True
            CheckBox9.Checked = False
            CheckBox10.Checked = False
        Else
            CheckBox9.Checked = False
            CheckBox10.Checked = False
            CheckBox9.Enabled = False
            CheckBox10.Enabled = False
        End If
    End Sub

    Private Sub CheckBox12_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox12.CheckedChanged
        If CheckBox12.Checked = True Then
            CheckBox13.Enabled = True
            CheckBox11.Enabled = True
            CheckBox14.Enabled = True
            CheckBox16.Enabled = True
            CheckBox17.Enabled = True
            CheckBox17.Enabled = True
            CheckBox22.Enabled = True

            CheckBox7.Enabled = True

            CheckBox3.Enabled = False
            CheckBox4.Enabled = False


            CheckBox3.Checked = False

            CheckBox4.Checked = False





            CheckBox18.Enabled = True

            CheckBox18.Checked = False



            CheckBox23.Enabled = True

            CheckBox23.Checked = False



            CheckBox3.Checked = False

            CheckBox4.Checked = False

    
        Else
            CheckBox13.Enabled = False
            CheckBox11.Enabled = False
            CheckBox14.Enabled = False
            CheckBox16.Enabled = False
            CheckBox17.Enabled = False
            CheckBox17.Enabled = False
           
            CheckBox23.Enabled = False

            CheckBox23.Checked = False




            CheckBox18.Enabled = False

            CheckBox18.Checked = False




            CheckBox22.Enabled = False
            CheckBox22.Checked = False


            CheckBox7.Enabled = False


            CheckBox7.Checked = False

            CheckBox3.Enabled = True
            CheckBox4.Enabled = True

            CheckBox13.Checked = False
            CheckBox11.Checked = False
            CheckBox14.Checked = False
            CheckBox16.Checked = False
            CheckBox17.Checked = False
            CheckBox17.Checked = False
        End If
    End Sub

    Private Sub CheckBox5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox5.CheckedChanged

    End Sub

    Private Sub CheckBox15_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox15.CheckedChanged
        If CheckBox15.Checked = True Then
            CheckBox19.Enabled = True
            CheckBox20.Enabled = True

        Else
            CheckBox19.Enabled = False
            CheckBox20.Enabled = False

            CheckBox19.Checked = False

            CheckBox20.Checked = False
        End If
    End Sub

    Private Sub CheckBox22_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox22.CheckedChanged

    End Sub

    Private Sub CheckBox23_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub CheckBox13_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox13.CheckedChanged
        If CheckBox13.Checked = False Then
            CheckBox3.Enabled = False
            CheckBox4.Enabled = False


            CheckBox3.Checked = False

            CheckBox4.Checked = False

        Else

            CheckBox3.Enabled = True
            CheckBox4.Enabled = True
        End If
    End Sub

    Private Sub CheckBox23_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox23.CheckedChanged
     

    End Sub

    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub CheckBox7_CheckedChanged_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox7.CheckedChanged

    End Sub
End Class