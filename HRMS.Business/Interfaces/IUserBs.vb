Imports HRMS.Model
Imports Infrastructure
Imports Infrastructure.Infrastructure.Utilities.ApiResponses

Public Interface IUserBs
    Function GetById(id As Long) As Task(Of ApiResponse(Of UserGetDto))
    Function Add(dto As UserPostDto) As Task(Of ApiResponse(Of UserGetDto))
    Function GetAll() As Task(Of ApiResponse(Of IEnumerable(Of UserGetDto)))
    Function Delete(id As Long) As Task(Of ApiResponse(Of NoData))
    Function Update(id As Long, dto As UserPutDto) As Task(Of ApiResponse(Of UserGetDto))
End Interface
