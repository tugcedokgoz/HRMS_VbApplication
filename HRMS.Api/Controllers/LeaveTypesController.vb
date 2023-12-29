Imports System
Imports HRMS.Business
Imports HRMS.Model
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.Extensions.Logging

Namespace HRMS.Api.Controllers
    Public Class LeaveTypesController
        Inherits BaseController

        Private ReadOnly _service As ILeavesTypeBs

        Public Sub New(service As ILeavesTypeBs)
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
        Async Function AddLeaveType(<FromBody> dto As LeavesTypePostDto) As Task(Of IActionResult)
            Dim response = Await _service.Add(dto)
            Return SendResponse(response)
        End Function

        <HttpDelete("{id}")>
        Async Function DeleteLeaveType(<FromRoute> id As Long) As Task(Of IActionResult)
            Dim response = Await _service.Delete(id)
            Return SendResponse(response)
        End Function
    End Class
End Namespace
