Imports HRMS.Model.Models
Imports Infrastructure

Public Class ImageRepository : Inherits BaseRepository(Of Image, Long, HRMSContext) : Implements IImageRepository
    Public Sub New(context As HRMSContext)
        MyBase.New(context)
        _context = context
    End Sub

    Private ReadOnly _context As HRMSContext

End Class
