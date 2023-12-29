Imports HRMS.Model
Imports Infrastructure
Imports Infrastructure.Infrastructure.Utilities.ApiResponses

Public Interface ILeavesTypeBs
    Function GetById(id As Long) As Task(Of ApiResponse(Of LeavesTypeGetDto))
    Function Add(dto As LeavesTypePostDto) As Task(Of ApiResponse(Of LeavesTypeGetDto))
    Function GetAll() As Task(Of ApiResponse(Of IEnumerable(Of LeavesTypeGetDto)))
    Function Delete(id As Long) As Task(Of ApiResponse(Of NoData))
    Function Update(id As Long, dto As LeavesTypePutDto) As Task(Of ApiResponse(Of LeavesTypeGetDto))

End Interface
