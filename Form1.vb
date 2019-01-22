
Imports System.IO
Public Class Form1
    Public Class Worker
        Public Name As String
        Public HourlyPay As String
        Public FICA As String
        Public Federal As String
        Public State As String

        Public Sub New(ByVal nme As String, ByVal hourly As String, ByVal fic As String,
                       ByVal federa As String, ByVal stat As String)
            Name = nme
            HourlyPay = hourly
            FICA = fic
            Federal = federa
            State = stat
        End Sub
    End Class

    Dim list As New List(Of Worker)
    Dim editmode As Boolean = False
    Dim addmode As Boolean = False

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click 'add button
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""

        addmode = True
        editmode = False
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox4.Enabled = True
        TextBox5.Enabled = True
        TextBox6.Enabled = True
        Button7.Text = "Add"
        Button7.Enabled = True

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click 'add/update button
        'Dim count As Integer
        If (addmode = True) Then

            ListBox2.Items.Add(TextBox2.Text) 'this is the name
            ListBox1.Items.Add(TextBox2.Text)

            Dim person As New Worker(TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, TextBox6.Text)
            list.Add(person)

            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""
            TextBox6.Text = ""

        ElseIf (editmode = True) Then
            Dim Index As Integer
            Index = ListBox2.SelectedIndex

            list.Item(Index).Name = TextBox2.Text

            list.Item(Index).HourlyPay = TextBox3.Text
            list.Item(Index).FICA = TextBox4.Text
            list.Item(Index).Federal = TextBox5.Text
            list.Item(Index).State = TextBox6.Text

            ListBox2.Items(ListBox2.SelectedIndex) = TextBox2.Text
            ListBox1.Items(ListBox2.SelectedIndex) = TextBox2.Text

            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""
            TextBox6.Text = ""
            editmode = False
            Button7.Text = ""
            Button7.Enabled = False

        End If
        'do nothing

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click 'edit mode
        addmode = False
        editmode = True
        TextBox2.Enabled = True 'name
        TextBox3.Enabled = True 'hourly
        TextBox4.Enabled = True 'Fica
        TextBox5.Enabled = True 'Federal
        TextBox6.Enabled = True 'state
        Button7.Text = "Change"
        Button7.Enabled = True
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load 'PageLoad
        Dim count As Integer
        Dim fStreamReader As StreamReader
        fStreamReader = File.OpenText("employees.txt")
        count = fStreamReader.ReadLine 'auto casts cus count is integer "Val" str -> int
        ListBox2.Items.Clear()
        For i = 0 To count - 1
            Dim name As String
            Dim HourlyPay As String
            Dim FICA As String
            Dim Federal As String
            Dim State As String
            name = fStreamReader.ReadLine
            HourlyPay = fStreamReader.ReadLine
            FICA = fStreamReader.ReadLine
            Federal = fStreamReader.ReadLine
            State = fStreamReader.ReadLine

            Dim person As New Worker(name, HourlyPay, FICA, Federal, State)
            list.Add(person)
            ListBox2.Items.Add(name)
            ListBox1.Items.Add(name)
        Next
        fStreamReader.Close()
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        'listBox of employees
        Button7.Text = ""
        Button7.Enabled = False
        TextBox2.Enabled = False 'name
        TextBox3.Enabled = False 'hourly
        TextBox4.Enabled = False 'Fica
        TextBox5.Enabled = False 'Federal
        TextBox6.Enabled = False 'state
        addmode = False
        editmode = False


        Dim Index As Integer
        Index = ListBox2.SelectedIndex
        If (Index <= list.Count And Index >= 0) Then
            TextBox2.Text = list.Item(Index).Name 'name
            TextBox3.Text = list.Item(Index).HourlyPay 'hourly
            TextBox4.Text = list.Item(Index).FICA 'Fica
            TextBox5.Text = list.Item(Index).Federal 'Federal
            TextBox6.Text = list.Item(Index).State 'state
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click 'save button
        Dim count As Integer
        count = ListBox2.Items.Count
        Dim fStreamWriter As StreamWriter
        fStreamWriter = File.CreateText("employees.txt")
        fStreamWriter.WriteLine(count)
        For i = 0 To count - 1
            fStreamWriter.WriteLine(list.Item(i).Name)
            fStreamWriter.WriteLine(list.Item(i).HourlyPay)
            fStreamWriter.WriteLine(list.Item(i).FICA)
            fStreamWriter.WriteLine(list.Item(i).Federal)
            fStreamWriter.WriteLine(list.Item(i).State)
        Next

        fStreamWriter.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click 'Delete button
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        Dim Index As Integer
        Index = ListBox2.SelectedIndex
        If (Index <= list.Count And Index >= 0) Then
            list.Remove(list.Item(Index))
            ListBox2.Items.RemoveAt(Index) 'ListBox2.SelectedItem()
            ListBox1.Items.RemoveAt(Index)
            ListBox2.Update()
            ListBox1.Update()
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        ' 7 name, 1 hours worked, 8 hourly pay, 9 fics, 10 federal, 11 state, 12 total pay
        Dim Index As Integer
        Index = ListBox1.SelectedIndex
        If (Index <= list.Count And Index >= 0) Then
            TextBox7.Text = list.Item(Index).Name
            TextBox8.Text = list.Item(Index).HourlyPay
            TextBox9.Text = list.Item(Index).FICA
            TextBox10.Text = list.Item(Index).Federal
            TextBox11.Text = list.Item(Index).State
            TextBox1.Text = ""
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Gross As Decimal
        Dim FICa As Decimal
        Dim federal As Decimal
        Dim state As Decimal
        Dim temp As Decimal
        Dim Index As Integer
        Index = ListBox1.SelectedIndex

        Gross = list.Item(Index).HourlyPay * TextBox1.Text
        FICa = Gross * list.Item(Index).FICA
        federal = Gross * list.Item(Index).Federal
        state = Gross * list.Item(Index).State
        temp = FICa + federal + state
        Gross -= temp
        TextBox12.Text = Gross
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If (TextBox1.Text = "") Then
            Button1.Enabled = False
        Else
            Button1.Enabled = True
        End If

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub
End Class
