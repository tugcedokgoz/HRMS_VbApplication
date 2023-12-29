Imports HRMS.Model
Imports Infrastructure
Imports Infrastructure.Infrastructure.Utilities.ApiResponses

Public Interface IEmployeeBs
    Function GetById(id As Long) As Task(Of ApiResponse(Of EmployeeGetDto))
    Function Add(dto As EmployeePostDto) As Task(Of ApiResponse(Of EmployeeGetDto))
    Function GetAll() As Task(Of ApiResponse(Of IEnumerable(Of EmployeeGetDto)))
    Function Delete(id As Long) As Task(Of ApiResponse(Of NoData))
    Function Update(id As Long, dto As EmployeePutDto) As Task(Of ApiResponse(Of EmployeeGetDto))
    'Function SearchByBirthdateAndLastname(birthdate As Date, lastname As String) As Task(Of ApiResponse(Of IEnumerable(Of EmployeeGetDto)))
    Function SearchEmployeesByLastNameAndBirthdate(lastname As String, birthdate As Date) As Task(Of ApiResponse(Of IEnumerable(Of EmployeeGetDto)))
    Function GetEmployeeReport(departmentId As Long?, startDate As Date?, endDate As Date?) As Task(Of ApiResponse(Of IEnumerable(Of EmployeeGetDto)))
    Function GetAnnualLeave(id As Long, dto As EmployeePutDto) As Task(Of ApiResponse(Of EmployeePutDto))

    Function GetByPosition(position As Long) As Task(Of ApiResponse(Of IEnumerable(Of EmployeeGetDto)))
End Interface
