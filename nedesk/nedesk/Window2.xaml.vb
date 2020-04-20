Public Class Window2
    Dim fileReader As String
    Dim hmm As Integer
    Dim novdir As String
    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        fileReader = My.Computer.FileSystem.ReadAllText(Environment.GetEnvironmentVariable("WINDIR") & "\novdir.txt")
        novdir = fileReader.Replace(vbCr, "").Replace(vbLf, "")


        hmm = 0

        If System.IO.File.Exists(novdir & "\err.txt") Then


            If System.IO.File.Exists(novdir & "\changeloge.txt") Then

                If System.IO.File.Exists(novdir & "\EULA.txt") Then

                    If System.IO.File.Exists(novdir & "\DJSTUFFREADME.txt") Then

                        hmm = 1

                    End If
                End If
            End If
        End If
        
        If hmm = 0 Then
            MsgBox("Documentation not currently installed")
            Me.Hide()

        End If



        If System.IO.File.Exists(novdir & "\err.txt") Then
            TextBox1.Text = My.Computer.FileSystem.ReadAllText(novdir & "\err.txt")
        End If
        If System.IO.File.Exists(novdir & "\changeloge.txt") Then
            TextBox3.Text = My.Computer.FileSystem.ReadAllText(novdir & "\changeloge.txt")
        End If
        If System.IO.File.Exists(novdir & "\EULA.txt") Then
            TextBox4.Text = My.Computer.FileSystem.ReadAllText(novdir & "\eula.txt")
        End If
        If System.IO.File.Exists(novdir & "\DJSTUFFREADME.txt") Then

            TextBox5.Text = My.Computer.FileSystem.ReadAllText(novdir & "\DJSTUFFREADME.txt")
        End If


    End Sub
End Class
