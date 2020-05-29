Imports System.IO

Class MainWindow
    Private dt As New Windows.Threading.DispatcherTimer()

    Private Shared ReadOnly color3uint8ToBrickColor As Dictionary(Of String, String) = New Dictionary(Of String, String) From {
    {"4294112243", "1"},
    {"4294901760", "1004"},
    {"4288986439", "119"},
    {"4294298928", "24"},
    {"4292511041", "106"},
    {"4291045404", "21"},
    {"4285215356", "104"},
    {"4279069100", "23"},
    {"4278226844", "107"},
    {"4283144011", "37"},
    {"4294506744", "1001"},
    {"4293256415", "208"},
    {"4291677645", "1002"},
    {"4288914085", "194"},
    {"4284702562", "199"},
    {"4279970357", "26"},
    {"4279308561", "1003"},
    {"4286549604", "1022"},
    {"4293040960", "105"},
    {"4293572754", "125"},
    {"4287986039", "153"},
    {"4287388575", "1023"},
    {"4285826717", "135"},
    {"4285438410", "102"},
    {"4286091394", "151"},
    {"4292330906", "5"},
    {"4294830733", "226"},
    {"4294946560", "1017"},
    {"4292511354", "101"},
    {"4293442248", "9"},
    {"4286626779", "11"},
    {"4279430868", "1018"},
    {"4288791692", "29"},
    {"4294954137", "1030"},
    {"4294967244", "1029"},
    {"4294953417", "1025"},
    {"4294928076", "1016"},
    {"4289832959", "1026"},
    {"4289715711", "1024"},
    {"4288672745", "1027"},
    {"4291624908", "1028"},
    {"4290887234", "1008"},
    {"4294967040", "1009"},
    {"4294901951", "1032"},
    {"4278190335", "1010"},
    {"4278255615", "1019"},
    {"4278255360", "1020"},
    {"4286340166", "217"},
    {"4291595881", "18"},
    {"4288700213", "38"},
    {"4284622289", "1031"},
    {"4290019583", "1006"},
    {"4278497260", "1013"},
    {"4290040548", "45"},
    {"4282023189", "1021"},
    {"4285087784", "192"},
    {"4289352960", "1014"},
    {"4288891723", "1007"},
    {"4289331370", "1015"},
    {"4280374457", "1012"},
    {"4278198368", "1011"},
    {"4280844103", "28"},
    {"4280763949", "141"}
}
    Dim path As String
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button1.Click
        Dim folderselect As New System.Windows.Forms.FolderBrowserDialog
        folderselect.ShowNewFolderButton = False
        If folderselect.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim fileEntries As String() = Directory.GetFiles(folderselect.SelectedPath, "*.rbxl", SearchOption.AllDirectories)
            ' Process the list of .txt files found in the directory. '
            Dim fileName As String

            For Each fileName In fileEntries
                If (System.IO.File.Exists(fileName)) Then
                    ListBox1.Items.Add(fileName)
                End If

            Next

            Dim fileEantries As String() = Directory.GetFiles(folderselect.SelectedPath, "*.rbxlx", SearchOption.AllDirectories)
            ' Process athe list of .txt files found in the directory. '
            Dim filaeName As String

            For Each filaeName In fileEantries
                If (System.IO.File.Exists(filaeName)) Then
                    ListBox1.Items.Add(filaeName)
                End If

            Next
            Button2.IsEnabled = True

        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button3.Click
   
        For Each item In ListBox1.Items
            'Do stuff
            ConvertMaps(File.ReadAllText(item.ToString), System.IO.Path.GetFileName(item.ToString))

        Next
        MsgBox("Conversion is complete.", 0 + vbInformation)

    End Sub
    Private Sub dt_Tick(ByVal sender As Object, ByVal e As EventArgs)

       

    End Sub

    Function ConvertMaps(ByVal filecontents As String, ByVal outfile As String)
        Try
            Dim terrainIndex As Integer = filecontents.IndexOf("<Item class=""Terrain""", StringComparison.Ordinal)

            If terrainIndex <> -1 Then
                Dim terrainEndIndex As Integer = filecontents.IndexOf("</Item>", terrainIndex, StringComparison.Ordinal)

                If terrainEndIndex <> -1 Then
                    filecontents = filecontents.Remove(terrainIndex, terrainEndIndex - terrainIndex + 7)
                End If
            End If

            If ColorCheckbox.IsChecked = True Then

                For Each entry As KeyValuePair(Of String, String) In color3uint8ToBrickColor
                    filecontents = filecontents.Replace("<Color3uint8 name=""Color3uint8"">" & entry.Key & "</Color3uint8>", "<int name=""BrickColor"">" & entry.Value & "</int>")
                Next
            End If

            If UnionCheckbox.IsChecked = False Then
                Dim unionIndex As Integer = filecontents.IndexOf("<Item class=""NonReplicatedCSGDictionaryService""", StringComparison.Ordinal)

                If unionIndex <> -1 Then
                    Dim binaryStringIndex As Integer = filecontents.IndexOf("<Item class=""BinaryStringValue""", unionIndex, StringComparison.Ordinal)

                    While binaryStringIndex <> -1
                        Dim binaryStringEndIndex As Integer = filecontents.IndexOf("</Item>", binaryStringIndex, StringComparison.Ordinal)

                        If binaryStringEndIndex <> -1 Then
                            filecontents = filecontents.Remove(binaryStringIndex, binaryStringEndIndex - binaryStringIndex + 7)
                        End If

                        binaryStringIndex = filecontents.IndexOf("<Item class=""BinaryStringValue""", binaryStringIndex, StringComparison.Ordinal)
                    End While
                End If
            End If

            If ScriptConvertCheckbox.IsChecked = True Then
                filecontents = filecontents.Replace("<ProtectedString name=""Source""><![CDATA[", "<ProtectedString name=""Source"">")
                filecontents = filecontents.Replace("]]></ProtectedString>", "</ProtectedString>")
                Dim scriptStartIndex As Integer = filecontents.IndexOf("<ProtectedString name=""Source"">", StringComparison.Ordinal)

                While scriptStartIndex <> -1
                    Dim scriptEndIndex As Integer = filecontents.IndexOf("</ProtectedString>", scriptStartIndex, StringComparison.Ordinal)

                    If scriptEndIndex <> -1 Then
                        Dim scriptBeforeContents As String = filecontents.Substring(scriptStartIndex + 31, scriptEndIndex - scriptStartIndex - 31)

                        If scriptBeforeContents.Length > 0 Then
                            Dim scriptAfterContents As String = scriptBeforeContents
                            scriptAfterContents = scriptAfterContents.Replace("""", "&quot;")
                            scriptAfterContents = scriptAfterContents.Replace("'", "&apos;")
                            scriptAfterContents = scriptAfterContents.Replace("<", "&lt;")
                            scriptAfterContents = scriptAfterContents.Replace(">", "&gt;")
                            filecontents = filecontents.Replace(scriptBeforeContents, scriptAfterContents)
                        End If
                    End If

                    scriptStartIndex = filecontents.IndexOf("<ProtectedString name=""Source"">", scriptEndIndex, StringComparison.Ordinal)
                End While
            End If

            If ChangeRbxassetidCheckbox.IsChecked = True Then
                filecontents = filecontents.Replace("rbxassetid://", "http://www.roblox.com/asset/?id=")
            End If

            If ConvertFoldersCheckbox.IsChecked = True Then
                filecontents = filecontents.Replace("<Item class=""Folder""", "<Item class=""Model""")
            End If
            Dim file As System.IO.StreamWriter
            file = My.Computer.FileSystem.OpenTextFileWriter(path & "\" & outfile, False)
            file.WriteLine(filecontents)
            file.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
       
    End Function

    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        Button2.IsEnabled = False
        Button3.IsEnabled = False
        Me.ResizeMode = Windows.ResizeMode.NoResize


    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button2.Click
        Dim folderselect As New System.Windows.Forms.FolderBrowserDialog
        folderselect.ShowNewFolderButton = False
        If folderselect.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            path = folderselect.SelectedPath

        End If
        Button3.IsEnabled = True
        Button1.IsEnabled = False
        Button2.IsEnabled = False

    End Sub

    Private Sub ConvertFoldersCheckbox_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles ConvertFoldersCheckbox.Checked

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button4.Click

        Dim lol As New Window1
        lol.ShowDialog()
    End Sub
End Class
