Imports AutoMapper
Imports HRMS.Model
Imports HRMS.Model.Models
Imports HRMS.Repository
Imports Infrastructure
Imports Infrastructure.Infrastructure.Utilities.ApiResponses

Public Class UserBs : Implements IUserBs

    Private ReadOnly _repo As IUserRepository
    Private ReadOnly _mapper As IMapper

    Public Sub New(repo As IUserRepository, mapper As IMapper)
        _repo = repo
        _mapper = mapper
    End Sub

    Public Async Function GetById(id As Long) As Task(Of ApiResponse(Of UserGetDto)) Implements IUserBs.GetById
        Dim user = Await _repo.GetByIdAsync(id)
        Dim mappedUser = _mapper.Map(Of UserGetDto)(user)

        Return ApiResponse(Of UserGetDto).Success(200, mappedUser)
    End Function

    Public Async Function Add(dto As UserPostDto) As Task(Of ApiResponse(Of UserGetDto)) Implements IUserBs.Add
        Dim newItem = _mapper.Map(Of User)(dto)
        Dim added = Await _repo.AddAsync(newItem)

        Dim newDto = _mapper.Map(Of UserGetDto)(added)

        Return ApiResponse(Of UserGetDto).Success(201, newDto)
    End Function

    Public Async Function GetAll() As Task(Of ApiResponse(Of IEnumerable(Of UserGetDto))) Implements IUserBs.GetAll
        Dim repoResponse = Await _repo.GetAllAsync()
        Dim dtoList = _mapper.Map(Of IEnumerable(Of UserGetDto))(repoResponse)

        Return ApiResponse(Of IEnumerable(Of UserGetDto)).Success(200, dtoList)
    End Function

    Public Async Function Delete(id As Long) As Task(Of ApiResponse(Of NoData)) Implements IUserBs.Delete
        Dim result = Await _repo.DeleteAsync(id)
        If result Then
            Return ApiResponse(Of NoData).Success(200)
        Else
            Return ApiResponse(Of NoData).Fail(404, "Item not found")
        End If
    End Function

    Public Async Function Update(id As Long, dto As UserPutDto) As Task(Of ApiResponse(Of UserGetDto)) Implements IUserBs.Update
        Dim existingItem = Await _repo.GetByIdAsync(id)
        If existingItem Is Nothing Then
            Return ApiResponse(Of UserGetDto).Fail(404, "Item not found")
        End If

        _mapper.Map(dto, existingItem)
        Await _repo.UpdateAsync(existingItem)

        Dim updatedDto = _mapper.Map(Of UserGetDto)(existingItem)

        Return ApiResponse(Of UserGetDto).Success(200, updatedDto)
    End Function
End Class
