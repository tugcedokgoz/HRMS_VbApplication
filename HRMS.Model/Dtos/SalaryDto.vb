Public Class SalaryGetDto

    Public Property Basesalary As Decimal

    Public Property Bonus As Decimal?

    Public Property Deductions As Decimal?

    Public Property Effectivedate As Date


End Class

Public Class SalaryPostDto
    Public Property Basesalary As Decimal

    Public Property Bonus As Decimal?

    Public Property Deductions As Decimal?

    Public Property Effectivedate As Date

    Public Property Employeeid As Long?
End Class

Public Class SalaryPutDto
    Public Property Id As Long

    Public Property Basesalary As Decimal

    Public Property Bonus As Decimal?

    Public Property Deductions As Decimal?

    Public Property Effectivedate As Date

    Public Property Employeeid As Long?

    Public Property Operation As String

End Class
