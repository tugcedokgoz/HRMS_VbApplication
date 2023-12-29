Imports System
Imports System.Collections.Generic
Imports Infrastructure

Namespace Models
    Partial Public Class Position : Inherits BaseEntity(Of Long)

        Public Property Positiontitle As String

        Public Property Description As String

        Public Property Salarygrade As Long


        Public Overridable ReadOnly Property Candidates As ICollection(Of Candidate) = New List(Of Candidate)()

        Public Overridable ReadOnly Property Employees As ICollection(Of Employee) = New List(Of Employee)()

        Public Overridable ReadOnly Property OpenPositions As ICollection(Of OpenPosition) = New List(Of OpenPosition)()
    End Class
End Namespace
