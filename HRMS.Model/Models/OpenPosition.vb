Imports System
Imports System.Collections.Generic
Imports Infrastructure

Namespace Models
    Partial Public Class OpenPosition : Inherits BaseEntity(Of Long)

        Public Property Positionid As Long

        Public Property Openinggdate As Date?

        Public Property Closingdate As Date?

        Public Property Description As String


        Public Overridable Property Position As Position
    End Class
End Namespace
