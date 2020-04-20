Imports MySql.Data.MySqlClient
Module Module1
    Public con As New MySqlConnection
    Public cmd As New MySqlCommand
    Public adapter As New MySqlDataAdapter
    Public data As New DataSet
    Public table As New DataTable

    Sub openCon()
        con.ConnectionString = "server=localhost;username=root;password=;database=db_tutorial"
        con.Open()
    End Sub
End Module
