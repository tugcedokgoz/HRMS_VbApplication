Imports System.Data
Imports System.Linq.Expressions
Imports HRMS.Model.Models
Imports Infrastructure
Imports Microsoft.Data.SqlClient
Imports Microsoft.EntityFrameworkCore

Public Class PerformanceReviewRepository : Inherits BaseRepository(Of PerformanceReview, Long, HRMSContext) : Implements IPerformanceReviewRepository

    Private ReadOnly _context As HRMSContext

    Public Sub New(context As HRMSContext)
        MyBase.New(context)
        _context = context
    End Sub

    Public Async Function RecordPerformanceReview(employeeId As Long, reviewDate As Date, score As Long, comments As String, reviewerId As Long) As Task(Of String) Implements IPerformanceReviewRepository.RecordPerformanceReview
        Return Await Task.Run(Function()
                                  Using conn As New SqlConnection(_context.Database.GetDbConnection().ConnectionString)
                                      Using cmd As New SqlCommand("sp_RecordPerformanceReview", conn)
                                          cmd.CommandType = CommandType.StoredProcedure
                                          cmd.Parameters.Add(New SqlParameter("@EmployeeID", employeeId))
                                          cmd.Parameters.Add(New SqlParameter("@ReviewDate", reviewDate))
                                          cmd.Parameters.Add(New SqlParameter("@Score", score))
                                          cmd.Parameters.Add(New SqlParameter("@Comments", If(String.IsNullOrWhiteSpace(comments), DBNull.Value, comments)))
                                          cmd.Parameters.Add(New SqlParameter("@ReviewerID", reviewerId))

                                          conn.Open()
                                          Dim result As Object = cmd.ExecuteScalar()
                                          conn.Close()

                                          Return If(result IsNot Nothing, result.ToString(), "Error: No result returned from stored procedure.")
                                      End Using
                                  End Using
                              End Function)
    End Function

    Public Function GetAllAsync() As Task(Of IEnumerable(Of PerformanceReview)) Implements IBaseRepository(Of PerformanceReview, Long).GetAllAsync
        Throw New NotImplementedException()
    End Function

    Public Function GetByIdAsync(id As Long) As Task(Of PerformanceReview) Implements IBaseRepository(Of PerformanceReview, Long).GetByIdAsync
        Throw New NotImplementedException()
    End Function

    Public Function GetAsync(Optional predicate As Expression(Of Func(Of PerformanceReview, Boolean)) = Nothing, Optional includeList As List(Of String) = Nothing) As Task(Of PerformanceReview) Implements IBaseRepository(Of PerformanceReview, Long).GetAsync
        Throw New NotImplementedException()
    End Function

    Public Function GetListAsync(Optional predicate As Expression(Of Func(Of PerformanceReview, Boolean)) = Nothing, Optional includeList As List(Of String) = Nothing) As Task(Of IEnumerable(Of PerformanceReview)) Implements IBaseRepository(Of PerformanceReview, Long).GetListAsync
        Throw New NotImplementedException()
    End Function

    Public Function AddAsync(entity As PerformanceReview) As Task(Of PerformanceReview) Implements IBaseRepository(Of PerformanceReview, Long).AddAsync
        Throw New NotImplementedException()
    End Function

    Public Function UpdateAsync(entity As PerformanceReview) As Task(Of PerformanceReview) Implements IBaseRepository(Of PerformanceReview, Long).UpdateAsync
        Throw New NotImplementedException()
    End Function

    Public Function DeleteAsync(id As Long) As Task(Of Boolean) Implements IBaseRepository(Of PerformanceReview, Long).DeleteAsync
        Throw New NotImplementedException()
    End Function

End Class
