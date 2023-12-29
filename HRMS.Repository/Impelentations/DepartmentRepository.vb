Imports System.Data
Imports HRMS.Model.Models
Imports Infrastructure
Imports Microsoft.Data.SqlClient
Imports Microsoft.EntityFrameworkCore

Public Class DepartmentRepository : Inherits BaseRepository(Of Department, Long, HRMSContext) : Implements IDepartmentRepository
    Public Sub New(context As HRMSContext)
        MyBase.New(context)
        _context = context
    End Sub

    Private ReadOnly _context As HRMSContext

    Public Async Function ManageDepartment(departmentId As Long?, departmentName As String, managerId As Long?, operation As String) As Task(Of String) Implements IDepartmentRepository.ManageDepartment
        Return Await Task.Run(Function()
                                  Using conn As New SqlConnection(_context.Database.GetDbConnection().ConnectionString)
                                      Dim manager
                                      If managerId.HasValue Then
                                          If managerId.Value = -1 Then
                                              manager = DBNull.Value
                                          Else
                                              manager = managerId.Value
                                          End If
                                      Else
                                          manager = DBNull.Value
                                      End If
                                      Using cmd As New SqlCommand("dbo.sp_ManageDepartment", conn)
                                          cmd.CommandType = CommandType.StoredProcedure
                                          cmd.Parameters.Add(New SqlParameter("@DepartmentID", If(departmentId.HasValue, departmentId.Value, DBNull.Value)))
                                          cmd.Parameters.Add(New SqlParameter("@DepartmentName", departmentName))
                                          cmd.Parameters.Add(New SqlParameter("@ManagerID", manager))
                                          cmd.Parameters.Add(New SqlParameter("@Operation", operation))

                                          conn.Open()
                                          Dim result As Object = cmd.ExecuteScalar()
                                          conn.Close()

                                          Return If(result IsNot Nothing, result.ToString(), "Error: No result returned from stored procedure.")
                                      End Using
                                  End Using
                              End Function)
    End Function

    Public Async Function ListPositionsByDepartmentId(departmentId As Long) As Task(Of List(Of Position)) Implements IDepartmentRepository.ListPositionsByDepartmentId
        Dim positions As New List(Of Position)()

        Using conn As New SqlConnection(_context.Database.GetDbConnection().ConnectionString)
            Using cmd As New SqlCommand("[dbo].[ListPositionsByDepartmentId]", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@DepartmentID", departmentId)

                Await conn.OpenAsync()
                Using reader As SqlDataReader = Await cmd.ExecuteReaderAsync()
                    While Await reader.ReadAsync()
                        Dim position As New Position() With {
                            .Id = reader.GetInt64(reader.GetOrdinal("ID")),
                            .Positiontitle = reader.GetString(reader.GetOrdinal("POSITIONTITLE"))
                        }
                        positions.Add(position)
                    End While
                End Using
            End Using
        End Using

        Return positions
    End Function
End Class
