
Imports System.Windows.Controls.Primitives

Public Class MainWindow

    Private Sub Add_Router(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        Dim idx As Integer
        Dim router As New CiscoRouterNode

        idx = canvasMain.Children.Add(router)   'idx is a unique on the canvas.
        router.nodeIdx = idx                    'update the default index 0 to new index
        router.Name = "router_" & idx           'give the router a unique name.

        Canvas.SetZIndex(router, 10)            'Move the router image to the foreground.

        'router.Name is the searchable key in the collection of nodes.
        colNodes.Add(router, router.Name)

        Debug.WriteLine("Add Router " & router.Name)

    End Sub


    Private Sub Add_Switch(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        Dim idx As Integer
        Dim switch As New CiscoSwitchNode
        idx = canvasMain.Children.Add(switch)
        Debug.WriteLine("Add Switch Clicked! canvasIndex = " & idx)

    End Sub

    Private Sub Add_TestSet(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        Dim idx As Integer
        Dim xena As New XenaTestNode
        idx = canvasMain.Children.Add(xena)
        Debug.WriteLine("Add Xena TestSet Clicked! canvasIndex = " & idx)

    End Sub


    Private Sub Add_Link(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        'Private Sub Add_Link(ByVal sender As Object, ByVal e As RoutedEventArgs)
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

        idx = canvasMain.Children.Add(myLink)

        'Add a default name.  Probably will change later.
        myLink.linkIdx = idx
        myLink.Name = "link_" & idx

        'myLink.Name is the searchable key in the collection of links.
        colLinks.Add(myLink, myLink.Name)

        Debug.WriteLine("Add Link Name = " & myLink.Name)

    End Sub

End Class

