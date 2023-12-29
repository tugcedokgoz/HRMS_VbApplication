Imports HRMS.Model.Models

Public Class LeavesTypeGetDto
    Public Property Id As Long
    Public Property Typename As String

    Public Property Description As String

    'Public Overridable ReadOnly Property Leaves As ICollection(Of Leaves) = New List(Of Leaves)()
End Class

Public Class LeavesTypePostDto
    Public Property Typename As String

    Public Property Description As String

End Class

Public Class LeavesTypePutDto
    Public Property Id As Long
    Public Property Typename As String

    Public Property Description As String

End Class