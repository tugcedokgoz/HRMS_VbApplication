Imports HRMS.Model.Models
Imports Infrastructure

Public Interface IPositionRepository : Inherits IBaseRepository(Of Position, Long)
    Function ManagePosition(positionId As Long?, positionTitle As String, description As String, salaryGrade As Long, operation As String) As Task(Of String)

End Interface
