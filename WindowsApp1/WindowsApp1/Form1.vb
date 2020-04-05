Public Class Form1
    Dim gender As String
    Dim genderBind As String
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        openCon()
        MsgBox("Connected!")
        con.Close()
        txtGenderBind.Visible = False
        loadTable()
    End Sub

    Private Sub radMale_CheckedChanged(sender As Object, e As EventArgs) Handles radMale.CheckedChanged
        gender = "Male"
    End Sub

    Private Sub radFemale_CheckedChanged(sender As Object, e As EventArgs) Handles radFemale.CheckedChanged
        gender = "Female"
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        openCon()
        Try
            cmd.Connection = con
            cmd.CommandText = "INSERT INTO tbl_sample (`FNAME`, `AGE`,`GENDER`, `BDAY`, `COURSE`) VALUES ('" &
                txtName.Text & "', " & txtAge.Text & ", '" & gender & "', '" & dtBday.Value.Date & "', '" &
                cmbProgram.SelectedItem & "')"
            cmd.ExecuteNonQuery()
            MsgBox("Successfully Added Record!")
            con.Close()
            txtName.Clear()
            txtAge.Clear()
            radMale.Checked = False
            radFemale.Checked = False
            cmbProgram.Text = ""
            loadTable()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub txtID_TextChanged(sender As Object, e As EventArgs) Handles txtID.TextChanged
        openCon()
        Try
            cmd.Connection = con
            cmd.CommandText = "SELECT * FROM tbl_sample WHERE ID='" & txtID.Text & "'"
            adapter.SelectCommand = cmd
            data.Clear()
            adapter.Fill(data, "List")

            txtName.DataBindings.Add("Text", data, "List.FNAME")
            txtName.DataBindings.Clear()

            txtAge.DataBindings.Add("Text", data, "List.AGE")
            txtAge.DataBindings.Clear()

            txtGenderBind.DataBindings.Add("Text", data, "List.GENDER")
            txtGenderBind.DataBindings.Clear()
            genderBind = txtGenderBind.Text

            If genderBind = "Male" Then
                radMale.Checked = True
            ElseIf genderBind = "Female" Then
                radFemale.Checked = True
            End If

            dtBday.DataBindings.Add("Value", data, "List.BDAY")
            dtBday.DataBindings.Clear()

            cmbProgram.DataBindings.Add("Text", data, "List.COURSE")
            cmbProgram.DataBindings.Clear()

            If txtID.Text = "" Then
                txtName.Clear()
                txtAge.Clear()
                txtGenderBind.Clear()
                radFemale.Checked = False
                radMale.Checked = False
                cmbProgram.Text = ""
            End If

            con.Close()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub loadTable()
        openCon()
        Try
            cmd.Connection = con
            cmd.CommandText = "SELECT * FROM tbl_sample"
            adapter.SelectCommand = cmd
            table.Clear()
            adapter.Fill(table)
            DataGridView1.DataSource = table
            con.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

End Class
