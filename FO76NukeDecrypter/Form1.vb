Public Class Form1
    Public WordList As List(Of String) = New List(Of String)
    Dim progress As Double = 0
    Dim t As Threading.Thread
    Dim exiting As Boolean = False
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ApplicationDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
        Dim LogfilePath = System.IO.Path.Combine(ApplicationDir, "words.txt")
        Dim filereader As System.IO.TextReader = My.Computer.FileSystem.OpenTextFileReader(LogfilePath)
        Dim inputstring As String = filereader.ReadLine
        Try
            While Not inputstring = "ZZZ"
                WordList.Add(inputstring)
                inputstring = filereader.ReadLine
            End While
        Catch ex As Exception
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
    Private Sub decrypt()
        Dim keyWordMap As Dictionary(Of String, String) = New Dictionary(Of String, String)
        Dim ciphermap As Dictionary(Of String, Dictionary(Of Char, Char)) = New Dictionary(Of String, Dictionary(Of Char, Char))
        Dim unscrambled As Dictionary(Of String, String) = New Dictionary(Of String, String)
        Invoke(Sub() ChangeEnables(False))
        If TB_Letters.Text.Count = TB_numbers.Text.Count Then
            Invoke(Sub() RTB_Output.Text = "")
            'get possible ciphers and decryption maps
            For Each s As String In WordList
                If s Like TB_Keyword.Text + "*" Then
                    Dim chars As List(Of Char) = New List(Of Char)
                    Dim k As String = ""
                    Dim startchar As Char = "a"
                    Dim charmap As Dictionary(Of Char, Char) = New Dictionary(Of Char, Char)
                    For Each c As Char In s
                        If Not chars.Contains(c) Then
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
            Dim totalwork As Double = WordList.Count * keyWordMap.Count
            Dim completedwork As Double = 0
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
                For Each s As String In WordList
                    If s.OrderBy(Function(c) c).ToArray = scrambled Then
                        Invoke(Sub() RTB_Output.Text += keyWordMap(p.Key) + " - " + p.Key + " - " + s + " - ")
                        For Each c As Char In s
                            Invoke(Sub() RTB_Output.Text += lettermap(c).ToString)
                        Next
                        Invoke(Sub() RTB_Output.Text += vbNewLine)
                    End If
                    completedwork += 1
                    progress = completedwork / totalwork * 100
                    If exiting Then
                        Exit For
                    End If
                Next
                If exiting Then
                    Exit For
                End If
            Next
        End If
        Invoke(Sub() ChangeEnables(True))
    End Sub
    Private Sub B_Decrypt_Click(sender As Object, e As EventArgs) Handles B_Decrypt.Click
        t = New Threading.Thread(AddressOf decrypt)
        Timer1.Enabled = True
        t.Start()
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
        ProgressBar1.Value = progress
        If RTB_Output.Enabled Then
            t.Join()
            Timer1.Enabled = False
            ProgressBar1.Value = 0
        End If
    End Sub
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        exiting = True
        If Not IsNothing(t) Then
            t.Join()
        End If
    End Sub
End Class
