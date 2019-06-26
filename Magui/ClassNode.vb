
Public Class ClassNode : Inherits UserControl
    'Abstract Class for all Nodes with polymorphism.

    Property prop As ClassNodeProperties = New ClassNodeProperties

    Protected dNode As Rectangle    'This is set by the child after instantiation.
    Protected dCanvas As Canvas     'This is set by the child after instantiation.

    Private mouseLocation As Point
    Private pointOrig As Point
    Private isMoved As Boolean = False

    Public Shared ReadOnly DeleteNodeEvent As RoutedEvent = EventManager.RegisterRoutedEvent(
        "DeleteNode",
        RoutingStrategy.Bubble,
        GetType(RoutedEventHandler),
        GetType(ClassNode))


    ' Provide CLR accessors for the event
    Public Custom Event DeleteNode As RoutedEventHandler
        AddHandler(ByVal value As RoutedEventHandler)
            Me.AddHandler(DeleteNodeEvent, value)
        End AddHandler

        RemoveHandler(ByVal value As RoutedEventHandler)
            Me.RemoveHandler(DeleteNodeEvent, value)
        End RemoveHandler

        RaiseEvent(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.RaiseEvent(e)
        End RaiseEvent
    End Event

    Public Sub New()

        pointOrig = New Point(0.0, 0.0)

    End Sub

    Public Function GetCenterPoint()
        Dim xLeft As Double = Canvas.GetLeft(dNode)
        Dim yTop As Double = Canvas.GetTop(dNode)
        Dim xMiddle = (xLeft + dNode.Width / 2)
        Dim yMiddle = (yTop + dNode.Height / 2)
        Dim myMiddlePoint = New Point(xMiddle, yMiddle)
        'Debug.WriteLine("myMiddlePoint.X = " & myMiddlePoint.X & " myMiddlePoint.Y = " & myMiddlePoint.Y)

        Return myMiddlePoint
    End Function

    Public Sub SetLocation(ByVal xLeft As Double, ByVal yTop As Double)
        'Dim xMiddle = (xLeft + dNode.Width / 2)
        'Dim yMiddle = (yTop + dNode.Height / 2)
        'Dim myMiddlePoint = New Point(xMiddle, yMiddle)
        Canvas.SetLeft(dNode, xLeft)
        Canvas.SetTop(dNode, yTop)
        'Debug.WriteLine("myMiddlePoint.X = " & myMiddlePoint.X & " myMiddlePoint.Y = " & myMiddlePoint.Y)

    End Sub

    Public Sub Edit_Node(ByVal sender As Object, ByVal e As MouseButtonEventArgs)

        Debug.WriteLine("Edit Node Clicked! Node: ")
        'Debug.WriteLine("My Class Name = " & Me.Name)

    End Sub

    Public Sub Delete_Node(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        Debug.WriteLine("ClassNode Delete_Node Clicked!")
        pointOrig = e.GetPosition(dNode)
        Debug.WriteLine("pointOrig.X = " & pointOrig.X & " pointOrig.Y = " & pointOrig.Y)
        Debug.WriteLine("my nodeId = " & Me.prop.Index & " my nodeType = " & Me.prop.Category & vbNewLine)

        'Bubble up the delete event to the MainWindow for processing.
        Dim newEventArgs As New RoutedEventArgs(DeleteNodeEvent)
        Me.RaiseEvent(newEventArgs)

    End Sub


    Public Sub Node_MouseDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)

        Dim myLocation As Point = e.GetPosition(dNode)

        pointOrig = New Point(myLocation.X, myLocation.Y)   'Starting point of user drag&drop.
        'Debug.WriteLine("pointOrig.x = " & pointOrig.X & " pointOrig.y = " & pointOrig.Y)

    End Sub

    Public Sub Node_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)

        mouseLocation = e.GetPosition(dCanvas)

        'If the user has the mouse button held down then he is dragging the node.
        'We need to update the canvas while the node is being dragged.
        If e.LeftButton = MouseButtonState.Pressed Then
            Me.prop.Left = (mouseLocation.X - pointOrig.X)
            Me.prop.Top = (mouseLocation.Y - pointOrig.Y)
            Canvas.SetLeft(dNode, Me.prop.Left)
            Canvas.SetTop(dNode, Me.prop.Top)
            isMoved = True  'node has been moved; we need to redraw the links.

            'Debug.WriteLine("mouseLocation.x = " & mouseLocation.X & " mouseLocation.y = " & mouseLocation.Y)
            'Debug.WriteLine("pointOrig.x = " & pointOrig.X & " pointOrig.y = " & pointOrig.Y)
        End If

    End Sub

    Public Sub Node_MouseUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        'If the node has been moved we need to redraw the links.
        'This will have to be cleaned up to support multiple nodes with many link.
        If isMoved Then
            For Each l As ClassLink In colLinks
                l.DrawLink()
            Next
        End If

    End Sub

End Class

