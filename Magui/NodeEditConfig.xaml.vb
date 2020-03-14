Imports System.Reflection

Partial Public Class NodeEditConfig
    Inherits Page
    Public Sub New()
        InitializeComponent()
    End Sub

    ' Custom constructor to pass node config data
    Public Sub New(ByRef myNode As ClassNode)
        Me.New()
        ' XAML bind to parent node data.
        DataContext = myNode

        'Use Reflection to get all the properties from the ClassNodeProperties object.
        Dim pi As PropertyInfo() = myNode.prop.GetType().GetProperties()

        'The listbox needs a list to iterate through.
        Dim listOfkvps As New List(Of ClassKVP)
        For Each i In pi
            'Debug.WriteLine("Adding " & i.Name & " = " & i.GetValue(myNode.prop) & " to kvps list.")
            Dim kvp As ClassKVP = New ClassKVP With {
                .key = i.Name,
                .value = i.GetValue(myNode.prop),
                .attr = GetNodePropertiesAttr(i.Name, myNode)
            }
            If (Not kvp.attr Like "None") Then
                Debug.WriteLine("Adding " & kvp.key & " = " & kvp.value & " attr = " & kvp.attr & " to kvps list.")
                listOfkvps.Add(kvp)
            End If
        Next

        kvpDataGrid.ItemsSource = listOfkvps

    End Sub

    Public Sub Edit_Value(ByVal sender As Object, ByVal e As RoutedEventArgs)

        Dim selectedKVP As ClassKVP = kvpDataGrid.SelectedItem

        Debug.WriteLine("NodeEditConfig.xaml.vb Name = " & DataContext.Name)
        If (selectedKVP Is Nothing) Then
            Return
        End If
        If (selectedKVP.attr Like "ViewEdit") Then
            'DataContect is myNode
            Dim editProp As New NodeEditProp(selectedKVP, DataContext)
            NavigationService.Navigate(editProp)
        Else
            MsgBox("You cannot edit a ViewOnly property.")
        End If



    End Sub

End Class
