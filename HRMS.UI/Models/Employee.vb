Public Class Employee
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
    Public Property Isactive As Boolean


    Public Property FullName() As String
        Get
            Return Firstname + " " + Lastname
        End Get
        Set(value As String)
        End Set
    End Property
End Class
