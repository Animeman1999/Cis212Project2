﻿Public Class Form1

    'Dim dataBaseFetcher As DataBaseFetcher = New DataBaseFetcher()
    Dim connectionString As String = "Server=DESKTOP-MBULVCJ\JEFFONE;Integrated Security=SSPI;Database=ScubaDealers;"

    Enum SearchByType
        CompanyName
        LastName
        Phonenumber
    End Enum

    Dim searchByChosen As SearchByType = SearchByType.CompanyName

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub BrowseListButton_Click(sender As Object, e As EventArgs) Handles BrowseListButton.Click, SearchByCompanyNameButton.Click,
                                                                                 SearchByLastNameButton.Click, SearchByPhoneNumberButton.Click,
                                                                                 AddNewCompanyButton.Click, TotalNumberOfContactsButton.Click
        Dim buttonSelected As Button = sender

        resetFontsOnButtons()
        buttonSelected.ForeColor = Color.Yellow

        SearchTextBox.Text = ""

        Select Case (buttonSelected.Name)
            Case BrowseListButton.Name
                'ResultsLabel.Text = "Company Name List"
                'DisableSearchItems()
                'BrowseDataGridView.Visible = True

                'PopulateDataGrid("SELECT CompanyName, CompanyID FROM Companies ORDER BY CompanyName") '8888888888888888888888888888888888888

            'Case BrowseByLastNameButton.Name
            '    ResultsLabel.Text = "Last Name List"
            '    DisableSearchItems()
            '    BrowseDataGridView.Visible = True

            '    PopulateDataGridViaDataSet("SELECT LastName, FirstName, CompanyName FROM sd_header ORDER BY LastName", "sd_header")

            Case SearchByCompanyNameButton.Name
                SearchLabel.Text = "Enter Name of Company"
                EnableSeachItems()
                searchByChosen = SearchByType.CompanyName

            Case SearchByLastNameButton.Name
                SearchLabel.Text = "Enter Name of Name"
                EnableSeachItems()
                searchByChosen = SearchByType.LastName

            Case SearchByPhoneNumberButton.Name
                SearchLabel.Text = "Enter Phone Number"
                EnableSeachItems()
                searchByChosen = SearchByType.Phonenumber

            Case AddNewCompanyButton.Name
                ResultsLabel.Text = "Add a new phone number"
                DisableSearchItems()

            Case TotalNumberOfContactsButton.Name
                ResultsLabel.Text = "Number of contacts"
                'PopulateScarlInteger("SELECT COUNT CompanyName FROM sd_header")

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
        SearchByPhoneNumberButton.ForeColor = Color.White
        AddNewCompanyButton.ForeColor = Color.White
        TotalNumberOfContactsButton.ForeColor = Color.White
    End Sub
End Class