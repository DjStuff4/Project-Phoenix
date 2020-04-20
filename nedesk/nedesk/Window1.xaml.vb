Public Class Window1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button1.Click
        If System.IO.File.Exists(Environment.CurrentDirectory & "\capp.txt") = True Then

            System.IO.File.Delete(Environment.CurrentDirectory & "\capp.txt")
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location)
            Application.Current.Shutdown()

        End If

        If CheckBox1.IsChecked = True Then
            Dim file As System.IO.StreamWriter
            file = My.Computer.FileSystem.OpenTextFileWriter(Environment.CurrentDirectory & "\capp.txt", True)
            file.WriteLine("1")
            file.Close()
        Else
            Dim file As System.IO.StreamWriter
            file = My.Computer.FileSystem.OpenTextFileWriter(Environment.CurrentDirectory & "\capp.txt", True)
            file.WriteLine("0")
            file.Close()
        End If

    End Sub

End Class
