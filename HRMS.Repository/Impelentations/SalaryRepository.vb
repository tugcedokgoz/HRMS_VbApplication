Imports Microsoft.Data.SqlClient
Imports Microsoft.EntityFrameworkCore
Imports System.Data

Public Class SalaryRepository
    Implements ISalaryRepository

    Private ReadOnly _context As HRMSContext ' Replace with your actual DbContext

    Public Sub New(context As HRMSContext)
        _context = context
    End Sub

    Public Async Function ManageSalary(salaryId As Long?, baseSalary As Decimal, bonus As Decimal?, deductions As Decimal?, effectiveDate As Date, employeeId As Long, operation As String) As Task(Of String) Implements ISalaryRepository.ManageSalary
        Return Await Task.Run(Function()
                                  Using conn As New SqlConnection(_context.Database.GetDbConnection().ConnectionString)
                                      Using cmd As New SqlCommand("sp_ManageSalary", conn)
                                          cmd.CommandType = CommandType.StoredProcedure
                                          cmd.Parameters.Add(New SqlParameter("@SalaryID", If(salaryId.HasValue, salaryId.Value, DBNull.Value)))
                                          cmd.Parameters.Add(New SqlParameter("@BaseSalary", baseSalary))
                                          cmd.Parameters.Add(New SqlParameter("@Bonus", If(bonus.HasValue, bonus.Value, DBNull.Value)))
                                          cmd.Parameters.Add(New SqlParameter("@Deductions", If(deductions.HasValue, deductions.Value, DBNull.Value)))
                                          cmd.Parameters.Add(New SqlParameter("@EffectiveDate", effectiveDate))
                                          cmd.Parameters.Add(New SqlParameter("@EmployeeID", employeeId))
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
