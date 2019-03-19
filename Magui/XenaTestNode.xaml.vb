﻿
Public Class XenaTestNode : Inherits ClassNode

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        'Pass the derived ucNode object reference to the base class.
        MyBase.dNode = Me.ucNode
        MyBase.dCanvas = Me.ucCanvas

    End Sub

    Private Sub SendCmd_Node(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        Dim xena As New XenaClient
        xena.Owner = Window.GetWindow(Me)
        xena.Show()
        Debug.WriteLine("SendCmd Node Clicked! Node: ")

    End Sub

End Class
