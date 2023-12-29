Imports HRMS.Api.HRMS.Api.Controllers
Imports HRMS.Business
Imports HRMS.Model
Imports Microsoft.AspNetCore.Mvc

Public Class PositionController
    Inherits BaseController
    Private ReadOnly _service As IPositionBs

    Public Sub New(service As IPositionBs)
        _service = service
    End Sub

    <HttpGet("GetById/{id}")>
    Public Async Function GetById(<FromRoute> id As Long) As Task(Of IActionResult)
        Dim response = Await _service.GetById(id)
        Return SendResponse(response)
    End Function

    <HttpGet("GetAll")>
    Public Async Function GetAll() As Task(Of IActionResult)
        Dim response = Await _service.GetAll()
        Return SendResponse(response)
    End Function

    <HttpPost("ManagePosition")>
    Public Async Function ManagePosition(<FromBody> dto As PositionPutDto) As Task(Of IActionResult)
        Dim response = Await _service.ManagePosition(dto.Id, dto.Positiontitle, dto.Description, dto.Salarygrade, dto.Operation)
        Return SendResponse(response)
    End Function
End Class
