Public Class PerformanceReviewGetDto

    Public Property Reviewdate As Date

    Public Property Score As Long

    Public Property Comments As String


End Class

Public Class PerformanceReviewPostDto

    Public Property Reviewdate As Date

    Public Property Score As Long

    Public Property Comments As String

    Public Property Employeeid As Long

    Public Property Reviewerid As Long
End Class


Public Class PerformanceReviewPutDto
    Public Property Id As Long

    Public Property Reviewdate As Date

    Public Property Score As Long

    Public Property Comments As String

    Public Property Employeeid As Long

    Public Property Reviewerid As Long
End Class
