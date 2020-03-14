Imports System.Reflection

Partial Public Class NodeEditProp
    Inherits Page
    Dim myNode As ClassNode

    Public Sub New()
        InitializeComponent()
    End Sub

    ' Custom constructor to pass node config data
    Public Sub New(ByRef kvp As ClassKVP, ByRef parentNode As ClassNode)
        Me.New()
        ' XAML bind to parent ClassKVP data.
        DataContext = kvp
        myNode = parentNode

        Debug.WriteLine("parentName = " & myNode.Name)
        Debug.WriteLine("Property: Key = " & DataContext.key & " Value = " & DataContext.value)

        tbName.Text = myNode.prop.Nickname

    End Sub

    Private Sub Save_Button_Click(sender As Object, e As RoutedEventArgs)
        ' Edit Node Config
        Debug.WriteLine("Saving " & myNode.Name & " Property: " & DataContext.key & " value = " & DataContext.Value)

        If (DataContext.key <> "") And (DataContext.value <> "") Then
            CallByName(myNode.prop, DataContext.key, CallType.Set, DataContext.value)
        End If
    End Sub


End Class
