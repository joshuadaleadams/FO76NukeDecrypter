Imports CommonClasses

Public Class Word
    Public Sub New(word As String)
        Me.Word = word
        Count = Me.Word.Count
        Anagram = Me.Word.OrderBy(Function(c) c).ToArray
        CalcKeyword()
        CalcDecryptList()
    End Sub
    Public Sub New()
        Word = ""
        Count = 0
        Anagram = ""
        DecryptList = "abcdefghijklmnopqrstuvwxyz"
        Keyword = ""
    End Sub
    Public Property Word As String
    Public Property Count As Integer
    Public Property Anagram As String
    Public Property DecryptList As String
    Public Property Keyword As String
    Public Sub CalcKeyword()
        Keyword = ""
        Dim characters As List(Of Char) = New List(Of Char)
        For Each c As Char In Word
            If Not characters.Contains(c) Then
                Keyword += c
                characters.Add(c)
            End If
        Next
    End Sub
    Public Sub CalcDecryptList()
        Dim DL(25) As Char
        Dim startchar As Char = "a"
        For Each c As Char In Keyword
            If "a" <= c And c <= "z" Then
                DL(AscW(c) - AscW("a")) = startchar
                startchar = ChrW(AscW(startchar) + 1)
            End If
        Next
        For i As Integer = 0 To 25
            Dim c As Char = ChrW(AscW("a") + i)
            If DL(i) = vbNullChar Then
                DL(i) = startchar
                startchar = ChrW(AscW(startchar) + 1)
            End If
        Next
        DecryptList = DL
    End Sub
    Public Overrides Function ToString() As String
        Return Word + "," + Count.ToString + "," + Anagram + "," + DecryptList + "," + Keyword
    End Function
    Public Sub InitFromString(input As String)
        Dim s() As String = input.Split(",")
        Word = s(0)
        Count = s(1)
        Anagram = s(2)
        DecryptList = s(3)
        Keyword = s(4)
    End Sub
    Public Overrides Function Equals(obj As Object) As Boolean
        Dim word = TryCast(obj, Word)
        Return word IsNot Nothing AndAlso
               Me.Word = word.Word
    End Function
    Public Overrides Function GetHashCode() As Integer
        Return Word.GetHashCode
    End Function
    Public Function DecryptLetters(en As String) As String
        Dim de As String = ""
        For Each c As Char In en
            If "a" <= c And c <= "z" Then
                de += DecryptList(AscW(c) - AscW("a"))
            End If
        Next
        Return de
    End Function
End Class

