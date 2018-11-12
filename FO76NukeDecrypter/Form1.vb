Imports CommonClasses

Public Class FO76DecryptorMain
    Dim maxthreads As Integer = 50
    Dim wordcount As Dictionary(Of Integer, List(Of Word)) = New Dictionary(Of Integer, List(Of Word))
    Dim results As List(Of List(Of DecryptResult)) = New List(Of List(Of DecryptResult))
    Dim threads As List(Of Threading.Thread) = New List(Of Threading.Thread)
    Dim masterWordList As List(Of Word) = New List(Of Word)
    Dim totalthreads As Integer = maxthreads
    Dim exiting As Boolean = False
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ApplicationDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
        Dim LogfilePath = System.IO.Path.Combine(ApplicationDir, "processed.txt")
        Dim filereader As System.IO.TextReader = My.Computer.FileSystem.OpenTextFileReader(LogfilePath)
        Dim inputstring As String = filereader.ReadLine
        For i As Integer = 0 To maxthreads - 1
            results.Add(New List(Of DecryptResult))
        Next
        Dim index As Integer = 0
        While Not inputstring = "end file"
            Dim w As Word = New Word
            w.InitFromString(inputstring)
            If Not wordcount.ContainsKey(w.Count) Then
                wordcount.Add(w.Count, New List(Of Word))
            End If
            wordcount(w.Count).Add(w)
            masterWordList.Add(w)
            index = (index + 1) Mod maxthreads
            inputstring = filereader.ReadLine
        End While

        TB_Letters.Text = My.Settings.letters
        TB_numbers.Text = My.Settings.numbers
        TB_Keyword.Text = My.Settings.keyword
    End Sub
    Private Sub ChangeEnables(b As Boolean)
        TB_Keyword.Enabled = b
        TB_Letters.Enabled = b
        TB_numbers.Enabled = b
        B_Decrypt.Enabled = b
    End Sub
    Private Sub Decrypt(ByRef keys As List(Of Word), ByRef anagrams As List(Of Word), ByRef res As List(Of DecryptResult))
        If TB_Letters.Text.Count = TB_numbers.Text.Count Then
            'for each possible keyword decrypt the input and create a leter to number map and unscrambled
            For Each w1 As Word In keys
                Dim stime As DateTime = Now
                Dim lettermap As Dictionary(Of Char, Integer) = New Dictionary(Of Char, Integer)
                Dim encrypted As String = TB_Letters.Text
                Dim numbers As String = TB_numbers.Text
                Dim decrypted As String = w1.DecryptLetters(encrypted)
                Dim scrambled As String = decrypted.OrderBy(Function(c) c).ToArray()
                For i As Integer = 0 To decrypted.Count - 1
                    Dim newchar As Char = decrypted(i)
                    Dim num As Integer = Integer.Parse(numbers(i))
                    lettermap.Add(newchar, num)
                Next
                For Each w2 As Word In anagrams
                    If w2.Anagram = scrambled Then
                        Dim keycode As String = ""
                        For Each c As Char In w2.Word
                            keycode += lettermap(c).ToString
                        Next
                        Dim dr As New DecryptResult(w1, w2.Word, keycode)
                        res.Add(dr)
                    End If
                    If exiting Then
                        Exit For
                    End If
                Next
                'Invoke(Sub() RTB_Output.Text += "thread: " + index.ToString + " time to calc keyword: " + w1.Word + " time: " + (Now - stime).TotalSeconds.ToString + " seconds" + vbNewLine)
                If exiting Then
                    Exit For
                End If
            Next
        End If
        Invoke(Sub() totalthreads -= 1)
    End Sub
    Dim starttime As DateTime = Now
    Private Sub B_Decrypt_Click(sender As Object, e As EventArgs) Handles B_Decrypt.Click
        For Each l As List(Of DecryptResult) In results
            l.Clear()
        Next
        threads.Clear()
        RTB_Output.Text = ""
        totalthreads = maxthreads
        starttime = Now
        Dim keylists As List(Of List(Of Word)) = New List(Of List(Of Word))
        For i As Integer = 1 To maxthreads
            keylists.Add(New List(Of Word))
        Next
        Dim ID As Integer = 0
        For Each w As Word In masterWordList
            If w.Word Like TB_Keyword.Text + "*" Then
                keylists(ID).Add(w)
                ID = (ID + 1) Mod maxthreads
            End If
        Next
        ChangeEnables(False)
        For i As Integer = 0 To maxthreads - 1
            Dim index As Integer = i
            Dim t As Threading.Thread = New Threading.Thread(Sub() Decrypt(keylists(index), wordcount(TB_Letters.Text.Count), results(index)))
            t.Start()
            threads.Add(t)
        Next
        Timer1.Enabled = True
    End Sub
    Private Sub TB_Keyword_TextChanged(sender As Object, e As EventArgs) Handles TB_Keyword.TextChanged
        My.Settings.keyword = TB_Keyword.Text
        My.Settings.Save()
    End Sub
    Private Sub TB_Letters_TextChanged(sender As Object, e As EventArgs) Handles TB_Letters.TextChanged
        My.Settings.letters = TB_Letters.Text
        My.Settings.Save()
    End Sub
    Private Sub TB_numbers_TextChanged(sender As Object, e As EventArgs) Handles TB_numbers.TextChanged
        My.Settings.numbers = TB_numbers.Text
        My.Settings.Save()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'ProgressBar1.Value = progress
        If totalthreads = 0 Then
            For Each t As Threading.Thread In threads
                t.Join()
            Next
            threads.Clear()
            Dim summary As List(Of DecryptResult) = New List(Of DecryptResult)
            For Each l As List(Of DecryptResult) In results
                For Each dr As DecryptResult In l
                    summary.Add(dr)
                Next
            Next
            summary = summary.OrderBy(Function(dr) dr.Keycode).ToList

            For Each dr As DecryptResult In summary
                RTB_Output.Text += dr.Keyword.Word + " - " + dr.Keyword.Keyword + " - " + dr.Word + " - " + dr.Keycode + vbNewLine
            Next
            Timer1.Enabled = False
            RTB_Output.Text += "total time: " + (Now - starttime).TotalSeconds.ToString + " seconds"
            ChangeEnables(True)
        End If
    End Sub
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        exiting = True
        For Each t As Threading.Thread In threads
            t.Join()
        Next
    End Sub
End Class
