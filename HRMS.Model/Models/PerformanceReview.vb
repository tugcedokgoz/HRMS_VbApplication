Imports System
Imports System.Collections.Generic
Imports Infrastructure

Namespace Models
    Partial Public Class PerformanceReview : Inherits BaseEntity(Of Long)

        Public Property Reviewdate As Date

        Public Property Score As Long

        Public Property Comments As String

        Public Property Employeeid As Long

        Public Property Reviewerid As Long


        Public Overridable Property Employee As Employee

        Public Overridable Property Reviewer As Employee
    End Class
End Namespace
