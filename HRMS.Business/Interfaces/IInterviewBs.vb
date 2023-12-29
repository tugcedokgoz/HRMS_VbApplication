Imports HRMS.Model
Imports Infrastructure
Imports Infrastructure.Infrastructure.Utilities.ApiResponses

Public Interface IInterviewBs
    Function GetById(id As Long) As Task(Of ApiResponse(Of InterviewGetDto))
    Function Add(dto As InterviewPostDto) As Task(Of ApiResponse(Of InterviewGetDto))
    Function GetAll() As Task(Of ApiResponse(Of IEnumerable(Of InterviewGetDto)))
    Function Delete(id As Long) As Task(Of ApiResponse(Of NoData))
    Function Update(id As Long, dto As InterviewPutDto) As Task(Of ApiResponse(Of InterviewGetDto))
    Function ScheduleInterview(interviewDate As Date, candidateId As Long, interviewerId As Long, Optional interviewNotes As String = Nothing, Optional interviewOutcome As String = Nothing) As Task(Of ApiResponse(Of String))
    Function ManageInterview(interviewId As Long, interviewDate As Date, candidateId As Long, interviewerId As Long, operation As String, Optional interviewNotes As String = Nothing, Optional interviewOutcome As String = Nothing) As Task(Of ApiResponse(Of String))
End Interface
