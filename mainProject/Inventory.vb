Imports System.Data.SqlClient
Imports System.Net.Configuration
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySqlConnector

Public Class Inventory
    Private db As String = "server=localhost;database=dandmbakery; user id=root;password='' ;ConvertZeroDateTime=True ;"
    Private conn As MySqlConnection
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000 ' WS_EX_COMPOSITED
            Return cp
        End Get
    End Property


    Private Sub inventory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.Size = Screen.PrimaryScreen.Bounds.Size
        Me.Location = New Point(0, 0)


        ListView1.Items.Clear()
        Label1.Text = "Item Name/Item ID/Category ID :"
        Label2.Text = "Category ID "
        Label3.Text = "Product ID "
        Label4.Text = "Product Name "
        Label5.Text = "Manufacturing Date "
        Label6.Text = "Expiration Date "
        Label9.Text = "Cost Price"
        Label13.Text = "MRP"
        Label11.Text = "Quantity"
        Label7.Text = "Product ID "
        Label8.Text = "Quantity "



        'Button2.Text = "Heart"
        'Button3.Text = "Add"
        'Button4.Text = "Edit"
        Button5.Text = "Save"
        Button6.Text = "Cancel"
        Button7.Text = "Save"
        Button8.Text = "Cancel"

        Panel1.Visible = False
        Panel2.Visible = False
        ListView1.Dock = DockStyle.Bottom

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
            ListView1.Items.Add(listviewitem)

        End While
        reader.Close()
        conn.Close()


        conn.Open()
        Dim com1 As New MySqlCommand("SELECT c.category_id, c.category_name, h.gst FROM categories c INNER JOIN hsn h ON c.hsn_code = h.hsn_code ORDER BY c.category_id ASC", conn)
        Dim reader1 As MySqlDataReader = com1.ExecuteReader()
        While reader1.Read()
            Dim id As String = reader1("category_id")
            Dim name As String = reader1("category_name")
            Dim gst As String = reader1("gst")
            ComboBox1.Items.Add(id + " - " + name)
        End While
        reader1.Close()
        conn.Close()



    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        Panel1.Visible = False
        Panel2.Visible = False
        Heart.Show()
        ListView1.Items.Clear()
        TextBox1.Clear()
        Using conn As New MySqlConnection(db)
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
                listviewitem.SubItems.Add(reader("mfd"))
                listviewitem.SubItems.Add(reader("exp"))
                ListView1.Items.Add(listviewitem)

            End While

        End Using


    End Sub


    Private Sub textbox1_textchange(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        ListView1.Items.Clear()
        Using conn As New MySqlConnection(db)
            conn.Open()
            Dim search As String = TextBox1.Text
            Dim com As New MySqlCommand("SELECT * FROM inventory WHERE category_id = @ci OR product_id = @ci OR product_name = @ci", conn)
            com.Parameters.AddWithValue("@ci", search)
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
                listviewitem.SubItems.Add(reader("mfd"))
                listviewitem.SubItems.Add(reader("exp"))
                ListView1.Items.Add(listviewitem)

            End While

            reader.Close()
            conn.Close()
        End Using
        If ListView1.Items.Count = 1 Then
            ListView1.Items(0).Selected = True

        End If
    End Sub
    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        ListView1.Items.Clear()
        If e.KeyCode = Keys.Down Then
            Dim conn As New MySqlConnection(db)
            conn.Open()
            Dim com As New MySqlCommand("SELECT * FROM inventory", conn)
            Dim reader As MySqlDataReader = com.ExecuteReader()
            While reader.Read()
                Dim listviewitem As New ListViewItem(reader("product_id").ToString())
                listviewitem.SubItems.Add(reader("product_name").ToString())
                listviewitem.SubItems.Add(reader("category_id").ToString())
                listviewitem.SubItems.Add(reader("gst").ToString())
                listviewitem.SubItems.Add(reader("cp").ToString())
                listviewitem.SubItems.Add(reader("sp").ToString())
                listviewitem.SubItems.Add(reader("MRP").ToString())
                listviewitem.SubItems.Add(reader("quantity").ToString())
                listviewitem.SubItems.Add(reader.GetDateTime("mfd"))
                listviewitem.SubItems.Add(reader.GetDateTime("exp"))
                ListView1.Items.Add(listviewitem)
            End While
            reader.Close()
            conn.Close()
            TextBox1.Clear()

        End If
    End Sub








    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Panel1.Visible = True
        DateTimePicker2.Value = DateTimePicker1.Value.AddDays(120)
        Dim conn As New MySqlConnection(db)
        conn.Open()
        Dim com As New MySqlCommand("SELECT IFNULL(MAX(product_id), 100000) + 1 AS nextId FROM `inventory`", conn)
        Dim nextId As Integer = com.ExecuteScalar()
        TextBox3.Text = nextId.ToString()
        conn.Close()
        Panel2.Visible = False
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Panel2.Visible = True
        If ListView1.SelectedItems.Count > 0 Then
            Dim selecteditem As ListViewItem = ListView1.SelectedItems(0)
            If selecteditem.SubItems.Count > 0 Then
                TextBox8.Text = selecteditem.SubItems(0).Text

            End If
        End If
        TextBox9.Focus()
        Panel1.Visible = False
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim conn As New MySqlConnection(db)
        conn.Open()
        ' Extract category_id from ComboBox1 selection
        Dim selectedCategory As String = ComboBox1.Text.Split(" "c)(0)

        ' Fetch GST percentage for the selected category
        Dim gstQuery As String = "SELECT h.gst FROM categories c INNER JOIN hsn h ON c.hsn_code = h.hsn_code WHERE c.category_id = @category_id"
        Dim gstCommand As New MySqlCommand(gstQuery, conn)
        gstCommand.Parameters.AddWithValue("@category_id", selectedCategory)
        Dim gstPercentage As Decimal = Convert.ToDecimal(gstCommand.ExecuteScalar())
        Dim sp As Integer = TextBox12.Text - (TextBox12.Text * gstPercentage) / 100

        ' Insert data into inventory table, including GST
        Dim com As New MySqlCommand("INSERT INTO `inventory` ( `product_id`,`product_name`,`category_id`, `gst`,`cp`,`sp`,`MRP`,`quantity`, `mfd`, `exp`) VALUES ( @product_id,@productName,@category_id, @gst,@cp,@sp,@MRP, @Quantity, @manufacturingDate, @expirationDate)", conn)
        com.Parameters.AddWithValue("@product_id", TextBox3.Text)
        com.Parameters.AddWithValue("@productName", TextBox5.Text)
        com.Parameters.AddWithValue("@category_id", selectedCategory)
        com.Parameters.AddWithValue("@gst", gstPercentage)
        com.Parameters.AddWithValue("@cp", TextBox6.Text)
        com.Parameters.AddWithValue("@MRP", TextBox12.Text)
        com.Parameters.AddWithValue("@sp", sp)
        com.Parameters.AddWithValue("@Quantity", TextBox2.Text)
        com.Parameters.AddWithValue("@manufacturingDate", DateTimePicker1.Value)
        com.Parameters.AddWithValue("@expirationDate", DateTimePicker2.Value)
        com.ExecuteNonQuery()

        ' Update the ListView1
        ListView1.Items.Clear()
        Dim com2 As New MySqlCommand("SELECT * FROM `inventory`", conn)
        Dim reader As MySqlDataReader = com2.ExecuteReader()
        While reader.Read()
            Dim listviewitem As New ListViewItem(reader("product_id").ToString)
            listviewitem.SubItems.Add(reader("product_name").ToString)
            listviewitem.SubItems.Add(reader("category_id").ToString)
            listviewitem.SubItems.Add(reader("gst").ToString)
            listviewitem.SubItems.Add(reader("cp").ToString)
            listviewitem.SubItems.Add(reader("sp").ToString)
            listviewitem.SubItems.Add(reader("MRP").ToString)
            listviewitem.SubItems.Add(reader("quantity").ToString)
            listviewitem.SubItems.Add(reader("mfd"))
            listviewitem.SubItems.Add(reader("exp"))
            ListView1.Items.Add(listviewitem)
        End While
        reader.Close()

        ' Insert data into sales table with GST and category_id
        Dim com4 As New MySqlCommand("INSERT INTO `sales` ( `product_id`, `product_name`, `category_id`, `gst` ,`cp`, `sp`,`MRP`,`quantity_sold`,`total_cp`,`total_sp`,`total_MRP`,`profit`) VALUES (@product_id, @productName, @category_id, @gst,@cp, @sp,@MRP,@quantity_sold,@totalcp,@totalsp,@totalMRP,@profit)", conn)
        com4.Parameters.AddWithValue("@product_id", TextBox3.Text)
        com4.Parameters.AddWithValue("@productName", TextBox5.Text)
        com4.Parameters.AddWithValue("@category_id", selectedCategory)
        com4.Parameters.AddWithValue("@gst", gstPercentage)
        com4.Parameters.AddWithValue("@cp", TextBox6.Text)
        com4.Parameters.AddWithValue("@MRP", TextBox12.Text)
        com4.Parameters.AddWithValue("@sp", sp)
        com4.Parameters.AddWithValue("@quantity_sold", 0)
        com4.Parameters.AddWithValue("@totalcp", 0)
        com4.Parameters.AddWithValue("@totalsp", 0)
        com4.Parameters.AddWithValue("@totalMRP", 0)
        com4.Parameters.AddWithValue("@profit", 0)
        com4.ExecuteNonQuery()

        ' Cleanup
        conn.Close()
        ComboBox1.Text = ""
        TextBox3.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox12.Clear()
        'TextBox7.Clear()
        TextBox2.Clear()

        ' Generate next product ID
        conn.Open()
        Dim com5 As New MySqlCommand("SELECT IFNULL(MAX(product_id), 100000) + 1 AS nextId FROM `inventory`", conn)
        Dim nextId As Integer = com5.ExecuteScalar()
        TextBox3.Text = nextId.ToString()
        conn.Close()
    End Sub


    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ComboBox1.Text = ""
        TextBox3.Clear()
        TextBox5.Clear()

        Panel1.Visible = False
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim newtotalquantity As Integer = 0

        For Each item As ListViewItem In ListView1.Items
            If (TextBox8.Text = item.SubItems(0).Text.ToString()) Then
                item.SubItems(3).Text = TextBox9.Text
            End If
        Next

        For Each item As ListViewItem In ListView1.Items

            Dim quantity As Integer = CInt(item.SubItems(3).Text)
            newtotalquantity += quantity

        Next

        TextBox5.Text = newtotalquantity
        Dim conn As New MySqlConnection(db)
        conn.Open()
        Dim com As New MySqlCommand("UPDATE inventory SET Quantity = @Quantity WHERE product_id = @ID;", conn)

        com.Parameters.AddWithValue("@ID", TextBox8.Text)
        com.Parameters.AddWithValue("@Quantity", TextBox9.Text)

        com.ExecuteNonQuery()
        ListView1.Items.Clear()
        Dim com2 As New MySqlCommand("SELECT * FROM `inventory`", conn)
        Dim reader As MySqlDataReader = com2.ExecuteReader()
        While reader.Read()
            Dim listviewitem As New ListViewItem(reader("product_id").ToString)
            listviewitem.SubItems.Add(reader("product_name").ToString)
            listviewitem.SubItems.Add(reader("category_id").ToString)
            listviewitem.SubItems.Add(reader("gst").ToString)
            listviewitem.SubItems.Add(reader("cp").ToString)
            listviewitem.SubItems.Add(reader("sp").ToString)
            listviewitem.SubItems.Add(reader("MRP").ToString)
            listviewitem.SubItems.Add(reader("quantity").ToString)
            listviewitem.SubItems.Add(reader("mfd"))
            listviewitem.SubItems.Add(reader("exp"))
            ListView1.Items.Add(listviewitem)
        End While



        conn.Close()

        Panel2.Visible = False
        TextBox9.Clear()
    End Sub


    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Panel1.Visible = False
        TextBox3.Clear()
        TextBox5.Clear()
        TextBox6.Clear()


    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        TextBox9.Clear()
        Panel2.Visible = False
    End Sub

    Private Sub main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Application.Exit()
    End Sub



End Class