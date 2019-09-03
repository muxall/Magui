

Public Class MainWindow

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        'Verify and / or create the default directory for Magui configs.
        VerifyCreateDir(defaultDir)

    End Sub

    Private Sub Open_File(ByVal sender As Object, ByVal e As RoutedEventArgs)

        ' Configure open file dialog box
        Dim dlg As New Microsoft.Win32.OpenFileDialog()
        dlg.InitialDirectory = defaultDir
        dlg.Filter = defaultFileFilter

        ' Show open file dialog box
        Dim result? As Boolean = dlg.ShowDialog(Me)

        ' Process open file dialog box results
        If result = True Then
            'First we need to delete ALL the nodes and associated links.
            If (colNodes.Count <> 0) Then
                For Each cn As ClassNode In colNodes
                    DeleteNode(cn)
                Next
            End If

            ' Open document
            Dim filename As String = dlg.FileName
            Dim success As Boolean = OpenTestbed(filename)
            If success Then
                For Each cn As ClassNode In colNodes
                    Debug.WriteLine("Adding Node to canvas: " & cn.Name)
                    canvasMain.Children.Add(cn)                 'Add Router to Canvas
                    cn.SetLocation(cn.prop.Left, cn.prop.Top)   'Moving node to its saved position on the Canvas
                    Canvas.SetZIndex(cn, 10)                    'Move the Router image to the foreground.
                Next
                For Each cl As ClassLink In colLinks
                    Debug.WriteLine("Adding Link to canvas: " & cl.Name)
                    canvasMain.Children.Add(cl)         'Add Router to Canvas
                    cl.SetEndpoints(cl.prop.NodeA, cl.prop.NodeB)   'Moving link to its saved position on the Canvas
                Next
            Else
                Dim tst = DialogBox("Config file has 0 nodes.", "Do you want to open another file?")
                If Not tst Then
                    'TODO: OpenFileDialog: open another file
                End If

            End If

            configFile = filename   'Set the active config to our new filename

        End If

    End Sub

    Private Sub Save_File(ByVal sender As Object, ByVal e As RoutedEventArgs)

        ' Configure save file dialog box
        If configFile Is Nothing Then
            SaveAsTestbed(defaultConfigFile)
        Else
            SaveTestbed(configFile)
        End If

    End Sub

    Private Sub SaveAs_File(ByVal sender As Object, ByVal e As RoutedEventArgs)

        If configFile Is Nothing Then
            SaveAsTestbed(defaultConfigFile)   'Default configFile name.
        Else
            SaveAsTestbed(configFile)
        End If

    End Sub

    Private Sub SaveAsTestbed(ByVal strFile As String)

        ' Configure saveAs file dialog box
        Dim dlg As New Microsoft.Win32.SaveFileDialog()
        dlg.FileName = strFile ' Default file name
        dlg.InitialDirectory = defaultDir
        dlg.Filter = defaultFileFilter

        ' Show save file dialog box
        Dim result? As Boolean = dlg.ShowDialog(Me)

        ' Process save file dialog box results
        If result = True Then
            Dim filename As String = dlg.FileName
            SaveTestbed(filename)
            configFile = filename
        End If
    End Sub

    Private Sub Add_Cisco_Router(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim Router As New CiscoRouterNode
        AddRouter(Router, "Cisco")
    End Sub

    Private Sub Add_Juniper_Router(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Debug.WriteLine("Add_Juniper_Router called...")
    End Sub

    Private Sub AddRouter(ByVal router As ClassNode, ByVal make As String)
        Dim idx As Integer
        'idx = canvasMain.Children.Add(Router)   'idx is a unique on the canvas.
        canvasMain.Children.Add(router)         'Add Router to Canvas

        'Get next available node index
        idx = GetAvailableNodeIndex()
        router.prop.Index = idx                          'update the default index 0 to new index
        router.prop.Category = "Router"                      'give the node a Category.
        router.Name = router.prop.Category & "_" & idx       'give the Router a unique name.
        router.prop.Nickname = router.Name                     'Default Nickname to router.name
        router.prop.HostName = router.Name                     'Default hostname to router.name
        router.prop.Make = make                          'Manufacturer's Name Ex: Cisco
        'router.prop.HostName = router.prop.Make & "_" & idx                     'Init the hostname

        Canvas.SetZIndex(router, 10)            'Move the Router image to the foreground.

        'Router.Name is the searchable key in the collection of nodes.
        colNodes.Add(router, router.Name)

        Debug.WriteLine("colNodes add Cisco router key: " & router.Name)
    End Sub


    Private Sub Add_Cisco_Switch(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim sw As New CiscoSwitchNode
        AddSwitch(sw, "Cisco")
    End Sub

    Private Sub Add_Metro_Switch(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Debug.WriteLine("Add_Metro_Switch called...")
    End Sub

    Private Sub AddSwitch(ByVal sw As ClassNode, make As String)
        Dim idx As Integer

        canvasMain.Children.Add(sw)
        idx = GetAvailableNodeIndex()           'Get next available node index
        sw.prop.Index = idx                   'update the default index 0 to new index
        sw.prop.Category = "Switch"    'give the Eswitch a Category.
        sw.Name = sw.prop.Category & "_" & idx         'give the Eswitch a unique name.
        sw.prop.Nickname = sw.Name                     'Default Nickname to sw.name
        sw.prop.HostName = sw.Name                     'Default hostname to sw.name
        sw.prop.Make = make                          'Manufacturer's Name Ex: Cisco
        'sw.prop.HostName = sw.prop.Make & "_" & idx                     'Init the hostname

        Canvas.SetZIndex(sw, 10)            'Move the Eswitch image to the foreground.

        'Eswitch.Name is the searchable key in the collection of nodes.
        colNodes.Add(sw, sw.Name)

        Debug.WriteLine("colNodes add switch key: " & sw.Name)
    End Sub

    Private Sub Add_Xena_TestSet(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim ts As New XenaTestSetNode
        AddTestSet(ts, "Xena")
    End Sub

    Private Sub Add_Linux_TestSet(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Debug.WriteLine("Add_Linux_TestSet called...")
    End Sub

    Private Sub AddTestSet(ByVal testset As ClassNode, ByVal make As String)
        Dim idx As Integer

        canvasMain.Children.Add(testset)
        idx = GetAvailableNodeIndex()       'Get next available node index
        testset.prop.Index = idx                    'update the default index 0 to new index
        testset.prop.Category = "TestSet"               'give the TestSet a Category
        testset.Name = testset.prop.Category & "_" & idx           'give the Xena a unique name.
        testset.prop.Nickname = testset.Name                     'Default Nickname to sw.name
        testset.prop.HostName = testset.Name                     'Default hostname to sw.name
        testset.prop.Make = make                          'Manufacturer's Name Ex: Xena
        'testset.prop.HostName = testset.prop.Make & "_" & idx                     'Init the hostname

        Canvas.SetZIndex(testset, 10)          'Move the Xena image to the foreground.

        colNodes.Add(testset, testset.Name)

        Debug.WriteLine("colNodes add TestSet key: " & testset.Name)
    End Sub

    Private Sub Add_Ethernet_Link(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim l As New ClassLink
        AddLink(l, "Ethernet")
    End Sub

    Private Sub Add_Fiber_Link(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim l As New ClassLink
        AddLink(l, "Fiber")
    End Sub

    Private Sub AddLink(ByVal link As ClassLink, ByVal cat As String)
        Dim idx As Integer

        Dim dlgLinkEditor As New LinkEditor
        dlgLinkEditor.Owner = Me
        dlgLinkEditor.ShowDialog()
        If Not dlgLinkEditor.DialogResult Then
            Return
        End If

        'Get selected nodes from ClassLink Editor.
        Dim aNodeName = dlgLinkEditor.lbNodeA.SelectedItem
        Dim bNodeName = dlgLinkEditor.lbNodeB.SelectedItem

        'Using a generic Object instead of specific Router, Switch or Test Set
        'This may get confusing?
        Dim aNodeObj As Object = colNodes.Item(aNodeName)
        Dim bNodeObj As Object = colNodes.Item(bNodeName)

        'Create and draw ClassLink.
        Dim myLink As New ClassLink(aNodeObj, bNodeObj)

        canvasMain.Children.Add(myLink)
        'Get next available ClassLink index
        idx = GetAvailableLinkIndex()

        'Add a default name.  Probably will change later.
        myLink.prop.Index = idx
        myLink.prop.Category = cat
        myLink.Name = myLink.prop.Category & "_" & idx


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

        Debug.Write("Node Category: ")
        Debug.WriteLine(e.Source.[GetType]().ToString())
        Debug.WriteLine("Node Name: " + cn.Name)
        Debug.WriteLine("")

        Me.DeleteNode(cn)

        'This isn't really needed since MainWindow is our root
        'but it is good practice.
        e.Handled = True

    End Sub

    Public Sub DeleteNode(ByVal cn As ClassNode)

        'First delete all dependent links associated with node
        Me.DeleteAllNodeLinks(cn)

        'Second, delete the node
        colNodes.Remove(cn.Name)
        canvasMain.Children.Remove(cn)

    End Sub

    Private Sub DeleteAllNodeLinks(cn As ClassNode)

        'Delete all links associated with node
        For Each l As ClassLink In colLinks
            If ((l.nodeA.Name.Equals(cn.Name)) Or (l.nodeB.Name.Equals(cn.Name))) Then
                Me.DeleteLink(l)
            End If
        Next

    End Sub

    Private Sub DeleteLink(l As ClassLink)

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

        ' Get the ClassLink that raised the event. 
        Dim l As ClassLink = DirectCast(e.Source, ClassLink)
        Debug.Write("ClassLink Type: ")
        Debug.WriteLine(e.Source.[GetType]().ToString())
        Debug.WriteLine("Node Name: " + l.Name)
        Debug.WriteLine("")

        Me.DeleteLink(l)

        'This isn't really needed since MainWindow is our root
        'but it is good practice.
        e.Handled = True

    End Sub

    Private Function GetAvailableNodeIndex()

        Dim idx As Integer
        For idx = 0 To MaxNodes
            Dim isFound As Boolean = False
            For Each cn As ClassNode In colNodes
                If cn.prop.Index.Equals(idx) Then
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
            For Each l As ClassLink In colLinks
                If l.prop.Index.Equals(idx) Then
                    isFound = True
                End If
            Next
            If Not isFound Then
                Return idx
            End If
        Next
        Return idx

    End Function

End Class

