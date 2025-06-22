Imports System.Diagnostics.Eventing.Reader
Imports System.Security
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Net.Configuration
Imports MySqlConnector
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar
Imports System.Data.SqlClient
Imports System.Dynamic
Imports System.Drawing.Printing
Imports ThoughtWorks.QRCode.Codec


Public Class Order

    Private db As String = "server=localhost;database=dandmbakery; user id=root;password='';"
    Private conn As MySqlConnection

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000 ' WS_EX_COMPOSITED
            Return cp
        End Get
    End Property

    'Private Sub ButtonPrint_Click(sender As Object, e As EventArgs) Handles ButtonPrint.Click
    '    PrintPreviewDialog1.Document = PrintDocument1
    '    PrintPreviewDialog1.ShowDialog()
    'End Sub

    'Private Sub ButtonPrint_Click(sender As Object, e As EventArgs) Handles ButtonPrint.Click
    '    PrintPreviewDialog1.Document = PrintDocument1
    '    PrintPreviewDialog1.WindowState = FormWindowState.Maximized
    '    PrintPreviewDialog1.ShowDialog()
    'End Sub





    Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim fontHeader As New Font("Georgia", 54, FontStyle.Bold)
        Dim fontSubHeader As New Font("Arial", 12, FontStyle.Bold)
        Dim fontBody As New Font("Arial", 16, FontStyle.Regular)
        Dim fontBodyBold As New Font("Arial", 16, FontStyle.Bold)
        Dim l As New Font("Arial", 20, FontStyle.Regular)
        Dim t As New Font("Georgia", 32, FontStyle.Regular)
        Dim s As New Font("Georgia", 32, FontStyle.Bold)
        Dim g As New Font("Georgia", 20, FontStyle.Regular)
        Dim tx As New Font("Arial", 24, FontStyle.Bold)
        Dim lineHeight As Single = fontBody.GetHeight(e.Graphics) + 2
        Dim x As Single = e.MarginBounds.Left
        Dim y As Single = e.MarginBounds.Top

        e.Graphics.DrawString("    D&M Bakery", fontHeader, Brushes.Black, x, y)
        y += lineHeight * 3
        e.Graphics.DrawString("----------------------------------------------------------------------", l, Brushes.Black, x, y)

        y += lineHeight + 10

        e.Graphics.DrawString("                              GSTIN:", fontBodyBold, Brushes.Black, x, y)
        e.Graphics.DrawString("                                           08AABCD1234F1ZV ", fontBody, Brushes.Black, x, y)
        y += lineHeight
        e.Graphics.DrawString("                               FSSAI No:", fontBodyBold, Brushes.Black, x, y)
        e.Graphics.DrawString("                                                1513013000311", fontBody, Brushes.Black, x, y)

        y += lineHeight - 5
        e.Graphics.DrawString("----------------------------------------------------------------------", l, Brushes.Black, x, y)



        y += lineHeight + 10
        e.Graphics.DrawString("Address: ", fontBodyBold, Brushes.Black, x, y)
        e.Graphics.DrawString("                Shop No 323 Khailand Market, Ajmer 305001", fontBody, Brushes.Black, x, y)
        y += lineHeight
        e.Graphics.DrawString("Phone: ", fontBodyBold, Brushes.Black, x, y)
        e.Graphics.DrawString("            +91 70148 37885", fontBody, Brushes.Black, x, y)
        y += lineHeight - 5
        e.Graphics.DrawString("----------------------------------------------------------------------", l, Brushes.Black, x, y)
        y += lineHeight

        e.Graphics.DrawString("                TAX INVOICE", t, Brushes.Black, x, y)


        y += lineHeight + 30

        e.Graphics.DrawString("Bill No:", fontBodyBold, Brushes.Black, x, y)
        e.Graphics.DrawString("            " & TextBox13.Text, fontBody, Brushes.Black, x, y)


        e.Graphics.DrawString("                                                 Date/Time:", fontBodyBold, Brushes.Black, x, y)
        e.Graphics.DrawString("                                                                     " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), fontBody, Brushes.Black, x, y)


        y += lineHeight - 5
        e.Graphics.DrawString("----------------------------------------------------------------------", l, Brushes.Black, x, y)

        y += lineHeight + 10


        e.Graphics.DrawString("Product ID       Product Name       Price       Quantity       Total", fontBodyBold, Brushes.Black, x, y)
        y += lineHeight - 5
        e.Graphics.DrawString("----------------------------------------------------------------------", l, Brushes.Black, x, y)




        Dim cnt As Integer = 0

        For Each item As ListViewItem In ListView1.Items
            If item.SubItems(2).Text = 0 Then
                cnt = 1
            End If
        Next

        If cnt = 1 Then
            y += lineHeight + 5

            e.Graphics.DrawString("      GST @ 0.0%", fontBodyBold, Brushes.Black, x, y)
            y += lineHeight - 20
            Dim columnWidths1 As Integer() = {155, 195, 100, 130}
            For Each item As ListViewItem In ListView1.Items
                If item.SubItems(2).Text = 0 Then
                    y += lineHeight


                    Dim id As String = item.SubItems(0).Text
                    Dim name As String = item.SubItems(1).Text
                    Dim price As String = item.SubItems(4).Text
                    Dim quantity As String = item.SubItems(5).Text
                    Dim total As String = item.SubItems(6).Text

                    e.Graphics.DrawString(id, fontBody, Brushes.Black, x, y)
                    e.Graphics.DrawString(name, fontBody, Brushes.Black, x + columnWidths1(0), y)
                    e.Graphics.DrawString(price, fontBody, Brushes.Black, x + columnWidths1(0) + columnWidths1(1), y)
                    e.Graphics.DrawString(quantity, fontBody, Brushes.Black, x + columnWidths1(0) + columnWidths1(1) + columnWidths1(2), y)
                    e.Graphics.DrawString(total, fontBody, Brushes.Black, x + columnWidths1(0) + columnWidths1(1) + columnWidths1(2) + columnWidths1(3), y)

                End If
            Next
        End If
        cnt = 0





        For Each item As ListViewItem In ListView1.Items
            If item.SubItems(2).Text = 2.5 Then
                cnt = 1
            End If
        Next

        If cnt = 1 Then
            y += lineHeight + 5

            e.Graphics.DrawString("      GST @ 5%", fontBodyBold, Brushes.Black, x, y)
            y += lineHeight - 20
            Dim columnWidths2 As Integer() = {155, 195, 100, 130}
            For Each item As ListViewItem In ListView1.Items
                If item.SubItems(2).Text = 2.5 Then
                    y += lineHeight


                    Dim id As String = item.SubItems(0).Text
                    Dim name As String = item.SubItems(1).Text
                    Dim price As String = item.SubItems(4).Text
                    Dim quantity As String = item.SubItems(5).Text
                    Dim total As String = item.SubItems(6).Text

                    e.Graphics.DrawString(id, fontBody, Brushes.Black, x, y)
                    e.Graphics.DrawString(name, fontBody, Brushes.Black, x + columnWidths2(0), y)
                    e.Graphics.DrawString(price, fontBody, Brushes.Black, x + columnWidths2(0) + columnWidths2(1), y)
                    e.Graphics.DrawString(quantity, fontBody, Brushes.Black, x + columnWidths2(0) + columnWidths2(1) + columnWidths2(2), y)
                    e.Graphics.DrawString(total, fontBody, Brushes.Black, x + columnWidths2(0) + columnWidths2(1) + columnWidths2(2) + columnWidths2(3), y)

                End If
            Next
        End If
        cnt = 0





        For Each item As ListViewItem In ListView1.Items
            If item.SubItems(2).Text = 6 Then
                cnt = 1
            End If
        Next

        If cnt = 1 Then
            y += lineHeight + 5

            e.Graphics.DrawString("      GST @ 12%", fontBodyBold, Brushes.Black, x, y)
            y += lineHeight - 20
            Dim columnWidths3 As Integer() = {155, 195, 100, 130}
            For Each item As ListViewItem In ListView1.Items
                If item.SubItems(2).Text = 6 Then
                    y += lineHeight

                    Dim id As String = item.SubItems(0).Text
                    Dim name As String = item.SubItems(1).Text
                    Dim price As String = item.SubItems(4).Text
                    Dim quantity As String = item.SubItems(5).Text
                    Dim total As String = item.SubItems(6).Text

                    e.Graphics.DrawString(id, fontBody, Brushes.Black, x, y)
                    e.Graphics.DrawString(name, fontBody, Brushes.Black, x + columnWidths3(0), y)
                    e.Graphics.DrawString(price, fontBody, Brushes.Black, x + columnWidths3(0) + columnWidths3(1), y)
                    e.Graphics.DrawString(quantity, fontBody, Brushes.Black, x + columnWidths3(0) + columnWidths3(1) + columnWidths3(2), y)
                    e.Graphics.DrawString(total, fontBody, Brushes.Black, x + columnWidths3(0) + columnWidths3(1) + columnWidths3(2) + columnWidths3(3), y)

                End If
            Next
        End If
        cnt = 0







        For Each item As ListViewItem In ListView1.Items
            If item.SubItems(2).Text = 9 Then
                cnt = 1
            End If
        Next
        If cnt = 1 Then
            y += lineHeight + 5

            e.Graphics.DrawString("      GST @ 18%", fontBodyBold, Brushes.Black, x, y)
            y += lineHeight - 20
            Dim columnWidths4 As Integer() = {155, 195, 100, 130}
            For Each item As ListViewItem In ListView1.Items
                If item.SubItems(2).Text = 9 Then
                    y += lineHeight

                    Dim id As String = item.SubItems(0).Text
                    Dim name As String = item.SubItems(1).Text
                    Dim price As String = item.SubItems(4).Text
                    Dim quantity As String = item.SubItems(5).Text
                    Dim total As String = item.SubItems(6).Text

                    e.Graphics.DrawString(id, fontBody, Brushes.Black, x, y)
                    e.Graphics.DrawString(name, fontBody, Brushes.Black, x + columnWidths4(0), y)
                    e.Graphics.DrawString(price, fontBody, Brushes.Black, x + columnWidths4(0) + columnWidths4(1), y)
                    e.Graphics.DrawString(quantity, fontBody, Brushes.Black, x + columnWidths4(0) + columnWidths4(1) + columnWidths4(2), y)
                    e.Graphics.DrawString(total, fontBody, Brushes.Black, x + columnWidths4(0) + columnWidths4(1) + columnWidths4(2) + columnWidths4(3), y)

                End If
            Next
        End If
        'cnt = 0






        y += lineHeight - 5
        e.Graphics.DrawString("----------------------------------------------------------------------", l, Brushes.Black, x, y)

        y += lineHeight

        e.Graphics.DrawString("Items: " & TextBox4.Text & "          Qty: " & TextBox5.Text & "          Net Amt: " & TextBox6.Text & " ", tx, Brushes.Black, x, y)


        y += lineHeight
        e.Graphics.DrawString("----------------------------------------------------------------------", l, Brushes.Black, x, y)



        y += lineHeight

        e.Graphics.DrawString("                               GST BREAKUP", g, Brushes.Black, x, y)


        y += lineHeight - 5

        e.Graphics.DrawString("----------------------------------------------------------------------", l, Brushes.Black, x, y)
        y += lineHeight + 10


        e.Graphics.DrawString("Sr No               Taxable Amt          CGST        SGST          Total", fontBodyBold, Brushes.Black, x, y)
        y += lineHeight - 5
        e.Graphics.DrawString("----------------------------------------------------------------------", l, Brushes.Black, x, y)


        y += lineHeight + 10

        Dim srNo As Integer = 1
        Dim netSP As Double = 0
        Dim netCGST As Double = 0
        Dim netSGST As Double = 0
        Dim netTOTAL As Double = 0
        Dim columnWidths As Integer() = {155, 195, 100, 130}
        cnt = 0






        For Each item As ListViewItem In ListView1.Items
            If item.SubItems(2).Text = 0 Then
                cnt = 1
            End If
        Next
        If cnt = 1 Then
            For Each item As ListViewItem In ListView1.Items
                If item.SubItems(2).Text = 0 Then
                    Dim sp As Double = item.SubItems(6).Text - item.SubItems(7).Text
                    Dim cgst As Double = Math.Round(item.SubItems(7).Text / 2.0, 2)
                    Dim sgst As Double = Math.Round(item.SubItems(7).Text / 2.0, 2)
                    Dim total As Double = item.SubItems(6).Text
                    netSP += sp
                    netCGST += cgst
                    netSGST += sgst
                    netTOTAL += total

                    e.Graphics.DrawString(srNo, fontBody, Brushes.Black, x, y)
                    e.Graphics.DrawString(sp, fontBody, Brushes.Black, x + columnWidths(0), y)
                    e.Graphics.DrawString(cgst, fontBody, Brushes.Black, x + columnWidths(0) + columnWidths(1), y)
                    e.Graphics.DrawString(sgst, fontBody, Brushes.Black, x + columnWidths(0) + columnWidths(1) + columnWidths(2), y)
                    e.Graphics.DrawString(total, fontBody, Brushes.Black, x + columnWidths(0) + columnWidths(1) + columnWidths(2) + columnWidths(3), y)

                    y += lineHeight

                    srNo += 1
                End If
            Next
        End If


        For Each item As ListViewItem In ListView1.Items
            If item.SubItems(2).Text = 2.5 Then
                cnt = 1
            End If
        Next
        If cnt = 1 Then
            For Each item As ListViewItem In ListView1.Items
                If item.SubItems(2).Text = 2.5 Then
                    Dim sp As Double = item.SubItems(6).Text - item.SubItems(7).Text
                    Dim cgst As Double = Math.Round(item.SubItems(7).Text / 2.0, 2)
                    Dim sgst As Double = Math.Round(item.SubItems(7).Text / 2.0, 2)
                    Dim total As Double = item.SubItems(6).Text
                    netSP += sp
                    netCGST += cgst
                    netSGST += sgst
                    netTOTAL += total

                    e.Graphics.DrawString(srNo, fontBody, Brushes.Black, x, y)
                    e.Graphics.DrawString(sp, fontBody, Brushes.Black, x + columnWidths(0), y)
                    e.Graphics.DrawString(cgst, fontBody, Brushes.Black, x + columnWidths(0) + columnWidths(1), y)
                    e.Graphics.DrawString(sgst, fontBody, Brushes.Black, x + columnWidths(0) + columnWidths(1) + columnWidths(2), y)
                    e.Graphics.DrawString(total, fontBody, Brushes.Black, x + columnWidths(0) + columnWidths(1) + columnWidths(2) + columnWidths(3), y)

                    y += lineHeight

                    srNo += 1
                End If
            Next
        End If



        For Each item As ListViewItem In ListView1.Items
            If item.SubItems(2).Text = 6 Then
                cnt = 1
            End If
        Next
        If cnt = 1 Then
            For Each item As ListViewItem In ListView1.Items
                If item.SubItems(2).Text = 6 Then
                    Dim sp As Double = item.SubItems(6).Text - item.SubItems(7).Text
                    Dim cgst As Double = Math.Round(item.SubItems(7).Text / 2.0, 2)
                    Dim sgst As Double = Math.Round(item.SubItems(7).Text / 2.0, 2)
                    Dim total As Double = item.SubItems(6).Text
                    netSP += sp
                    netCGST += cgst
                    netSGST += sgst
                    netTOTAL += total

                    e.Graphics.DrawString(srNo, fontBody, Brushes.Black, x, y)
                    e.Graphics.DrawString(sp, fontBody, Brushes.Black, x + columnWidths(0), y)
                    e.Graphics.DrawString(cgst, fontBody, Brushes.Black, x + columnWidths(0) + columnWidths(1), y)
                    e.Graphics.DrawString(sgst, fontBody, Brushes.Black, x + columnWidths(0) + columnWidths(1) + columnWidths(2), y)
                    e.Graphics.DrawString(total, fontBody, Brushes.Black, x + columnWidths(0) + columnWidths(1) + columnWidths(2) + columnWidths(3), y)

                    y += lineHeight

                    srNo += 1
                End If
            Next
        End If


        For Each item As ListViewItem In ListView1.Items
            If item.SubItems(2).Text = 9 Then
                cnt = 1
            End If
        Next
        If cnt = 1 Then
            For Each item As ListViewItem In ListView1.Items
                If item.SubItems(2).Text = 9 Then
                    Dim sp As Double = item.SubItems(6).Text - item.SubItems(7).Text
                    Dim cgst As Double = Math.Round(item.SubItems(7).Text / 2.0, 2)
                    Dim sgst As Double = Math.Round(item.SubItems(7).Text / 2.0, 2)
                    Dim total As Double = item.SubItems(6).Text
                    netSP += sp
                    netCGST += cgst
                    netSGST += sgst
                    netTOTAL += total

                    e.Graphics.DrawString(srNo, fontBody, Brushes.Black, x, y)
                    e.Graphics.DrawString(sp, fontBody, Brushes.Black, x + columnWidths(0), y)
                    e.Graphics.DrawString(cgst, fontBody, Brushes.Black, x + columnWidths(0) + columnWidths(1), y)
                    e.Graphics.DrawString(sgst, fontBody, Brushes.Black, x + columnWidths(0) + columnWidths(1) + columnWidths(2), y)
                    e.Graphics.DrawString(total, fontBody, Brushes.Black, x + columnWidths(0) + columnWidths(1) + columnWidths(2) + columnWidths(3), y)

                    y += lineHeight

                    srNo += 1
                End If
            Next
        End If



        y += lineHeight - 30

        e.Graphics.DrawString("----------------------------------------------------------------------", l, Brushes.Black, x, y)
        y += lineHeight + 10

        e.Graphics.DrawString("", fontBodyBold, Brushes.Black, x, y)
        e.Graphics.DrawString(netSP, fontBodyBold, Brushes.Black, x + columnWidths(0), y)
        e.Graphics.DrawString(netCGST, fontBodyBold, Brushes.Black, x + columnWidths(0) + columnWidths(1), y)
        e.Graphics.DrawString(netSGST, fontBodyBold, Brushes.Black, x + columnWidths(0) + columnWidths(1) + columnWidths(2), y)
        e.Graphics.DrawString(netTOTAL, fontBodyBold, Brushes.Black, x + columnWidths(0) + columnWidths(1) + columnWidths(2) + columnWidths(3), y)


        y += lineHeight

        e.Graphics.DrawString("----------------------------------------------------------------------", l, Brushes.Black, x, y)





        y += lineHeight + 30

        Dim discount As Double = CDbl(TextBox6.Text) - CDbl(TextBox24.Text)
        e.Graphics.DrawString("            ***DISCOUNT AMT: " & discount & "***", tx, Brushes.Black, x, y)





        y += lineHeight + 30


        e.Graphics.DrawString("  ***PAYABLE AMT: " & TextBox24.Text & "***", s, Brushes.Black, x, y)







        y += lineHeight + 30


        Dim gbarcode As New MessagingToolkit.Barcode.BarcodeEncoder
        Try
            Dim barcodeimage As Image
            barcodeimage = New Bitmap(gbarcode.Encode(MessagingToolkit.Barcode.BarcodeFormat.Code128, TextBox13.Text))
            e.Graphics.DrawImage(barcodeimage, 185, y, 400, 100)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        y += lineHeight + 80


        e.Graphics.DrawString("********** Thank You,  Visit Again **********", tx, Brushes.Black, x, y)


    End Sub


















    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListView1.Columns(7).Width = 0
        ListView1.Columns(8).Width = 0
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        PictureBox1.Visible = False
        TextBox13.Text = GetNextBillNumber()
        Me.WindowState = FormWindowState.Maximized
        Me.Size = Screen.PrimaryScreen.Bounds.Size
        Me.Location = New Point(0, 0)



        Label1.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        Label2.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        Label3.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        Label4.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        Label5.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        Label6.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        Label7.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        Label8.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        Label9.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        Button1.Anchor = AnchorStyles.None
        Button2.Anchor = AnchorStyles.None
        Button3.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        Button4.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        Panel1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom
        Panel2.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom
        ListView1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom
        ListView1.Dock = DockStyle.Bottom

        conn = New MySqlConnection(db)
        conn.Open()
        Dim com As New MySqlCommand("SELECT DISTINCT category_id,category_name from categories", conn)
        'Dim com As New MySqlCommand("SELECT * FROM inventory", conn)
        Dim reader As MySqlDataReader = com.ExecuteReader()

        ComboBox1.AutoCompleteMode = AutoCompleteMode.Suggest
        ComboBox1.AutoCompleteSource = AutoCompleteSource.ListItems

        Label1.Text = "Product ID"
        Label2.Text = "Product Name"
        Label3.Text = "Price"
        Label4.Text = "Quantity"
        Label5.Text = "Items"
        Label6.Text = "Quantity"
        Label7.Text = "Net Amount"
        Label8.Text = "Item ID "
        Label9.Text = "Update Quantity"
        Label10.Text = "Product ID"
        Label11.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        Label11.Visible = False
        Label12.Text = "Customer Phone Number "
        Label13.Text = "Bill Number"
        Label14.Text = "Select Bill"
        Label15.Text = "GST%"
        Label16.Text = "Category ID"
        Label17.Text = "Customer Name"
        Label18.Text = "Date Of Birth"
        'Label15.Text = "Cost Price"
        'Button1.BackgroundImage = My.Resources.add_to_cart
        'Button2.BackgroundImage = My.Resources.pen
        'Button3.BackgroundImage = My.Resources.empty_cart
        Button4.Text = "Save"
        Button5.Text = "Cancel"
        Button6.Text = "YES"
        Button7.Text = "NO"
        'Button8.Text = "Heart"
        'Button9.Text = "Bill"
        'Button10.Text = "New.."
        'Button11.Text = "Keep bill On Hold"
        'Button12.Text = "Take bill from Hold"
        RefreshComboBox2()

        ComboBox3.Items.Clear()
        ComboBox1.Items.Clear()
        'If reader.Read() Then
        While reader.Read

            Dim categoryid As Integer = reader("category_id")
            Dim categoryname As String = reader("category_name")
            ComboBox3.Items.Add(categoryid & " - " & categoryname)
        End While
        'Dim product As String = reader("product_name").ToString()
        'Dim sp As Integer = CInt(reader("sp"))
        'End If


        Me.AcceptButton = Button1

        conn.Close()
        ComboBox3.Focus()

    End Sub
    Private Sub ComboBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBox1.KeyDown
        If e.KeyCode = Keys.Right Then
            TextBox3.Focus()
        End If
    End Sub
    Private Sub ComboBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBox3.KeyDown
        If e.KeyCode = Keys.Right Then
            ComboBox1.Focus()
        End If
    End Sub

    Private Sub textbox1_keydown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Right Then
            TextBox3.Focus()
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        conn.Open()

        Dim selectedID As Integer = CInt(ComboBox1.SelectedItem)
        Dim com As New MySqlCommand("SELECT product_name, gst, MRP FROM inventory WHERE product_id = @id", conn)
        com.Parameters.AddWithValue("@id", selectedID)
        Dim reader As MySqlDataReader = com.ExecuteReader()
        If reader.Read() Then
            Dim product As String = reader("product_name").ToString()
            'Dim cp As Integer = CInt(reader("cp"))
            Dim price As Integer = CInt(reader("MRP"))
            TextBox1.Text = product
            TextBox2.Text = price.ToString()
            Dim gst As Double = CDbl(reader("gst"))
            TextBox15.Text = gst / 2
            TextBox23.Text = ((price * gst) / 100) / 2
        End If

        conn.Close()


    End Sub


    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        ComboBox1.Items.Clear()
        ComboBox1.Text = ""
        TextBox1.Clear()
        TextBox2.Clear()
        conn.Open()

        Dim com As New MySqlCommand("SELECT product_id from inventory where category_id=@category_id", conn)
        com.Parameters.AddWithValue("@category_id", ComboBox3.SelectedItem)
        Dim reader As MySqlDataReader = com.ExecuteReader()
        While reader.Read()
            Dim product_id As Integer = reader("product_id")
            ComboBox1.Items.Add(product_id)
        End While

        conn.Close()

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim quantitydb As Integer = 0
        Dim product_namedb As String = String.Empty
        Dim requestedQuantity As Integer

        conn.Open()
        Dim com As New MySqlCommand("SELECT product_name, quantity FROM inventory WHERE product_id = @product_id", conn)
        com.Parameters.AddWithValue("@product_id", ComboBox1.Text)

        Dim reader As MySqlDataReader = com.ExecuteReader()
        If reader.Read() Then
            quantitydb = Convert.ToInt32(reader("quantity"))
            product_namedb = reader("product_name").ToString()
        End If
        reader.Close()

        If String.IsNullOrWhiteSpace(TextBox3.Text) Then
            requestedQuantity = 1
        Else
            requestedQuantity = Convert.ToInt32(TextBox3.Text)
        End If

        If (quantitydb - requestedQuantity) <= 10 Then
            MessageBox.Show("INSUFFICIENT QUANTITY OF * " & product_namedb & " * ", "OUT OF STOCK", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            ComboBox3.Text = ""
            ComboBox3.Focus()
        Else
            getdata()
        End If
        conn.Close()
        ComboBox1.Text = ""
        ComboBox3.Text = ""
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Panel1.Visible = True
        Panel2.Visible = False
        If ListView1.SelectedItems.Count > 0 Then
            Dim selecteditem As ListViewItem = ListView1.SelectedItems(0)
            If selecteditem.SubItems.Count > 0 Then
                TextBox7.Text = selecteditem.SubItems(0).Text

            End If
        End If
        TextBox9.Focus()


    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim newtotalquantity As Integer = 0
        Dim newtotalprice As Integer = 0
        For Each item As ListViewItem In ListView1.Items
            If (TextBox7.Text = item.SubItems(0).Text.ToString()) Then
                item.SubItems(5).Text = TextBox9.Text
            End If
        Next

        For Each item As ListViewItem In ListView1.Items
            'Dim cp As Integer = CInt(item.SubItems(2).Text)
            Dim sp As Integer = CInt(item.SubItems(4).Text)
            Dim quantity As Integer = CInt(item.SubItems(5).Text)
            item.SubItems(6).Text = item.SubItems(4).Text * item.SubItems(5).Text
            newtotalquantity += quantity
            newtotalprice += sp * quantity
            item.SubItems(7).Text = item.SubItems(5).Text * ((item.SubItems(2).Text * item.SubItems(4).Text) / 100)
            item.SubItems(8).Text = item.SubItems(5).Text * ((item.SubItems(2).Text * item.SubItems(4).Text) / 100)

        Next

        TextBox5.Text = newtotalquantity
        TextBox6.Text = newtotalprice.ToString()

        Panel1.Visible = False
        TextBox7.Clear()
        TextBox9.Clear()
    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Panel1.Visible = False
        TextBox7.Clear()
        TextBox9.Clear()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Panel2.Visible = True
        Panel1.Visible = False
        If ListView1.SelectedItems.Count > 0 Then
            Dim selecteditem As ListViewItem = ListView1.SelectedItems(0)
            If selecteditem.SubItems.Count > 0 Then
                TextBox10.Text = selecteditem.SubItems(0).Text

            End If
        End If

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If ListView1.SelectedItems.Count > 0 Then
            ListView1.Items.Remove(ListView1.SelectedItems(0))
        End If

        Dim newtotalquantity1 As Integer = 0
        Dim newtotalprice1 As Integer = 0
        Dim typesOfQuantity1 As Integer = ListView1.Items.Count

        For Each item As ListViewItem In ListView1.Items
            'Dim cp As Integer = CInt(item.SubItems(2).Text)
            Dim sp As Integer = CInt(item.SubItems(4).Text)
            Dim quantity As Integer = CInt(item.SubItems(5).Text)
            item.SubItems(6).Text = item.SubItems(4).Text * item.SubItems(5).Text
            newtotalquantity1 += quantity
            newtotalprice1 += sp * quantity
        Next

        TextBox5.Text = newtotalquantity1
        TextBox6.Text = newtotalprice1
        TextBox4.Text = typesOfQuantity1
        Panel2.Visible = False
        Panel2.Visible = False
        TextBox10.Clear()
    End Sub


    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Panel2.Visible = False
        TextBox10.Clear()

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Panel1.Visible = False
        Panel2.Visible = False
        ComboBox1.SelectedIndex = -1
        ComboBox3.SelectedIndex = -1
        If (TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox13.Text = "" Or TextBox14.Text = "" Or ListView1.Items.Count = 0) Then
            Me.Hide()
            Heart.Show()
        Else
            TextBox14.Clear()
            TextBox13.Clear()
            TextBox4.Clear()
            TextBox5.Clear()
            TextBox6.Clear()
            TextBox13.Clear()
            ListView1.Items.Clear()
            Me.Hide()
            Heart.Show()

        End If
        conn.Open()
        Heart.TextBox5.Clear()
        Heart.TextBox6.Clear()
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
        Heart.TextBox5.Text = totalQuantity.ToString()
        Heart.TextBox6.Text = netamount.ToString()
        reader.Close()
        conn.Close()



        Heart.Chart1.Series.Clear()
        Heart.bar()

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If (TextBox14.Text = "" Or ListView1.Items.Count = 0) Then
            MessageBox.Show("No Customer Name or No items bought", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If


        If (ListView1.Items.Count <= 0) Then
            MessageBox.Show("No items bought", "Empty item list", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else

            Panel3.Visible = True
            PictureBox2.Visible = False
            TextBox16.Visible = False
            Button10.Visible = False

            PictureBox3.Visible = False
            PictureBox4.Visible = False

            If (DateTimePicker1.Value.Day = DateTime.Now.Day And DateTimePicker1.Value.Month = DateTime.Now.Month And TextBox6.Text > 250) Then
                TextBox24.Text = TextBox6.Text - (TextBox6.Text / 100) * 20

            Else
                If (TextBox6.Text >= 500 And TextBox6.Text < 1000) Then
                    TextBox24.Text = TextBox6.Text - (TextBox6.Text / 100) * 5
                ElseIf (TextBox6.Text >= 1000 And TextBox6.Text < 1500) Then
                    TextBox24.Text = TextBox6.Text - (TextBox6.Text / 100) * 10
                ElseIf (TextBox6.Text >= 1500 And TextBox6.Text < 2500) Then
                    TextBox24.Text = TextBox6.Text - (TextBox6.Text / 100) * 15
                ElseIf (TextBox6.Text >= 2500) Then
                    TextBox24.Text = TextBox6.Text - (TextBox6.Text / 100) * 20
                ElseIf (TextBox6.Text < 500) Then
                    TextBox24.Text = TextBox6.Text
                End If
            End If
            TextBox16.Text = TextBox24.Text
            ComboBox3.Text = ""

        End If
        ComboBox1.SelectedIndex = -1
        ComboBox3.SelectedIndex = -1
    End Sub

    'Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
    '    TextBox13.Text = GetNextBillNumber()
    'End Sub

    Private Sub main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Application.Exit()

    End Sub



    Private Function GetNextBillNumber() As Integer

        Dim conn As New MySqlConnection(db)
        conn.Open()
        Dim command As New MySqlCommand("SELECT MAX(bill_no) FROM customer", conn)
        Dim result As Object = command.ExecuteScalar()

        If result Is DBNull.Value Then
            Return 1000001
        Else
            Return CInt(result) + 1
        End If
        conn.Close()
    End Function

    Private Sub getdata()


        If TextBox1.Text <> "" AndAlso TextBox2.Text <> "" Then
            Dim id As Integer = CInt(ComboBox1.Text)
            Dim tot As Integer = 0
            Dim existingItem = ListView1.Items.Cast(Of ListViewItem).FirstOrDefault(Function(item) item.SubItems(0).Text = id.ToString())
            Dim existingprice = ListView1.Items.Cast(Of ListViewItem).FirstOrDefault(Function(item) item.SubItems(0).Text = id.ToString())
            If existingItem IsNot Nothing Then
                TextBox6.Text = 0

                Dim currentQuantity As Integer = CInt(existingItem.SubItems(5).Text)
                Dim newQuantity As Integer
                If TextBox3.Text = "" Then
                    newQuantity = 1
                Else
                    If TextBox3.Text <> "" AndAlso Integer.TryParse(TextBox3.Text, newQuantity) Then
                        newQuantity = CInt(TextBox3.Text)
                    Else
                        MsgBox("Enter Numeric Value")
                        TextBox3.Clear()
                        TextBox3.Focus()
                    End If
                End If
                existingItem.SubItems(5).Text = (currentQuantity + newQuantity).ToString()
                existingprice.SubItems(6).Text = (currentQuantity + newQuantity) * TextBox2.Text

                Dim totalquantity As Integer = 0
                For Each item As ListViewItem In ListView1.Items
                    totalquantity += CInt(item.SubItems(5).Text)
                    item.SubItems(7).Text = item.SubItems(5).Text * TextBox23.Text
                    item.SubItems(8).Text = item.SubItems(5).Text * TextBox23.Text
                Next
                TextBox5.Text = totalquantity.ToString()
                tot = 0
                For Each item As ListViewItem In ListView1.Items
                    tot += CInt(item.SubItems(6).Text)
                Next
                TextBox6.Text = tot.ToString()
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
                TextBox15.Clear()
                TextBox23.Clear()

                ComboBox1.Focus()
                If (ComboBox1.Focused) Then
                    ComboBox1.Text = ""
                End If
            Else

                Dim quantity As Integer
                If TextBox3.Text = "" Then
                    quantity = 1
                    Dim listViewItem As New ListViewItem(id.ToString())
                    listViewItem.SubItems.Add(TextBox1.Text)
                    listViewItem.SubItems.Add(TextBox15.Text)
                    listViewItem.SubItems.Add(TextBox15.Text)
                    listViewItem.SubItems.Add(TextBox2.Text)
                    listViewItem.SubItems.Add(quantity.ToString())
                    listViewItem.SubItems.Add(TextBox2.Text * quantity)
                    listViewItem.SubItems.Add(TextBox23.Text * quantity)
                    listViewItem.SubItems.Add(TextBox23.Text * quantity)

                    ListView1.Items.Add(listViewItem)

                    Dim totalquantity As Integer = 0
                    Dim totalprice As Integer = 0
                    Dim typesOfQuantity As Integer = ListView1.Items.Count

                    For Each item As ListViewItem In ListView1.Items
                        totalquantity += CInt(item.SubItems(5).Text)
                        totalprice += item.SubItems(4).Text * item.SubItems(5).Text
                        item.SubItems(7).Text = item.SubItems(5).Text * TextBox23.Text
                        item.SubItems(8).Text = item.SubItems(5).Text * TextBox23.Text
                    Next
                    TextBox4.Text = typesOfQuantity
                    TextBox5.Text = totalquantity
                    TextBox6.Text = totalprice

                    TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox3.Clear()
                    TextBox15.Clear()
                    ComboBox1.Focus()
                    If (ComboBox1.Focused) Then
                        ComboBox1.Text = ""
                    End If
                Else
                    If TextBox3.Text <> "" AndAlso Integer.TryParse(TextBox3.Text, quantity) Then
                        quantity = CInt(TextBox3.Text)
                        Dim listViewItem As New ListViewItem(id.ToString())
                        listViewItem.SubItems.Add(TextBox1.Text)
                        listViewItem.SubItems.Add(TextBox15.Text)
                        listViewItem.SubItems.Add(TextBox15.Text)
                        listViewItem.SubItems.Add(TextBox2.Text)
                        listViewItem.SubItems.Add(quantity.ToString())
                        listViewItem.SubItems.Add(TextBox2.Text * quantity)
                        listViewItem.SubItems.Add(TextBox23.Text * quantity)
                        listViewItem.SubItems.Add(TextBox23.Text * quantity)

                        ListView1.Items.Add(listViewItem)

                        Dim totalquantity As Integer = 0
                        Dim totalprice As Integer = 0
                        Dim typesOfQuantity As Integer = ListView1.Items.Count

                        For Each item As ListViewItem In ListView1.Items
                            totalquantity += CInt(item.SubItems(5).Text)
                            totalprice += item.SubItems(4).Text * item.SubItems(5).Text
                            'item.SubItems(7).Text = item.SubItems(5).Text * TextBox23.Text
                            'item.SubItems(8).Text = item.SubItems(5).Text * TextBox23.Text
                        Next
                        TextBox4.Text = typesOfQuantity
                        TextBox5.Text = totalquantity
                        TextBox6.Text = totalprice

                        TextBox1.Clear()
                        TextBox2.Clear()
                        TextBox3.Clear()
                        TextBox15.Clear()
                        ComboBox1.Focus()
                        If (ComboBox1.Focused) Then
                            ComboBox1.Text = ""
                        End If
                    Else
                        MsgBox("Enter Numeric Value")
                        TextBox3.Clear()
                        TextBox3.Focus()

                    End If
                End If

            End If
        End If

        'conn.Close()


    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Dim phonenumber As String = TextBox14.Text
        Dim customername As String = TextBox25.Text
        Dim dob As DateTime = DateTimePicker1.Value


        Using conn As New MySqlConnection(db)
            conn.Open()
            If (TextBox14.Text = "" Or ListView1.Items.Count = 0) Then
                MsgBox("No Customer Name or No items bought", MessageBoxButtons.OK)
            Else
                Try
                    getdata()

                    For Each item As ListViewItem In ListView1.Items
                        Dim billno As Integer = CInt(TextBox13.Text)
                        Dim id As Integer = CInt(item.SubItems(0).Text)
                        Dim proName As String = item.SubItems(1).Text
                        'Dim cp As Integer = item.SubItems(2).Text
                        Dim sp As Integer = item.SubItems(4).Text
                        Dim quantity As Integer = CInt(item.SubItems(5).Text)
                        Dim totprice As Integer = CInt(item.SubItems(6).Text)
                        Dim net As Integer = quantity * totprice
                        Dim cgst As Double = CDbl(item.SubItems(2).Text)
                        Dim sgst As Double = CDbl(item.SubItems(3).Text)
                        Dim SGSTamt As Double = CDbl(item.SubItems(7).Text)
                        Dim CGSTamt As Double = CDbl(item.SubItems(8).Text)


                        Using com As New MySqlCommand("INSERT INTO onhold (bill_no,phoneNo, customer_name,dob, product_id, product_name, cgst, sgst,SGSTamt,CGSTamt,MRP, quantity, net_amt) VALUES (@billno,@phoneNo, @customername,@dob, @proid, @items, @cgst, @sgst,@sgstamt,@cgstamt,@MRP, @quantity, @netamt)", conn)
                            com.Parameters.AddWithValue("@billno", billno)
                            com.Parameters.AddWithValue("@proid", id)
                            com.Parameters.AddWithValue("@items", proName)
                            'com.Parameters.AddWithValue("@cp", cp)
                            com.Parameters.AddWithValue("@MRP", sp)
                            com.Parameters.AddWithValue("@quantity", quantity)
                            com.Parameters.AddWithValue("@netamt", totprice)
                            com.Parameters.AddWithValue("@customername", customername)
                            com.Parameters.AddWithValue("@dob", dob)
                            com.Parameters.AddWithValue("@phoneNo", phonenumber)
                            com.Parameters.AddWithValue("@cgst", cgst)
                            com.Parameters.AddWithValue("@sgst", sgst)
                            com.Parameters.AddWithValue("@sgstamt", SGSTamt)
                            com.Parameters.AddWithValue("@cgstamt", CGSTamt)

                            com.ExecuteNonQuery()
                        End Using
                    Next

                    ListView1.Items.Clear()
                    TextBox4.Clear()
                    TextBox5.Clear()
                    TextBox6.Clear()

                    MsgBox("Bill On Hold")

                Catch ex As Exception
                    MsgBox("Error: " & ex.Message)
                End Try

                TextBox13.Clear()
                TextBox14.Clear()
                TextBox25.Clear()
                DateTimePicker1.Value = DateTime.Now

            End If
            conn.Close()
        End Using
        RefreshComboBox2()
        TextBox13.Text = GetNextBillNumber()





    End Sub




    Sub button12_click(sender As Object, e As EventArgs) Handles Button12.Click
        ListView1.Items.Clear()


        Dim tquantity As Integer = 0
        Dim totquantity As Integer = 0
        Dim netamt As Integer = 0
        Using conn As New MySqlConnection(db)
            conn.Open()

            Dim customer_name As String = ComboBox2.SelectedItem
            Dim com As New MySqlCommand("select customer_name,phoneNo,dob,product_id,product_name ,cgst, sgst,SGSTamt,CGSTamt,MRP, quantity, net_amt from onhold where customer_name = @customer_name", conn)
            com.Parameters.AddWithValue("@customer_name", customer_name)

            Using reader As MySqlDataReader = com.ExecuteReader()
                If reader.Read() Then
                    TextBox14.Text = reader("phoneNo").ToString()
                    TextBox25.Text = reader("customer_name").ToString()
                    DateTimePicker1.Value = Convert.ToDateTime(reader("dob"))
                    TextBox13.Text = GetNextBillNumber()



                    Do
                        Dim listviewitem As New ListViewItem(reader("product_id").ToString())
                        listviewitem.SubItems.Add(reader("product_name").ToString())
                        'listviewitem.SubItems.Add(reader("cp").ToString())
                        listviewitem.SubItems.Add(reader("cgst").ToString())
                        listviewitem.SubItems.Add(reader("sgst").ToString())
                        listviewitem.SubItems.Add(reader("MRP").ToString())
                        listviewitem.SubItems.Add(reader("quantity").ToString())
                        listviewitem.SubItems.Add(reader("net_amt").ToString())
                        listviewitem.SubItems.Add(reader("SGSTamt").ToString())
                        listviewitem.SubItems.Add(reader("CGSTamt").ToString())
                        ListView1.Items.Add(listviewitem)


                        tquantity += 1
                        totquantity += CInt(reader("quantity"))
                        netamt += CInt(reader("net_amt"))
                    Loop While reader.Read()


                End If
            End Using

            TextBox4.Text = tquantity.ToString()
            TextBox5.Text = totquantity.ToString()
            TextBox6.Text = netamt.ToString()
            conn.Close()
        End Using


        Using conn As New MySqlConnection(db)
            conn.Open()
            Dim customer_name As String = ComboBox2.SelectedItem
            Dim com As New MySqlCommand("delete from onhold where customer_name = @customer_name", conn)
            com.Parameters.AddWithValue("@customer_name", customer_name)
            com.ExecuteNonQuery()
            conn.Close()
        End Using
        ComboBox2.Text = " "

        RefreshComboBox2()
    End Sub

    Private Sub RefreshComboBox2()
        Using conn As New MySqlConnection(db)
            conn.Open()
            Dim com1 As New MySqlCommand("SELECT DISTINCT customer_name FROM onHold", conn)
            Using reader1 As MySqlDataReader = com1.ExecuteReader()
                ComboBox2.Items.Clear()
                While reader1.Read()
                    Dim customer_name As String = reader1("customer_name")
                    ComboBox2.Items.Add(customer_name)
                End While
            End Using
            conn.Close()
        End Using
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim customerName As String = TextBox25.Text.Trim()
        Dim phoneNumber As String = TextBox14.Text.Trim()

        Using conn As New MySqlConnection(db)
            conn.Open()

            If (RadioButton1.Checked Or RadioButton2.Checked Or RadioButton3.Checked) Then
                If (String.IsNullOrEmpty(customerName) Or ListView1.Items.Count = 0 Or String.IsNullOrEmpty(phoneNumber)) Then
                    MessageBox.Show("No Customer Name or No items bought", "ADD ITEMS", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                getdata()

                ' Insert items into the customer table
                For Each item As ListViewItem In ListView1.Items
                    Dim billno As Integer = CInt(TextBox13.Text)
                    Dim id As Integer = CInt(item.SubItems(0).Text)
                    Dim proName As String = item.SubItems(1).Text
                    Dim sp As Integer = CInt(item.SubItems(4).Text)
                    Dim quantity As Integer = CInt(item.SubItems(5).Text)
                    Dim totprice As Integer = CInt(item.SubItems(6).Text)
                    Dim cgst As Double = CDbl(item.SubItems(2).Text)
                    Dim sgst As Double = CDbl(item.SubItems(3).Text)

                    Using com As New MySqlCommand("INSERT INTO customer (bill_no,phoneNo, customer_name, product_id, product_name, cgst, sgst, MRP, quantity, net_amt, DnT) VALUES (@billno,@phoneNo, @customername, @proid, @items, @cgst, @sgst, @MRP, @quantity, @netamt, @time)", conn)
                        com.Parameters.AddWithValue("@billno", billno)
                        com.Parameters.AddWithValue("@proid", id)
                        com.Parameters.AddWithValue("@items", proName)
                        com.Parameters.AddWithValue("@MRP", sp)
                        com.Parameters.AddWithValue("@quantity", quantity)
                        com.Parameters.AddWithValue("@netamt", totprice)
                        com.Parameters.AddWithValue("@customername", customerName)
                        com.Parameters.AddWithValue("@phoneNo", phoneNumber)
                        com.Parameters.AddWithValue("@time", Label11.Text)
                        com.Parameters.AddWithValue("@cgst", cgst)
                        com.Parameters.AddWithValue("@sgst", sgst)

                        com.ExecuteNonQuery()
                    End Using
                Next

                ' Update inventory and sales
                For Each item As ListViewItem In ListView1.Items
                    Dim quantity As Integer = CInt(item.SubItems(5).Text)
                    Dim id As Integer = CInt(item.SubItems(0).Text)

                    Using cmd As New MySqlCommand("UPDATE inventory SET quantity = quantity - @quantity WHERE product_id = @id;", conn)
                        cmd.Parameters.AddWithValue("@id", id)
                        cmd.Parameters.AddWithValue("@quantity", quantity)
                        cmd.ExecuteNonQuery()
                    End Using

                    Using cmd As New MySqlCommand("UPDATE sales SET quantity_sold = quantity_sold + @quantity WHERE product_id = @id;", conn)
                        cmd.Parameters.AddWithValue("@id", id)
                        cmd.Parameters.AddWithValue("@quantity", quantity)
                        cmd.ExecuteNonQuery()
                    End Using

                    Using cmd As New MySqlCommand("UPDATE sales SET total_cp = quantity_sold * cp, total_sp = quantity_sold * sp, total_MRP = quantity_sold * MRP, profit = CASE WHEN total_sp > total_cp THEN total_sp - total_cp ELSE 0 END WHERE product_id = @id;", conn)
                        cmd.Parameters.AddWithValue("@id", id)
                        cmd.ExecuteNonQuery()
                    End Using
                Next

                ' Check if the phone number and name already exist
                If phoneNumber.Length = 10 AndAlso IsNumeric(phoneNumber) Then
                    Dim checkCommand As New MySqlCommand("SELECT * FROM customer_personal WHERE phonenumber=@pn AND name=@name", conn)
                    checkCommand.Parameters.AddWithValue("@pn", phoneNumber)
                    checkCommand.Parameters.AddWithValue("@name", customerName)

                    Using reader As MySqlDataReader = checkCommand.ExecuteReader()
                        If reader.HasRows Then
                            'MessageBox.Show("A record with this phone number and name already exists.")
                        Else
                            reader.Close() ' Close the reader before executing another command

                            ' Insert new customer data
                            Dim dob As DateTime = DateTimePicker1.Value ' Get the date from the DateTimePicker

                            Dim insertCommand As New MySqlCommand("INSERT INTO customer_personal (name, phonenumber, dob) VALUES (@name, @pn, @dob)", conn)
                            insertCommand.Parameters.AddWithValue("@name", customerName)
                            insertCommand.Parameters.AddWithValue("@pn", phoneNumber)
                            insertCommand.Parameters.AddWithValue("@dob", dob)

                            insertCommand.ExecuteNonQuery()
                            'MessageBox.Show("New customer added successfully.")
                        End If
                    End Using
                Else
                    MessageBox.Show("Please enter a valid 10-digit phone number.")
                End If

                ' Print the receipt
                PrintDocument1.DefaultPageSettings.Margins = New Drawing.Printing.Margins(50, 50, 50, 50) ' Adjust margins if needed
                PrintDocument1.DefaultPageSettings.PaperSize = New Drawing.Printing.PaperSize("", 770, 1500) ' Use A4 paper size
                PrintPreviewDialog1.Document = PrintDocument1
                PrintPreviewDialog1.WindowState = FormWindowState.Maximized
                PrintPreviewDialog1.ShowDialog()

                ' Clear the ListView and input fields
                ListView1.Items.Clear()
                TextBox4.Clear()
                TextBox5.Clear()
                TextBox6.Clear()
                TextBox13.Clear()
                TextBox14.Clear()
                TextBox13.Text = GetNextBillNumber() ' Assuming this function gets the next bill number
                TextBox16.Clear()
                TextBox25.Clear()
                DateTimePicker1.Value = DateTime.Now
                Panel3.Visible = False
                PictureBox1.Visible = False
            Else
                MsgBox("SELECT PAYMENT METHOD")
            End If

            conn.Close()
            RadioButton1.Checked = False
            RadioButton2.Checked = False
            RadioButton3.Checked = False
        End Using
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Panel3.Visible = False
        Panel4.Visible = False
        TextBox21.Clear()
        TextBox14.Clear()
        PictureBox1.Visible = False
        TextBox16.Clear()
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        RadioButton3.Checked = False
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked Then
            Dim objqrcode As QRCodeEncoder = New QRCodeEncoder
            Dim img As Image
            Dim btm As Bitmap
            Dim str As String = "upi://pay?pa=7878601233@ibl&am=" & TextBox24.Text
            objqrcode.QRCodeScale = 3
            img = objqrcode.Encode(str)
            btm = New Bitmap(img)
            btm.Save("qrimage.jpg")
            PictureBox1.ImageLocation = "qrimage.jpg"



            PictureBox3.Visible = True
            PictureBox1.Visible = True
            Timer1.Interval = 5000
            Timer1.Start()

            PictureBox2.Visible = False
            PictureBox4.Visible = False
        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        PictureBox1.Visible = False
        RadioButton2.Checked = False
        Timer1.Stop()


    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked Then


            PictureBox1.Visible = False
            PictureBox2.Visible = False
            PictureBox3.Visible = False
            Button10.Visible = False
            TextBox16.Visible = False

            PictureBox4.Visible = True

            'Panel4.Visible = True
            'TextBox21.Text = TextBox14.Text
            'TextBox18.Focus()
            'TextBox18.PasswordChar = "●"
        End If
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        If (TextBox18.Text.Length = 4 Or TextBox18.Text.Length = 6) Then
            'PrintPreviewDialog1.Document = PrintDocument1
            'PrintPreviewDialog1.WindowState = FormWindowState.Maximized
            'PrintPreviewDialog1.ShowDialog()

            'MessageBox.Show("PAYMENT SUCCESSFUL !!", "CARD PAY", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Panel4.Visible = False
            TextBox18.Clear()
            TextBox21.Clear()
            Button10.PerformClick()
        Else
            MessageBox.Show("INVALID PIN Length", "INVALID", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

    End Sub



    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Using conn As New MySqlConnection(db)
            Dim category_id, category_name As String
            conn.Open()
            Dim search As String = TextBox1.Text
            Dim com As New MySqlCommand("SELECT i.*, c.category_name FROM inventory i JOIN categories c ON i.category_id = c.category_id WHERE i.product_name = @pn", conn)
            com.Parameters.AddWithValue("@pn", search)
            Dim reader As MySqlDataReader = com.ExecuteReader()
            While reader.Read()
                category_id = reader("category_id")
                category_name = reader("category_name")
                ComboBox3.Text = category_id & " - " & category_name
                ComboBox1.Text = reader("product_id")
                TextBox1.Text = reader("product_name")
                TextBox2.Text = reader("MRP")
                TextBox15.Text = reader("gst") / 2
                Dim gstamount As String = (TextBox15.Text * TextBox2.Text) / 100
                TextBox23.Text = gstamount
            End While

            reader.Close()
            conn.Close()
        End Using

    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        PictureBox2.Visible = True
        TextBox16.Visible = True
        Button10.Visible = True

        PictureBox1.Visible = False
        PictureBox3.Visible = False
        PictureBox4.Visible = False
    End Sub

    Private Sub TextBox14_TextChanged(sender As Object, e As EventArgs) Handles TextBox14.TextChanged
        ' Only proceed if the length of the phone number is 10
        If TextBox14.Text.Length = 10 Then
            Using conn As New MySqlConnection(db)
                Dim phonenumber As String = TextBox14.Text.Trim() ' Get the phone number from the TextBox and trim any whitespace

                ' Validate that the phone number is exactly 10 digits and numeric
                If IsNumeric(phonenumber) Then
                    conn.Open()

                    Dim com As New MySqlCommand("SELECT * FROM customer_personal WHERE phonenumber=@pn", conn)
                    com.Parameters.AddWithValue("@pn", phonenumber)
                    Dim reader As MySqlDataReader = com.ExecuteReader()

                    If reader.HasRows Then
                        While reader.Read()
                            TextBox25.Text = reader("name").ToString()
                            DateTimePicker1.Value = Convert.ToDateTime(reader("dob"))
                        End While
                    End If

                    reader.Close()
                    conn.Close()
                Else
                    MessageBox.Show("Please enter a valid 10-digit phone number.")
                End If
            End Using
        End If
    End Sub

    Private Sub Label17_Click(sender As Object, e As EventArgs) Handles Label17.Click

    End Sub
End Class