Imports HRMS.Api.Controllers
Imports HRMS.Api.HRMS.Api.Controllers
Imports HRMS.Business
Imports HRMS.Model
Imports Infrastructure.Infrastructure.Utilities.ApiResponses
Imports Microsoft.AspNetCore.Mvc

Public Class LeaveController
    Inherits BaseController

    Private ReadOnly _service As ILeaveBs

    Public Sub New(service As ILeaveBs)
        _service = service
    End Sub

    ' ... other actions ...

    <HttpPost("ManageLeave")>
    Public Async Function ManageLeave(<FromBody> dto As LeafPutDto) As Task(Of IActionResult)
        Dim response = Await _service.ManageLeave(dto.Id, dto.Startdate, dto.Enddate, dto.Status, dto.Employeeid, dto.Leavetypeid, dto.Operation)
        Return SendResponse(response)
    End Function


    <HttpPost("subtractAnnualLeave/{id}")>
    Public Async Function SubtractAnnualLeave(id As Long, <FromBody> dto As LeafPutDto) As Task(Of IActionResult)

        Dim result As ApiResponse(Of LeafPutDto) = Await _service.SubtractAnnualLeave(id, dto)
        Return Ok(result.Data)

    End Function

    <HttpGet("GetByEmployee/{employeeId}")>
    Public Async Function GetByEmployee(<FromRoute> employeeId As Long) As Task(Of IActionResult)
        Dim result As ApiResponse(Of List(Of LeafGetDto)) = Await _service.GetByEmployeeId(employeeId)
        Return SendResponse(result)
    End Function

    <HttpGet>
    Public Async Function GetAll() As Task(Of IActionResult)
        Dim result As ApiResponse(Of List(Of LeafGetDto)) = Await _service.GetAll()
        Return SendResponse(result)
    End Function

    <HttpDelete("{id}")>
    Async Function DeleteLeave(<FromRoute> id As Long) As Task(Of IActionResult)
        Dim response = Await _service.Delete(id)
        Return SendResponse(response)
    End Function
End Class
