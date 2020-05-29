Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Text

Class MainWindow
    Dim lol As String
    ' Environment.CurrentDirectory & "\shareddata\charcustom\" & ComboBox1.SelectedValue
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button1.Click
        Try
            If Not TextBox1.Text = "" Then
                If Not TextBox2.Text = "" Then

                    If Not lol = "" Then

                        My.Computer.Network.DownloadFile("http://www.roblox.com/asset/?id=" & TextBox1.Text, Environment.CurrentDirectory & "\temp.rbxm")
                        If IO.File.ReadAllText(Environment.CurrentDirectory & "\temp.rbxm").Contains("Object moved") Then

                            Dim s As String = IO.File.ReadAllText(Environment.CurrentDirectory & "\temp.rbxm")
                            Dim i As Integer = s.IndexOf(Chr(34))
                            Dim f As String = s.Substring(i + 1, s.IndexOf(Chr(34), i + 1) - i - 1)
                            FileSystem.Kill(Environment.CurrentDirectory & "\temp.rbxm")

                            My.Computer.Network.DownloadFile(f.Replace("https", "http"), Environment.CurrentDirectory & "\temp.rbxm")
                            CreateItem()
                        Else
                            CreateItem()
                        End If

                    Else
                        MsgBox("Please input a item type.", 0 + 16)
                    End If
                Else
                    MsgBox("Please input a item name.", 0 + 16)
                End If



            Else
                MsgBox("Please input a item ID.", 0 + 16)
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try


    End Sub

    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        If System.IO.File.Exists(Environment.CurrentDirectory & "\temp.rbxm") Then
            FileSystem.Kill(Environment.CurrentDirectory & "\temp.rbxm")
        End If
    End Sub

    Sub CreateItem()
        FileCopy(Environment.CurrentDirectory & "\temp.rbxm", Environment.CurrentDirectory & "\shareddata\charcustom\" & lol.ToString & "\" & TextBox2.Text & ".rbxm")

        FileCopy(Environment.CurrentDirectory & "\PREVIEW.PNG", Environment.CurrentDirectory & "\shareddata\charcustom\" & lol.ToString & "\" & TextBox2.Text & ".PNG")

        Dim path As String = Environment.CurrentDirectory & "\shareddata\charcustom\" & lol & "\" & TextBox2.Text & "_desc.txt"

        ' Create or overwrite the file.
        Dim fs As FileStream = File.Create(path)

        ' Add text to the file.
        Dim info As Byte() = New UTF8Encoding(True).GetBytes("Generated with FauxOnlineClothing")
        fs.Write(info, 0, info.Length)
        fs.Close()
        MsgBox("Completed!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
    End Sub

    Private Sub RadioButton1_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles RadioButton1.Checked
        lol = "tshirts"
    End Sub

    Private Sub RadioButton2_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles RadioButton2.Checked
        lol = "shirts"
    End Sub

    Private Sub RadioButton3_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles RadioButton3.Checked
        lol = "pants"
    End Sub
End Class
