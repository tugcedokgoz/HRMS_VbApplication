Imports System
Imports System.Collections.Generic
Imports Infrastructure

Namespace Models
    Partial Public Class Salary : Inherits BaseEntity(Of Long)

        Public Property Basesalary As Decimal

        Public Property Bonus As Decimal?

        Public Property Deductions As Decimal?

        Public Property Effectivedate As Date

        Public Property Employeeid As Long?


        Public Overridable Property Employee As Employee
    End Class
End Namespace
