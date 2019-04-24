Imports System.IO
Imports System.IO.Compression
Imports System.Net

Public Class Form1
    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Try
            WebBrowser1.Navigate(Replace(TextBox6.Text, "*PROJECTID*", TextBox1.Text))
        Catch ex As Exception
            WebBrowser1.Hide()
        End Try
    End Sub

    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        If TextBox6.Text.Contains("scratch.mit.edu/projects/embed/") Then

            If WebBrowser1.Document.Title.Contains("Scratch") Then
                WebBrowser1.Visible = False

            Else
                WebBrowser1.Visible = True
            End If
        Else
            WebBrowser1.Visible = True

        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If WindowState = FormWindowState.Maximized Then
            WindowState = FormWindowState.Normal
        End If

        If WebBrowser1.Visible = False Then
            Label3.Text = "Invalid"
        Else
            Label3.Text = ""
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FolderBrowserDialog1.SelectedPath = TextBox2.Text
        FolderBrowserDialog1.ShowDialog()
        TextBox2.Text = FolderBrowserDialog1.SelectedPath
    End Sub

    Private Sub FolderBrowserDialog1_HelpRequest(sender As Object, e As EventArgs) Handles FolderBrowserDialog1.HelpRequest

    End Sub

    Private Sub FolderBrowserDialog1_Disposed(sender As Object, e As EventArgs) Handles FolderBrowserDialog1.Disposed

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Directory.SetCurrentDirectory(My.Application.Info.DirectoryPath)
        If Label3.Visible = True OrElse TextBox1.Text = "" Then
            MsgBox("The scratch project I.D you entered Is invalid. Please Try again, Or If you don't know what a ""Project I.D"" is then read the first tab.", MsgBoxStyle.Critical)
            Exit Sub
        End If
        If Label6.Text.ToLower.Contains("click to browse") Then
            MsgBox("Select a taskbar icon. If you don't know where to get one there are plenty of icons online. You can try searching for ""ICO Converter"" to convert your picture to an icon.", MsgBoxStyle.Critical)
            Exit Sub
        End If
        If IO.File.Exists(Label6.Text) = False Then
            MsgBox("Select a taskbar icon. If you don't know where to get one there are plenty of icons online. You can try searching for ""ICO Converter"" to convert your picture to an icon.", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If ListBox1.SelectedItem = "" Then
            MsgBox("Select a look.", MsgBoxStyle.Critical)
            Exit Sub
        End If
        If IO.File.Exists("runthing.exe") = False Then
            Dim address2 As String = "http://edxtech.uk/data/scratchtoexe/valid/invalidate.php?code=" & File.ReadAllText("install.dat")
            Dim client2 As WebClient = New WebClient()
            Dim reader2 As StreamReader = New StreamReader(client2.OpenRead(address2))
            MsgBox("runthing.exe does not exist, your installation is currupted. Therefore you have been banned. Please reinstall.", MsgBoxStyle.Critical)
            End
        End If

        Try
            My.Computer.FileSystem.WriteAllText(TextBox2.Text & "\test.dat", "test", False)
            Threading.Thread.Sleep(1000)
            Kill(TextBox2.Text & "\test.dat")
        Catch ex As Exception
            MsgBox("The destination folder is invalid. Please select another.", MsgBoxStyle.Critical)
            Exit Sub
        End Try
        Dim loc = My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\EDXSTETEMP"
        Try
            Me.Icon = New Icon(Label6.Text)

        Catch ex As Exception
            MsgBox("The icon you've selected can't be used because it's corrupted or invalid. If you don't know where to get a compatible icon there are plenty of icons online. You can try searching for ""ICO Converter"" to convert your picture to an icon.", MsgBoxStyle.Critical)
            Exit Sub
        End Try
        Try

            Try
                IO.Directory.CreateDirectory(loc)
            Catch ex As Exception

                If IO.Directory.Exists(loc) Then

                    For Each fil As String In IO.Directory.GetFiles(loc)
                        Try
                            Kill(fil)
                        Catch ex2 As Exception

                        End Try
                    Next
                Else
                    MsgBox("There was an unknown error creating a temp folder. Try running the tool as an administrator or contact me for more help. " & ex.ToString, MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End Try
            FileCopy(Label6.Text, loc & "\icon.ico")

            My.Computer.FileSystem.WriteAllText(loc & "\title.dat", TextBox3.Text, False)
            My.Computer.FileSystem.WriteAllText(loc & "\creator.dat", TextBox4.Text, False)
            My.Computer.FileSystem.WriteAllText(loc & "\link.dat", "https://scratch.mit.edu/projects/embed/" & TextBox1.Text & "/?autostart=true", False)
            My.Computer.FileSystem.WriteAllText(loc & "\nointernet.dat", TextBox5.Text, False)
            My.Computer.FileSystem.WriteAllText(loc & "\pannel.dat", ListBox1.SelectedItem, False)
            If CheckBox1.Checked = False Then
                My.Computer.FileSystem.WriteAllText(loc & "\hideinfo.dat", ListBox1.SelectedItem, False)
            End If
            'Compress CORE.dll
            Dim startPath As String = loc
            Dim zipPath As String = TextBox2.Text & "\CORE.dll"

            ZipFile.CreateFromDirectory(startPath, zipPath)

            FileCopy("runthing.exe", TextBox2.Text & "\" & TextBox3.Text & ".exe")
            Process.Start(TextBox2.Text)
            MsgBox("Built and compiled EXE! Remember not to forget CORE.dll when moving or compressing it.", MsgBoxStyle.Information)
            End
        Catch ex As Exception
            My.Computer.Clipboard.SetText(ex.ToString & vbCrLf & "Computer specs:" & My.Computer.Info.OSFullName & "," & My.Computer.Info.TotalPhysicalMemory & " RAM," & My.Computer.Info.InstalledUICulture.ToString)
            MsgBox("Failed to build EXE, error copied to clipboard.", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs)
        help.ShowDialog()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Threading.Thread.Sleep(2000)
        IO.Directory.SetCurrentDirectory(My.Application.Info.DirectoryPath)
        If IO.File.Exists("install.dat") = False Then
            MsgBox("Could not verify this app with the server. Please reinstall the offical version at www.edxtech.uk/data/scratchtoexe.", MsgBoxStyle.Critical, "Who are you again?")
            End
        End If

        Try
            Dim address As String = "http://edxtech.uk/data/scratchtoexe/valid/isapp.php?code=" & File.ReadAllText("install.dat")
            Dim client As WebClient = New WebClient()
            Dim reader As StreamReader = New StreamReader(client.OpenRead(address))
            Dim textthing = reader.ReadToEnd
            If textthing = "" Then
                MsgBox("This software has been BANNED. Please reinstall the offical version at www.edxtech.uk/app/ste.", MsgBoxStyle.Critical, "BANNED LOL GET REKT SON 360 NO-SCOPED!!")
                End
            Else
                If textthing.Contains("Computer specs:" & My.Computer.Info.OSFullName & "," & My.Computer.Info.TotalPhysicalMemory & " RAM " & My.Computer.Name) = False Then
                    Dim address2 As String = "http://edxtech.uk/data/scratchtoexe/valid/invalidate.php?code=" & File.ReadAllText("install.dat")
                    Dim client2 As WebClient = New WebClient()
                    Dim reader2 As StreamReader = New StreamReader(client.OpenRead(address2))

                    MsgBox("This application has been banned because this computer does not have the same specs as registered. Please reinstall using the offical link www.edxtech.uk/app/ste", MsgBoxStyle.Critical)
                    End
                End If

            End If
        Catch ex As Exception
            MsgBox("Failed To connect To server, please check your internet connection and try again", MsgBoxStyle.Critical, "Can't start")
            End
        End Try
        Label1.Text = Replace(Label1.Text, "*PROGRAMID*", File.ReadAllText("install.dat"))
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub TabPage4_Click(sender As Object, e As EventArgs) Handles TabPage4.Click

    End Sub

    Private Sub Label6_Click_1(sender As Object, e As EventArgs) Handles Label6.Click
        OpenFileDialog1.ShowDialog()
        Label6.Text = OpenFileDialog1.FileName
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            MsgBox("Note: Phosphorus player is faster but doesn't support lists. If you want full support, please select 'Scratch'", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = True Then
            MsgBox("Note: Phosphorus player is faster but doesn't support lists. This also hides the green flag bar. If you want full support, please select 'Scratch'", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click

    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedTab Is TabPage4 Then
            Me.Height = 286
        Else
            Me.Height = 353
        End If
    End Sub
End Class