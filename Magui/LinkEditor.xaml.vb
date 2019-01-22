Public Class LinkEditor

    Public Sub New()
        InitializeComponent()
        InitEditor()
    End Sub

    Private Sub InitEditor()
        'Using generic Object because node could be a router, switch or test set.
        'This might get confusing.
        For Each node As Object In colNodes
            lbNodeA.Items.Add(node.Name)
            lbNodeB.Items.Add(node.Name)
        Next

    End Sub

    Private Sub Cancel_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Debug.WriteLine("Dialog Cancel Button Clicked")
        DialogResult = False
    End Sub


    Private Sub Ok_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        ' Don't accept the dialog box if there is invalid data
        If Not IsValid() Then
            'Return to dialog fix mistake.
            Return
        End If

        ' Dialog box accepted and close.
        Debug.WriteLine("Dialog OK Button Clicked")
        DialogResult = True

    End Sub

    Private Function IsValid()

        If (lbNodeA.SelectedIndex = -1) Or (lbNodeB.SelectedIndex = -1) Then
            MsgBox("You must select two endpoint nodes.")
            Return False
        Else
            Return True
        End If

    End Function

End Class
