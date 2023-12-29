Public Class CandidateGetDto
    Public Property Id As Long
    Public Property Firstname As String
    Public Property Lastname As String
    Public Property Applicationdate As Date
    Public Property Resumelink As String
    Public Property Appliedpositionid As Long
    Public Property Isactive As Boolean

    Public Property Appliedposition As PositionGetDto

    ' İlişkili varlıkları da DTO formunda dahil etmek iyi bir uygulamadır.
    'Public Property Appliedposition As GetPositionDto
    'Public Property Interviews As ICollection(Of GetInterviewDto)
End Class

Public Class CandidatePostDto
    Public Property Firstname As String
    Public Property Lastname As String
    Public Property Applicationdate As Date
    Public Property Resumelink As String
    Public Property Appliedpositionid As Long
End Class

Public Class CandidatePutDto
    Public Property Id As Long
    Public Property Firstname As String
    Public Property Lastname As String
    Public Property Applicationdate As Date
    Public Property Resumelink As String
    Public Property Appliedpositionid As Long
    Public Property Isactive As Boolean
End Class