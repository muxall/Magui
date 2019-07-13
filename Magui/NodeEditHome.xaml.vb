Imports System.Reflection

Class NodeEditHome

    Public Sub New()
        InitializeComponent()
    End Sub

    ' Custom constructor to pass  data
    Public Sub New(ByVal myNode As ClassNode)
        Me.New()
        ' XAML bind to myNode data.
        DataContext = myNode

        Dim lstItems As New List(Of String)
        lstItems.Add("Configuration")
        lstItems.Add("Status")
        lstItems.Add("Testing")
        lstItems.Add("Reports")

        'Set the listbox source to our new list.
        PropertiesListBox.ItemsSource = lstItems

    End Sub


    Private Sub View_Button_Click(sender As Object, e As RoutedEventArgs)
        ' Edit Node Config
        Dim nodeConfig As New NodeEditConfig(DataContext)
        NavigationService.Navigate(nodeConfig)

    End Sub
End Class
