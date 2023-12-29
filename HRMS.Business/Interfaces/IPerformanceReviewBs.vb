Imports Infrastructure.Infrastructure.Utilities.ApiResponses

Public Interface IPerformanceReviewBs
    Function RecordPerformanceReview(employeeId As Long, reviewDate As Date, score As Long, comments As String, reviewerId As Long) As Task(Of ApiResponse(Of String))
End Interface
