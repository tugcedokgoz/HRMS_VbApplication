Imports HRMS.Model
Imports Infrastructure
Imports Infrastructure.Infrastructure.Utilities.ApiResponses

Public Interface IImageBs
    Function GetById(id As Long) As Task(Of ApiResponse(Of ImageGetDto))
    Function Add(dto As ImageUploadDto) As Task(Of ApiResponse(Of ImageGetDto))
    Function GetAll() As Task(Of ApiResponse(Of IEnumerable(Of ImageGetDto)))
    Function Delete(id As Long) As Task(Of ApiResponse(Of NoData))
    Function Update(id As Long, dto As ImagePutDto) As Task(Of ApiResponse(Of ImageGetDto))
End Interface
