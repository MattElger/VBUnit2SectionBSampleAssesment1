Imports System.IO
Public Class Customers
    Private Structure ClientData  'Creating a database.
        Public FirstName As String
        Public Address1 As String
        Public ClientID As String
        Public Address2 As String
        Public PhoneNumber As String
        Public DateOfBirth As String
        Public County As String
        Public LastName As String
    End Structure

    Private Sub Customers_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If Dir$("customerdetails.txt") = "" Then    'checks if the text document is empty 
            Dim sw As New StreamWriter(Application.StartupPath & "\customerdetails.txt", True) 'gets the location of the text document and makes sure its present and if it isnt it creates a new file.
            sw.WriteLine("                                                                                                                                                                                                                                                                                                                                    ")
            sw.Close()
            MsgBox("A new database has been created", vbExclamation, "Warning!") 'tells the user if a new database is made.
        End If
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        If txtFName.Text.Length > 30 Then 'if the data input into the name textbox is more than 30 chars long then it will output a error message and end the sub so it doesnt save the null data.
            MsgBox("Too many characters in First Name.")
            Exit Sub
        End If
        If txtLName.Text.Length > 30 Then
            MsgBox("Too many characters in Last Name.")
        End If
        If txtAddress2.Text.Length > 30 Then 'if the data input into the address line 2 textbox is more than 30 chars long then it will output a error message and end the sub so it doesnt save the null data.
            MsgBox("Too many characters in Address Line 2.")
            Exit Sub
        End If
        If txtAddress1.Text.Length > 30 Then 'if the data input into the address line 1 textbox is more than 30 chars long then it will output a error message and end the sub so it doesnt save the null data.
            MsgBox("Too many characters in Address Line 1.")
            Exit Sub
        End If
        If txtCounty.Text.Length > 30 Then 'if the data input into the county textbox is more than 30 chars long then it will output a error message and end the sub so it doesnt save the null data.
            MsgBox("Too many characters in County.")
            Exit Sub
        End If
        If CStr(Val(txtPhoneNumber.Text)).Length <> 11 Then 'if the data input into the phone number textbox is more than 30 chars long then it will output a error message and end the sub so it doesnt save the null data.
            MsgBox("Too many or little characters in Phone Number.")
            Exit Sub
        End If
        If txtClientID.Text.Length > 5 Then 'if the data input into the client id textbox is more than 5 chars long then it will output a error message and end the sub so it doesnt save the null data.
            MsgBox("Too many characters in Client ID.")
            Exit Sub
        End If
        Dim ClientDataCheck() As String = File.ReadAllLines(Dir$("customerdetails.txt"))
        For I = 0 To UBound(ClientDataCheck)
            If txtClientID.Text = Trim(Mid(ClientDataCheck(I), 182, 5)) Then 'checks if a record already has the client id entered.
                MsgBox("There is already a record with that Client ID.")
                Exit Sub
            End If

        Next I



        Dim MissingData As Integer 'if you haven't entered any data into the textboxes it will be added to missing data variable to later do a presence check.
        If txtFName.Text = "" Then MissingData = MissingData + 1
        If txtAddress1.Text = "" Then MissingData = MissingData + 1
        If txtClientID.Text = "" Then MissingData = MissingData + 1
        If txtCounty.Text = "" Then MissingData = MissingData + 1
        If txtPhoneNumber.Text = "" Then MissingData = MissingData + 1
        If MissingData >= 1 Then 'presence check on the data input by the user
            MsgBox("You are missing " & MissingData & " items of data.")
            Exit Sub
        End If

        'checks if the data entered by the user is already a record in the database.
        For I = 0 To UBound(ClientDataCheck)
            Dim CountGot As Integer
            If Trim(Mid(ClientDataCheck(I), 1, 30)) = txtFName.Text Then CountGot = CountGot + 1 'Checks eack item of data entered by the user.
            If Trim(Mid(ClientDataCheck(I), 31, 30)) = txtLName.Text Then CountGot = CountGot + 1
            If Trim(Mid(ClientDataCheck(I), 61, 30)) = txtAddress1.Text Then CountGot = CountGot + 1
            If Trim(Mid(ClientDataCheck(I), 91, 30)) = txtAddress2.Text Then CountGot = CountGot + 1
            If Trim(Mid(ClientDataCheck(I), 121, 30)) = txtCounty.Text Then CountGot = CountGot + 1
            If Trim(Mid(ClientDataCheck(I), 151, 20)) = dtpDOB.Text Then CountGot = CountGot + 1
            If Trim(Mid(ClientDataCheck(I), 171, 11)) = Val(txtPhoneNumber.Text) Then CountGot = CountGot + 1
            If Trim(Mid(ClientDataCheck(I), 182, 5)) = txtClientID.Text Then CountGot = CountGot + 1
            If CountGot = 8 Then 'If all 8 items of data are the same as an already stored record the user is notified and the record is not saved twice.
                MsgBox("Record is already stored on out system.")
                Exit Sub
            End If
        Next I


        Dim ClientDataEntered As New ClientData
        Dim sw As New StreamWriter(Application.StartupPath & "\customerdetails.txt", True) 'gets the directory for the text document the program is using.
        ClientDataEntered.FirstName = LSet(txtFName.Text, 30) 'outputs the data input by the user into the textbox that has it's own allocated memory slot. 
        ClientDataEntered.LastName = LSet(txtLName.Text, 30)
        ClientDataEntered.Address1 = LSet(txtAddress1.Text, 30)
        ClientDataEntered.Address2 = LSet(txtAddress2.Text, 30)
        ClientDataEntered.County = LSet(txtCounty.Text, 30)
        ClientDataEntered.DateOfBirth = LSet(dtpDOB.Text, 20)
        ClientDataEntered.PhoneNumber = LSet(Val(txtPhoneNumber.Text), 11)
        ClientDataEntered.ClientID = LSet(txtClientID.Text, 5)

        sw.WriteLine(ClientDataEntered.FirstName & ClientDataEntered.LastName & ClientDataEntered.Address1 & ClientDataEntered.Address2 & ClientDataEntered.County & ClientDataEntered.DateOfBirth & ClientDataEntered.PhoneNumber & ClientDataEntered.ClientID) 'outputs the data gathered in a textbox.
        sw.Close()
    End Sub

    Private Sub btnRetrive_Click(sender As System.Object, e As System.EventArgs) Handles btnRetrive.Click
        btnRetrive.Visible = False
        btnBack.Visible = True
        cmbClientID.Visible = True
        btnSave.Visible = False
        txtAddress1.Enabled = False
        txtAddress2.Enabled = False
        txtFName.Enabled = False
        txtLName.Enabled = False
        txtPhoneNumber.Enabled = False
        dtpDOB.Enabled = False
        txtCounty.Enabled = False
        txtClientID.Enabled = False
        Dim ClientData() As String = File.ReadAllLines(Dir$("customerdetails.txt")) 'gets the data inside the text box linked to the program.
        For I = 0 To UBound(ClientData) 'retrives all the records client ids stored inside the text document and outputs them in a combo box for the user to select a record from.
            cmbClientID.Items.Add(Trim(Mid(ClientData(I), 182, 5)))
        Next I

    End Sub

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        'resets for a new record to be entered.
        btnRetrive.Visible = True
        btnBack.Visible = False
        cmbClientID.Visible = False
        btnSave.Visible = True
        txtAddress1.Text = ""
        txtClientID.Text = ""
        txtFName.Text = ""
        txtLName.Text = ""
        txtAddress2.Text = ""
        txtCounty.Text = ""
        dtpDOB.Value = Date.Now
        txtPhoneNumber.Text = ""
        txtAddress1.Enabled = True
        txtAddress2.Enabled = True
        txtFName.Enabled = True
        txtLName.Enabled = True
        txtPhoneNumber.Enabled = True
        dtpDOB.Enabled = True
        txtCounty.Enabled = True
        txtClientID.Enabled = True
        cmbClientID.Items.Clear()
    End Sub

    Private Sub cmbClientID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbClientID.SelectedIndexChanged
        Dim ClientData() As String = File.ReadAllLines(Dir$("customerdetails.txt")) 'gets the data inside the text box linked to the program.

        For I = 0 To UBound(ClientData) 'retrives all the records stored inside the text document and outputs them in a message box for the user.
            If Trim(Mid(ClientData(I), 182, 5)) = cmbClientID.SelectedItem Then
                txtFName.Text = Trim(Mid(ClientData(I), 1, 30))
                txtLName.Text = Trim(Mid(ClientData(I), 31, 30))
                txtAddress1.Text = Trim(Mid(ClientData(I), 61, 30))
                txtAddress2.Text = Trim(Mid(ClientData(I), 91, 30))
                txtCounty.Text = Trim(Mid(ClientData(I), 121, 30))
                If Trim(Mid(ClientData(I), 151, 20)) = "" Then
                Else
                    dtpDOB.Value = Trim(Mid(ClientData(I), 151, 20)) & " 11:30 PM"
                End If
                txtPhoneNumber.Text = Trim(Mid(ClientData(I), 171, 11))
                txtClientID.Text = Trim(Mid(ClientData(I), 182, 5))
            End If
        Next I
    End Sub
End Class