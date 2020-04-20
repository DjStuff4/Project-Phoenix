Class MainWindow 
    Dim fileReader As String
    Dim fileReader2 As String
    Dim novdir As String
    Dim capp As String


    Private Sub Image2_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Image2.MouseUp

        If System.IO.File.Exists(novdir & "\novetus.exe") Then
            Process.Start(novdir & "\novetus.exe")
       
        Else
            MsgBox("Feature not installed.")
        End If
            
     
    End Sub

    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        fileReader = My.Computer.FileSystem.ReadAllText(Environment.GetEnvironmentVariable("WINDIR") & "\novdir.txt")
        novdir = fileReader.Replace(vbCr, "").Replace(vbLf, "")




    End Sub

    Private Sub Image3_ImageFailed() Handles Image3.MouseUp
        Dim win2 As New Window2
        win2.ShowDialog()
    End Sub

    Private Sub Image8_ImageFailed()

    End Sub

    Private Sub Image8_ImageFailed_1(ByVal sender As System.Object, ByVal e As System.Windows.ExceptionRoutedEventArgs)

    End Sub

    Private Sub Image6_ImageFailed() Handles Image6.MouseUp
        System.Windows.Application.Current.Shutdown()
    End Sub

    Private Sub Image4_ImageFailed() Handles Image4.MouseUp
        


        If System.IO.File.Exists(novdir & "\necins\necins.exe") Then
            Process.Start(novdir & "\necins\necins.exe")
       
        Else
            MsgBox("Feature not installed.")
        End If
    End Sub

    Private Sub Image5_ImageFailed() Handles Image5.MouseUp
        If System.IO.File.Exists(novdir & "\auto.exe") Then
            Process.Start(novdir & "\auto.exe")
           
        Else
            MsgBox("Feature not installed.")
        End If
    End Sub

    Private Sub Image7_ImageFailed() Handles Image7.MouseUp
        If System.IO.File.Exists(novdir & "\novetus.exe") Then
            Process.Start(novdir & "\novetus.exe", "-sdk")
           
        Else
            MsgBox("Feature not installed.")
        End If
    End Sub
End Class
