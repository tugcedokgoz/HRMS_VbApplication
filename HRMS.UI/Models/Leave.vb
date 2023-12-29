Imports DocuSign.eSign.Model

Public Class Leave
    Public Property Id As Long
    Public Property Firstname As String

    Public Property Lastname As String
    Public Property Employee As Employee
    Public Property Startdate As Date?

    Public Property Enddate As Date?

    Public Property Status As String

    Public Property Employeeid As Long?

    Public Property Leavetypeid As Long?


    Public Property Typename As String

    Public Property Description As String
    Public Property LeaveType As LeaveType
    Public Property FullName As String
        Get
            Return Firstname + " " + Lastname
        End Get
        Set(value As String)

        End Set
    End Property
End Class
