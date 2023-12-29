Imports System
Imports System.Collections.Generic
Imports Infrastructure

Namespace Models
    Partial Public Class Image : Inherits BaseEntity(Of Long)

        Public Property Imagename As String

        Public Property Imagepath As String

        Public Property Imagetype As String


        Public Property Employeeid As Long?

        Public Property Candİdateid As Long?

        Public Overridable Property Candİdate As Candidate

        Public Overridable Property Employee As Employee
    End Class
End Namespace
