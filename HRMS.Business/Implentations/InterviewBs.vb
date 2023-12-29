Imports AutoMapper
Imports HRMS.Model
Imports HRMS.Model.Models
Imports HRMS.Repository
Imports Infrastructure
Imports Infrastructure.Infrastructure.Utilities.ApiResponses

Public Class InterviewBs : Implements IInterviewBs
    Private ReadOnly _repo As IInterviewRepository
    Private ReadOnly _mapper As IMapper

    Public Sub New(repo As IInterviewRepository, mapper As IMapper)
        _repo = repo
        _mapper = mapper
    End Sub
    Public Async Function GetById(id As Long) As Task(Of ApiResponse(Of InterviewGetDto)) Implements IInterviewBs.GetById
        Dim interview = Await _repo.GetByIdAsync(id)
        Dim mappedInterview = _mapper.Map(Of InterviewGetDto)(interview)

        Return ApiResponse(Of InterviewGetDto).Success(200, mappedInterview)
    End Function

    Public Async Function Add(dto As InterviewPostDto) As Task(Of ApiResponse(Of InterviewGetDto)) Implements IInterviewBs.Add
        Dim newItem = _mapper.Map(Of Interview)(dto)
        Dim added = Await _repo.AddAsync(newItem)

        Dim newDto = _mapper.Map(Of InterviewGetDto)(added)

        Return ApiResponse(Of InterviewGetDto).Success(201, newDto)
    End Function

    Public Async Function GetAll() As Task(Of ApiResponse(Of IEnumerable(Of InterviewGetDto))) Implements IInterviewBs.GetAll
        Dim includeList As New List(Of String)
        includeList.Add("Candidate")
        includeList.Add("Interviewer")
        Dim repoResponse = Await _repo.GetListAsync(includeList:=includeList)
        Dim dtoList = _mapper.Map(Of IEnumerable(Of InterviewGetDto))(repoResponse)

        Return ApiResponse(Of IEnumerable(Of InterviewGetDto)).Success(200, dtoList)
    End Function

    Public Async Function Delete(id As Long) As Task(Of ApiResponse(Of NoData)) Implements IInterviewBs.Delete
        Dim result = Await _repo.DeleteAsync(id)
        If result Then
            Return ApiResponse(Of NoData).Success(200)
        Else
            Return ApiResponse(Of NoData).Fail(404, "Item not found")
        End If
    End Function

    Public Async Function Update(id As Long, dto As InterviewPutDto) As Task(Of ApiResponse(Of InterviewGetDto)) Implements IInterviewBs.Update
        Dim existingItem = Await _repo.GetByIdAsync(id)
        If existingItem Is Nothing Then
            Return ApiResponse(Of InterviewGetDto).Fail(404, "Item not found")
        End If

        _mapper.Map(dto, existingItem)
        Await _repo.UpdateAsync(existingItem)

        Dim updatedDto = _mapper.Map(Of InterviewGetDto)(existingItem)

        Return ApiResponse(Of InterviewGetDto).Success(200, updatedDto)
    End Function

    Public Async Function ScheduleInterview(interviewDate As Date, candidateId As Long, interviewerId As Long, Optional interviewNotes As String = Nothing, Optional interviewOutcome As String = Nothing) As Task(Of ApiResponse(Of String)) Implements IInterviewBs.ScheduleInterview
        Dim result = Await _repo.ScheduleInterview(interviewDate, candidateId, interviewerId, interviewNotes, interviewOutcome)

        If Not String.IsNullOrWhiteSpace(result) Then
            Return ApiResponse(Of String).Success(200, result)
        Else
            Return ApiResponse(Of String).Fail(500, "An error occurred while scheduling the interview.")
        End If
    End Function

    Public Async Function ManageInterview(interviewId As Long, interviewDate As Date, candidateId As Long, interviewerId As Long, operation As String, Optional interviewNotes As String = Nothing, Optional interviewOutcome As String = Nothing) As Task(Of ApiResponse(Of String)) Implements IInterviewBs.ManageInterview
        Dim result = Await _repo.ManageInterview(interviewId, interviewDate, candidateId, interviewerId, operation, interviewNotes, interviewOutcome)

        If Not String.IsNullOrWhiteSpace(result) Then
            Return ApiResponse(Of String).Success(200, result)
        Else
            Return ApiResponse(Of String).Fail(500, "An error occurred while scheduling the interview.")
        End If
    End Function
End Class
