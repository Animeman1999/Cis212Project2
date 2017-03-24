﻿Public Class AllContactRelatedTables

    Private COLUMN_NUMBER As Integer = 14
    Private databaseFetcher As DataBaseFetcher = New DataBaseFetcher
    Private _contactData(COLUMN_NUMBER) As String
    Private _employeeID As String
    Private _phoneID As String
    Private _addressID As String
    Private _companyID As Integer
    Private _companyName As String
    Private _lastName As String
    Private _firstName As String
    Private _employeeTypesDescription As String
    Private _phoneType As String
    Private _address1 As String
    Private _phoneNumber As String
    Private _address2 As String
    Private _city As String
    Private _state As String
    Private _postalCode As String
    Public ReadOnly Property ErrorMessage As String


    Public Property companyName As String
        Get
            Return _companyName.Replace("''", "'")
        End Get

        Set(value As String)
            _companyName = value.Trim().Replace("'", "''")
        End Set
    End Property

    Public Property lastName As String
        Get
            Return _lastName.Replace("''", "'")
        End Get

        Set(value As String)
            _lastName = value.Trim().Replace("'", "''")
        End Set
    End Property
    Public Property firstName As String
        Get
            Return _firstName.Replace("''", "'")
        End Get
        Set(value As String)
            _firstName = value.Trim().Replace("'", "''")
        End Set
    End Property
    Public Property employeeTypesDescription As String
        Get
            Return _employeeTypesDescription.Replace("''", "'")
        End Get
        Set(value As String)
            _employeeTypesDescription = value.Trim().Replace("'", "''")
        End Set
    End Property
    Public Property phoneNumber As String
        Get
            Return _phoneNumber.Replace("''", "'")
        End Get
        Set(value As String)
            _phoneNumber = value.Trim().Replace("'", "''")
        End Set
    End Property
    Public Property phoneType As String
        Get
            Return _phoneType.Replace("''", "'")
        End Get
        Set(value As String)
            _phoneType = value.Trim().Replace("'", "''")
        End Set
    End Property
    Public Property address1 As String
        Get
            Return _address1.Replace("''", "'")
        End Get
        Set(value As String)
            _address1 = value.Trim().Replace("'", "''")
        End Set
    End Property
    Public Property address2 As String
        Get
            Return _address2.Replace("''", "'")
        End Get
        Set(value As String)
            _address2 = value.Trim().Replace("'", "''")
        End Set
    End Property
    Public Property city As String
        Get
            Return _city.Replace("''", "'")
        End Get
        Set(value As String)
            _city = value.Trim().Replace("'", "''")
        End Set
    End Property
    Public Property state As String
        Get
            Return _state.Replace("''", "'")
        End Get
        Set(value As String)
            _state = value.Trim().Replace("'", "''")
        End Set
    End Property
    Public Property postalCode As String
        Get
            Return _postalCode.Replace("''", "'")
        End Get
        Set(value As String)
            _postalCode = value.Trim().Replace("'", "''")
        End Set
    End Property


    Public Sub FetchSingleContactInclusiveData(ByVal connectionString As String, ByVal companyId As Integer)
        _ErrorMessage = ""
        _contactData = databaseFetcher.getOleDataReader($"SELECT CompanyName, LastName, FirstName, EmployeeTypes.Description, dbo.Phones.Phone, dbo.PhoneTypes.Description,
                                                        dbo.Addresses.Address1, Address2, City, State, PostalCode, EmployeeID, PhoneID, AddressID
                                                        FROM dbo.Employees JOIN dbo.Companies ON Companies.CompanyID = Employees.CompanyID
                                                        JOIN	dbo.EmployeeTypes ON	EmployeeTypes.EmployeeTypeID = Employees.EmployeeTypeID 
                                                        JOIN dbo.Phones ON	Phones.CompanyID = Companies.CompanyID
                                                        JOIN dbo.PhoneTypes ON PhoneTypes.PhoneTypeID = Phones.PhoneTypeID
                                                        JOIN dbo.Addresses ON Addresses.CompanyID = Companies.CompanyID
                                                        WHERE Companies.CompanyID = {companyId} ORDER BY LastName", COLUMN_NUMBER, connectionString)
        companyName = _contactData(0)
        lastName = _contactData(1)
        firstName = _contactData(2)
        employeeTypesDescription = _contactData(3)
        phoneNumber = _contactData(4)
        phoneType = _contactData(5)
        address1 = _contactData(6)
        address2 = _contactData(7)
        city = _contactData(8)
        state = _contactData(9)
        postalCode = _contactData(10)
        _employeeID = _contactData(11)
        _phoneID = _contactData(12)
        _addressID = _contactData(13)
        _companyID = companyId

        _ErrorMessage = databaseFetcher.ErrorMessage
    End Sub

    Public Sub UpdateContactInformation(ByVal connectionString As String)
        _ErrorMessage = ""
        '_contactData(0) = companyName
        '_contactData(1) = lastName
        '_contactData(2) = firstName
        '_contactData(3) = employeeTypesDescription
        '_contactData(4) = phoneNumber
        '_contactData(5) = phoneType
        '_contactData(6) = address1
        '_contactData(7) = address2
        '_contactData(8) = city
        '_contactData(9) = state
        '_contactData(10) = postalCode

        databaseFetcher.CreateOleDbCommand($"BEGIN TRY
	                                            BEGIN TRANSACTION
		                                            UPDATE dbo.Employees
		                                            SET LastName = '{_lastName}', FirstName = '{_firstName}',EmployeeTypeID = 1
		                                            WHERE EmployeeID = {_employeeID}

		                                            UPDATE dbo.Companies
		                                            SET CompanyName = '{_companyName}'
		                                            WHERE CompanyID = {_companyID}

		                                            UPDATE dbo.Phones
		                                            SET Phone = '{_phoneNumber}',PhoneTypeID = 4
		                                            WHERE PhoneID = {_phoneID}

		                                            UPDATE dbo.Addresses
		                                            SET Address1 = '{_address1}', Address2 = '{_address2}',
			                                            City = '{_city}', State = '{_state}', PostalCode = '{_postalCode}'
		                                            WHERE AddressID = {_addressID}
	                                            COMMIT
                                            END TRY
                                            BEGIN CATCH
	                                            IF @@TRANCOUNT > 0
	                                            ROLLBACK
                                            END CATCH", connectionString)

        _ErrorMessage = databaseFetcher.ErrorMessage
    End Sub

    Public Sub DeleteContact(ByVal connectionString As String)
        _ErrorMessage = ""
        databaseFetcher.CreateOleDbCommand($"BEGIN TRY
	                                            BEGIN TRANSACTION
		                                            DELETE FROM dbo.Employees
		                                            WHERE EmployeeID = {_employeeID}

		                                            DELETE FROM dbo.Companies
		                                            WHERE CompanyID = {_companyID}

		                                            DELETE FROM dbo.Phones
		                                            WHERE PhoneID = {_phoneID}

		                                            DELETE FROM dbo.Addresses
		                                            WHERE AddressID = {_addressID}
	                                            COMMIT
                                            END TRY
                                            BEGIN CATCH
	                                            IF @@TRANCOUNT > 0
	                                            ROLLBACK
                                            END CATCH", connectionString)

        _ErrorMessage = databaseFetcher.ErrorMessage
    End Sub

    Public Sub AddNewContact(ByVal connectionString As String)
        _ErrorMessage = ""

        databaseFetcher.CreateOleDbCommand($"BEGIN TRY
	                                        BEGIN TRANSACTION
		                                        DECLARE @companyId INT
		                                        INSERT INTO dbo.Companies
		                                        VALUES ('{_companyName}')
		                                        SET	@companyId = @@Identity
		

		                                        INSERT INTO dbo.Employees
		                                        VALUES (@companyId, 1, '{_firstName}', '{_lastName}')

		                                        INSERT INTO dbo.Phones
		                                        VALUES (@companyId, 4, '{_phoneNumber}')

		                                        INSERT INTO dbo.Addresses
		                                        VALUES (@companyId, 2,'{_address1}', '{_address2}',
			                                        '{_city}', '{_state}', '{_postalCode}')
	                                        COMMIT
                                        END TRY
                                        BEGIN CATCH
	                                        IF @@TRANCOUNT > 0
	                                        ROLLBACK
                                        END CATCH", connectionString)

        _ErrorMessage = databaseFetcher.ErrorMessage

    End Sub

    Public Sub ClearData()
        companyName = ""
        lastName = ""
        firstName = ""
        employeeTypesDescription = ""
        phoneNumber = ""
        phoneType = ""
        address1 = ""
        city = ""
        state = ""
        postalCode = ""
        _employeeID = ""
        _phoneID = ""
        _addressID = ""
        _companyID = ""
    End Sub

End Class
