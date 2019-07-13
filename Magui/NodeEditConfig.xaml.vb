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
            Debug.WriteLine("Adding " & i.Name & " = " & i.GetValue(myNode.prop) & " to kvps list.")
            Dim kvp As ClassKVP = New ClassKVP With {
                .key = i.Name,
                .value = i.GetValue(myNode.prop)
            }
            listOfkvps.Add(kvp)
        Next

        kvpDataGrid.ItemsSource = listOfkvps

    End Sub

End Class
