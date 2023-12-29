Imports HRMS.Api.HRMS.Api.Controllers
Imports HRMS.Business
Imports HRMS.Model
Imports Microsoft.AspNetCore.Mvc

Public Class EmployeeController
    Inherits BaseController

    Private ReadOnly _service As IEmployeeBs

    Public Sub New(service As IEmployeeBs)
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
    Async Function AddEmployee(<FromBody> dto As EmployeePostDto) As Task(Of IActionResult)
        Dim response = Await _service.Add(dto)
        Return SendResponse(response)
    End Function

    <HttpDelete("{id}")>
    Async Function DeleteEmployee(<FromRoute> id As Long) As Task(Of IActionResult)
        Dim response = Await _service.Delete(id)
        Return SendResponse(response)
    End Function

    '<HttpGet("Search")>
    'Async Function SearchByBirthdateAndLastname(<FromQuery> birthdate As Date, <FromQuery> lastname As String) As Task(Of IActionResult)
    '    Dim response = Await _service.SearchByBirthdateAndLastname(birthdate, lastname)
    '    Return SendResponse(response)
    'End Function

    <HttpGet("SearchByLastNameAndBirthdate")>
    Public Async Function SearchEmployeesByLastNameAndBirthdate(<FromQuery> lastname As String, <FromQuery> birthdate As Date) As Task(Of IActionResult)
        ' Service katmanını kullanarak çalışanları ara
        Dim response = Await _service.SearchEmployeesByLastNameAndBirthdate(lastname, birthdate)

        ' Yanıtı döndür
        Return SendResponse(response)
    End Function

    <HttpGet("Report")>
    Public Async Function GetEmployeeReport(Optional departmentId As Long? = Nothing, Optional startDate As Date? = Nothing, Optional endDate As Date? = Nothing) As Task(Of IActionResult)
        Dim response = Await _service.GetEmployeeReport(departmentId, startDate, endDate)
        Return SendResponse(response)
    End Function
    <HttpPost("GetAnnualLeave/{id}")>
    Async Function GetAnnualLeave(id As Long, dto As EmployeePutDto) As Task(Of IActionResult)
        Dim response = Await _service.GetAnnualLeave(id, dto)
        Return SendResponse(response)
    End Function

    <HttpGet("GetByPosition")>
    Async Function GetByPosition(<FromQuery> positionId As Long) As Task(Of IActionResult)
        Dim response = Await _service.GetByPosition(positionId)
        Return SendResponse(response)
    End Function
End Class
