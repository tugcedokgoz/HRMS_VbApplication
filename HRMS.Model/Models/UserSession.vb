Imports System
Imports System.Collections.Generic
Imports Infrastructure

Namespace Models
    Partial Public Class UserSession : Inherits BaseEntity(Of Long)

        Public Property UserId As Long?

        Public Property CreatedAt As Date?

        Public Property ExpiresAt As Date?

        Public Property LastActivity As Date?

        Public Property Ipaddress As String

        Public Property UserAgent As String

        Public Property Token As String


        Public Overridable Property User As User
    End Class
End Namespace
