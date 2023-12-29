Imports HRMS.Api.HRMS.Api.Controllers
Imports HRMS.Business
Imports HRMS.Model
Imports Microsoft.AspNetCore.Mvc

Public Class CandidateController
    Inherits BaseController

    Private ReadOnly _service As ICandidateBs

    Public Sub New(service As ICandidateBs)
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
    Async Function AddCandidate(<FromBody> dto As CandidatePostDto) As Task(Of IActionResult)
        Dim response = Await _service.Add(dto)
        Return SendResponse(response)
    End Function

    <HttpDelete("{id}")>
    Async Function DeleteCandidate(<FromRoute> id As Long) As Task(Of IActionResult)
        Dim response = Await _service.Delete(id)
        Return SendResponse(response)
    End Function


    <HttpPost("Procedure")>
    Async Function Procedure(<FromBody> dto As CandidatePostDto) As Task(Of IActionResult)
        Dim response = Await _service.AddNewCandidateProcedure(dto.Firstname, dto.Lastname, dto.Applicationdate, dto.Resumelink, dto.Appliedpositionid)
        Return Ok(response)
    End Function
End Class

