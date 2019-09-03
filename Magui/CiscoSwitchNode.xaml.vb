
Public Class CiscoSwitchNode : Inherits ClassNode

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        'Pass the derived ucNode object reference to the base class.
        MyBase.dNode = Me.ucNode
        MyBase.dCanvas = Me.ucCanvas

        txtLabel.DataContext = prop

    End Sub

    Private Sub SendCmd_Node(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        Dim switch As New CiscoClient
        switch.Owner = Window.GetWindow(Me)
        switch.Show()
        Debug.WriteLine("SendCmd Node Clicked! Node: ")

    End Sub

End Class
