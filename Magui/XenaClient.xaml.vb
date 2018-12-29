Imports System.Net.Sockets
Imports System.Windows.Threading

Public Class XenaClient

    Dim client As TcpClient
    Dim stream As NetworkStream
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
        TimerRecv.Interval = New TimeSpan(0, 0, 0, 0, 500)
    End Sub

    Private Sub BtnConnect_Click() Handles BtnConnect.Click
        Dim server As String = TxtIpAddr.Text
        Dim port As Integer = TxtIpPort.Text
        If BtnConnect.Content Like "Connect" Then
            Connect(server, port)
        ElseIf BtnConnect.Content Like "Disconnect" Then
            Disconnect()
        End If
    End Sub

    Private Sub BtnSend_Click() Handles BtnSend.Click
        Try
            ' Send the message to the connected TcpServer. 
            Dim cmd As String = TxtSend.Text & vbCrLf
            Dim data As [Byte]() = System.Text.Encoding.ASCII.GetBytes(cmd)
            stream.Write(data, 0, data.Length)
            Debug.WriteLine("Sent:" & cmd)
            TxtSend.Clear()
            'Enable a 500ms receive TimerRecv to compensate for network delay.
            TimerRecv.Start()
        Catch e As ArgumentNullException
            Debug.WriteLine("ArgumentNullException: {0}", e)
            'Console.WriteLine("ArgumentNullException: {0}", e)
        End Try
    End Sub

    Private Sub Connect(server As [String], port As [Int32])
        Try
            ' Create a TcpClient.
            client = New TcpClient(server, port)
            'If it doesn't throw an exception set the button name to Disconnect.
            BtnConnect.Content = "Disconnect"

            ' Get a client stream for reading and writing.
            '  Stream stream = client.GetStream();
            stream = client.GetStream()
        Catch e As SocketException
            Debug.WriteLine("SocketException: {0}", e)
            BtnConnect.Content = "Connect"
        End Try
    End Sub

    Private Sub Disconnect()
        Try
            ' Close everything.
            stream.Close()
            client.Close()
            BtnConnect.Content = "Connect"

        Catch e As ArgumentNullException
            Debug.WriteLine("ArgumentNullException: {0}", e)
        Catch e As SocketException
            Debug.WriteLine("SocketException: {0}", e)
        End Try

    End Sub

    Private Sub TimerRecv_Tick(ByVal sender As Object, ByVal e As EventArgs)
        TimerRecv.Stop()
        GetResponse()
    End Sub
    Private Sub GetResponse()
        ' Receive the TcpServer.response.
        ' Buffer to store the response bytes.
        Dim resp = New [Byte](256) {}
        ' String to store the response ASCII representation.
        Dim responseData As [String] = [String].Empty

        ' Read the first batch of the TcpServer response bytes.
        If stream.DataAvailable Then
            Dim bytes As Int32 = stream.Read(resp, 0, resp.Length)
            responseData = System.Text.Encoding.ASCII.GetString(resp, 0, bytes)
            TxtResp.AppendText(responseData & vbCrLf)
            Debug.WriteLine("Received:" & responseData)
        Else
            TxtResp.AppendText("No response" & vbCrLf)
            Debug.WriteLine("No response")
        End If


    End Sub

End Class
