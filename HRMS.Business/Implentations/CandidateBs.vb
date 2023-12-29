Imports AutoMapper
Imports HRMS.Model
Imports HRMS.Model.Models
Imports HRMS.Repository
Imports Infrastructure
Imports Infrastructure.Infrastructure.Utilities.ApiResponses

Public Class CandidateBs : Implements ICandidateBs

    Private ReadOnly _repo As ICandidateRepository
    Private ReadOnly _mapper As IMapper

    Public Sub New(repo As ICandidateRepository, mapper As IMapper)
        _repo = repo
        _mapper = mapper
    End Sub

    Public Async Function GetById(id As Long) As Task(Of ApiResponse(Of CandidateGetDto)) Implements ICandidateBs.GetById
        Dim candidate = Await _repo.GetByIdAsync(id)
        Dim mappedCandidate = _mapper.Map(Of CandidateGetDto)(candidate)

        Return ApiResponse(Of CandidateGetDto).Success(200, mappedCandidate)
    End Function

    Public Async Function Add(dto As CandidatePostDto) As Task(Of ApiResponse(Of CandidateGetDto)) Implements ICandidateBs.Add
        Dim newItem = _mapper.Map(Of Candidate)(dto)
        Dim added = Await _repo.AddAsync(newItem)

        Dim newDto = _mapper.Map(Of CandidateGetDto)(added)

        Return ApiResponse(Of CandidateGetDto).Success(201, newDto)
    End Function

    Public Async Function GetAll() As Task(Of ApiResponse(Of IEnumerable(Of CandidateGetDto))) Implements ICandidateBs.GetAll
        Dim includeList As New List(Of String)
        includeList.Add("Appliedposition")
        Dim repoResponse = Await _repo.GetListAsync(includeList:=includeList)
        Dim dtoList = _mapper.Map(Of IEnumerable(Of CandidateGetDto))(repoResponse)

        Return ApiResponse(Of IEnumerable(Of CandidateGetDto)).Success(200, dtoList)
    End Function

    Public Async Function Delete(id As Long) As Task(Of ApiResponse(Of NoData)) Implements ICandidateBs.Delete
        Dim result = Await _repo.DeleteAsync(id)
        If result Then
            Return ApiResponse(Of NoData).Success(200)
        Else
            Return ApiResponse(Of NoData).Fail(404, "Item not found")
        End If
    End Function

    Public Async Function Update(id As Long, dto As CandidatePutDto) As Task(Of ApiResponse(Of CandidateGetDto)) Implements ICandidateBs.Update
        Dim existingItem = Await _repo.GetByIdAsync(id)
        If existingItem Is Nothing Then
            Return ApiResponse(Of CandidateGetDto).Fail(404, "Item not found")
        End If

        _mapper.Map(dto, existingItem)
        Await _repo.UpdateAsync(existingItem)

        Dim updatedDto = _mapper.Map(Of CandidateGetDto)(existingItem)

        Return ApiResponse(Of CandidateGetDto).Success(200, updatedDto)
    End Function

    Public Async Function AddNewCandidateProcedure(firstName As String, lastName As String, applicationDate As Date, resumeLink As String, appliedPositionId As Long) As Task(Of String) Implements ICandidateBs.AddNewCandidateProcedure
        Dim message = Await _repo.AddNewCandidateProcedure(firstName, lastName, applicationDate, resumeLink, appliedPositionId)

        Return message
    End Function
End Class
