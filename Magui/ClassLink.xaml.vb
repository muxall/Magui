Public Class ClassLink


    Public nodeA As ClassNode
    Public nodeB As ClassNode

    Property prop As ClassLinkProperties = New ClassLinkProperties

    ' Create a custom routed event by first registering a RoutedEventID
    ' This event uses the bubbling routing strategy
    Public Shared ReadOnly DeleteLinkEvent As RoutedEvent = EventManager.RegisterRoutedEvent(
        "DeleteLink",
        RoutingStrategy.Bubble,
        GetType(RoutedEventHandler),
        GetType(ClassLink))

    ' Provide CLR accessors for the event
    Public Custom Event DeleteLink As RoutedEventHandler
        AddHandler(ByVal value As RoutedEventHandler)
            Me.AddHandler(DeleteLinkEvent, value)
        End AddHandler

        RemoveHandler(ByVal value As RoutedEventHandler)
            Me.RemoveHandler(DeleteLinkEvent, value)
        End RemoveHandler

        RaiseEvent(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.RaiseEvent(e)
        End RaiseEvent
    End Event

    Public Sub New()
        InitializeComponent() 'Instantiate ucLink in xaml
    End Sub

    Public Sub New(ByVal aNode As Object, bNode As Object)

        InitializeComponent()   'Instantiate ucLink in xaml
        Me.nodeA = aNode
        Me.nodeB = bNode
        prop.nodeA = nodeA.Name
        prop.nodeB = nodeB.Name
        DrawLink()

    End Sub

    Public Sub SetEndpoints(ByVal nA As String, ByVal nB As String)
        For Each cn As ClassNode In colNodes
            If nA Like cn.Name Then
                Me.nodeA = cn
            End If
            If nB Like cn.Name Then
                Me.nodeB = cn
            End If
        Next
        Me.DrawLink()   'Redraw link with new endpoints.
    End Sub

    Public Sub DrawLink()
        'Get the location of each node.  GetLocation returns the middle point of the node.
        Dim aNodePoint As Point = Me.nodeA.GetCenterPoint()
        Dim bNodePoint As Point = Me.nodeB.GetCenterPoint()
        Me.ucLink.X1 = aNodePoint.X
        Me.ucLink.Y1 = aNodePoint.Y
        Me.ucLink.X2 = bNodePoint.X
        Me.ucLink.Y2 = bNodePoint.Y
        Debug.WriteLine("Draw Link NodeA.X1 = " & aNodePoint.X &
                        " Draw Link NodeA.Y1 = " & aNodePoint.Y &
                        " Draw Link NodeB.X2 = " & bNodePoint.X &
                        " Draw Link NodeB.Y2 = " & bNodePoint.Y)
    End Sub

    Private Sub Edit_Link(ByVal sender As Object, ByVal e As MouseButtonEventArgs)

        Debug.WriteLine("Edit Link Clicked! ")

    End Sub

    Private Sub Delete_Link(ByVal sender As Object, ByVal e As MouseButtonEventArgs)

        Debug.WriteLine("Class Link Delete_Link Clicked! ")

        'Bubble up the delete event to the MainWindow for processing.
        Dim newEventArgs As New RoutedEventArgs(DeleteLinkEvent)
        Me.RaiseEvent(newEventArgs)

    End Sub

End Class
