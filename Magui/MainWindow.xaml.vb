
Imports System.Windows.Controls.Primitives

Public Class MainWindow

    Private Sub Add_Router(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        Dim idx As Integer
        Dim Router As New CiscoRouterNode

        idx = canvasMain.Children.Add(Router)   'idx is a unique on the canvas.
        Router.nodeIdx = idx                    'update the default index 0 to new index
        Router.Name = "Router_" & idx           'give the Router a unique name.

        Canvas.SetZIndex(Router, 10)            'Move the Router image to the foreground.

        'Router.Name is the searchable key in the collection of nodes.
        colNodes.Add(Router, Router.Name)

        Debug.WriteLine("Add Router " & Router.Name)

    End Sub


    Private Sub Add_Switch(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        Dim idx As Integer
        Dim Eswitch As New CiscoSwitchNode
        idx = canvasMain.Children.Add(Eswitch)
        Eswitch.nodeIdx = idx                    'update the default index 0 to new index
        Eswitch.Name = "Eswitch_" & idx           'give the Eswitch a unique name.

        Canvas.SetZIndex(Eswitch, 10)            'Move the Eswitch image to the foreground.

        'Eswitch.Name is the searchable key in the collection of nodes.
        colNodes.Add(Eswitch, Eswitch.Name)

        Debug.WriteLine("Add Eswitch Clicked! canvasIndex = " & idx)

    End Sub

    Private Sub Add_TestSet(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        Dim idx As Integer
        Dim Xena As New XenaTestNode
        idx = canvasMain.Children.Add(Xena)
        Xena.nodeIdx = idx                    'update the default index 0 to new index
        Xena.Name = "Xena_" & idx           'give the Xena a unique name.

        Canvas.SetZIndex(Xena, 10)            'Move the Xena image to the foreground.

        'Xena.Name is the searchable key in the collection of nodes.
        colNodes.Add(Xena, Xena.Name)

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

