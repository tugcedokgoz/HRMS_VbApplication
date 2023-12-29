Imports HRMS.Api.Controllers
Imports HRMS.Api.HRMS.Api.Controllers
Imports HRMS.Business
Imports HRMS.Model
Imports Microsoft.AspNetCore.Mvc

Public Class SalaryController
    Inherits BaseController

    Private ReadOnly _service As ISalaryBs

    Public Sub New(service As ISalaryBs)
        _service = service
    End Sub

    ' ... other actions ...

    <HttpPost("ManageSalary")>
    Public Async Function ManageSalary(<FromBody> dto As SalaryPutDto) As Task(Of IActionResult)
        Dim response = Await _service.ManageSalary(dto.Id, dto.Basesalary, dto.Bonus, dto.Deductions, dto.Effectivedate, dto.Employeeid, dto.Operation)
        Return SendResponse(response)
    End Function
End Class
