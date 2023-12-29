Imports HRMS.Model.Models

Public Class LeafGetDto
    Public Property Id As Long
    Public Property Startdate As Date?

    Public Property Enddate As Date?

    Public Property Status As String

    Public Property Leavetype As LeavesTypeGetDto

    Public Property Employee As EmployeeGetDto
End Class


Public Class LeafPostDto
    Public Property Startdate As Date?

    Public Property Enddate As Date?

    Public Property Status As String

    Public Property Employeeid As Long?

    Public Property Leavetypeid As Long?
End Class


Public Class LeafPutDto
    Public Property Id As Long
    Public Property Startdate As Date?

    Public Property Enddate As Date?

    Public Property Status As String

    Public Property Employeeid As Long?

    Public Property Leavetypeid As Long?
    Public Property Operation As String
End Class