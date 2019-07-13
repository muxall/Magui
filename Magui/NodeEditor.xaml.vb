Public Class NodeEditor
    Public Property myNode As ClassNode

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(sender As ClassNode)
        InitializeComponent()

        'myNode is passed here from ClassNode.Edit_Node()
        myNode = sender
        Debug.WriteLine("Edit_Node: " & myNode.Name)

        'Pass myNode to Node Edit Home page
        Dim NodeEditHomePage As New NodeEditHome(myNode)
        NavigationService.Navigate(NodeEditHomePage)
    End Sub

End Class
