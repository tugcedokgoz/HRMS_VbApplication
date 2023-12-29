Imports HRMS.Repository
Imports Infrastructure.Infrastructure.Utilities.ApiResponses

Public Class SalaryBs
    Implements ISalaryBs

    Private ReadOnly _repo As ISalaryRepository

    Public Sub New(repo As ISalaryRepository)
        _repo = repo
    End Sub

    Public Async Function ManageSalary(salaryId As Long?, baseSalary As Decimal, bonus As Decimal?, deductions As Decimal?, effectiveDate As Date, employeeId As Long, operation As String) As Task(Of ApiResponse(Of String)) Implements ISalaryBs.ManageSalary
        Dim result = Await _repo.ManageSalary(salaryId, baseSalary, bonus, deductions, effectiveDate, employeeId, operation)

        If Not String.IsNullOrWhiteSpace(result) Then
            Return ApiResponse(Of String).Success(200, result)
        Else
            Return ApiResponse(Of String).Fail(500, "An error occurred during the salary management operation.")
        End If
    End Function
End Class
