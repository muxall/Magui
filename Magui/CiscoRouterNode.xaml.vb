
Public Class CiscoRouterNode : Inherits ClassNode

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        'Pass the derived ucNode object reference to the base class.
        MyBase.dNode = Me.ucNode
        MyBase.dCanvas = Me.ucCanvas

    End Sub

    Private Sub SendCmd_Node(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        Dim router As New CiscoClient
        router.Owner = Window.GetWindow(Me)
        router.Show()
        Debug.WriteLine("SendCmd Node Clicked! Node: ")

    End Sub

End Class
