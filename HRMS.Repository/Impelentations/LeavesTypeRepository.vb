Imports HRMS.Model.Models
Imports Infrastructure

Public Class LeavesTypeRepository : Inherits BaseRepository(Of LeavesType, Long, HrmsContext) : Implements ILeavesTypeRepository

    Public Sub New(context As HrmsContext)
        MyBase.New(context)
    End Sub
End Class
