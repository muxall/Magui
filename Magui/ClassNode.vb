﻿
Public Class ClassNode : Inherits UserControl
    'Abstract Class for all Nodes with polymorphism.

    Public nodeType = "node"
    Public nodeIdx = 0

    Private mouseLocation As Point
    Private pointOrig As Point
    Private isMoved As Boolean = False

    Public Property dNode As Rectangle
    Public Property dCanvas As Canvas


    Public Sub New()

        pointOrig = New Point(0.0, 0.0)

    End Sub

    Public Function GetLocation()
        Dim xLeft As Double = Canvas.GetLeft(dNode)
        Dim yTop As Double = Canvas.GetTop(dNode)
        Dim xMiddle = (xLeft + dNode.Width / 2)
        Dim yMiddle = (yTop + dNode.Height / 2)
        Dim myMiddlePoint = New Point(xMiddle, yMiddle)
        'Debug.WriteLine("myMiddlePoint.X = " & myMiddlePoint.X & " myMiddlePoint.Y = " & myMiddlePoint.Y)

        Return myMiddlePoint
    End Function

    Public Sub Edit_Node(ByVal sender As Object, ByVal e As MouseButtonEventArgs)

        Debug.WriteLine("Edit Node Clicked! Node: ")
        'Debug.WriteLine("My Class Name = " & Me.Name)

    End Sub

    Public Sub Delete_Node(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        Debug.WriteLine("Delete Node Clicked! Node: ")
        pointOrig = e.GetPosition(dNode)
        Debug.WriteLine("pointOrig.X = " & pointOrig.X & " pointOrig.Y = " & pointOrig.Y)
        Debug.WriteLine("my nodeId = " & Me.nodeIdx & " my nodeType = " & Me.nodeType & vbNewLine)
        'colNodes.Remove(Me.nodeId)

    End Sub

    Public Sub Node_MouseDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)

        Dim myLocation As Point = e.GetPosition(dNode)

        pointOrig = New Point(myLocation.X, myLocation.Y)
        Debug.WriteLine("pointOrig.x = " & pointOrig.X & " pointOrig.y = " & pointOrig.Y)

    End Sub

    Public Sub Node_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseEventArgs)

        mouseLocation = e.GetPosition(dCanvas)

        If e.LeftButton = MouseButtonState.Pressed Then
            Canvas.SetLeft(dNode, (mouseLocation.X - pointOrig.X))
            Canvas.SetTop(dNode, (mouseLocation.Y - pointOrig.Y))
            isMoved = True  'node has been moved

            'Debug.WriteLine("mouseLocation.x = " & mouseLocation.X & " mouseLocation.y = " & mouseLocation.Y)
            'Debug.WriteLine("pointOrig.x = " & pointOrig.X & " pointOrig.y = " & pointOrig.Y)
        End If

    End Sub

    Public Sub Node_MouseUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        'If the node has been moved we need to redraw the links.
        'This will have to be cleaned up to support multiple nodes with many link.
        If isMoved Then
            For Each l As Link In colLinks
                l.DrawLink()
            Next
        End If

    End Sub

End Class

