Public Class Form1
    Dim masterwordlist As List(Of List(Of String)) = New List(Of List(Of String))
    Dim characterSort As Dictionary(Of Integer, List(Of String)) = New Dictionary(Of Integer, List(Of String))
    Dim progress As Double = 0
    Dim starttime As DateTime = Now
    Dim t As List(Of Threading.Thread) = New List(Of Threading.Thread)
    Dim exiting As Boolean = False
    Dim threadcount As Integer = 0
    Dim maxthreads As Integer = 4
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ApplicationDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
        Dim LogfilePath = System.IO.Path.Combine(ApplicationDir, "words.txt")
        Dim filereader As System.IO.TextReader = My.Computer.FileSystem.OpenTextFileReader(LogfilePath)
        Dim inputstring As String = filereader.ReadLine
        For i As Integer = 0 To maxthreads - 1
            masterwordlist.Add(New List(Of String))
        Next
        Dim index As Integer = 0
        Try
            While Not inputstring = "ZZZ"
                masterwordlist.Item(index).Add(inputstring)
                If Not characterSort.ContainsKey(inputstring.Count) Then
                    characterSort.Add(inputstring.Count, New List(Of String))
                End If
                characterSort(inputstring.Count).Add(inputstring)
                index = (index + 1) Mod maxthreads
                inputstring = filereader.ReadLine
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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
    Private Sub decrypt(ByVal wordlist As List(Of String), ByVal anagrms As List(Of String))
        Invoke(Sub() threadcount += 1)
        Dim message As String = ""
        Dim keyWordMap As Dictionary(Of String, String) = New Dictionary(Of String, String)
        Dim ciphermap As Dictionary(Of String, Dictionary(Of Char, Char)) = New Dictionary(Of String, Dictionary(Of Char, Char))
        Dim unscrambled As Dictionary(Of String, String) = New Dictionary(Of String, String)
        If TB_Letters.Text.Count = TB_numbers.Text.Count Then
            'get possible ciphers and decryption maps
            For Each s As String In wordlist
                If s Like TB_Keyword.Text + "*" Then
                    Dim chars As List(Of Char) = New List(Of Char)
                    Dim k As String = ""
                    Dim startchar As Char = "a"
                    Dim charmap As Dictionary(Of Char, Char) = New Dictionary(Of Char, Char)
                    For Each c As Char In s
                        If Not chars.Contains(c) And "a" <= c And c <= "z" Then
                            chars.Add(c)
                            k += c
                            charmap.Add(c, startchar)
                            startchar = Chr(Asc(startchar) + 1)
                        End If
                    Next
                    For i As Integer = Asc("a") To Asc("z")
                        Dim c As Char = Chr(i)
                        If Not chars.Contains(c) Then
                            charmap.Add(c, startchar)
                            startchar = Chr(Asc(startchar) + 1)
                        End If
                    Next
                    If Not keyWordMap.ContainsKey(k) Then
                        keyWordMap.Add(k, s)
                        ciphermap.Add(k, charmap)
                    End If
                End If
            Next
            'for each possible keyword decrypt the input and create a leter to number map and unscrambled
            For Each p As KeyValuePair(Of String, Dictionary(Of Char, Char)) In ciphermap
                Dim cipher As Dictionary(Of Char, Char) = p.Value
                Dim lettermap As Dictionary(Of Char, Integer) = New Dictionary(Of Char, Integer)
                Dim scrambled As String = ""
                Dim encypted As String = TB_Letters.Text
                Dim numbers As String = TB_numbers.Text
                For i As Integer = 0 To encypted.Count - 1
                    Dim newchar As Char = cipher(encypted.Chars(i))
                    Dim num As Integer = Integer.Parse(numbers(i))
                    lettermap.Add(newchar, num)
                    scrambled += newchar
                Next
                scrambled = scrambled.OrderBy(Function(c) c).ToArray()
                For Each s As String In anagrms
                    If s.OrderBy(Function(c) c).ToArray = scrambled Then
                        message += keyWordMap(p.Key) + " - " + p.Key + " - " + s + " - "
                        For Each c As Char In s
                            message += lettermap(c).ToString
                        Next
                        message += vbNewLine
                    End If
                    If exiting Then
                        Exit For
                    End If
                Next
                If exiting Then
                    Exit For
                End If
            Next
        End If
        Invoke(Sub() RTB_Output.Text += message)
        Invoke(Sub() threadcount -= 1)
    End Sub
    Private Sub B_Decrypt_Click(sender As Object, e As EventArgs) Handles B_Decrypt.Click
        starttime = Now
        RTB_Output.Text = ""
        ChangeEnables(False)
        For i As Integer = 0 To maxthreads - 1
            Dim index As Integer = i
            t.Add(New Threading.Thread(Sub() decrypt(masterwordlist.Item(index), characterSort.Item(TB_Letters.Text.Count))))
        Next
        Timer1.Enabled = True
        For Each th As Threading.Thread In t
            th.Start()
        Next
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
        If threadcount = 0 Then
            For Each th As Threading.Thread In t
                th.Join()
            Next
            t.Clear()
            ChangeEnables(True)
            Timer1.Enabled = False
            RTB_Output.Text += "Elapsed time: " + (Now - starttime).TotalSeconds.ToString + " seconds"
        End If
    End Sub
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        exiting = True
        If Not IsNothing(t) Then
            For Each th As Threading.Thread In t
                th.Join()
            Next
            t.Clear()
        End If
    End Sub
End Class
