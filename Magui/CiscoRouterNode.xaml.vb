
Public Class CiscoRouterNode

    Dim mouseLocation As Point
    Dim pointOrig As Point
    Dim transPoint As TranslateTransform

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

    End Sub

    Public Sub New(ByVal c As CiscoRouterNode)

        InitializeComponent()
        Me.ucNode.Height = c.ucNode.Height
        Me.ucNode.Width = c.ucNode.Height
        Me.ucNode.Fill = c.ucNode.Fill

    End Sub

    Private Sub SendCmd_Node(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        Dim router As New CiscoClient
        router.Show()
        Debug.WriteLine("SendCmd Node Clicked! Node: ")

    End Sub

    Private Sub Edit_Node(ByVal sender As Object, ByVal e As MouseButtonEventArgs)

        Debug.WriteLine("Edit Node Clicked! Node: ")

    End Sub

    Private Sub Delete_Node(ByVal sender As Object, ByVal e As MouseButtonEventArgs)

        Debug.WriteLine("Delete Node Clicked! Node: ")

    End Sub

    Private Sub Node_MouseDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)

        Dim myLocation As Point = e.GetPosition(ucNode)

        pointOrig = New Point(myLocation.X, myLocation.Y)
        'Debug.WriteLine("pointOrig.x = " & pointOrig.X & " pointOrig.y = " & pointOrig.Y)

        transPoint = New TranslateTransform(pointOrig.X, pointOrig.Y)
        'Debug.WriteLine("transPoint.x = " & transPoint.X & " transPoint.y = " & transPoint.Y)

    End Sub

    Private Sub Node_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseEventArgs)

        mouseLocation = e.GetPosition(ucCanvas)

        If e.LeftButton = MouseButtonState.Pressed Then
            transPoint.X = (mouseLocation.X - pointOrig.X)
            transPoint.Y = (mouseLocation.Y - pointOrig.Y)
            ucNode.RenderTransform = transPoint

            'Debug.WriteLine("mouseLocation.x = " & mouseLocation.X & " mouseLocation.y = " & mouseLocation.Y)
            'Debug.WriteLine("pointOrig.x = " & pointOrig.X & " pointOrig.y = " & pointOrig.Y)
            'Debug.WriteLine("transPoint.x = " & transPoint.X & " transPoint.y = " & transPoint.Y)

        End If

    End Sub

End Class
