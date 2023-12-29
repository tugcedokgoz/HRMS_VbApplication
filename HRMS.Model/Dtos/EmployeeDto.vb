Imports HRMS.Model.Models

Public Class EmployeeGetDto
    Public Property Id As Long

    Public Property Firstname As String
    Public Property Lastname As String
    Public Property Birthdate As Date
    Public Property Gender As String
    Public Property Hiredate As Date
    Public Property Email As String
    Public Property Phonenumber As String
    Public Property Positionid As Long
    Public Property Departmanid As Long
    Public Property Managerid As Long
    Public Property Annualleave As Integer?

    Public Property Leaves As ICollection(Of Leaf) = New List(Of Leaf)()

End Class
Public Class EmployeePostDto
    Public Property Firstname As String
    Public Property Lastname As String
    Public Property Birthdate As Date
    Public Property Gender As String
    Public Property Hiredate As Date
    Public Property Email As String
    Public Property Phonenumber As String
    Public Property Positionid As Long
    Public Property Departmanid As Long
    Public Property Managerid As Long
    Public Property Annualleave As Integer?
End Class
Public Class EmployeePutDto
    Public Property Id As Long
    Public Property Firstname As String
    Public Property Lastname As String
    Public Property Birthdate As Date
    Public Property Gender As String
    Public Property Hiredate As Date
    Public Property Email As String
    Public Property Phonenumber As String
    Public Property Positionid As Long
    Public Property Departmanid As Long
    Public Property Managerid As Long
    Public Property Annualleave As Integer?
End Class

