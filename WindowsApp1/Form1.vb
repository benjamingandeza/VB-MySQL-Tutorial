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

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        txtID.Text = DataGridView1.Item("ID", DataGridView1.CurrentRow.Index).Value
        txtName.Text = DataGridView1.Item("FNAME", DataGridView1.CurrentRow.Index).Value
        txtAge.Text = DataGridView1.Item("AGE", DataGridView1.CurrentRow.Index).Value

        Dim gen As String
        gen = DataGridView1.Item("GENDER", DataGridView1.CurrentRow.Index).Value

        If gen = "Male" Then
            radMale.Checked = True
        ElseIf gen = "Female" Then
            radFemale.Checked = True
        End If

        dtBday.Value = DataGridView1.Item("BDAY", DataGridView1.CurrentRow.Index).Value
        cmbProgram.Text = DataGridView1.Item("COURSE", DataGridView1.CurrentRow.Index).Value

    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        openCon()
        Try
            cmd.Connection = con
            cmd.CommandText = "UPDATE tbl_sample SET FNAME = '" & txtName.Text & "', AGE = '" & txtAge.Text & "', GENDER = '" &
                gender & "', BDAY = '" & dtBday.Value.Date & "', COURSE = '" & cmbProgram.SelectedItem & "' WHERE ID = '" & txtID.Text & "'"
            cmd.ExecuteNonQuery()
            con.Close()
            MsgBox("Successfully Updated Record!")
            Call loadTable()
            txtName.Clear()
            txtAge.Clear()
            radFemale.Checked = False
            radMale.Checked = False
            cmbProgram.Text = ""
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
End Class
