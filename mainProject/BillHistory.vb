Imports System.Diagnostics.Eventing
Imports MySqlConnector

Public Class BillHistory
    Private db As String = "server=localhost;database=dandmbakery;user id=root;password='';"

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000 ' WS_EX_COMPOSITED
            Return cp
        End Get
    End Property
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ComboBox1.Items.Clear()
        ListView1.Items.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        Me.Hide()
        heart.Show()

    End Sub

    Private Sub BillHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.Size = Screen.PrimaryScreen.Bounds.Size

        Me.Location = New Point(0, 0)
        ListView1.Dock = DockStyle.Bottom

        Label1.Text = "Items"
        Label2.Text = "Quantity"
        Label3.Text = "Net Amount"
        Label4.Text = "Customer Name"
        Label8.Text = "Phone Number"

        ComboBox1.Items.Clear()
        ListView1.Items.Clear()

        Using conn As New MySqlConnection(db)
            conn.Open()

            Dim com As New MySqlCommand("SELECT DISTINCT bill_no FROM customer", conn)
            Using reader As MySqlDataReader = com.ExecuteReader()
                ComboBox1.Items.Clear()
                While reader.Read()
                    Dim id As Integer = reader("bill_no")
                    ComboBox1.Items.Add(id)
                End While
            End Using
        End Using
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ListView1.Items.Clear()
        Dim tquantity, totquantity, netamt As Integer
        Using conn As New MySqlConnection(db)
            conn.Open()
            Dim selectedID As Integer = CInt(ComboBox1.SelectedItem)
            Dim com As New MySqlCommand("SELECT customer_name,product_id,phoneNo, product_name,cgst, sgst, MRP, quantity, net_amt, DnT FROM customer WHERE bill_no = @billno", conn)
            com.Parameters.AddWithValue("@billno", selectedID)
            Using reader As MySqlDataReader = com.ExecuteReader()
                If reader.Read() Then
                    TextBox4.Text = reader("customer_name").ToString()
                    TextBox5.Text = reader("DnT").ToString()
                    TextBox6.Text = reader("phoneNo").ToString()

                    Do
                        Dim listviewitem As New ListViewItem(reader("product_id").ToString())
                        listviewitem.SubItems.Add(reader("product_name").ToString())
                        listviewitem.SubItems.Add(reader("MRP").ToString())
                        listviewitem.SubItems.Add(reader("quantity").ToString())
                        listviewitem.SubItems.Add(reader("net_amt").ToString())
                        listviewitem.SubItems.Add(reader("cgst").ToString())
                        listviewitem.SubItems.Add(reader("sgst").ToString())
                        ListView1.Items.Add(listviewitem)
                    Loop While reader.Read()

                    For Each item As ListViewItem In ListView1.Items
                        tquantity = ListView1.Items.Count
                        totquantity += CInt(item.SubItems(3).Text)
                        netamt += CInt(item.SubItems(4).Text)
                    Next
                    TextBox1.Text = tquantity
                    TextBox2.Text = totquantity
                    TextBox3.Text = netamt

                End If
            End Using
        End Using
    End Sub

    Private Sub main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Application.Exit()

    End Sub


End Class