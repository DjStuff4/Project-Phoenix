Imports System.Text
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Drawing


Class MainWindow

    <DllImport("kernel32")>
    Private Shared Function GetPrivateProfileString(ByVal section As String, ByVal key As String, ByVal def As String, ByVal retVal As StringBuilder, ByVal size As Integer, ByVal filePath As String) As Integer
    End Function


    Public Function GetIniValue(ByVal section As String, ByVal key As String, ByVal filename As String, Optional ByVal defaultValue As String = "") As String
        Dim sb As New StringBuilder(500)
        If GetPrivateProfileString(section, key, defaultValue, sb, sb.Capacity, filename) > 0 Then
            Return sb.ToString
        Else
            Return defaultValue
        End If
    End Function
    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        Dim _ini As New INI(Environment.CurrentDirectory & "\CONFIG.ini")
        For Each oof In _ini.GetSectionNames()
            ListBox1.Items.Add(oof)
        Next

     
    End Sub

    Private Sub ListBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles ListBox1.MouseDoubleClick

        If Not ListBox1.SelectedValue = "" Then


            Dim loc As String
            If GetIniValue(ListBox1.SelectedValue, "location", Environment.CurrentDirectory & "\CONFIG.ini") = "!currentdir!" Then
                loc = Environment.CurrentDirectory
                Try
                    Process.Start(loc & "\" & GetIniValue(ListBox1.SelectedValue, "EXE", Environment.CurrentDirectory & "\CONFIG.ini"), GetIniValue(ListBox1.SelectedValue, "ARGS", Environment.CurrentDirectory & "\CONFIG.ini"))
                Catch ex As Exception
                    MsgBox(ex.ToString)
                    MsgBox(loc & "\" & GetIniValue(ListBox1.SelectedValue, "EXE", Environment.CurrentDirectory & "\CONFIG.ini") & GetIniValue(ListBox1.SelectedValue, "ARGS", Environment.CurrentDirectory & "\CONFIG.ini"))
                End Try

            Else
                loc = GetIniValue(ListBox1.SelectedValue, "location", Environment.CurrentDirectory & "\CONFIG.ini")

                If loc.Replace("\\", "\").EndsWith("\") Then
                    Try
                        Process.Start(loc & GetIniValue(ListBox1.SelectedValue, "EXE", Environment.CurrentDirectory & "\CONFIG.ini"), GetIniValue(ListBox1.SelectedValue, "ARGS", Environment.CurrentDirectory & "\CONFIG.ini"))

                    Catch ex As Exception
                        MsgBox(ex.ToString)
                        MsgBox(loc & "\" & GetIniValue(ListBox1.SelectedValue, "EXE", Environment.CurrentDirectory & "\CONFIG.ini") & GetIniValue(ListBox1.SelectedValue, "ARGS", Environment.CurrentDirectory & "\CONFIG.ini"))

                    End Try

                Else
                    Try
                        Process.Start(loc & GetIniValue(ListBox1.SelectedValue, "EXE", Environment.CurrentDirectory & "\CONFIG.ini"), GetIniValue(ListBox1.SelectedValue, "ARGS", Environment.CurrentDirectory & "\CONFIG.ini"))

                    Catch ex As Exception
                        MsgBox(ex.ToString)
                        MsgBox(loc & "\" & GetIniValue(ListBox1.SelectedValue, "EXE", Environment.CurrentDirectory & "\CONFIG.ini") & GetIniValue(ListBox1.SelectedValue, "ARGS", Environment.CurrentDirectory & "\CONFIG.ini"))

                    End Try

                End If
            End If

        End If

    End Sub
    Private Function BitmapToImageSource(ByVal bitmap As System.Drawing.Bitmap) As BitmapImage
        Using memory As MemoryStream = New MemoryStream()
            bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp)
            memory.Position = 0
            Dim bitmapimage As BitmapImage = New BitmapImage()
            bitmapimage.BeginInit()
            bitmapimage.StreamSource = memory
            bitmapimage.CacheOption = BitmapCacheOption.OnLoad
            bitmapimage.EndInit()
            Return bitmapimage
        End Using
    End Function
    Private Sub ListBox1_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles ListBox1.SelectionChanged
        If Not ListBox1.SelectedValue = "" Then

            TextBox1.Text = GetIniValue(ListBox1.SelectedValue, "DESC", Environment.CurrentDirectory & "\CONFIG.ini")
                 End If


    End Sub

End Class
