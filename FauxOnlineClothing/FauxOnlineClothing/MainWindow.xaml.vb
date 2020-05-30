Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Text
Imports System.Net

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
        If System.IO.File.Exists(Environment.CurrentDirectory & "\tempdesc.html") Then
            FileSystem.Kill(Environment.CurrentDirectory & "\tempdesc.html")
        End If
        If System.IO.File.Exists(Environment.CurrentDirectory & "\PREVIEW.PNG") Then
            FileSystem.Kill(Environment.CurrentDirectory & "\PREVIEW.PNG")
        End If
    End Sub

    Sub CreateItem()
        pullitemdesc()

        FileCopy(Environment.CurrentDirectory & "\temp.rbxm", Environment.CurrentDirectory & "\shareddata\charcustom\" & lol.ToString & "\" & TextBox2.Text & ".rbxm")

        FileCopy(Environment.CurrentDirectory & "\PREVIEW.PNG", Environment.CurrentDirectory & "\shareddata\charcustom\" & lol.ToString & "\" & TextBox2.Text & ".PNG")



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

    Private Sub RadioButton4_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        lol = "hats"
    End Sub

    Sub pullitemdesc()


        Dim request As WebRequest = WebRequest.Create("https://www.roblox.com/catalog/" & TextBox1.Text)
        Using response As WebResponse = request.GetResponse()
            Using reader As New StreamReader(response.GetResponseStream())
                Dim html As String = reader.ReadToEnd()
                File.WriteAllText(Environment.CurrentDirectory & "\tempdesc.html", html)
            End Using
        End Using



        If IO.File.ReadAllText(Environment.CurrentDirectory & "\tempdesc.html").Contains("Object moved") Then

            Dim sa As String = IO.File.ReadAllText(Environment.CurrentDirectory & "\tempdesc.html")
            Dim ia As Integer = sa.IndexOf(Chr(34))
            Dim fa As String = sa.Substring(ia + 1, sa.IndexOf(Chr(34), ia + 1) - ia - 1)
            FileSystem.Kill(Environment.CurrentDirectory & "\tempdesc.html")

            Dim reaquest As WebRequest = WebRequest.Create("https://www.roblox.com/catalog/" & TextBox1.Text)
            Using response As WebResponse = reaquest.GetResponse()
                Using reader As New StreamReader(response.GetResponseStream())
                    Dim html As String = reader.ReadToEnd()
                    File.WriteAllText(Environment.CurrentDirectory & "\tempdesc.html", html)
                End Using
            End Using


        End If




        Dim s As String = IO.File.ReadAllText(Environment.CurrentDirectory & "\tempdesc.html")
        Dim i As Integer = s.IndexOf("class=""description-content font-body text wait-for-i18n-format-render "">")



        Dim f As String = s.Substring(i + 1, s.IndexOf("</p>", i + 1) - i - 1)



        f = f.Replace("lass=""description-content font-body text wait-for-i18n-format-render "">", "")

        Dim lolwut As String = Environment.CurrentDirectory & "\shareddata\charcustom\" & lol.ToString & "\" & TextBox2.Text & "_desc.txt"

        Dim fileA As System.IO.StreamWriter
        fileA = My.Computer.FileSystem.OpenTextFileWriter(lolwut, False)
        fileA.WriteLine(f)
        fileA.Close()

        pullpreview()

    End Sub


    Function After(ByVal value As String, ByVal a As String) As String
        ' Get index of argument and return substring after its position.
        Dim posA As Integer = value.LastIndexOf(a)
        If posA = -1 Then
            Return ""
        End If
        Dim adjustedPosA As Integer = posA + a.Length
        If adjustedPosA >= value.Length Then
            Return ""
        End If
        Return value.Substring(adjustedPosA)
    End Function
    Sub pullpreview()

        Dim s As String = IO.File.ReadAllText(Environment.CurrentDirectory & "\tempdesc.html")
        Dim i As Integer = s.IndexOf("asset-thumbnail-3d/json?assetId=2665843588"" >")



        Dim f As String = s.Substring(i + 1, s.IndexOf("'/></span>", i + 1) - i - 1)
        f = After(f, "src='")


        My.Computer.Network.DownloadFile(f.Replace("https", "http"), Environment.CurrentDirectory & "\preview.png")

    End Sub

End Class
