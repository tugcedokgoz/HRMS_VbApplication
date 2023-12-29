Imports System
Imports System.Collections.Generic
Imports Infrastructure

Namespace Models
    Partial Public Class LeavesType : Inherits BaseEntity(Of Long)

        Public Property Typename As String

        Public Property Description As String


        Public Overridable ReadOnly Property Leaves As ICollection(Of Leaf) = New List(Of Leaf)()
    End Class
End Namespace
