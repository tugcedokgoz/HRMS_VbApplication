Imports System
Imports System.Collections.Generic
Imports Infrastructure

Namespace Models
    Partial Public Class Employee : Inherits BaseEntity(Of Long)

        Public Property Firstname As String

        Public Property Lastname As String

        Public Property Birthdate As Date

        Public Property Gender As String

        Public Property Hiredate As Date

        Public Property Email As String

        Public Property Phonenumber As String

        Public Property Positionid As Long

        Public Property Departmanid As Long

        Public Property Managerid As Long?

        Public Property Annualleave As Integer?


        Public Overridable Property Departman As Department

        Public Overridable ReadOnly Property Departments As ICollection(Of Department) = New List(Of Department)()

        Public Overridable ReadOnly Property Interviews As ICollection(Of Interview) = New List(Of Interview)()

        Public Overridable ReadOnly Property InverseManager As ICollection(Of Employee) = New List(Of Employee)()

        Public Overridable ReadOnly Property Leaves As ICollection(Of Leaf) = New List(Of Leaf)()

        Public Overridable Property Manager As Employee

        Public Overridable ReadOnly Property PerformanceReviewEmployees As ICollection(Of PerformanceReview) = New List(Of PerformanceReview)()

        Public Overridable ReadOnly Property PerformanceReviewReviewers As ICollection(Of PerformanceReview) = New List(Of PerformanceReview)()

        Public Overridable Property Position As Position

        Public Overridable ReadOnly Property Salaries As ICollection(Of Salary) = New List(Of Salary)()
    End Class
End Namespace
