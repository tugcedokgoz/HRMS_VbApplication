Public Class Candidate
    Public Property Id As String
    Public Property Firstname As String

    Public Property Lastname As String

    Public Property Applicationdate As Date

    Public Property Resumelink As String

    Public Property Appliedpositionid As Long


    Public Property Appliedposition As Position


    Public Property FullName As String
        Get
            Return Firstname + " " + Lastname
        End Get
        Set(value As String)

        End Set
    End Property

End Class
