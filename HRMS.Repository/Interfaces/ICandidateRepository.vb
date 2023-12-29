Imports System.Numerics
Imports HRMS.Model.Models
Imports Infrastructure

Public Interface ICandidateRepository : Inherits IBaseRepository(Of Candidate, Long)
    Function AddNewCandidateProcedure(firstName As String, lastName As String, applicationDate As Date, resumeLink As String, appliedPositionId As Long) As Task(Of String)

End Interface
