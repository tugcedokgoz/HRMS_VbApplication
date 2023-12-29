Public Class PositionGetDto
    Public Property Id As Long
    Public Property Positiontitle As String

    Public Property Description As String

    Public Property Salarygrade As Long
End Class


Public Class PositionPostDto
    Public Property Positiontitle As String

    Public Property Description As String

    Public Property Salarygrade As Long
End Class


Public Class PositionPutDto

    Public Property Id As Long

    Public Property Positiontitle As String

    Public Property Description As String

    Public Property Salarygrade As Long

    Public Property Operation As String

End Class
