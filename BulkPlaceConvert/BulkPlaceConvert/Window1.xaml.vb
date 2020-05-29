Public Class Window1

    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        Me.ResizeMode = Windows.ResizeMode.NoResize
        WebBrowser1.Navigate("http://www.designcontest.com")
    End Sub
End Class
