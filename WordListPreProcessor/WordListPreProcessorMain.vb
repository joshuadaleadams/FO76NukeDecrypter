Imports CommonClasses

Public Class WordListPreProcessorMain
    Dim maxthreads As Integer = 1000
    Dim messages(maxthreads - 1) As String
    Dim threads As List(Of Threading.Thread) = New List(Of Threading.Thread)
    Dim wordLists(maxthreads - 1) As List(Of String)
    Dim masterwordlist As List(Of String) = New List(Of String)
    Private Sub B_Start_Click(sender As Object, e As EventArgs) Handles B_Start.Click
        Dim ApplicationDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
        Dim wordlistPath = System.IO.Path.Combine(ApplicationDir, "words.txt")
        Dim processedpath = System.IO.Path.Combine(ApplicationDir, "processed.txt")
        For i As Integer = 0 To maxthreads - 1
            messages(i) = ""
            wordLists(i) = New List(Of String)
        Next
        Dim filereader As System.IO.TextReader = My.Computer.FileSystem.OpenTextFileReader(wordlistPath)
        Dim inputstring As String = filereader.ReadLine
        Try
            While Not inputstring = "ZZZ"
                Dim parsedInput As String = ""
                For Each c As Char In inputstring.ToLower
                    If "a" <= c And c <= "z" Then
                        parsedInput += c
                    End If
                Next
                Dim index As Integer = Math.Abs(parsedInput.GetHashCode) Mod maxthreads
                If Not masterwordlist.Contains(parsedInput) Then
                    wordLists(index).Add(parsedInput)
                    masterwordlist.Add(parsedInput)
                End If
                inputstring = filereader.ReadLine
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        For i As Integer = 0 To maxthreads - 1
            Dim j As Integer = i
            threads.Add(New Threading.Thread(Sub() ProcessWork(j)))
        Next
        For Each t As Threading.Thread In threads
            t.Start()
        Next
        For Each t As Threading.Thread In threads
            t.Join()
        Next
        threads.Clear()
        Dim filewriter As System.IO.TextWriter = My.Computer.FileSystem.OpenTextFileWriter(processedpath, False)
        For i As Integer = 0 To maxthreads - 1
            If Not messages(i) = "" Then
                filewriter.Write(messages(i))
            End If
        Next
        filewriter.Close()
    End Sub
    Private Sub ProcessWork(i As Integer)
        For Each s As String In wordLists(i)
            Dim w As Word = New Word(s)
            messages(i) += w.ToString + vbNewLine
        Next
    End Sub
End Class
