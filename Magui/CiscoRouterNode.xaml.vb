
Public Class CiscoRouterNode : Inherits ClassNode
    'Dim bindLabel As New Binding
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        'Pass the derived ucNode object reference to the base class.
        MyBase.dNode = Me.ucNode
        MyBase.dCanvas = Me.ucCanvas

        'bindLabel.Mode = BindingMode.OneWay
        'bindLabel.Source = prop.Nickname
        'txtLabel.SetBinding(TextBlock.TextProperty, bindLabel)
        'txtLabel.Text = "vbLabel"

        txtLabel.DataContext = prop

    End Sub

    Private Sub SendCmd_Node(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        Dim router As New CiscoClient
        router.Owner = Window.GetWindow(Me)
        router.Show()
        Debug.WriteLine("SendCmd Node Clicked! Node: ")

    End Sub

End Class
