Imports System.IO
Imports System.Reflection

Public Module modIO

    'Class used to process data
    Public Class ClassKVP
        Public Property key As String
        Public Property value As String
        Public Property attr As String  'ViewOnly, ViewEdit, None
    End Class

    Public Sub VerifyCreateDir(dir As String)
        'TODO: VerifyCreateDir needs a Try/Catch to return true or false to the calling sub.
        If Not (Directory.Exists(dir)) Then
            Directory.CreateDirectory(dir)
        End If
    End Sub


    Public Function OpenTestbed(ByVal strFile As String)

        Dim obj As Object = New Object
        Dim sr As StreamReader = New StreamReader(strFile)
        Dim c As ClassKVP
        Do
            c = getConfig(sr)   'Load one line of config from  file.

            'Parse Line
            If (c.key Like "CiscoRouter_START*") Then
                obj = New CiscoRouterNode
            ElseIf (c.key Like "CiscoRouter_END*") Then
                colNodes.Add(obj, obj.Name)

            ElseIf (c.key Like "CiscoSwitch_START*") Then
                obj = New CiscoSwitchNode
            ElseIf (c.key Like "CiscoSwitch_END*") Then
                colNodes.Add(obj, obj.Name)

            ElseIf (c.key Like "XenaTestSet_START*") Then
                obj = New XenaTestSetNode
            ElseIf (c.key Like "XenaTestSet_END*") Then
                colNodes.Add(obj, obj.Name)

            ElseIf (c.key Like "*LINK_START*") Then
                obj = New ClassLink
            ElseIf (c.key Like "*LINK_END*") Then
                colLinks.Add(obj, obj.Name)
            Else
                If (c.key <> "") And (c.value <> "") Then
                    Dim key As String = c.key
                    Dim value As String = c.value
                    If key Like "Name" Then
                        CallByName(obj, key, CallType.Set, value)
                    Else
                        CallByName(obj.prop, key, CallType.Set, value)
                    End If
                End If
            End If
        Loop Until c.key = "EOF:"

        sr.Close()  'Close the streamreader

        If colNodes.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function getConfig(ByVal sr As StreamReader) As ClassKVP
        Dim c As New ClassKVP
        Dim line As String
        Try
            line = sr.ReadLine
            If line Is Nothing Then
                c.key = "EOF:"
                c.value = ""
            ElseIf (line Like "*START*") Or (line Like "*END*") Then
                c.key = Trim(line)
                c.value = ""
            ElseIf (line Like "*=*") Then
                Dim temp() = Split(line, "=", 2)
                Dim key = temp(0)
                Dim value = temp(1)
                c.key = Trim(temp(0))
                c.value = Trim(temp(1))
            Else
                c.key = ""
                c.value = ""
            End If
        Catch ex As Exception
            c.key = "EOF:"
            c.value = ""
        End Try

        Return c

    End Function

    Public Sub SaveTestbed(ByVal strFile As String)
        ' Save document
        Using sw As StreamWriter = New StreamWriter(strFile)
            If colNodes.Count > 0 Then
                'Dim test As New NodeProperties
                For Each node As ClassNode In colNodes
                    Dim pi As PropertyInfo() = node.prop.GetType().GetProperties()
                    Dim MakeAndType As String = (node.prop.Make & node.prop.Category)
                    sw.WriteLine(MakeAndType & "_START")
                    sw.WriteLine("Name = " & node.Name)
                    For Each i In pi
                        Debug.WriteLine(i.Name & " = " & i.GetValue(node.prop))
                        sw.WriteLine(i.Name & " = " & i.GetValue(node.prop))
                    Next
                    sw.WriteLine(MakeAndType & "_END")
                Next
            End If
            If colLinks.Count > 0 Then
                For Each link As ClassLink In colLinks
                    Dim pi As PropertyInfo() = link.prop.GetType().GetProperties()
                    sw.WriteLine("LINK_START")
                    sw.WriteLine("Name = " & link.Name)
                    For Each i In pi
                        Debug.WriteLine(i.Name & " = " & i.GetValue(link.prop))
                        sw.WriteLine(i.Name & " = " & i.GetValue(link.prop))
                    Next
                    sw.WriteLine("LINK_END")
                Next
            End If
            sw.Close()
        End Using
    End Sub

    'This the getter function for ClassNodesProperties attributes.
    Public Function GetNodePropertiesAttr(ByVal key As String, ByVal myNode As ClassNode) As String
        Dim attr As String = "None"
        If (myNode.prop.bEdit.Contains(key)) Then
            attr = "ViewEdit"
            Return attr
        ElseIf (myNode.prop.bView.Contains(key)) Then
            attr = "ViewOnly"
            Return attr
        Else
            Return attr
        End If
    End Function



    Public Function DialogBox(ByVal title As String, ByVal msg As String) As Boolean
        Dim style As MsgBoxStyle
        Dim response As MsgBoxResult
        style = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Critical Or MsgBoxStyle.YesNo
        ' Display message.
        response = MsgBox(msg, style, title)
        If response = MsgBoxResult.Yes Then
            Return True
        Else
            Return False
        End If
    End Function
End Module
