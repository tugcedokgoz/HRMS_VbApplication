Public Class InterviewGetDto
    Public Property Id As Long

    Public Property Interviewdate As Date

    Public Property Interviewnotes As String

    Public Property Interviewoutcome As String

    Public Overridable Property Candidate As CandidateGetDto

    Public Overridable Property Interviewer As EmployeeGetDto

End Class

Public Class InterviewPostDto
    Public Property Interviewdate As Date

    Public Property Interviewnotes As String

    Public Property Interviewoutcome As String

    Public Property Candidateid As Long

    Public Property Interviewerid As Long?
End Class


Public Class InterviewPutDto
    Public Property Id As Long
    Public Property Interviewdate As Date

    Public Property Interviewnotes As String

    Public Property Interviewoutcome As String

    Public Property Candidateid As Long

    Public Property Interviewerid As Long?

    Public Property Operation As String
End Class