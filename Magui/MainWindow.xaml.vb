
Public Class MainWindow

    Private Sub Add_Router(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        Dim router As New CiscoRouterNode
        canvasMain.Children.Add(router)
        Debug.WriteLine("Add Router Clicked!")

    End Sub

    Private Sub Add_Switch(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        Dim switch As New CiscoSwitchNode
        canvasMain.Children.Add(switch)
        Debug.WriteLine("Add Switch Clicked! ")

    End Sub

    Private Sub Add_TestSet(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        Dim xena As New XenaTestNode
        canvasMain.Children.Add(xena)
        Debug.WriteLine("Add Xena TestSet Clicked!")

    End Sub

    Private Sub Add_Link(ByVal sender As Object, ByVal e As MouseButtonEventArgs)

        Debug.WriteLine("Add Link Clicked!")
        ' Add a Line Element
        Dim myLine As New Line()
        myLine.Stroke = Brushes.LightSteelBlue
        myLine.X1 = 1
        myLine.X2 = 50
        myLine.Y1 = 1
        myLine.Y2 = 50
        myLine.HorizontalAlignment = HorizontalAlignment.Left
        myLine.VerticalAlignment = VerticalAlignment.Center
        myLine.StrokeThickness = 2
        canvasMain.Children.Add(myLine)

    End Sub

End Class

