Imports AutoMapper
Imports HRMS.Model
Imports HRMS.Model.Models
Imports HRMS.Repository
Imports Infrastructure
Imports Infrastructure.Infrastructure.Utilities.ApiResponses

Public Class DepartmentBs : Implements IDepartmentBs

    Private ReadOnly _repo As IDepartmentRepository
    Private ReadOnly _mapper As IMapper

    Public Sub New(repo As IDepartmentRepository, mapper As IMapper)
        _repo = repo
        _mapper = mapper
    End Sub

    Public Async Function GetById(id As Long) As Task(Of ApiResponse(Of DepartmentGetDto)) Implements IDepartmentBs.GetById
        Dim department = Await _repo.GetByIdAsync(id)
        Dim mappedDepartment = _mapper.Map(Of DepartmentGetDto)(department)

        Return ApiResponse(Of DepartmentGetDto).Success(200, mappedDepartment)
    End Function

    Public Async Function GetAll() As Task(Of ApiResponse(Of IEnumerable(Of DepartmentGetDto))) Implements IDepartmentBs.GetAll
        Dim includeList As New List(Of String)

        includeList.Add("Manager")
        Dim repoResponse = Await _repo.GetListAsync(includeList:=includeList)
        Dim dtoList = _mapper.Map(Of IEnumerable(Of DepartmentGetDto))(repoResponse)

        Return ApiResponse(Of IEnumerable(Of DepartmentGetDto)).Success(200, dtoList)
    End Function



    Public Async Function ManageDepartment(departmentId As Long?, departmentName As String, managerId As Long?, operation As String) As Task(Of ApiResponse(Of String)) Implements IDepartmentBs.ManageDepartment
        Dim result = Await _repo.ManageDepartment(departmentId, departmentName, managerId, operation)

        If Not String.IsNullOrWhiteSpace(result) Then
            Return ApiResponse(Of String).Success(200, result)
        Else
            Return ApiResponse(Of String).Fail(500, "An error occurred during the department management operation.")
        End If
    End Function

    Public Async Function GetByFilter(departmentName As String, Optional departmentManager As Long = Nothing) As Task(Of ApiResponse(Of IEnumerable(Of DepartmentGetDto))) Implements IDepartmentBs.GetByFilter
        Dim includeList As New List(Of String)
        Dim repoResponse As IEnumerable(Of Department)
        includeList.Add("Manager")
        If departmentManager = 0 Then
            repoResponse = Await _repo.GetListAsync(includeList:=includeList, predicate:=Function(p) p.Departmentname.Contains(departmentName) Or p.Managerid = departmentManager)
        Else
            repoResponse = Await _repo.GetListAsync(includeList:=includeList, predicate:=Function(p) p.Departmentname.Contains(departmentName) And p.Managerid = departmentManager)
        End If
        Dim dtoList = _mapper.Map(Of IEnumerable(Of DepartmentGetDto))(repoResponse)

        Return ApiResponse(Of IEnumerable(Of DepartmentGetDto)).Success(200, dtoList)
    End Function

    Public Async Function ListPositionsByDepartmentId(departmentId As Long) As Task(Of ApiResponse(Of IEnumerable(Of PositionGetDto))) Implements IDepartmentBs.ListPositionsByDepartmentId
        Try
            Dim positions = Await _repo.ListPositionsByDepartmentId(departmentId)

            ' AutoMapper kullanarak Position nesnelerini PositionGetDto'ya dönüştür
            Dim positionDtos = _mapper.Map(Of IEnumerable(Of PositionGetDto))(positions)

            Return ApiResponse(Of IEnumerable(Of PositionGetDto)).Success(200, positionDtos)
        Catch ex As Exception
            ' Hata durumunda uygun bir ApiResponse döndür
            Return ApiResponse(Of IEnumerable(Of PositionGetDto)).Fail(500, ex.Message)
        End Try
    End Function
End Class
