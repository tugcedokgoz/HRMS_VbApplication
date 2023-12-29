Imports HRMS.Api.HRMS.Api.Controllers
Imports HRMS.Business
Imports HRMS.Model
Imports Microsoft.AspNetCore.Mvc

Public Class InterviewController
    Inherits BaseController

    Private ReadOnly _service As IInterviewBs

    Public Sub New(service As IInterviewBs)
        _service = service
    End Sub
    <HttpGet("GetById/{id}")>
    Async Function GetById(<FromRoute> id As Long) As Task(Of IActionResult)
        Dim response = Await _service.GetById(id)
        Return SendResponse(response)
    End Function


    <HttpGet("GetAll")>
    Async Function GetAll() As Task(Of IActionResult)
        Dim response = Await _service.GetAll()
        Return SendResponse(response)
    End Function

    <HttpPost>
    Async Function AddInterview(<FromBody> dto As InterviewPostDto) As Task(Of IActionResult)
        Dim response = Await _service.Add(dto)
        Return SendResponse(response)
    End Function

    <HttpDelete("{id}")>
    Async Function DeleteInterview(<FromRoute> id As Long) As Task(Of IActionResult)
        Dim response = Await _service.Delete(id)
        Return SendResponse(response)
    End Function

    <HttpPost("Schedule")>
    Public Async Function ScheduleInterview(<FromBody> dto As InterviewPostDto) As Task(Of IActionResult)
        Dim response = Await _service.ScheduleInterview(dto.Interviewdate, dto.Candidateid, dto.Interviewerid, dto.Interviewnotes, dto.Interviewoutcome)
        Return SendResponse(response)
    End Function

    <HttpPost("Manage")>
    Public Async Function ManageInterview(<FromBody> dto As InterviewPutDto) As Task(Of IActionResult)
        Dim response = Await _service.ManageInterview(dto.Id, dto.Interviewdate, dto.Candidateid, dto.Interviewerid, dto.Operation, dto.Interviewnotes, dto.Interviewoutcome)
        Return SendResponse(response)
    End Function
End Class

