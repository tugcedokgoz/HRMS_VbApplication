Imports HRMS.Model
Imports Infrastructure
Imports Infrastructure.Infrastructure.Utilities.ApiResponses

Public Interface ICandidateBs
    Function GetById(id As Long) As Task(Of ApiResponse(Of CandidateGetDto))
    Function Add(dto As CandidatePostDto) As Task(Of ApiResponse(Of CandidateGetDto))
    Function GetAll() As Task(Of ApiResponse(Of IEnumerable(Of CandidateGetDto)))
    Function Delete(id As Long) As Task(Of ApiResponse(Of NoData))
    Function Update(id As Long, dto As CandidatePutDto) As Task(Of ApiResponse(Of CandidateGetDto))
    Function AddNewCandidateProcedure(firstName As String, lastName As String, applicationDate As Date, resumeLink As String, appliedPositionId As Long) As Task(Of String)
End Interface
