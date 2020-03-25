Public Class Form1
    Dim gender As String
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        openCon()
        MsgBox("Connected!")
        con.Close()

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
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
End Class
