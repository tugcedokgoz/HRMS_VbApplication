Imports System.Data
Imports HRMS.Model.Models
Imports Infrastructure
Imports Microsoft.Data.SqlClient
Imports Microsoft.EntityFrameworkCore
Public Class CandidateRepository : Inherits BaseRepository(Of Candidate, Long, HRMSContext) : Implements ICandidateRepository

    Private ReadOnly _context As HRMSContext
    Public Sub New(context As HRMSContext)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function AddNewCandidateProcedure(firstName As String, lastName As String, applicationDate As Date, resumeLink As String, appliedPositionId As Long) As Task(Of String) Implements ICandidateRepository.AddNewCandidateProcedure
        Return Task.Run(Function()
                            Using conn As New SqlConnection(_context.Database.GetDbConnection().ConnectionString)
                                Using cmd As New SqlCommand("dbo.sp_AddNewCandidate", conn)
                                    cmd.CommandType = CommandType.StoredProcedure
                                    cmd.Parameters.Add(New SqlParameter("@FirstName", firstName))
                                    cmd.Parameters.Add(New SqlParameter("@LastName", lastName))
                                    cmd.Parameters.Add(New SqlParameter("@ApplicationDate", applicationDate))
                                    cmd.Parameters.Add(New SqlParameter("@ResumeLink", resumeLink))
                                    cmd.Parameters.Add(New SqlParameter("@AppliedPositionID", appliedPositionId))

                                    conn.Open()
                                    Dim result As Object = cmd.ExecuteScalar()
                                    conn.Close()

                                    If result IsNot Nothing Then
                                        Return result.ToString()
                                    Else
                                        Return "Error: No result returned from stored procedure."
                                    End If
                                End Using
                            End Using
                        End Function)
    End Function
End Class
