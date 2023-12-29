Imports System.Data
Imports HRMS.Model.Models
Imports Infrastructure
Imports Microsoft.Data.SqlClient
Imports Microsoft.EntityFrameworkCore

Public Class InterviewRepository : Inherits BaseRepository(Of Interview, Long, HRMSContext) : Implements IInterviewRepository
    Public Sub New(context As HRMSContext)
        MyBase.New(context)
        _context = context
    End Sub

    Private ReadOnly _context As HRMSContext

    Public Async Function ScheduleInterview(interviewDate As Date, candidateId As Long, interviewerId As Long, Optional interviewNotes As String = Nothing, Optional interviewOutcome As String = Nothing) As Task(Of String) Implements IInterviewRepository.ScheduleInterview
        Return Await Task.Run(Function()
                                  Using conn As New SqlConnection(_context.Database.GetDbConnection().ConnectionString)
                                      Using cmd As New SqlCommand("dbo.sp_ScheduleInterview", conn)
                                          Dim interv
                                          If interviewerId = 0 Then
                                              interv = DBNull.Value
                                          Else
                                              interv = interviewerId
                                          End If
                                          cmd.CommandType = CommandType.StoredProcedure
                                          cmd.Parameters.Add(New SqlParameter("@InterviewDate", interviewDate))
                                          cmd.Parameters.Add(New SqlParameter("@CandidateID", candidateId))
                                          cmd.Parameters.Add(New SqlParameter("@InterviewerID", interv))
                                          cmd.Parameters.Add(New SqlParameter("@InterviewNotes", If(interviewNotes, DBNull.Value)))
                                          cmd.Parameters.Add(New SqlParameter("@InterviewOutcome", If(interviewOutcome, DBNull.Value)))

                                          conn.Open()
                                          Dim result As Object = cmd.ExecuteScalar()
                                          conn.Close()

                                          Return If(result IsNot Nothing, result.ToString(), "Error: No result returned from stored procedure.")
                                      End Using
                                  End Using
                              End Function)
    End Function


    Public Async Function ManageInterview(interviewId As Long, interviewDate As Date, candidateId As Long, interviewerId As Long, operation As String, Optional interviewNotes As String = Nothing, Optional interviewOutcome As String = Nothing) As Task(Of String) Implements IInterviewRepository.ManageInterview
        Return Await Task.Run(Function()
                                  Using conn As New SqlConnection(_context.Database.GetDbConnection().ConnectionString)
                                      Using cmd As New SqlCommand("dbo.sp_ManageInterview", conn)
                                          cmd.CommandType = CommandType.StoredProcedure

                                          ' Setting parameter values
                                          ' Assuming a new interview is being scheduled, InterviewId is set to 0 or an appropriate default value
                                          cmd.Parameters.Add(New SqlParameter("@InterviewId", interviewId))
                                          cmd.Parameters.Add(New SqlParameter("@InterviewDate", interviewDate))
                                          cmd.Parameters.Add(New SqlParameter("@CandidateID", candidateId))

                                          ' If interviewerId is 0, set it to NULL
                                          Dim interviewerParameter As New SqlParameter("@InterviewerID", If(interviewerId = 0, DBNull.Value, interviewerId))
                                          cmd.Parameters.Add(interviewerParameter)

                                          ' If interviewNotes is Nothing, set it to NULL
                                          Dim notesParameter As New SqlParameter("@InterviewNotes", If(interviewNotes, DBNull.Value))
                                          cmd.Parameters.Add(notesParameter)

                                          ' If interviewOutcome is Nothing, set it to NULL
                                          Dim outcomeParameter As New SqlParameter("@InterviewOutcome", If(interviewOutcome, DBNull.Value))
                                          cmd.Parameters.Add(outcomeParameter)

                                          ' Set the operation to 'ADD' for scheduling a new interview
                                          cmd.Parameters.Add(New SqlParameter("@Operation", operation))

                                          conn.Open()
                                          Dim result As Object = cmd.ExecuteScalar()
                                          conn.Close()

                                          Return If(result IsNot Nothing, result.ToString(), "Error: No result returned from stored procedure.")
                                      End Using
                                  End Using
                              End Function)
    End Function

End Class
