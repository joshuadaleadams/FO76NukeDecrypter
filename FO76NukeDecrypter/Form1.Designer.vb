<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FO76DecryptorMain
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
        Me.components = New System.ComponentModel.Container()
        Me.TB_Keyword = New System.Windows.Forms.TextBox()
        Me.L_keyword = New System.Windows.Forms.Label()
        Me.RTB_Output = New System.Windows.Forms.RichTextBox()
        Me.L_output = New System.Windows.Forms.Label()
        Me.B_Decrypt = New System.Windows.Forms.Button()
        Me.L_Letters = New System.Windows.Forms.Label()
        Me.TB_Letters = New System.Windows.Forms.TextBox()
        Me.L_Numbers = New System.Windows.Forms.Label()
        Me.TB_numbers = New System.Windows.Forms.TextBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.CB_AllowDuplicateKeywordLetters = New System.Windows.Forms.CheckBox()
        Me.NUD_MaxThreads = New System.Windows.Forms.NumericUpDown()
        Me.L_maxThreads = New System.Windows.Forms.Label()
        CType(Me.NUD_MaxThreads, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TB_Keyword
        '
        Me.TB_Keyword.Location = New System.Drawing.Point(12, 25)
        Me.TB_Keyword.Name = "TB_Keyword"
        Me.TB_Keyword.Size = New System.Drawing.Size(87, 20)
        Me.TB_Keyword.TabIndex = 0
        '
        'L_keyword
        '
        Me.L_keyword.AutoSize = True
        Me.L_keyword.Location = New System.Drawing.Point(12, 9)
        Me.L_keyword.Name = "L_keyword"
        Me.L_keyword.Size = New System.Drawing.Size(48, 13)
        Me.L_keyword.TabIndex = 1
        Me.L_keyword.Text = "Keyword"
        '
        'RTB_Output
        '
        Me.RTB_Output.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RTB_Output.Location = New System.Drawing.Point(12, 64)
        Me.RTB_Output.Name = "RTB_Output"
        Me.RTB_Output.ReadOnly = True
        Me.RTB_Output.Size = New System.Drawing.Size(437, 140)
        Me.RTB_Output.TabIndex = 2
        Me.RTB_Output.Text = ""
        '
        'L_output
        '
        Me.L_output.AutoSize = True
        Me.L_output.Location = New System.Drawing.Point(12, 48)
        Me.L_output.Name = "L_output"
        Me.L_output.Size = New System.Drawing.Size(39, 13)
        Me.L_output.TabIndex = 3
        Me.L_output.Text = "Output"
        '
        'B_Decrypt
        '
        Me.B_Decrypt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.B_Decrypt.Location = New System.Drawing.Point(12, 210)
        Me.B_Decrypt.Name = "B_Decrypt"
        Me.B_Decrypt.Size = New System.Drawing.Size(75, 23)
        Me.B_Decrypt.TabIndex = 4
        Me.B_Decrypt.Text = "Decrypt"
        Me.B_Decrypt.UseVisualStyleBackColor = True
        '
        'L_Letters
        '
        Me.L_Letters.AutoSize = True
        Me.L_Letters.Location = New System.Drawing.Point(109, 9)
        Me.L_Letters.Name = "L_Letters"
        Me.L_Letters.Size = New System.Drawing.Size(39, 13)
        Me.L_Letters.TabIndex = 6
        Me.L_Letters.Text = "Letters"
        '
        'TB_Letters
        '
        Me.TB_Letters.Location = New System.Drawing.Point(109, 25)
        Me.TB_Letters.Name = "TB_Letters"
        Me.TB_Letters.Size = New System.Drawing.Size(87, 20)
        Me.TB_Letters.TabIndex = 5
        '
        'L_Numbers
        '
        Me.L_Numbers.AutoSize = True
        Me.L_Numbers.Location = New System.Drawing.Point(202, 9)
        Me.L_Numbers.Name = "L_Numbers"
        Me.L_Numbers.Size = New System.Drawing.Size(49, 13)
        Me.L_Numbers.TabIndex = 8
        Me.L_Numbers.Text = "Numbers"
        '
        'TB_numbers
        '
        Me.TB_numbers.Location = New System.Drawing.Point(202, 25)
        Me.TB_numbers.Name = "TB_numbers"
        Me.TB_numbers.Size = New System.Drawing.Size(87, 20)
        Me.TB_numbers.TabIndex = 7
        '
        'Timer1
        '
        Me.Timer1.Interval = 200
        '
        'CB_AllowDuplicateKeywordLetters
        '
        Me.CB_AllowDuplicateKeywordLetters.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CB_AllowDuplicateKeywordLetters.AutoSize = True
        Me.CB_AllowDuplicateKeywordLetters.Location = New System.Drawing.Point(93, 214)
        Me.CB_AllowDuplicateKeywordLetters.Name = "CB_AllowDuplicateKeywordLetters"
        Me.CB_AllowDuplicateKeywordLetters.Size = New System.Drawing.Size(178, 17)
        Me.CB_AllowDuplicateKeywordLetters.TabIndex = 9
        Me.CB_AllowDuplicateKeywordLetters.Text = "Allow Duplicate Keyword Letters"
        Me.CB_AllowDuplicateKeywordLetters.UseVisualStyleBackColor = True
        '
        'NumericUpDown1
        '
        Me.NUD_MaxThreads.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.NUD_MaxThreads.Location = New System.Drawing.Point(357, 213)
        Me.NUD_MaxThreads.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.NUD_MaxThreads.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NUD_MaxThreads.Name = "NumericUpDown1"
        Me.NUD_MaxThreads.Size = New System.Drawing.Size(49, 20)
        Me.NUD_MaxThreads.TabIndex = 10
        Me.NUD_MaxThreads.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'L_maxThreads
        '
        Me.L_maxThreads.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.L_maxThreads.AutoSize = True
        Me.L_maxThreads.Location = New System.Drawing.Point(282, 215)
        Me.L_maxThreads.Name = "L_maxThreads"
        Me.L_maxThreads.Size = New System.Drawing.Size(69, 13)
        Me.L_maxThreads.TabIndex = 11
        Me.L_maxThreads.Text = "Max Threads"
        '
        'FO76DecryptorMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(461, 239)
        Me.Controls.Add(Me.L_maxThreads)
        Me.Controls.Add(Me.NUD_MaxThreads)
        Me.Controls.Add(Me.CB_AllowDuplicateKeywordLetters)
        Me.Controls.Add(Me.L_Numbers)
        Me.Controls.Add(Me.TB_numbers)
        Me.Controls.Add(Me.L_Letters)
        Me.Controls.Add(Me.TB_Letters)
        Me.Controls.Add(Me.B_Decrypt)
        Me.Controls.Add(Me.L_output)
        Me.Controls.Add(Me.RTB_Output)
        Me.Controls.Add(Me.L_keyword)
        Me.Controls.Add(Me.TB_Keyword)
        Me.Name = "FO76DecryptorMain"
        Me.Text = "Fallout 76 Nuke Code Decryptor"
        CType(Me.NUD_MaxThreads, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TB_Keyword As System.Windows.Forms.TextBox
    Friend WithEvents L_keyword As System.Windows.Forms.Label
    Friend WithEvents RTB_Output As System.Windows.Forms.RichTextBox
    Friend WithEvents L_output As System.Windows.Forms.Label
    Friend WithEvents B_Decrypt As System.Windows.Forms.Button
    Friend WithEvents L_Letters As System.Windows.Forms.Label
    Friend WithEvents TB_Letters As System.Windows.Forms.TextBox
    Friend WithEvents L_Numbers As System.Windows.Forms.Label
    Friend WithEvents TB_numbers As System.Windows.Forms.TextBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents CB_AllowDuplicateKeywordLetters As CheckBox
    Friend WithEvents NUD_MaxThreads As NumericUpDown
    Friend WithEvents L_maxThreads As Label
End Class
