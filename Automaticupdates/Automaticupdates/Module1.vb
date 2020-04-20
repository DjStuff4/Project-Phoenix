Imports System.Net

Module Module1

    Sub Main()
        Dim remoteUri As String = "http://toastgame.dx.am/novver.txt"
        Dim fileName As String = Environment.GetEnvironmentVariable("WINDIR") & "\novver.txt"
        Dim password As String = "..."
        Dim username As String = "..."


        Using client As New WebClient()

            client.Credentials = New NetworkCredential(username, password)
            client.DownloadFile(remoteUri, fileName)
        End Using


        Dim fileReaderaa As String
        fileReaderaa = My.Computer.FileSystem.ReadAllText(Environment.GetEnvironmentVariable("WINDIR") & "\NOVDIR.txt")


        Dim fileReader As String = fileReaderaa.Replace(vbCr, "").Replace(vbLf, "")

        Dim fileReadaera As String
        fileReadaera = My.Computer.FileSystem.ReadAllText(fileReader & "\djep.txt")


        Dim fileReadaer As String = fileReadaera.Replace(vbCr, "").Replace(vbLf, "")



        Dim fileReade2r As String
        fileReade2r = My.Computer.FileSystem.ReadAllText(Environment.GetEnvironmentVariable("WINDIR") & "\NOVVER.txt")


        If fileReadaer = fileReade2r Then

            MsgBox("Djstuffs Novetus pack is up to date!")
        Else
            Dim ans As String
            ans = MsgBox("A new version of Djstuffs Novetus Pack Is Out! Would you like to upgrade? Your installed version: " & fileReadaer & " New Version:" & fileReade2r, vbYesNo)
            If ans = vbYes Then
                remoteUri = "http://toastgame.dx.am/novlink.txt"
                fileName = Environment.GetEnvironmentVariable("WINDIR") & "\novlink.txt"
                password = "..."
                username = "..."


                Using client As New WebClient()

                    client.Credentials = New NetworkCredential(username, password)
                    client.DownloadFile(remoteUri, fileName)
                End Using


                Dim fileReade2raaaa As String
                fileReade2raaaa = My.Computer.FileSystem.ReadAllText(Environment.GetEnvironmentVariable("WINDIR") & "\novlink.txt")

                Process.Start(fileReade2raaaa)
            End If
        End If



      
    End Sub

End Module
