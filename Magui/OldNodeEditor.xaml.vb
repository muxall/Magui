Public Class OldNodeEditor

    Public Sub New()
        InitializeComponent()
        'InitEditor()
    End Sub

    Private Sub Add_Button_Click(sender As Object, e As RoutedEventArgs)
        Dim k1 = New TextBox()
        k1.Text = "Key1"
        k1.Height = "23"
        k1.Width = "162"
        neCanvas.Children.Add(k1)
        Canvas.SetLeft(k1, 36)
        Canvas.SetTop(k1, 97)


    End Sub
End Class
