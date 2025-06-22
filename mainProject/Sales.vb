Imports System.Diagnostics.Eventing.Reader
Imports System.Security
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Net.Configuration
Imports MySqlConnector
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar
Imports System.Data.SqlClient
Imports System.Windows.Forms.DataVisualization.Charting

Public Class Sales
    Private db As String = "server=localhost;database=dandmbakery; user id=root;password='';"
    Private conn As MySqlConnection

    Private Sub Sales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label6.Text = "PORTION OF SALES"
        Chart1.Series.Clear()
        Me.WindowState = FormWindowState.Maximized
        Me.Size = Screen.PrimaryScreen.Bounds.Size

        Me.Location = New Point(0, 0)
        Label1.Text = "Total Cost Price"
        Label2.Text = "Total Selling Price"
        Label3.Text = "Total Profit Amount"
        Label4.Text = "Total MRP"
        Label5.Text = "Search ProductID/Product Name/CategoryID :"

        ListView1.Dock = DockStyle.Bottom
        ListView1.Items.Clear()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        ComboBox1.Text = ""
        'conn = New MySqlConnection(db)
        'conn.Open()
        'Dim com As New MySqlCommand("SELECT * FROM sales", conn)
        'Dim reader As MySqlDataReader = com.ExecuteReader()
        'While reader.Read()
        '    Dim listviewitem As New ListViewItem(reader("product_id").ToString)
        '    listviewitem.SubItems.Add(reader("product_name").ToString)
        '    listviewitem.SubItems.Add(reader("category_id").ToString)
        '    listviewitem.SubItems.Add(reader("gst"))
        '    listviewitem.SubItems.Add(reader("cp"))
        '    listviewitem.SubItems.Add(reader("sp"))
        '    listviewitem.SubItems.Add(reader("MRP"))
        '    listviewitem.SubItems.Add(reader("quantity_sold"))
        '    listviewitem.SubItems.Add(reader("total_cp"))
        '    listviewitem.SubItems.Add(reader("total_sp"))
        '    listviewitem.SubItems.Add(reader("total_MRP"))
        '    listviewitem.SubItems.Add(reader("profit"))
        '    ListView1.Items.Add(listviewitem)
        'End While
        'reader.Close()
        'conn.Close()





    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        ComboBox1.Text = ""
        Chart1.Series.Clear()
        Heart.Show()
    End Sub
    Private Sub main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Application.Exit()

    End Sub



    Public Sub bar(timePeriod As String)
        Chart1.Series.Clear()
        Dim series As New Series()
        series.Name = "Product Sales"
        series.ChartType = SeriesChartType.Column
        Chart1.Series.Add(series)
        Dim colors As Color() = {Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Purple, Color.Cyan}

        ' Database connection
        Dim conn As New MySqlConnection(db)
        conn.Open()

        Dim query As String = ""

        If ComboBox1.Text = "1-MONTH" Then
            timePeriod = "1-MONTH"
            query = "SELECT cat.category_name, COALESCE(SUM(c.quantity), 0) AS total_quantity FROM Customer c JOIN Inventory inv ON c.product_id = inv.product_id JOIN Categories cat ON inv.category_id = cat.category_id WHERE c.DnT >= DATE_SUB(CURDATE(), INTERVAL 1 MONTH) GROUP BY cat.category_name;"
        ElseIf ComboBox1.Text = "3-MONTHS" Then
            timePeriod = "3-MONTHS"
            query = "SELECT cat.category_name, COALESCE(SUM(c.quantity), 0) AS total_quantity FROM Customer c JOIN Inventory inv ON c.product_id = inv.product_id JOIN Categories cat ON inv.category_id = cat.category_id WHERE c.DnT >= DATE_SUB(CURDATE(), INTERVAL 3 MONTH) GROUP BY cat.category_name;"
        ElseIf ComboBox1.Text = "6-MONTHS" Then
            timePeriod = "6-MONTHS"
            query = "SELECT cat.category_name, COALESCE(SUM(c.quantity), 0) AS total_quantity FROM Customer c JOIN Inventory inv ON c.product_id = inv.product_id JOIN Categories cat ON inv.category_id = cat.category_id WHERE c.DnT >= DATE_SUB(CURDATE(), INTERVAL 6 MONTH) GROUP BY cat.category_name;"
        ElseIf ComboBox1.Text = "1-YEAR" Then
            timePeriod = "1-YEAR"
            query = "SELECT cat.category_name, COALESCE(SUM(c.quantity), 0) AS total_quantity FROM Customer c JOIN Inventory inv ON c.product_id = inv.product_id JOIN Categories cat ON inv.category_id = cat.category_id WHERE c.DnT >= DATE_SUB(CURDATE(), INTERVAL 1 YEAR) GROUP BY cat.category_name;"
        Else
            MessageBox.Show("Invalid time period specified.")
                    conn.Close()
                    Return
                End If

                ' Check if the query is empty
                If String.IsNullOrEmpty(query) Then
            MessageBox.Show("No valid query was generated.")
            conn.Close()
            Return
        End If

        Dim com3 As New MySqlCommand(query, conn)
        Dim reader1 As MySqlDataReader = com3.ExecuteReader()
        Dim colorIndex As Integer = 0
        Dim maxQuantity As Integer = Integer.MinValue
        Dim minQuantity As Integer = Integer.MaxValue

        While reader1.Read()
            Dim totalQuantitySold As Integer = Convert.ToInt32(reader1("total_quantity"))
            series.Points.AddXY(reader1("category_name").ToString(), totalQuantitySold)
            series.Points(series.Points.Count - 1).Color = colors(colorIndex Mod colors.Length)
            colorIndex += 1
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



    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        ListView1.Items.Clear()
        Using conn As New MySqlConnection(db)
            conn.Open()
            Dim search As String = TextBox5.Text
            Dim com As New MySqlCommand("SELECT * FROM sales WHERE category_id = @ci OR product_id = @ci OR product_name = @ci", conn)
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
                listviewitem.SubItems.Add(reader("quantity_sold").ToString)
                listviewitem.SubItems.Add(reader("total_cp").ToString)
                listviewitem.SubItems.Add(reader("total_sp").ToString)
                listviewitem.SubItems.Add(reader("total_MRP").ToString)
                listviewitem.SubItems.Add(reader("profit").ToString)
                ListView1.Items.Add(listviewitem)

            End While

            reader.Close()
            conn.Close()
        End Using
        If ListView1.Items.Count = 1 Then
            ListView1.Items(0).Selected = True

        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim query As String = ""

        If ComboBox1.Text = "1-MONTH" Then
            query = "SELECT c.product_id, i.product_name, i.category_id, i.gst, i.Cp AS cost_price, i.SP AS selling_price, i.MRP AS maximum_retail_price, COALESCE(SUM(c.quantity), 0) AS total_quantity_sold, COALESCE(SUM(i.Cp * c.quantity), 0) AS total_cp, COALESCE(SUM(i.SP * c.quantity), 0) AS total_sp, COALESCE(SUM(i.MRP * c.quantity), 0) AS total_mrp, COALESCE(SUM((i.SP - i.Cp) * c.quantity), 0) AS total_profit FROM Customer c LEFT JOIN Inventory i ON c.product_id = i.product_id WHERE c.DnT >= DATE_SUB(CURDATE(), INTERVAL 1 MONTH) GROUP BY c.product_id, i.product_name, i.category_id, i.gst, i.Cp, i.SP, i.MRP;"
            bar("1-MONTH")
        ElseIf ComboBox1.Text = "3-MONTHS" Then
            query = "SELECT c.product_id, i.product_name, i.category_id, i.gst, i.Cp AS cost_price, i.SP AS selling_price, i.MRP AS maximum_retail_price, COALESCE(SUM(c.quantity), 0) AS total_quantity_sold, COALESCE(SUM(i.Cp * c.quantity), 0) AS total_cp, COALESCE(SUM(i.SP * c.quantity), 0) AS total_sp, COALESCE(SUM(i.MRP * c.quantity), 0) AS total_mrp, COALESCE(SUM((i.SP - i.Cp) * c.quantity), 0) AS total_profit FROM Customer c LEFT JOIN Inventory i ON c.product_id = i.product_id WHERE c.DnT >= DATE_SUB(CURDATE(), INTERVAL 3 MONTH) GROUP BY c.product_id, i.product_name, i.category_id, i.gst, i.Cp, i.SP, i.MRP;"
            bar("3-MONTH")
        ElseIf ComboBox1.Text = "6-MONTHS" Then
            query = "SELECT c.product_id, i.product_name, i.category_id, i.gst, i.Cp AS cost_price, i.SP AS selling_price, i.MRP AS maximum_retail_price, COALESCE(SUM(c.quantity), 0) AS total_quantity_sold, COALESCE(SUM(i.Cp * c.quantity), 0) AS total_cp, COALESCE(SUM(i.SP * c.quantity), 0) AS total_sp, COALESCE(SUM(i.MRP * c.quantity), 0) AS total_mrp, COALESCE(SUM((i.SP - i.Cp) * c.quantity), 0) AS total_profit FROM Customer c LEFT JOIN Inventory i ON c.product_id = i.product_id WHERE c.DnT >= DATE_SUB(CURDATE(), INTERVAL 6 MONTH) GROUP BY c.product_id, i.product_name, i.category_id, i.gst, i.Cp, i.SP, i.MRP;"
            bar("6-MONTH")
        ElseIf ComboBox1.Text = "1-YEAR" Then
            query = "SELECT c.product_id, i.product_name, i.category_id, i.gst, i.Cp AS cost_price, i.SP AS selling_price, i.MRP AS maximum_retail_price, COALESCE(SUM(c.quantity), 0) AS total_quantity_sold, COALESCE(SUM(i.Cp * c.quantity), 0) AS total_cp, COALESCE(SUM(i.SP * c.quantity), 0) AS total_sp, COALESCE(SUM(i.MRP * c.quantity), 0) AS total_mrp, COALESCE(SUM((i.SP - i.Cp) * c.quantity), 0) AS total_profit FROM Customer c LEFT JOIN Inventory i ON c.product_id = i.product_id WHERE c.DnT >= DATE_SUB(CURDATE(), INTERVAL 1 YEAR) GROUP BY c.product_id, i.product_name, i.category_id, i.gst, i.Cp, i.SP, i.MRP;"
            bar("1-YEAR")
        End If

        Using conn As New MySqlConnection(db)
            Dim com As New MySqlCommand(query, conn)
            conn.Open()
            Dim totalCP, totalSP, totalMRP, totalProfit As Decimal
            ListView1.Items.Clear()

            Try
                Dim reader As MySqlDataReader = com.ExecuteReader()
                While reader.Read()

                    Dim listviewitem As New ListViewItem(reader("product_id").ToString())
                    listviewitem.SubItems.Add(reader("product_name").ToString())
                    listviewitem.SubItems.Add(reader("category_id").ToString())
                    listviewitem.SubItems.Add(reader("gst").ToString())
                    listviewitem.SubItems.Add(reader("cost_price").ToString())
                    listviewitem.SubItems.Add(reader("selling_price").ToString())
                    listviewitem.SubItems.Add(reader("maximum_retail_price").ToString())
                    listviewitem.SubItems.Add(reader("total_quantity_sold").ToString())
                    listviewitem.SubItems.Add(reader("total_cp").ToString())
                    listviewitem.SubItems.Add(reader("total_sp").ToString())
                    listviewitem.SubItems.Add(reader("total_mrp").ToString())
                    listviewitem.SubItems.Add(reader("total_profit").ToString())
                    ListView1.Items.Add(listviewitem)
                    totalCP += CInt(listviewitem.SubItems(8).Text)
                    totalSP += CInt(listviewitem.SubItems(9).Text)
                    totalMRP += CInt(listviewitem.SubItems(10).Text)
                    totalProfit += CInt(listviewitem.SubItems(11).Text)
                    TextBox1.Text = totalCP.ToString + " Rs."
                    TextBox2.Text = totalSP.ToString + " Rs."
                    TextBox3.Text = totalProfit.ToString + " Rs."
                    TextBox4.Text = totalMRP.ToString + " Rs."
                End While
            Finally
                conn.Close()
            End Try
        End Using
    End Sub
End Class
