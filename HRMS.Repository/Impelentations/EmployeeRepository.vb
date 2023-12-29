Imports System.Data
Imports HRMS.Model.Models
Imports Infrastructure
Imports Microsoft.Data.SqlClient
Imports Microsoft.EntityFrameworkCore

Public Class EmployeeRepository : Inherits BaseRepository(Of Employee, Long, HRMSContext) : Implements IEmployeeRepository
    Public Property _context As HRMSContext
    Public Sub New(context As HRMSContext)
        MyBase.New(context)
        _context = context
    End Sub


    'Public Async Function SearchByBirthdateAndLastnameProcedure(birthdate As Date, lastname As String) As Task(Of IEnumerable(Of Employee)) Implements IEmployeeRepository.SearchByBirthdateAndLastnameProcedure
    '    ' Doğru tarih biçimi ile SqlParameter oluştur
    '    Dim bdParam = New SqlParameter("@birthdate", SqlDbType.Date)
    '    bdParam.Value = birthdate.Date.ToString("yyyy-MM-dd")

    '    ' SqlParameter kullanımını güncelle
    '    Dim lnParam = New SqlParameter("@lastname", SqlDbType.NVarChar)
    '    lnParam.Value = lastname

    '    ' Stored procedure çağrısını düzelt
    '    Return Await _context.Employees.FromSqlRaw("EXEC dbo.GetEmployeeByLastNameAndBirthDate @birthdate, @lastname", bdParam, lnParam).ToListAsync()

    'End Function


    Public Async Function GetEmployeeReport(departmentId As Long?, startDate As Date?, endDate As Date?) As Task(Of IEnumerable(Of Employee)) Implements IEmployeeRepository.GetEmployeeReport
        Return Await Task.Run(Function()
                                  Using conn As New SqlConnection(_context.Database.GetDbConnection().ConnectionString)
                                      Using cmd As New SqlCommand("sp_EmployeeReport", conn)
                                          cmd.CommandType = CommandType.StoredProcedure
                                          cmd.Parameters.Add(New SqlParameter("@DepartmentID", If(departmentId.HasValue, departmentId.Value, DBNull.Value)))
                                          cmd.Parameters.Add(New SqlParameter("@StartDate", If(startDate.HasValue, startDate.Value, DBNull.Value)))
                                          cmd.Parameters.Add(New SqlParameter("@EndDate", If(endDate.HasValue, endDate.Value, DBNull.Value)))

                                          conn.Open()
                                          Dim reader As SqlDataReader = cmd.ExecuteReader()
                                          Dim employees As New List(Of Employee)()

                                          While reader.Read()
                                              ' Assuming Employee is a class that matches your database schema
                                              Dim employee As New Employee()
                                              ' Map reader fields to employee properties here
                                              employees.Add(employee)
                                          End While

                                          conn.Close()
                                          Return employees
                                      End Using
                                  End Using
                              End Function)
    End Function

    Public Async Function SearchEmployeesByLastNameAndBirthdate(lastname As String, birthdate As Date) As Task(Of IEnumerable(Of Employee)) Implements IEmployeeRepository.SearchEmployeesByLastNameAndBirthdate
        Return Await Task.Run(Function()
                                  Using conn As New SqlConnection(_context.Database.GetDbConnection().ConnectionString)
                                      Using cmd As New SqlCommand("SearchEmployeesByLastNameAndBirthdate", conn)
                                          cmd.CommandType = CommandType.StoredProcedure
                                          cmd.Parameters.Add(New SqlParameter("@lastname", lastname))
                                          cmd.Parameters.Add(New SqlParameter("@birthdate", birthdate))

                                          conn.Open()
                                          Dim reader As SqlDataReader = cmd.ExecuteReader()
                                          Dim employees As New List(Of Employee)()

                                          While reader.Read()
                                              Dim employee As New Employee()
                                              ' Burada, SqlDataReader'dan alınan verileri Employee nesnesinin özelliklerine atayın.
                                              employee.Id = If(IsDBNull(reader("ID")), Nothing, Convert.ToInt64(reader("ID")))
                                              employee.Firstname = If(IsDBNull(reader("FIRSTNAME")), String.Empty, reader("FIRSTNAME").ToString())
                                              employee.Lastname = If(IsDBNull(reader("LASTNAME")), String.Empty, reader("LASTNAME").ToString())
                                              employee.Birthdate = If(IsDBNull(reader("BIRTHDATE")), Nothing, Convert.ToDateTime(reader("BIRTHDATE")))
                                              employee.Gender = If(IsDBNull(reader("GENDER")), String.Empty, reader("GENDER").ToString())
                                              employee.Hiredate = If(IsDBNull(reader("HIREDATE")), Nothing, Convert.ToDateTime(reader("HIREDATE")))
                                              employee.Email = If(IsDBNull(reader("EMAIL")), String.Empty, reader("EMAIL").ToString())
                                              employee.Phonenumber = If(IsDBNull(reader("PHONENUMBER")), String.Empty, reader("PHONENUMBER").ToString())
                                              employee.Positionid = If(IsDBNull(reader("POSITIONID")), Nothing, Convert.ToInt64(reader("POSITIONID")))
                                              employee.Departmanid = If(IsDBNull(reader("DEPARTMANID")), Nothing, Convert.ToInt64(reader("DEPARTMANID")))
                                              employee.Managerid = If(IsDBNull(reader("MANAGERID")), Nothing, Convert.ToInt64(reader("MANAGERID")))
                                              employee.Annualleave = If(IsDBNull(reader("ANNUALLEAVE")), Nothing, Convert.ToInt32(reader("ANNUALLEAVE")))

                                              employees.Add(employee)
                                          End While

                                          conn.Close()
                                          Return employees
                                      End Using
                                  End Using
                              End Function)
    End Function
End Class