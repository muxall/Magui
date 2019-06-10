Module ModGlobals
    Public colNodes As New Collection
    Public colLinks As New Collection
    Public MaxNodes As Integer = 1000
    Public MaxLinks As Integer = 4000
    Public defaultDir = (My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\magui")
    Public defaultFileFilter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
    Public configFile As String
    Public defaultConfigFile As String = "MaguiConfig.txt"
End Module
