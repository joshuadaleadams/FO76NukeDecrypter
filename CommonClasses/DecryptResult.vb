Imports CommonClasses

Public Class DecryptResult
    Public Sub New(keyword As Word, word As String, keycode As String)
        Me.Keyword = keyword
        Me.Word = word
        Me.keycode = keycode
    End Sub

    Public Property Keyword As Word
    Public Property Word As String
    Public Property Keycode As String
End Class
