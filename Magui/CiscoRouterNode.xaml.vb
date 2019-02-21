
Public Class CiscoRouterNode

    Public nodeType = "router"
    Public nodeIdx = 0

    Private mouseLocation As Point
    Private pointOrig As Point
    Private isMoved As Boolean = False


    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        pointOrig = New Point(0.0, 0.0)
        Me.Name = "className"
        Debug.WriteLine("My Class Name = " & Me.Name)
    End Sub


    Public Function GetLocation()
        Dim xLeft As Double = Canvas.GetLeft(ucNode)
        Dim yTop As Double = Canvas.GetTop(ucNode)
        Dim xMiddle = (xLeft + ucNode.Width / 2)
        Dim yMiddle = (yTop + ucNode.Height / 2)
        Dim myMiddlePoint = New Point(xMiddle, yMiddle)
        'Debug.WriteLine("myMiddlePoint.X = " & myMiddlePoint.X & " myMiddlePoint.Y = " & myMiddlePoint.Y)

        Return myMiddlePoint
    End Function


    Private Sub SendCmd_Node(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        Dim router As New CiscoClient
        router.Owner = Window.GetWindow(Me)
        router.Show()
        Debug.WriteLine("SendCmd Node Clicked! Node: ")

    End Sub

    Private Sub Edit_Node(ByVal sender As Object, ByVal e As MouseButtonEventArgs)

        Debug.WriteLine("Edit Node Clicked! Node: ")
        Debug.WriteLine("My Class Name = " & Me.Name)

    End Sub

    Private Sub Delete_Node(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        Debug.WriteLine("Delete Node Clicked! Node: ")
        pointOrig = e.GetPosition(ucNode)
        Debug.WriteLine("pointOrig.X = " & pointOrig.X & " pointOrig.Y = " & pointOrig.Y)
        Debug.WriteLine("my nodeId = " & Me.nodeIdx & " my nodeType = " & Me.nodeType & vbNewLine)
        'colNodes.Remove(Me.nodeId)


    End Sub

    Private Sub Node_MouseDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)

        Dim myLocation As Point = e.GetPosition(ucNode)

        pointOrig = New Point(myLocation.X, myLocation.Y)
        'Debug.WriteLine("pointOrig.x = " & pointOrig.X & " pointOrig.y = " & pointOrig.Y)

    End Sub

    Private Sub Node_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseEventArgs)

        mouseLocation = e.GetPosition(ucCanvas)

        If e.LeftButton = MouseButtonState.Pressed Then
            Canvas.SetLeft(ucNode, (mouseLocation.X - pointOrig.X))
            Canvas.SetTop(ucNode, (mouseLocation.Y - pointOrig.Y))
            isMoved = True  'node has been moved

            'Debug.WriteLine("mouseLocation.x = " & mouseLocation.X & " mouseLocation.y = " & mouseLocation.Y)
            'Debug.WriteLine("pointOrig.x = " & pointOrig.X & " pointOrig.y = " & pointOrig.Y)
        End If

    End Sub

    Private Sub Node_MouseUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        'If the node has been moved we need to redraw the links.
        'This will have to be cleaned up to support multiple nodes with many link.
        If isMoved Then
            For Each l As Link In colLinks
                l.DrawLink()
            Next
        End If

    End Sub

End Class
