﻿Public Class Form1

    Dim companiesAndEmployeesTables As CompaniesAndEmployeesTables = New CompaniesAndEmployeesTables
    Dim employeesTable As EmployeesTable = New EmployeesTable
    Dim connectionString As String = "Server=DESKTOP-MBULVCJ\JEFFONE;Integrated Security=SSPI;Database=ScubaDealers;"

    Enum SearchByType
        CompanyName
        LastName
    End Enum

    Dim searchByChosen As SearchByType = SearchByType.CompanyName

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DisableSearchItems()
        SearchTextBox.Text = ""
        ResultsLabel.Text = ""
    End Sub

    Private Sub BrowseListButton_Click(sender As Object, e As EventArgs) Handles BrowseListButton.Click, SearchByCompanyNameButton.Click,
                                                                                 SearchByLastNameButton.Click, AddNewCompanyButton.Click,
                                                                                 TotalNumberOfContactsButton.Click
        Dim buttonSelected As Button = sender

        resetFontsOnButtons()
        buttonSelected.ForeColor = Color.Yellow

        SearchTextBox.Text = ""

        Select Case (buttonSelected.Name)
            Case BrowseListButton.Name
                ResultsLabel.Text = "Last Name List"
                DisableSearchItems()
                BrowseDataGridView.Visible = True
                companiesAndEmployeesTables.FetchBrowseDataSet(connectionString)
                BrowseDataGridView.DataSource = companiesAndEmployeesTables.dataSet.Tables(0)

            Case SearchByCompanyNameButton.Name
                SearchLabel.Text = "Enter Name of Company"
                EnableSeachItems()
                searchByChosen = SearchByType.CompanyName

            Case SearchByLastNameButton.Name
                SearchLabel.Text = "Enter Name of Name"
                EnableSeachItems()
                searchByChosen = SearchByType.LastName

            Case AddNewCompanyButton.Name
                ResultsLabel.Text = "Add a new phone number"
                DisableSearchItems()

            Case TotalNumberOfContactsButton.Name
                ResultsLabel.Text = "Number of contacts"
                SearchLabel.Visible = True
                employeesTable.CreateCount(connectionString)
                SearchLabel.Text = "Number of Contacts: " & employeesTable.contactCount

        End Select
    End Sub


    Private Sub EnableSeachItems()
        SearchLabel.Visible = True
        SearchButton.Visible = True
        SearchTextBox.Visible = True
    End Sub

    Private Sub DisableSearchItems()
        SearchLabel.Visible = False
        SearchButton.Visible = False
        SearchTextBox.Visible = False
        BrowseDataGridView.Visible = False
    End Sub

    Private Sub resetFontsOnButtons()
        BrowseListButton.ForeColor = Color.White
        SearchByCompanyNameButton.ForeColor = Color.White
        SearchByLastNameButton.ForeColor = Color.White
        AddNewCompanyButton.ForeColor = Color.White
        TotalNumberOfContactsButton.ForeColor = Color.White
    End Sub

    Private Sub BrowseDataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles BrowseDataGridView.CellClick

        DetailInformatinLabel.Text = BrowseDataGridView.SelectedCells(0).Value.ToString() & " " & BrowseDataGridView.CurrentCell.RowIndex.ToString

    End Sub

    Private Sub SearchButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click

        If SearchTextBox.Text.Trim() <> "" Then
            DisableSearchItems()
            BrowseDataGridView.Visible = True
            Select Case (searchByChosen)
                Case SearchByType.CompanyName
                    companiesAndEmployeesTables.FetchCompanyNameDataSet(connectionString, SearchTextBox.Text)
                    BrowseDataGridView.DataSource = companiesAndEmployeesTables.dataSet.Tables(0)
                Case SearchByType.LastName
                    companiesAndEmployeesTables.FetchLastNameDataSet(connectionString, SearchTextBox.Text)
                    BrowseDataGridView.DataSource = companiesAndEmployeesTables.dataSet.Tables(0)
            End Select
        Else
            MsgBox("You must enter text to search by.")

        End If


    End Sub
End Class
