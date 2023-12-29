Imports HRMS.Model
Imports Infrastructure
Imports Infrastructure.Infrastructure.Utilities.ApiResponses

Public Interface IPositionBs
    Function GetById(id As Long) As Task(Of ApiResponse(Of PositionGetDto))
    Function GetAll() As Task(Of ApiResponse(Of IEnumerable(Of PositionGetDto)))
    Function ManagePosition(positionId As Long?, positionTitle As String, description As String, salaryGrade As Long, operation As String) As Task(Of ApiResponse(Of String))

End Interface
