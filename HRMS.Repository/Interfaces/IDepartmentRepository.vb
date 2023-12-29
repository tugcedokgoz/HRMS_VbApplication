Imports HRMS.Model.Models
Imports Infrastructure

Public Interface IDepartmentRepository : Inherits IBaseRepository(Of Department, Long)
    Function ManageDepartment(departmentId As Long?, departmentName As String, managerId As Long?, operation As String) As Task(Of String)
    Function ListPositionsByDepartmentId(departmentId As Long) As Task(Of List(Of Position))
End Interface
