Imports System.Windows.Forms

Public Class Dialog2


    Private Sub Dialog2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText(Environment.CurrentDirectory & "\DJSTUFFREADME.txt")
        RichTextBox1.Text = fileReader
    End Sub

    Private Sub RichTextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox1.TextChanged

    End Sub
End Class
