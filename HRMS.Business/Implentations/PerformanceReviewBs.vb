Imports HRMS.Repository
Imports Infrastructure.Infrastructure.Utilities.ApiResponses

Public Class PerformanceReviewBs
    Implements IPerformanceReviewBs

    Private ReadOnly _repo As IPerformanceReviewRepository

    Public Sub New(repo As IPerformanceReviewRepository)
        _repo = repo
    End Sub

    Public Async Function RecordPerformanceReview(employeeId As Long, reviewDate As Date, score As Long, comments As String, reviewerId As Long) As Task(Of ApiResponse(Of String)) Implements IPerformanceReviewBs.RecordPerformanceReview
        Dim result = Await _repo.RecordPerformanceReview(employeeId, reviewDate, score, comments, reviewerId)

        If Not String.IsNullOrWhiteSpace(result) Then
            Return ApiResponse(Of String).Success(200, result)
        Else
            Return ApiResponse(Of String).Fail(500, "An error occurred during recording the performance review.")
        End If
    End Function
End Class
