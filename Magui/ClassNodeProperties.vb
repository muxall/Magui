Imports System.ComponentModel

Public Class ClassNodeProperties
    'Implements INotifyPropertyChanged

    'Class used to process data
    Public Property HostName As String = "hostname"
    'Private myNickname As String = "cnplabel"
    Public Property Nickname As String = "cnpLabel"
    Public Property Index As Integer = 0
    Public Property Category As String = "node"
    Public Property Make As String = "make"
    Public Property Model As String = "model"
    Public Property Left As Double = 0.0
    Public Property Top As Double = 0.0

    'Public Property Nickname() As String
    '    Get
    '        Return myNickname
    '    End Get
    '    Set(ByVal value As String)
    '        myNickname = value
    '        ' Call OnPropertyChanged whenever the property is updated
    '        OnPropertyChanged("Nickname")
    '    End Set
    'End Property

    'Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    '' Create the OnPropertyChanged method to raise the event
    'Protected Sub OnPropertyChanged(ByVal name As String)
    '    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
    'End Sub

End Class
