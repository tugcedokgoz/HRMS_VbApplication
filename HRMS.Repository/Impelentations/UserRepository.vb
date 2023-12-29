Imports HRMS.Model.Models
Imports Infrastructure

Public Class UserRepository : Inherits BaseRepository(Of User, Long, HRMSContext) : Implements IUserRepository
    Public Sub New(context As HRMSContext)
        MyBase.New(context)
    End Sub

End Class
