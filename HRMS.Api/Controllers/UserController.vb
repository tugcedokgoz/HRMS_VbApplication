Imports HRMS.Api.HRMS.Api.Controllers
Imports HRMS.Business
Imports HRMS.Model
Imports Microsoft.AspNetCore.Mvc

Public Class UserController
    Inherits BaseController
    Private ReadOnly _service As IUserBs

    Public Sub New(service As IUserBs)
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
    Async Function AddUser(<FromBody> dto As UserPostDto) As Task(Of IActionResult)
        Dim response = Await _service.Add(dto)
        Return SendResponse(response)
    End Function

    <HttpDelete("{id}")>
    Async Function DeleteUser(<FromRoute> id As Long) As Task(Of IActionResult)
        Dim response = Await _service.Delete(id)
        Return SendResponse(response)
    End Function
End Class
