Imports AutoMapper
Imports HRMS.Model
Imports HRMS.Repository
Imports Infrastructure
Imports Infrastructure.Infrastructure.Utilities.ApiResponses

Public Class LeaveBs
    Implements ILeaveBs

    Private ReadOnly _repo As ILeaveRepository
    Private ReadOnly _mapper As IMapper
    Public Sub New(repo As ILeaveRepository, Optional mapper As IMapper = Nothing)
        _repo = repo
        _mapper = mapper
    End Sub

    Public Async Function ManageLeave(leaveId As Long?, startDate As DateTime, endDate As DateTime, status As String, employeeId As Long, leaveTypeId As Long, operation As String) As Task(Of ApiResponse(Of String)) Implements ILeaveBs.ManageLeave
        Dim result = Await _repo.ManageLeave(leaveId, startDate, endDate, status, employeeId, leaveTypeId, operation)

        If Not String.IsNullOrWhiteSpace(result) Then
            Return ApiResponse(Of String).Success(200, result)
        Else
            Return ApiResponse(Of String).Fail(500, "An error occurred during the leave management operation.")
        End If
    End Function

    Public Async Function SubtractAnnualLeave(id As Long, dto As LeafPutDto) As Task(Of ApiResponse(Of LeafPutDto)) Implements ILeaveBs.SubtractAnnualLeave
        ' Veritabanından kişiyi id'ye göre al
        Dim includeList As List(Of String) = New List(Of String)
        includeList.Add("Employee")
        Dim employee = Await _repo.GetAsync(Function(e) e.Id = id, includeList)

        ' Eğer kişi bulunamazsa hata döndür
        If employee Is Nothing Then
            Return ApiResponse(Of LeafPutDto).Fail(404, "Employee not found")
        End If

        ' Başlangıç tarihi bitiş tarihinden büyükse hata döndür
        If dto.Startdate > dto.Enddate Then
            Return ApiResponse(Of LeafPutDto).Fail(400, "Invalid date range")
        End If

        ' İzin süresini hesapla
        Dim leaveDuration As TimeSpan = dto.Enddate - dto.Startdate

        Dim requestedLeaveDays As Integer = CInt(leaveDuration.TotalDays)
        If employee IsNot Nothing Then
            ' Şimdi, Annualleave'yi kontrol et ve güncelle
            If employee.Employee IsNot Nothing Then
                ' Kullanılan izin süresini annualleave'den düş
                employee.Employee.Annualleave -= requestedLeaveDays

                ' Veritabanındaki ANNUALLEAVE sütununu güncelle
                Await _repo.UpdateAsync(employee)

                ' Include updated Employee information in response
                dto = _mapper.Map(Of LeafPutDto)(employee)

                ' ApiResponse'ı döndür
                Return ApiResponse(Of LeafPutDto).Success(200, dto)
            Else
                ' employee.Employee Nothing ise bu durumu ele alın
            End If
        Else
            ' employee Nothing ise bu durumu ele alın
        End If
    End Function

    Public Async Function GetByEmployeeId(employeeId As Long) As Task(Of ApiResponse(Of List(Of LeafGetDto))) Implements ILeaveBs.GetByEmployeeId
        Dim includeList As New List(Of String)
        includeList.Add("Leavetype")
        includeList.Add("Employee")
        Dim repoResponse = Await _repo.GetListAsync(Function(l) l.Employeeid = employeeId, includeList)

        Dim mapped = _mapper.Map(Of List(Of LeafGetDto))(repoResponse)

        Return ApiResponse(Of List(Of LeafGetDto)).Success(200, mapped)
    End Function

    Public Async Function GetAll() As Task(Of ApiResponse(Of List(Of LeafGetDto))) Implements ILeaveBs.GetAll
        Dim includeList As New List(Of String)
        includeList.Add("Leavetype")

        includeList.Add("Employee")

        Dim repoResponse = Await _repo.GetListAsync(includeList:=includeList)
        Dim mapped = _mapper.Map(Of List(Of LeafGetDto))(repoResponse)

        Return ApiResponse(Of List(Of LeafGetDto)).Success(200, mapped)
    End Function

    Public Async Function Delete(id As Long) As Task(Of ApiResponse(Of Infrastructure.NoData)) Implements ILeaveBs.Delete
        Dim result = Await _repo.DeleteAsync(id)
        If result Then
            Return ApiResponse(Of NoData).Success(200)
        Else
            Return ApiResponse(Of NoData).Fail(404, "Item not found")
        End If
    End Function
End Class
