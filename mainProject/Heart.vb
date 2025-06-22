Imports Microsoft.ReportingServices.Diagnostics.Internal
Imports MySqlConnector
Imports System.Data.SqlClient
Imports System.Diagnostics.Eventing
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Heart
    Private db As String = "server=localhost;database=dandmbakery; user id=root;password='' ;ConvertZeroDateTime=True ;"
    Private conn As MySqlConnection
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000 ' WS_EX_COMPOSITED
            Return cp
        End Get
    End Property
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        bar()
        emp_count()
        manager_count()


        Panel1.Visible = False
        Panel2.Visible = False

        Me.WindowState = FormWindowState.Maximized
        'Me.Size = Screen.PrimaryScreen.Bounds.Size
        Me.Location = New Point(0, 0)

        Button1.Anchor = AnchorStyles.None
        Button2.Anchor = AnchorStyles.None
        Button3.Anchor = AnchorStyles.None

        Label1.Text = "ORDER"
        Label2.Text = "INVENTORY"
        Label3.Text = "BILL HISTORY"
        Label4.Text = "SALES"
        Label5.Text = "NUMBER OF EMPLOYEES"
        Label32.Text = "NUMBER OF MANAGERS"
        Label7.Text = "TOTAL AMOUNT RECEIVED TODAY"
        Label6.Text = "TOTAL PRODUCT SOLD TODAY"
        Label8.Text = "USERNAME"
        Label9.Text = "PHONE NUMBER"
        Label10.Text = "EMAIL-ID"
        Label11.Text = "ADDRESS"
        Label12.Text = "CITY"
        Label13.Text = "DOB"
        Label14.Text = "DOJ"
        Label15.Text = "USERNAME"
        Label16.Text = "EMPLOYEE DETAILS"
        Label17.Text = "PHONE NUMBER"
        Label18.Text = "EMAIL-ID"
        Label19.Text = "ADDRESS"
        Label20.Text = "CITY"
        Label21.Text = "DOB"
        Label22.Text = "DOJ"
        Label23.Text = "EMPLOYEES DETAILS"
        Label30.Text = "EMPLOYEE AND MANAGER DETAILS"
        'Button4.Text = "EMPLOYEE DETAILS"
        'Button7.Text = "YOUR DETAILS"
        'Button9.Text = "SAVE"
        Button10.Text = "DELETE"

        TextBox1.Text = "D & M " & vbNewLine & "BAKERY"

        TextBox2.Dock = DockStyle.Top
        TextBox21.Dock = DockStyle.Top
        TextBox2.TextAlign = HorizontalAlignment.Right
        TextBox21.TextAlign = HorizontalAlignment.Right

        Timer1.Start()
        Timer1.Interval = 1000
        TextBox3.Dock = DockStyle.Bottom
        TextBox5.Clear()
        conn = New MySqlConnection(db)
        Dim totalQuantity As Integer = 0
        Dim netamount As Integer = 0


        conn.Open()
        Dim com As New MySqlCommand("SELECT SUM(quantity) AS total_quantity FROM customer WHERE DATE(DnT)=CURDATE()", conn)
        Dim result As Object = com.ExecuteScalar()
        If result IsNot DBNull.Value Then
            totalQuantity = Convert.ToInt32(result)
        End If
        TextBox5.Text = totalQuantity.ToString()

        Dim com1 As New MySqlCommand("SELECT SUM(net_amt) AS total_amount FROM customer WHERE DATE(DnT)=CURDATE()", conn)
        Dim result1 As Object = com1.ExecuteScalar
        If result1 IsNot DBNull.Value Then
            netamount = Convert.ToInt32(result1)
        End If
        TextBox6.Text = netamount.ToString()





        conn.Close()



    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Order.ComboBox1.Items.Clear()
        conn = New MySqlConnection(db)
        conn.Open()
        Using com As New MySqlCommand("SELECT * FROM inventory", conn)
            Using Reader As MySqlDataReader = com.ExecuteReader()
                While Reader.Read()
                    Dim id As Integer = Reader("product_id")
                    Dim product As String = Reader("product_name").ToString()
                    Dim category As Integer = Reader("category_id")
                    Dim sp As Integer = CInt(Reader("MRP"))

                    Order.ComboBox1.Items.Add(id)
                End While
            End Using
        End Using
        Order.TextBox1.Clear()
        Order.TextBox2.Clear()
        Order.TextBox3.Clear()
        Order.TextBox4.Clear()
        Order.TextBox5.Clear()
        Order.TextBox6.Clear()
        Order.ComboBox1.Text = ""
        Order.ListView1.Items.Clear()


        conn.Close()


        Order.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()

        Sales.TextBox1.Clear()
        Sales.TextBox2.Clear()
        Sales.TextBox3.Clear()
        Sales.TextBox4.Clear()
        Sales.TextBox5.Clear()
        Sales.ComboBox1.Text = ""
        Sales.ListView1.Items.Clear()
        'conn = New MySqlConnection(db)

        'conn.Open()
        'Dim com As New MySqlCommand("SELECT * FROM sales", conn)
        'Dim reader As MySqlDataReader = com.ExecuteReader()
        'While reader.Read()
        '    Dim listviewitem As New ListViewItem(reader("product_id").ToString)
        '    listviewitem.SubItems.Add(reader("product_name").ToString)
        '    listviewitem.SubItems.Add(reader("category_id"))
        '    listviewitem.SubItems.Add(reader("gst"))
        '    listviewitem.SubItems.Add(reader("cp"))
        '    listviewitem.SubItems.Add(reader("sp"))
        '    listviewitem.SubItems.Add(reader("MRP"))
        '    listviewitem.SubItems.Add(reader("quantity_sold"))
        '    listviewitem.SubItems.Add(reader("total_cp"))
        '    listviewitem.SubItems.Add(reader("total_sp"))
        '    listviewitem.SubItems.Add(reader("total_MRP"))
        '    listviewitem.SubItems.Add(reader("profit"))
        '    Sales.ListView1.Items.Add(listviewitem)


        'End While
        'reader.Close()

        'conn.Close()
        Sales.Show()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
        Inventory.ListView1.Items.Clear()
        Dim conn As New MySqlConnection(db)
        conn.Open()
        Dim com As New MySqlCommand("select * from inventory ", conn)
        Dim reader As MySqlDataReader = com.ExecuteReader()
        While reader.Read()
            Dim listviewitem As New ListViewItem(reader("product_id").ToString)
            listviewitem.SubItems.Add(reader("product_name").ToString)
            listviewitem.SubItems.Add(reader("category_id").ToString)
            listviewitem.SubItems.Add(reader("gst").ToString)
            listviewitem.SubItems.Add(reader("cp").ToString)
            listviewitem.SubItems.Add(reader("sp").ToString)
            listviewitem.SubItems.Add(reader("MRP").ToString)
            listviewitem.SubItems.Add(reader("quantity").ToString)
            listviewitem.SubItems.Add(reader.GetDateTime("mfd"))
            listviewitem.SubItems.Add(reader.GetDateTime("exp"))
            Inventory.ListView1.Items.Add(listviewitem)

        End While
        reader.Close()
        conn.Close()
        Inventory.Show()
    End Sub



    Private Sub main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Application.Exit()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
        BillHistory.ListView1.Items.Clear()
        BillHistory.ComboBox1.Text = ""
        BillHistory.TextBox1.Clear()
        BillHistory.TextBox2.Clear()
        BillHistory.TextBox3.Clear()
        BillHistory.TextBox4.Clear()
        Using conn As New MySqlConnection(db)
            conn.Open()

            Dim com As New MySqlCommand("SELECT DISTINCT bill_no FROM customer", conn)
            Using reader As MySqlDataReader = com.ExecuteReader()
                BillHistory.ComboBox1.Items.Clear()
                While reader.Read()
                    Dim id As Integer = reader("bill_no")
                    BillHistory.ComboBox1.Items.Add(id)
                End While
            End Using
        End Using
        BillHistory.Show()

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Hide()
        Main.Button7.Show()
        Main.Button8.Show()
        Main.Button14.Show()
        Panel1.Visible = False
        Panel2.Visible = False


        ComboBox1.Text = ""
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox9.Clear()
        TextBox10.Clear()
        TextBox11.Clear()
        TextBox12.Clear()

        ComboBox2.Text = ""
        ComboBox3.Text = ""
        TextBox14.Clear()
        TextBox22.Clear()
        TextBox23.Clear()
        TextBox24.Clear()
        TextBox25.Clear()
        TextBox26.Clear()

        Main.Show()


        conn.Open()
        Dim totalQuantity As Integer = 0
        Dim netamount As Integer = 0
        Dim com As New MySqlCommand("SELECT SUM(quantity) AS total_quantity, SUM(net_amt) AS total_amount FROM customer WHERE DATE(DnT) = CURDATE()", conn)
        Dim reader As MySqlDataReader = com.ExecuteReader()
        If reader.Read() Then
            If Not IsDBNull(reader("total_quantity")) Then
                totalQuantity = Convert.ToInt32(reader("total_quantity"))
            End If
            If Not IsDBNull(reader("total_amount")) Then
                netamount = Convert.ToInt32(reader("total_amount"))
            End If
        End If
        TextBox5.Text = totalQuantity.ToString()
        TextBox6.Text = netamount.ToString()
        reader.Close()
        conn.Close()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        TextBox3.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ComboBox1.Items.Clear()

        Panel1.Visible = True
        Dim conn As New MySqlConnection(db)
        conn.Open()
        Dim com As New MySqlCommand("select * from emp ", conn)
        Dim reader As MySqlDataReader = com.ExecuteReader()
        While reader.Read()
            Dim empname = reader("username")
            ComboBox1.Items.Add(empname)
        End While
        conn.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        combobox1_refresh()
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click

        Panel1.Visible = False
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox9.Clear()
        TextBox10.Clear()
        TextBox11.Clear()
        TextBox12.Clear()
        ComboBox1.Text = ""

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Panel2.Visible = True
        Dim conn As New MySqlConnection(db)
        Dim selectedUser As String = TextBox2.Text
        conn.Open()
        Dim com As New MySqlCommand("select * from emp where username=@username", conn)
        com.Parameters.AddWithValue("@username", selectedUser)
        Dim reader As MySqlDataReader = com.ExecuteReader()
        If reader.Read() Then
            TextBox13.Text = reader("username")
            'TextBox14.Text = reader("password_hash")
            TextBox15.Text = reader("phone")
            TextBox16.Text = reader("email")
            TextBox17.Text = reader("address")
            TextBox18.Text = reader("city")
            TextBox19.Text = reader("dob")
            TextBox20.Text = reader("yoj")
        End If
        conn.Close()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Panel2.Visible = False
    End Sub

    'Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
    '    Dim conn As New MySqlConnection(db)
    '    Dim selectedUser As String = TextBox2.Text
    '    conn.Open()
    '    Dim com As New MySqlCommand("UPDATE emp SET password_hash=@password, phone=@phone, address=@address where username=@username", conn)
    '    com.Parameters.AddWithValue("@username", selectedUser)
    '    com.Parameters.AddWithValue("@password", TextBox14.Text)
    '    com.Parameters.AddWithValue("@phone", TextBox15.Text)
    '    com.Parameters.AddWithValue("@address", TextBox17.Text)
    '    com.ExecuteNonQuery()

    '    conn.Close()
    '    Panel2.Visible = False

    'End Sub


    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        conn = New MySqlConnection(db)
        conn.Open()
        Dim del As String = ComboBox1.Text()
        Dim com2 As New MySqlCommand("Delete from emp where username=@name", conn)
        com2.Parameters.AddWithValue("@name", del)
        Dim delrow As Integer = com2.ExecuteNonQuery()

        MessageBox.Show("Employee Deleted!", "", MessageBoxButtons.OK, MessageBoxIcon.Information)


        ComboBox1.Text = ""
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox9.Clear()
        TextBox10.Clear()
        TextBox11.Clear()
        TextBox12.Clear()
        conn.Close()
        Panel1.Visible = False
        combobox1_refresh()
        emp_count()

    End Sub


    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.MouseHover
        PictureBox2.Visible = False
    End Sub

    Private Sub TextBox6_mouseleave(sender As Object, e As EventArgs) Handles TextBox6.MouseLeave
        PictureBox2.Visible = True
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        ComboBox3.Items.Clear()
        Panel3.Visible = True


    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Panel3.Visible = False
        ComboBox2.Text = ""
        ComboBox3.Text = ""
        TextBox14.Clear()
        TextBox22.Clear()
        TextBox23.Clear()
        TextBox24.Clear()
        TextBox25.Clear()
        TextBox26.Clear()

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        combobox3_refresh()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim position As String = ComboBox2.Text
        Dim query As String = ""

        Dim conn As New MySqlConnection(db)
        conn.Open()
        If position = "MANAGERS" Then
            query = "DELETE FROM manager where username=@username"
        ElseIf position = "EMPLOYEES" Then
            query = "DELETE FROM emp where username=@username"
        End If
        Dim com As New MySqlCommand(query, conn)
        com.Parameters.AddWithValue("@username", ComboBox3.Text())
        Dim reader As MySqlDataReader = com.ExecuteReader()

        MessageBox.Show("User Deleted!", "", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ComboBox2.Text = ""
        ComboBox3.Text = ""
        TextBox14.Clear()
        TextBox22.Clear()
        TextBox23.Clear()
        TextBox24.Clear()
        TextBox25.Clear()
        TextBox26.Clear()
        conn.Close()
        Panel3.Visible = False
        combobox3_refresh()
        emp_count()
        manager_count()
    End Sub



    Public Sub combobox1_refresh()

        Dim conn As New MySqlConnection(db)
        Dim selectedUser As String = ComboBox1.SelectedItem
        conn.Open()
        Dim com As New MySqlCommand("select * from emp where username=@username", conn)
        com.Parameters.AddWithValue("@username", selectedUser)
        Dim reader As MySqlDataReader = com.ExecuteReader()
        If reader.Read() Then
            TextBox7.Text = reader("phone")
            TextBox8.Text = reader("email")
            TextBox9.Text = reader("address")
            TextBox10.Text = reader("city")
            TextBox11.Text = reader("dob")
            TextBox12.Text = reader("yoj")

        End If
        conn.Close()
    End Sub

    Public Sub combobox3_refresh()
        Dim position As String = ComboBox2.Text
        Dim query As String = ""


        If String.IsNullOrEmpty(ComboBox3.Text) Then

            Return
        End If

        If position = "MANAGERS" Then
            query = "SELECT * FROM manager WHERE username = @username"
        ElseIf position = "EMPLOYEES" Then
            query = "SELECT * FROM emp WHERE username = @username"
        Else

            Return
        End If

        Using conn As New MySqlConnection(db)
            Try
                conn.Open()
                Using com As New MySqlCommand(query, conn)
                    com.Parameters.AddWithValue("@username", ComboBox3.Text.Trim())
                    Using reader As MySqlDataReader = com.ExecuteReader()
                        If reader.Read() Then

                            TextBox14.Text = reader("phone").ToString()
                            TextBox22.Text = reader("email").ToString()
                            TextBox23.Text = reader("address").ToString()
                            TextBox24.Text = reader("city").ToString()
                            TextBox25.Text = reader("dob").ToString()
                            TextBox26.Text = reader("yoj").ToString()
                        Else
                            MessageBox.Show("No data found for the selected username.")
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message)
            End Try
        End Using
    End Sub
    Public Sub emp_count()
        conn = New MySqlConnection(db)
        conn.Open()
        Dim com2 As New MySqlCommand("Select Count(*) from emp", conn)
        Dim result2 As Object = com2.ExecuteScalar()
        If result2 IsNot DBNull.Value Then
            Dim count As Integer = result2
            TextBox4.Text = count
        End If
        conn.Close()
    End Sub

    Public Sub manager_count()
        conn = New MySqlConnection(db)
        conn.Open()
        Dim com2 As New MySqlCommand("Select Count(*) from manager", conn)
        Dim result2 As Object = com2.ExecuteScalar()
        If result2 IsNot DBNull.Value Then
            Dim count As Integer = result2
            TextBox27.Text = count
        End If
        conn.Close()
    End Sub
    Public Sub bar()
        Chart1.Series.Clear()

        Dim series As New Series()
        series.Name = "Product Sales"
        series.ChartType = SeriesChartType.Column
        Chart1.Series.Add(series)

        Dim conn As New MySqlConnection(db)
        conn.Open()

        Dim colors As Color() = {Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Purple, Color.Cyan}
        Dim com3 As New MySqlCommand("SELECT cat.category_name, COALESCE(SUM(c.quantity), 0) AS total_quantity_sold FROM categories cat LEFT JOIN inventory i ON cat.category_id = i.category_id LEFT JOIN customer c ON i.product_id = c.product_id AND DATE(c.DnT) = CURDATE() GROUP BY cat.category_name;", conn)
        Dim reader1 As MySqlDataReader = com3.ExecuteReader()

        Dim colorIndex As Integer = 0
        Dim maxQuantity As Integer = Integer.MinValue
        Dim minQuantity As Integer = Integer.MaxValue

        While reader1.Read()
            Dim totalQuantitySold As Integer = Convert.ToInt32(reader1("total_quantity_sold"))
            series.Points.AddXY(reader1("category_name").ToString(), totalQuantitySold)
            series.Points(series.Points.Count - 1).Color = colors(colorIndex Mod colors.Length)
            colorIndex += 1

            ' Update max and min quantities
            If totalQuantitySold > maxQuantity Then
                maxQuantity = totalQuantitySold
            End If
            If totalQuantitySold < minQuantity Then
                minQuantity = totalQuantitySold
            End If
        End While

        conn.Close()

        Chart1.ChartAreas(0).AxisX.Title = "Product Categories"
        Chart1.ChartAreas(0).AxisY.Title = "Quantity Sold"


        If series.Points.Count > 0 Then
            Chart1.ChartAreas(0).AxisY.Minimum = Math.Max(0, minQuantity - 10)
            Chart1.ChartAreas(0).AxisY.Maximum = maxQuantity + 10
        End If


    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        ComboBox3.Items.Clear()
        Dim position As String = ComboBox2.Text
        Dim query As String = ""

        Dim conn As New MySqlConnection(db)
        conn.Open()
        If position = "MANAGERS" Then
            query = "SELECT * FROM manager"
        ElseIf position = "EMPLOYEES" Then
            query = "SELECT * FROM emp"
        End If
        Dim com As New MySqlCommand(query, conn)
        Dim reader As MySqlDataReader = com.ExecuteReader()
        While reader.Read()
            Dim empname = reader("username")
            ComboBox3.Items.Add(empname)
        End While
        conn.Close()
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Dim position As String = ComboBox2.Text
        'Dim query As String = ""

        Dim username As String = ComboBox3.Text
        'Dim plainpassword As String = TextBox18.Text
        Dim phone As String = TextBox14.Text
        Dim email As String = TextBox22.Text
        Dim address As String = TextBox23.Text
        Dim city As String = TextBox24.Text

        Dim conn As New MySqlConnection(db)
        conn.Open()
        If position = "MANAGERS" Then



            Dim update As New MySqlCommand("UPDATE manager SET phone = @phone, email = @email, address = @address, city = @city WHERE username = @username", conn)
            update.Parameters.AddWithValue("@username", username)
            'update.Parameters.AddWithValue("@password_hash", hashedPassword)
            update.Parameters.AddWithValue("@phone", phone)
            update.Parameters.AddWithValue("@email", email)
            update.Parameters.AddWithValue("@address", address)
            update.Parameters.AddWithValue("@city", city)
            'update.Parameters.AddWithValue("@dob", dob)
            'update.Parameters.AddWithValue("@yoj", yoj)
            update.ExecuteNonQuery()
            MessageBox.Show("MANAGER Updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Information)






        ElseIf position = "EMPLOYEES" Then
            Dim update As New MySqlCommand("UPDATE emp SET phone = @phone, email = @email, address = @address, city = @city WHERE username = @username", conn)
            update.Parameters.AddWithValue("@username", username)
            'update.Parameters.AddWithValue("@password_hash", hashedPassword)
            update.Parameters.AddWithValue("@phone", phone)
            update.Parameters.AddWithValue("@email", email)
            update.Parameters.AddWithValue("@address", address)
            update.Parameters.AddWithValue("@city", city)
            'update.Parameters.AddWithValue("@dob", dob)
            'update.Parameters.AddWithValue("@yoj", yoj)
            update.ExecuteNonQuery()
            MessageBox.Show("EMPLOYEE Updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If




        ComboBox2.Text = ""
        ComboBox3.Text = ""
        TextBox14.Clear()
        TextBox22.Clear()
        TextBox23.Clear()
        TextBox24.Clear()
        TextBox25.Clear()
        TextBox26.Clear()
        conn.Close()
        Panel3.Visible = False
        combobox3_refresh()
        emp_count()
        manager_count()
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Dim position As String = ComboBox1.Text
        'Dim query As String = ""

        Dim username As String = ComboBox1.Text
        'Dim plainpassword As String = TextBox18.Text
        Dim phone As String = TextBox7.Text
        Dim email As String = TextBox8.Text
        Dim address As String = TextBox9.Text
        Dim city As String = TextBox10.Text

        Dim conn As New MySqlConnection(db)
        conn.Open()


        Dim update As New MySqlCommand("UPDATE emp SET phone = @phone, email = @email, address = @address, city = @city WHERE username = @username", conn)
        update.Parameters.AddWithValue("@username", username)
        'update.Parameters.AddWithValue("@password_hash", hashedPassword)
        update.Parameters.AddWithValue("@phone", phone)
        update.Parameters.AddWithValue("@email", email)
        update.Parameters.AddWithValue("@address", address)
        update.Parameters.AddWithValue("@city", city)
        'update.Parameters.AddWithValue("@dob", dob)
        'update.Parameters.AddWithValue("@yoj", yoj)
        update.ExecuteNonQuery()
        MessageBox.Show("EMPLOYEE Updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Information)







        ComboBox1.Text = ""
        'ComboBox3.Text = ""
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox9.Clear()
        TextBox10.Clear()
        TextBox11.Clear()
        TextBox12.Clear()
        conn.Close()
        Panel1.Visible = False
        combobox1_refresh()
        emp_count()
        manager_count()
    End Sub
End Class
