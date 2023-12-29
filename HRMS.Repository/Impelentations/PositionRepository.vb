Imports System.Data
Imports HRMS.Model.Models
Imports Infrastructure
Imports Microsoft.Data.SqlClient
Imports Microsoft.EntityFrameworkCore

Public Class PositionRepository : Inherits BaseRepository(Of Position, Long, HRMSContext) : Implements IPositionRepository

    Public Sub New(context As HRMSContext)
        MyBase.New(context)
        _context = context
    End Sub

    Private ReadOnly _context As HRMSContext ' Replace with your actual DbContext

    Public Async Function ManagePosition(positionId As Long?, positionTitle As String, description As String, salaryGrade As Long, operation As String) As Task(Of String) Implements IPositionRepository.ManagePosition
        Return Await Task.Run(Function()
                                  Using conn As New SqlConnection(_context.Database.GetDbConnection().ConnectionString)
                                      Using cmd As New SqlCommand("sp_ManagePosition", conn)
                                          cmd.CommandType = CommandType.StoredProcedure
                                          cmd.Parameters.Add(New SqlParameter("@PositionID", If(positionId.HasValue, positionId.Value, DBNull.Value)))
                                          cmd.Parameters.Add(New SqlParameter("@PositionTitle", positionTitle))
                                          cmd.Parameters.Add(New SqlParameter("@Description", If(String.IsNullOrWhiteSpace(description), DBNull.Value, description)))
                                          cmd.Parameters.Add(New SqlParameter("@SalaryGrade", salaryGrade))
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
