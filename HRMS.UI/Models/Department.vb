Public Class Department
    Public Property Id As Long
    Public Property Departmentname As String
    Public Property Managerid As Long?
    Public Property Isactive As Boolean

    Public Property Manager As Employee
End Class
