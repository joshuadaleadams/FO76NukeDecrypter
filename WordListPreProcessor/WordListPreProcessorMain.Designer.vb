<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WordListPreProcessorMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.B_Start = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'B_Start
        '
        Me.B_Start.Location = New System.Drawing.Point(15, 19)
        Me.B_Start.Name = "B_Start"
        Me.B_Start.Size = New System.Drawing.Size(75, 23)
        Me.B_Start.TabIndex = 0
        Me.B_Start.Text = "Start"
        Me.B_Start.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(400, 172)
        Me.Controls.Add(Me.B_Start)
        Me.Name = "Form1"
        Me.Text = "Wordlist Preprocessor"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents B_Start As Button
End Class
