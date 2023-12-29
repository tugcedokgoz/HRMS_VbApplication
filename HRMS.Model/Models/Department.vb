Imports System
Imports System.Collections.Generic
Imports Infrastructure

Namespace Models
    Partial Public Class Department : Inherits BaseEntity(Of Long)

        Public Property Departmentname As String

        Public Property Managerid As Long?


        Public Overridable ReadOnly Property Employees As ICollection(Of Employee) = New List(Of Employee)()

        Public Overridable Property Manager As Employee
    End Class
End Namespace
