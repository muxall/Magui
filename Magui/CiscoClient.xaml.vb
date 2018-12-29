
Imports Renci.SshNet
Imports Renci.SshNet.Common
Imports System.Windows.Threading

Public Class CiscoClient
    Dim client As SshClient
    Dim stream As ShellStream
    Dim TimerRecv As DispatcherTimer

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        InitTimer()
    End Sub

    Private Sub InitTimer()
        TimerRecv = New DispatcherTimer()
        AddHandler TimerRecv.Tick, AddressOf TimerRecv_Tick
        'TimeSpan is days,hours,minutes,seconds,milliseconds
        TimerRecv.Interval = New TimeSpan(0, 0, 0, 1, 0)
    End Sub

    Private Sub BtnConnect_Click() Handles BtnConnect.Click
        Dim server As String = TxtIpAddr.Text
        Dim port As Integer = TxtIpPort.Text
        Dim username As String = "cisco"
        Dim password As String = "cisco"
        Dim connectionInfo = New PasswordConnectionInfo(server, port, username, password)
        connectionInfo.Timeout = TimeSpan.FromSeconds(5)
        If BtnConnect.Content Like "Connect" Then
            Connect(connectionInfo)
        ElseIf BtnConnect.Content Like "Disconnect" Then
            Disconnect()
        End If
    End Sub

    Private Sub Connect(connectionInfo As [PasswordConnectionInfo])
        Try
            ' Create a SshClient
            client = New SshClient(connectionInfo)
            client.Connect()

            'If it doesn't throw an exception set the button name to Disconnect.
            BtnConnect.Content = "Disconnect"

            'Dim dict = New Dictionary(Of TerminalModes, UInt32)
            'dict.Add(TerminalModes.ECHO, 0)
            'dict.Item(TerminalModes.ECHO) = 0
            'stream = client.CreateShellStream("vt100", 80, 24, 800, 600, 1024, dict)

            stream = client.CreateShellStream("CLI", 0, 0, 0, 0, 1024)

            ' disable terminal paging.
            TxtResp.AppendText("Disable terminal paging." & vbCrLf)
            stream.WriteLine("terminal length 0")
            stream.Flush()
            TimerRecv.Start()

        Catch e As ArgumentException
            Debug.WriteLine("Host or Username is invalid: {0}", e)
            BtnConnect.Content = "Connect"
        Catch e As SshException
            Debug.WriteLine("SshException: {0}", e)
            BtnConnect.Content = "Connect"
        End Try
    End Sub

    Private Sub BtnSend_Click() Handles BtnSend.Click
        Try
            ' Send the message to the connected DUT. 
            Dim cmd = TxtSend.Text
            stream.WriteLine(cmd)
            stream.Flush()
            TxtResp.AppendText("Sent: " & cmd & vbCrLf)
            Debug.WriteLine("Sent:" & cmd)
            TxtSend.Clear()
            'Enable a 1000ms receive timer to compensate for network delay.
            TimerRecv.Start()
        Catch e As ArgumentNullException
            Debug.WriteLine("ArgumentNullException: {0}", e)
            'Console.WriteLine("ArgumentNullException: {0}", e)
        End Try
    End Sub

    Private Sub TimerRecv_Tick(ByVal sender As Object, ByVal e As EventArgs)
        TimerRecv.Stop()
        GetResponse()
    End Sub

    Private Sub GetResponse()
        ' Read the first batch of the SshServer response .
        If stream.DataAvailable Then
            Dim strResponse = stream.Read()

            TxtResp.AppendText("Received: " & strResponse & vbCrLf)
            Debug.WriteLine("Received:" & strResponse)
        Else
            TxtResp.AppendText("No response" & vbCrLf)
            Debug.WriteLine("No response")
        End If

    End Sub


    Private Sub Disconnect()
        Try
            TxtResp.AppendText("Disconnecting...")
            stream.Close()
            client.Disconnect()
            BtnConnect.Content = "Connect"
            TxtResp.AppendText("Done!" & vbCrLf)
        Catch e As ObjectDisposedException
            Debug.WriteLine("ObjectDisposedException: {0}", e)
        End Try

    End Sub

End Class
