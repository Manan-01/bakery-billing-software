Imports System.Drawing
Imports MySqlConnector
Imports System.Security.Cryptography
Imports System.Text


Public Class Main
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000 ' WS_EX_COMPOSITED
            Return cp
        End Get
    End Property

    Private images As Image() = {My.Resources.M_D_Bakers, My.Resources.Orange_Black_Simple_Modern_Baked_Cakes_Presentation, My.Resources.Untitled_4_}
    Private Const duration As Integer = 2500
    Private currentIndex = 0
    Private totalImages = 3

    Private db As String = "server=localhost;database=dandmbakery; user id=root;password='' ;ConvertZeroDateTime=True ;"
    Private conn As MySqlConnection
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Interval = duration
        Timer1.Start()
        Me.BackgroundImage = images(currentIndex)
        Button7.Visible = False
        Button8.Visible = False
        Button14.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False

        Label5.Text = "Username"
        Label6.Text = "Password"
        Label7.Text = "Username"
        Label8.Text = "Password"
        Label9.Text = "Username"
        Label10.Text = "Password"
        Label11.Text = "Phone Number "
        Label12.Text = "Email ID "
        Label13.Text = "Address"
        Label14.Text = "City"
        Label15.Text = "DOB"
        Label16.Text = "YOJ"
        Label19.Text = "Username"
        Label20.Text = "Password"
        Label21.Text = "Phone Number "
        Label22.Text = "Email ID "
        Label23.Text = "Address"
        Label24.Text = "City"
        Label25.Text = "DOB"
        Label26.Text = "YOJ"
        Button5.Text = "Login"
        Button6.Text = "Login"
        Button17.Text = "Login"
        Button11.Text = "NEW USER"
        Button15.Text = "NEW USER"
        Button12.Text = "ADD"
        Button19.Text = "ADD"
        Label17.Text = "Username"
        Label18.Text = "Password"

        Me.WindowState = FormWindowState.Maximized
        PictureBox1.Dock = DockStyle.Top
        PictureBox1.Image = My.Resources.output_onlinegiftools
        PictureBox2.Image = My.Resources.donut_unscreen

        'Me.Size = Screen.PrimaryScreen.Bounds.Size
        'Me.Location = New Point(0, 0)
        Heart.TextBox4.Visible = False
        Heart.TextBox5.Visible = False
        Heart.TextBox6.Visible = False
        Heart.TextBox27.Visible = False
        Heart.TextBox27.Enabled = False
        Heart.Label32.Visible = False
        Heart.Label32.Enabled = False
        Heart.TextBox4.Enabled = False
        Heart.TextBox5.Enabled = False
        Heart.TextBox6.Enabled = False
        Heart.Label5.Enabled = False
        Heart.Label6.Enabled = False
        Heart.Label7.Enabled = False
        Heart.Label5.Visible = False
        Heart.Label6.Visible = False
        Heart.Label7.Visible = False
        Heart.TextBox13.Enabled = False
        Heart.TextBox13.Visible = False
        'Heart.TextBox14.Enabled = False
        'Heart.TextBox14.Visible = False
        Heart.TextBox15.Enabled = False
        Heart.TextBox15.Visible = False
        Heart.TextBox16.Enabled = False
        Heart.TextBox16.Visible = False
        Heart.TextBox17.Enabled = False
        Heart.TextBox17.Visible = False
        Heart.TextBox18.Enabled = False
        Heart.TextBox18.Visible = False
        Heart.TextBox19.Enabled = False
        Heart.TextBox19.Visible = False
        Heart.TextBox20.Enabled = False
        Heart.TextBox20.Visible = False
        Heart.Label15.Visible = False
        Heart.Label15.Enabled = False
        'Heart.Label16.Visible = False
        'Heart.Label16.Enabled = False
        Heart.Label17.Visible = False
        Heart.Label17.Enabled = False
        Heart.Label18.Visible = False
        Heart.Label18.Enabled = False
        Heart.Label19.Visible = False
        Heart.Label19.Enabled = False
        Heart.Label20.Visible = False
        Heart.Label20.Enabled = False
        Heart.Label21.Visible = False
        Heart.Label21.Enabled = False
        Heart.Label22.Visible = False
        Heart.Label22.Enabled = False

        CenterPictureBox()

        Label5.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        Label6.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        Label7.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        Label8.Anchor = AnchorStyles.Top Or AnchorStyles.Left

        Button5.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        Button6.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        Panel3.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom
        Panel4.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom


        TextBox4.PasswordChar = "●"
        TextBox8.PasswordChar = "●"
        TextBox16.PasswordChar = "●"
    End Sub



    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Me.AcceptButton = Button5
        Panel3.Visible = True
        TextBox3.Focus()
        Button7.Hide()
        Heart.Button2.Visible = False
        Heart.Button2.Enabled = False
        Heart.Label4.Visible = False
        Panel4.Visible = False
        Heart.Button3.Visible = False
        Heart.Button3.Enabled = False
        Heart.Label2.Visible = False
        Panel6.Visible = False
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox15.Clear()
        TextBox16.Clear()
        Button8.Show()
        Button14.Show()
    End Sub










    Private Function HashPass(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(password)
            Dim hash As Byte() = sha256.ComputeHash(bytes)
            Return BitConverter.ToString(hash).Replace("-", "").ToLower()
        End Using
    End Function

    ' Function to retrieve the stored hash from the database
    Private Function GetStoredPasswordHash(username As String) As String
        Try
            Using connection As New MySqlConnection(db)
                connection.Open()
                Dim query As String = "SELECT password_hash FROM emp WHERE username = @username"
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@username", username)
                    Dim result As Object = command.ExecuteScalar()
                    If result IsNot Nothing Then
                        Return Convert.ToString(result)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return String.Empty
    End Function

    Private Function GetStoredPasswordHash1(username As String) As String
        Try
            Using connection As New MySqlConnection(db)
                connection.Open()
                Dim query As String = "SELECT password_hash FROM manager WHERE username = @username"
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@username", username)
                    Dim result As Object = command.ExecuteScalar()
                    If result IsNot Nothing Then
                        Return Convert.ToString(result)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return String.Empty
    End Function











    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Heart.TextBox4.Visible = False
        Heart.TextBox27.Visible = False
        Heart.TextBox27.Enabled = False
        Heart.Label32.Visible = False
        Heart.Label32.Enabled = False
        Heart.Button11.Visible = False
        Heart.TextBox5.Visible = False
        Heart.TextBox6.Visible = False

        Heart.TextBox4.Enabled = False
        Heart.TextBox5.Enabled = False
        Heart.TextBox6.Enabled = False
        Heart.PictureBox2.Visible = False

        Dim conn As New MySqlConnection(db)
        Dim username As String = TextBox3.Text
        Dim inputPassword As String = TextBox4.Text
        conn.Open()
        Dim com As New MySqlCommand("select password_hash from emp where username=@username", conn)
        com.Parameters.AddWithValue("@username", username)
        Dim reader As Object = com.ExecuteScalar()



        Dim storedHash As String = GetStoredPasswordHash(username)

        Dim hashedInputPassword As String = HashPassword(inputPassword)













        If reader IsNot Nothing Then
            If String.Compare(storedHash, hashedInputPassword) = 0 Then
                Heart.TextBox2.Text = UCase(TextBox3.Text)
                Me.Hide()
                Heart.Show()
                Heart.Button2.Visible = False
                Heart.Button2.Enabled = False
                Heart.Label4.Visible = False
                Heart.Button3.Visible = False
                Heart.Button3.Enabled = False
                Heart.Label2.Visible = False
                Panel3.Visible = False
                Heart.Button4.Visible = False
                Heart.Button4.Enabled = False
                Heart.Button11.Visible = False
                Heart.Button11.Enabled = False
                Heart.Label5.Visible = False
                Heart.Label6.Visible = False
                Heart.Label7.Visible = False
                Heart.Label5.Enabled = False
                Heart.Label6.Enabled = False
                Heart.Label7.Enabled = False
                Heart.Button7.Visible = True
                Heart.Button7.Enabled = True
                Heart.Chart1.Visible = False
                Heart.Chart1.Enabled = False
                'Heart.Button9.Visible = True
                'Heart.Button9.Enabled = True
                Heart.TextBox13.Enabled = True
                Heart.TextBox13.Visible = True
                'Heart.TextBox14.Enabled = True
                'Heart.TextBox14.Visible = True
                Heart.TextBox15.Enabled = True
                Heart.TextBox15.Visible = True
                Heart.TextBox16.Enabled = True
                Heart.TextBox16.Visible = True
                Heart.TextBox17.Enabled = True
                Heart.TextBox17.Visible = True
                Heart.TextBox18.Enabled = True
                Heart.TextBox18.Visible = True
                Heart.TextBox19.Enabled = True
                Heart.TextBox19.Visible = True
                Heart.TextBox20.Enabled = True
                Heart.TextBox20.Visible = True
                Heart.Label15.Visible = True
                Heart.Label15.Enabled = True
                Heart.Label16.Visible = True
                Heart.Label16.Enabled = True
                Heart.Label17.Visible = True
                Heart.Label17.Enabled = True
                Heart.Label18.Visible = True
                Heart.Label18.Enabled = True
                Heart.Label19.Visible = True
                Heart.Label19.Enabled = True
                Heart.Label20.Visible = True
                Heart.Label20.Enabled = True
                Heart.Label21.Visible = True
                Heart.Label21.Enabled = True
                Heart.Label22.Visible = True
                Heart.Label22.Enabled = True
                Heart.Panel3.Visible = False
                TextBox3.Clear()
                TextBox4.Clear()

            Else
                MessageBox.Show("Invalid Username or Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                TextBox3.Clear()
                TextBox4.Clear()
                TextBox3.Focus()
            End If
        Else
            MessageBox.Show("Invalid Username or Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox3.Clear()
            TextBox4.Clear()
            TextBox3.Focus()
        End If

        conn.Close()

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Me.AcceptButton = Button6
        Panel4.Visible = True
        TextBox7.Focus()
        Button8.Hide()
        Heart.Button2.Visible = True
        Heart.Button2.Enabled = True
        Heart.Label4.Visible = True
        Heart.Button3.Visible = True
        Heart.Button3.Enabled = True
        Heart.Label2.Visible = True

        Panel3.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox15.Clear()
        TextBox16.Clear()
        Button7.Show()
        Button14.Show()

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim conn As New MySqlConnection(db)
        If TextBox7.Text = "admin" And TextBox8.Text = "adminpass" Then
            Heart.TextBox2.Text = UCase(TextBox7.Text)
            Me.Hide()
            Heart.Chart1.Visible = True
            Heart.Chart1.Enabled = True
            Heart.PictureBox2.Visible = True
            Heart.Button2.Visible = True
            Heart.Button2.Enabled = True
            Heart.Button3.Visible = True
            Heart.PictureBox2.Visible = True
            Heart.Button3.Enabled = True
            Heart.Label2.Visible = True
            Heart.Label4.Visible = True
            Heart.Show()
            Panel4.Visible = False

            TextBox7.Clear()
            TextBox8.Clear()

            Heart.TextBox4.Visible = True
            Heart.TextBox27.Visible = True
            Heart.TextBox27.Enabled = True
            Heart.Label32.Visible = True
            Heart.Label32.Enabled = True
            Heart.TextBox5.Visible = True
            Heart.TextBox6.Visible = True
            Heart.TextBox4.Enabled = True
            Heart.TextBox5.Enabled = True
            Heart.TextBox6.Enabled = True
            Heart.Label5.Enabled = True
            Heart.Label6.Enabled = True
            Heart.Label7.Enabled = True
            Heart.Label5.Visible = True
            Heart.Label6.Visible = True
            Heart.Label7.Visible = True
            Heart.Button7.Visible = False
            Heart.Button7.Enabled = False
            Heart.Button4.Visible = False
            Heart.Button4.Enabled = False
            Heart.Button11.Visible = True
            Heart.Button11.Enabled = True
            'Heart.Button9.Visible = False
            'Heart.Button9.Enabled = False
            Heart.TextBox13.Enabled = False
            Heart.TextBox13.Visible = False
            'Heart.TextBox14.Enabled = False
            'Heart.TextBox14.Visible = False
            Heart.TextBox15.Enabled = False
            Heart.TextBox15.Visible = False
            Heart.TextBox16.Enabled = False
            Heart.TextBox16.Visible = False
            Heart.TextBox17.Enabled = False
            Heart.TextBox17.Visible = False
            Heart.TextBox18.Enabled = False
            Heart.TextBox18.Visible = False
            Heart.TextBox19.Enabled = False
            Heart.TextBox19.Visible = False
            Heart.TextBox20.Enabled = False
            Heart.TextBox20.Visible = False
            Heart.Label15.Visible = False
            Heart.Label15.Enabled = False
            Heart.Label16.Visible = False
            Heart.Label16.Enabled = False
            Heart.Label17.Visible = False
            Heart.Label17.Enabled = False
            Heart.Label18.Visible = False
            Heart.Label18.Enabled = False
            Heart.Label19.Visible = False
            Heart.Label19.Enabled = False
            Heart.Label20.Visible = False
            Heart.Label20.Enabled = False
            Heart.Label21.Visible = False
            Heart.Label21.Enabled = False
            Heart.Label22.Visible = False
            Heart.Label22.Enabled = False
            Heart.Panel3.Visible = False
        Else
            MessageBox.Show("Invalid Username or Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox7.Clear()
            TextBox8.Clear()
            TextBox7.Focus()
        End If
    End Sub



    Private Sub main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Application.Exit()

    End Sub

    Private Sub CenterPictureBox()

        PictureBox2.Left = (Me.ClientSize.Width - PictureBox2.Width) / 2
        PictureBox2.Top = (Me.ClientSize.Height - PictureBox2.Height) / 2
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        currentIndex += 1
        If currentIndex >= totalImages Then
            currentIndex = 0
        End If
        Me.BackgroundImage = images(currentIndex)
        If currentIndex = totalImages - 1 Then

            Timer1.Enabled = False
            PictureBox1.Visible = False
            PictureBox2.Visible = False

            Button7.Visible = True
            Button8.Visible = True
            Button14.Visible = True
        End If


    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Panel3.Visible = False
        TextBox3.Clear()
        TextBox4.Clear()
        'Button7.Show()
        Button7.Visible = True
        Button8.Visible = True
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Panel4.Visible = False
        Button8.Show()
        TextBox7.Clear()
        TextBox8.Clear()
    End Sub


    Private Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(password)
            Dim hash As Byte() = sha256.ComputeHash(bytes)
            Return BitConverter.ToString(hash).Replace("-", "").ToLower()
        End Using
    End Function




    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        If (TextBox9.Text = "" Or TextBox10.Text = "" Or TextBox11.Text = "" Or TextBox12.Text = "" Or TextBox13.Text = "" Or TextBox14.Text = "") Then
            MessageBox.Show("Please fill all the details")
            Return
        End If


        Dim conn As New MySqlConnection(db)
        Dim username As String = TextBox9.Text
        Dim plainpassword As String = TextBox10.Text
        Dim phone As String = TextBox11.Text
        Dim email As String = TextBox12.Text
        Dim address As String = TextBox13.Text
        Dim city As String = TextBox14.Text
        Dim dob As DateTime = DateTimePicker1.Text
        Dim yoj As DateTime = DateTimePicker2.Text



        Dim hashedPassword As String = HashPassword(plainpassword)


        conn.Open()
        Dim com As New MySqlCommand("SELECT COUNT(*) FROM emp WHERE username = @username", conn)
        com.Parameters.AddWithValue("@username", username)
        Dim user As Integer = Convert.ToInt32(com.ExecuteScalar)
        If user > 0 Then
            MessageBox.Show("USER ALREADY EXISTS !", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox9.Clear()
            TextBox10.Clear()

        Else
            If (TextBox9.Text = "" Or TextBox10.Text = "" Or TextBox11.Text = "" Or TextBox12.Text = "" Or TextBox13.Text = "" Or TextBox14.Text = "") Then
                MessageBox.Show("Fill All the details asked ", "INCOMPLETE DETAILS", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else

                Dim insert As New MySqlCommand("INSERT into emp(username,password_hash,phone,email,address,city,dob,yoj) values (@username,@password_hash,@phone,@email,@address,@city,@dob,@yoj)", conn)
                insert.Parameters.AddWithValue("@username", username)
                insert.Parameters.AddWithValue("@password_hash", hashedPassword)
                insert.Parameters.AddWithValue("@phone", phone)
                insert.Parameters.AddWithValue("@email", email)
                insert.Parameters.AddWithValue("@address", address)
                insert.Parameters.AddWithValue("@city", city)
                insert.Parameters.AddWithValue("@dob", dob)
                insert.Parameters.AddWithValue("@yoj", yoj)
                insert.ExecuteNonQuery()
                MessageBox.Show("User Registered!", "NEW USER", MessageBoxButtons.OK, MessageBoxIcon.Information)
                TextBox9.Clear()
                TextBox10.Clear()
                TextBox11.Clear()
                TextBox12.Clear()
                TextBox13.Clear()
                TextBox14.Clear()
            End If
            Panel5.Visible = False
            Button8.Visible = True
            Button14.Visible = True
        End If


        Heart.emp_count()
        conn.Close()
        'Button7.Visible = True
        'Button8.Visible = True
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Panel5.Visible = True
        Button8.Visible = False
        Button14.Visible = False

    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Panel5.Visible = False
        Button8.Visible = True
        Button14.Visible = True
        TextBox9.Clear()
        TextBox10.Clear()
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Me.AcceptButton = Button17
        Panel6.Visible = True
        TextBox15.Focus()
        Button14.Visible = False
        Button7.Visible = True
        Button8.Visible = True
        Panel4.Visible = False
        Panel3.Visible = False
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox7.Clear()
        TextBox8.Clear()
    End Sub

    Private Sub textbox15_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox15.KeyDown
        If e.KeyCode = Keys.Escape Then
            TextBox16.Focus()
        End If

    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Button7.Visible = True
        Button8.Visible = True
        Button14.Visible = True
        Panel6.Visible = False
        TextBox15.Clear()
        TextBox16.Clear()
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Panel7.Visible = True
        Button7.Visible = False
        Button8.Visible = False

    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        If (TextBox17.Text = "" Or TextBox18.Text = "" Or TextBox19.Text = "" Or TextBox20.Text = "" Or TextBox21.Text = "" Or TextBox22.Text = "") Then
            MessageBox.Show("Please fill all the details")
            Return
        End If


        Dim conn As New MySqlConnection(db)
        Dim username As String = TextBox17.Text
        Dim plainpassword As String = TextBox18.Text
        Dim phone As String = TextBox19.Text
        Dim email As String = TextBox20.Text
        Dim address As String = TextBox21.Text
        Dim city As String = TextBox22.Text
        Dim dob As DateTime = DateTimePicker3.Text
        Dim yoj As DateTime = DateTimePicker4.Text



        Dim hashedPassword As String = HashPassword(plainpassword)


        conn.Open()
        Dim com As New MySqlCommand("SELECT COUNT(*) FROM manager WHERE username = @username", conn)
        com.Parameters.AddWithValue("@username", username)
        Dim user As Integer = Convert.ToInt32(com.ExecuteScalar)
        If user > 0 Then
            MessageBox.Show("USER ALREADY EXISTS !", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox9.Clear()
            TextBox10.Clear()

        Else
            If (TextBox17.Text = "" Or TextBox18.Text = "" Or TextBox19.Text = "" Or TextBox20.Text = "" Or TextBox21.Text = "" Or TextBox22.Text = "") Then
                MessageBox.Show("Fill All the details asked ", "INCOMPLETE DETAILS", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else

                Dim insert As New MySqlCommand("INSERT into manager(username,password_hash,phone,email,address,city,dob,yoj) values (@username,@password_hash,@phone,@email,@address,@city,@dob,@yoj)", conn)
                insert.Parameters.AddWithValue("@username", username)
                insert.Parameters.AddWithValue("@password_hash", hashedPassword)
                insert.Parameters.AddWithValue("@phone", phone)
                insert.Parameters.AddWithValue("@email", email)
                insert.Parameters.AddWithValue("@address", address)
                insert.Parameters.AddWithValue("@city", city)
                insert.Parameters.AddWithValue("@dob", dob)
                insert.Parameters.AddWithValue("@yoj", yoj)
                insert.ExecuteNonQuery()
                MessageBox.Show("User Registered!", "NEW USER", MessageBoxButtons.OK, MessageBoxIcon.Information)
                TextBox17.Clear()
                TextBox18.Clear()
                TextBox19.Clear()
                TextBox20.Clear()
                TextBox21.Clear()
                TextBox22.Clear()
            End If
            Panel7.Visible = False
            Button7.Visible = True
            Button8.Visible = True
        End If


        Heart.emp_count()
        Heart.manager_count()
        conn.Close()
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        Panel7.Visible = False
        Button7.Visible = True
        Button8.Visible = True
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click

        Dim conn As New MySqlConnection(db)
        Dim username As String = TextBox15.Text
        Dim inputPassword As String = TextBox16.Text
        conn.Open()
        Dim com As New MySqlCommand("select password_hash from manager where username=@username", conn)
        com.Parameters.AddWithValue("@username", username)
        Dim reader As Object = com.ExecuteScalar()



        Dim storedHash As String = GetStoredPasswordHash1(username)

        Dim hashedInputPassword As String = HashPassword(inputPassword)



        If reader IsNot Nothing Then
            If String.Compare(storedHash, hashedInputPassword) = 0 Then
                Heart.TextBox2.Text = UCase(TextBox15.Text)
                Heart.Label5.Visible = True
                Heart.Label6.Visible = True
                Heart.Label7.Visible = True
                Heart.Label5.Enabled = True
                Heart.Label6.Enabled = True
                Heart.Label7.Enabled = True
                Heart.Button2.Visible = False
                Heart.Button2.Enabled = False
                Heart.Label4.Visible = False
                Heart.Button3.Visible = True
                Heart.Button3.Enabled = True
                Heart.Button7.Visible = False
                Heart.Button7.Enabled = False
                Heart.Button4.Visible = True
                Heart.Button4.Enabled = True
                Heart.Button11.Visible = False
                Heart.Button11.Enabled = False
                Heart.Label2.Visible = True
                Heart.PictureBox2.Visible = False
                Panel6.Visible = False
                Me.Hide()
                Heart.Show()
                Heart.TextBox4.Visible = True
                Heart.TextBox27.Visible = False
                Heart.TextBox27.Enabled = False
                Heart.Label32.Enabled = False
                Heart.Label32.Visible = False
                Heart.TextBox5.Visible = True
                Heart.TextBox6.Visible = True
                Heart.TextBox4.Enabled = True
                Heart.TextBox5.Enabled = True
                Heart.TextBox6.Visible = False
                Heart.Label7.Visible = False
                Heart.Panel3.Visible = False
                TextBox15.Clear()
                TextBox16.Clear()

            Else
                MessageBox.Show("Invalid Username or Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                TextBox15.Clear()
                TextBox16.Clear()
                TextBox15.Focus()
            End If
        Else
            MessageBox.Show("Invalid Username or Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox15.Clear()
            TextBox16.Clear()
            TextBox15.Focus()
        End If

        conn.Close()



    End Sub


End Class