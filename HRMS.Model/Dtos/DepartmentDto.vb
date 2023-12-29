Imports HRMS.Model.Models

Public Class DepartmentGetDto
    Public Property Id As Long

    Public Property Departmentname As String
    Public Property Managerid As Long?
    Public Property Isactive As Boolean

    Public Property Manager As EmployeeGetDto
End Class

Public Class DepartmentPostDto
    Public Property Departmentname As String
    Public Property Managerid As Long?
End Class

Public Class DepartmentPutDto
    Public Property Id As Long
    Public Property Departmentname As String
    Public Property Managerid As Long?
    Public Property Operation As String
End Class
