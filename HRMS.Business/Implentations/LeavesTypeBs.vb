Imports AutoMapper
Imports HRMS.Model
Imports HRMS.Model.Models
Imports HRMS.Repository
Imports Infrastructure
Imports Infrastructure.Infrastructure.Utilities.ApiResponses

Public Class LeavesTypeBs : Implements ILeavesTypeBs

    Private ReadOnly _repo As ILeavesTypeRepository
    Private ReadOnly _mapper As IMapper

    Public Sub New(repo As ILeavesTypeRepository, mapper As IMapper)
        _repo = repo
        _mapper = mapper
    End Sub

    Public Async Function GetById(id As Long) As Task(Of ApiResponse(Of LeavesTypeGetDto)) Implements ILeavesTypeBs.GetById
        Dim leaveType = Await _repo.GetByIdAsync(id)
        Dim mappedLeaveType = _mapper.Map(Of LeavesTypeGetDto)(leaveType)

        Return ApiResponse(Of LeavesTypeGetDto).Success(200, mappedLeaveType)
    End Function

    Public Async Function Add(dto As LeavesTypePostDto) As Task(Of ApiResponse(Of LeavesTypeGetDto)) Implements ILeavesTypeBs.Add
        Dim newItem = _mapper.Map(Of LeavesType)(dto)
        Dim added = Await _repo.AddAsync(newItem)

        Dim newDto = _mapper.Map(Of LeavesTypeGetDto)(added)

        Return ApiResponse(Of LeavesTypeGetDto).Success(201, newDto)
    End Function

    Public Async Function GetAll() As Task(Of ApiResponse(Of IEnumerable(Of LeavesTypeGetDto))) Implements ILeavesTypeBs.GetAll
        Dim repoResponse = Await _repo.GetAllAsync()
        Dim dtoList = _mapper.Map(Of IEnumerable(Of LeavesTypeGetDto))(repoResponse)

        Return ApiResponse(Of IEnumerable(Of LeavesTypeGetDto)).Success(200, dtoList)
    End Function

    Public Async Function Delete(id As Long) As Task(Of ApiResponse(Of NoData)) Implements ILeavesTypeBs.Delete
        Dim result = Await _repo.DeleteAsync(id)
        If result Then
            Return ApiResponse(Of NoData).Success(200)
        Else
            Return ApiResponse(Of NoData).Fail(404, "Item not found")
        End If
    End Function

    Public Async Function Update(id As Long, dto As LeavesTypePutDto) As Task(Of ApiResponse(Of LeavesTypeGetDto)) Implements ILeavesTypeBs.Update
        Dim existingItem = Await _repo.GetByIdAsync(id)
        If existingItem Is Nothing Then
            Return ApiResponse(Of LeavesTypeGetDto).Fail(404, "Item not found")
        End If

        _mapper.Map(dto, existingItem)
        Await _repo.UpdateAsync(existingItem)

        Dim updatedDto = _mapper.Map(Of LeavesTypeGetDto)(existingItem)

        Return ApiResponse(Of LeavesTypeGetDto).Success(200, updatedDto)
    End Function


End Class
