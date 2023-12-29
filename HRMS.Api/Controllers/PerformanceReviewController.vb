Imports HRMS.Api.HRMS.Api.Controllers
Imports HRMS.Business
Imports HRMS.Model
Imports Microsoft.AspNetCore.Mvc

Public Class PerformanceReviewController
    Inherits BaseController

    Private ReadOnly _service As IPerformanceReviewBs

    Public Sub New(service As IPerformanceReviewBs)
        _service = service
    End Sub

    ' ... other actions ...

    <HttpPost("RecordReview")>
    Public Async Function RecordReview(<FromBody> dto As PerformanceReviewPutDto) As Task(Of IActionResult)
        Dim response = Await _service.RecordPerformanceReview(dto.Id, dto.Reviewdate, dto.Score, dto.Comments, dto.Reviewerid)
        Return SendResponse(response)
    End Function
End Class
