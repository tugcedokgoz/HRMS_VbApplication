Imports HRMS.Model.Models
Imports Infrastructure

Public Interface IPerformanceReviewRepository : Inherits IBaseRepository(Of PerformanceReview, Long)
    Function RecordPerformanceReview(employeeId As Long, reviewDate As Date, score As Long, comments As String, reviewerId As Long) As Task(Of String)
End Interface
