Imports HRMS.Model.Models
Imports Infrastructure

Public Interface IInterviewRepository : Inherits IBaseRepository(Of Interview, Long)
    Function ScheduleInterview(interviewDate As Date, candidateId As Long, interviewerId As Long, Optional interviewNotes As String = Nothing, Optional interviewOutcome As String = Nothing) As Task(Of String)
    Function ManageInterview(interviewId As Long, interviewDate As Date, candidateId As Long, interviewerId As Long, operation As String, Optional interviewNotes As String = Nothing, Optional interviewOutcome As String = Nothing) As Task(Of String)
End Interface
