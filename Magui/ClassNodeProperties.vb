Imports System.ComponentModel

Public Class ClassNodeProperties
    'A Class of Key Value pairs that have public getters and setters.  If you add a Public Property aka Key,
    'You must give it view or edit permissions otherwise it will be hidden from the GUI.

    'Location X,Y coordinates.
    Public Property Left As Double = 0.0
    Public Property Top As Double = 0.0

    'Node data
    Public Property Index As Integer = 0
    Public Property Category As String = "node"
    Public Property Make As String = "make"
    Public Property Model As String = "model"
    Public Property HostName As String = "hostname"
    Public Property Nickname As String = "cnpLabel"

    'bView and bEdit are arrays of strings that set the attributes for the Public Property keys above.
    ' None, ViewOnly, and ViewEdit are attr for bView and bEdit.  See modIO.vb for getter function.
    Public bView() = {"Category", "Make"}
    Public bEdit() = {"HostName", "Nickname", "Model"}

End Class
