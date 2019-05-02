﻿
Imports System.Windows.Controls.Primitives

Public Class MainWindow

    Private Sub Add_Router(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        Dim idx As Integer
        Dim Router As New CiscoRouterNode

        'idx = canvasMain.Children.Add(Router)   'idx is a unique on the canvas.
        canvasMain.Children.Add(Router)         'Add Router to Canvas

        'Get next available node index
        idx = GetAvailableNodeIndex()
        Router.nodeIdx = idx                    'update the default index 0 to new index
        Router.Name = "Router_" & idx           'give the Router a unique name.

        Canvas.SetZIndex(Router, 10)            'Move the Router image to the foreground.

        'Router.Name is the searchable key in the collection of nodes.
        colNodes.Add(Router, Router.Name)

        Debug.WriteLine("Add Router " & Router.Name)

    End Sub

    Private Function GetAvailableNodeIndex()

        Dim idx As Integer
        For idx = 0 To MaxNodes
            Dim isFound As Boolean = False
            For Each cn As ClassNode In colNodes
                If cn.nodeIdx.Equals(idx) Then
                    isFound = True
                End If
            Next
            If Not isFound Then
                Return idx
            End If
        Next
        Return idx

    End Function

    Private Function GetAvailableLinkIndex()

        Dim idx As Integer
        For idx = 0 To MaxLinks
            Dim isFound As Boolean = False
            For Each l As Link In colLinks
                If l.linkIdx.Equals(idx) Then
                    isFound = True
                End If
            Next
            If Not isFound Then
                Return idx
            End If
        Next
        Return idx

    End Function


    Private Sub Add_Switch(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        Dim idx As Integer
        Dim Eswitch As New CiscoSwitchNode
        canvasMain.Children.Add(Eswitch)
        idx = GetAvailableNodeIndex()           'Get next available node index
        Eswitch.nodeIdx = idx                   'update the default index 0 to new index
        Eswitch.Name = "Eswitch_" & idx         'give the Eswitch a unique name.

        Canvas.SetZIndex(Eswitch, 10)            'Move the Eswitch image to the foreground.

        'Eswitch.Name is the searchable key in the collection of nodes.
        colNodes.Add(Eswitch, Eswitch.Name)

        Debug.WriteLine("Add Eswitch Clicked! canvasIndex = " & idx)

    End Sub

    Private Sub Add_TestSet(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        Dim idx As Integer
        Dim Xena As New XenaTestNode
        canvasMain.Children.Add(Xena)
        idx = GetAvailableNodeIndex()       'Get next available node index
        Xena.nodeIdx = idx                  'update the default index 0 to new index
        Xena.Name = "Xena_" & idx           'give the Xena a unique name.

        Canvas.SetZIndex(Xena, 10)          'Move the Xena image to the foreground.

        'Xena.Name is the searchable key in the collection of nodes.
        colNodes.Add(Xena, Xena.Name)

        Debug.WriteLine("Add Xena TestSet Clicked! canvasIndex = " & idx)

    End Sub


    Private Sub Add_Link(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        Dim idx As Integer

        Dim dlgLinkEditor As New LinkEditor
        dlgLinkEditor.Owner = Me
        dlgLinkEditor.ShowDialog()
        If Not dlgLinkEditor.DialogResult Then
            Return
        End If

        'Get selected nodes from Link Editor.
        Dim aNodeName = dlgLinkEditor.lbNodeA.SelectedItem
        Dim bNodeName = dlgLinkEditor.lbNodeB.SelectedItem

        'Using a generic Object instead of specific Router, Switch or Test Set
        'This may get confusing?
        Dim aNodeObj As Object = colNodes.Item(aNodeName)
        Dim bNodeObj As Object = colNodes.Item(bNodeName)

        'Create and draw link.
        Dim myLink As New Link(aNodeObj, bNodeObj)

        canvasMain.Children.Add(myLink)
        'Get next available Link index
        idx = GetAvailableLinkIndex()

        'Add a default name.  Probably will change later.
        myLink.linkIdx = idx
        myLink.Name = "link_" & idx

        'myLink.Name is the searchable key in the collection of links.
        For Each l In colLinks
            Debug.WriteLine("Link Name: " + l.Name)
        Next
        colLinks.Add(myLink, myLink.Name)

        Debug.WriteLine("Add Link Name = " & myLink.Name)

    End Sub

    Private Sub Delete_Node(sender As Object, e As RoutedEventArgs)
        Debug.WriteLine("MainWindow Delete_Node called! ")

        'Get Event information.
        Debug.WriteLine("RoutedEvent Name: " + e.RoutedEvent.Name)
        Debug.WriteLine("RoutedEvent Owner: " + e.RoutedEvent.OwnerType.ToString)
        Debug.WriteLine("Handled: " + e.Handled.ToString)
        Debug.Write("Routing strategy: ")
        Debug.WriteLine(e.RoutedEvent.RoutingStrategy)
        Debug.WriteLine("")

        ' Get the class node that raised the event. 
        Dim cn As ClassNode = DirectCast(e.Source, ClassNode)

        Debug.Write("Node Type: ")
        Debug.WriteLine(e.Source.[GetType]().ToString())
        Debug.WriteLine("Node Name: " + cn.Name)
        Debug.WriteLine("")

        'First delete all dependent links associated with node
        For Each l As Link In colLinks
            If ((l.nodeA.Name.Equals(cn.Name)) Or (l.nodeB.Name.Equals(cn.Name))) Then
                Me.Delete_Link(l)
            End If
        Next

        'Second, delete the node
        colNodes.Remove(cn.Name)
        canvasMain.Children.Remove(cn)

        'This isn't really needed since MainWindow is our root
        'but it is good practice.
        e.Handled = True

    End Sub

    Private Sub Delete_Link(l As Link)

        colLinks.Remove(l.Name)
        canvasMain.Children.Remove(l)

    End Sub

    Private Sub Delete_Link(sender As Object, e As RoutedEventArgs)
        Debug.WriteLine("MainWindow Event Delete_Link called! ")

        'Get Event information.
        Debug.WriteLine("RoutedEvent Name: " + e.RoutedEvent.Name)
        Debug.WriteLine("RoutedEvent Owner: " + e.RoutedEvent.OwnerType.ToString)
        Debug.WriteLine("Handled: " + e.Handled.ToString)
        Debug.Write("Routing strategy: ")
        Debug.WriteLine(e.RoutedEvent.RoutingStrategy)
        Debug.WriteLine("")

        ' Get the link that raised the event. 
        Dim l As Link = DirectCast(e.Source, Link)
        Debug.Write("Link Type: ")
        Debug.WriteLine(e.Source.[GetType]().ToString())
        Debug.WriteLine("Node Name: " + l.Name)
        Debug.WriteLine("")

        Me.Delete_Link(l)

        'This isn't really needed since MainWindow is our root
        'but it is good practice.
        e.Handled = True

    End Sub

End Class

