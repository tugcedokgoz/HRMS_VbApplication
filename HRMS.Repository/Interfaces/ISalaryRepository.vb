Public Interface ISalaryRepository
    Function ManageSalary(salaryId As Long?, baseSalary As Decimal, bonus As Decimal?, deductions As Decimal?, effectiveDate As Date, employeeId As Long, operation As String) As Task(Of String)

End Interface
