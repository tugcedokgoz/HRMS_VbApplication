Imports AutoMapper
Imports HRMS.Model
Imports HRMS.Model.Models
Imports HRMS.Repository
Imports Infrastructure
Imports Infrastructure.Infrastructure.Utilities.ApiResponses

Public Class EmployeeBs : Implements IEmployeeBs

    Private ReadOnly _repo As IEmployeeRepository
    Private ReadOnly _mapper As IMapper

    Public Sub New(repo As IEmployeeRepository, mapper As IMapper)
        _repo = repo
        _mapper = mapper
    End Sub

    Public Async Function GetById(id As Long) As Task(Of ApiResponse(Of EmployeeGetDto)) Implements IEmployeeBs.GetById
        Dim employee = Await _repo.GetByIdAsync(id)
        Dim mappedEmployee = _mapper.Map(Of EmployeeGetDto)(employee)

        Return ApiResponse(Of EmployeeGetDto).Success(200, mappedEmployee)
    End Function

    Public Async Function Add(dto As EmployeePostDto) As Task(Of ApiResponse(Of EmployeeGetDto)) Implements IEmployeeBs.Add
        Dim newItem = _mapper.Map(Of Employee)(dto)
        Dim added = Await _repo.AddAsync(newItem)

        Dim newDto = _mapper.Map(Of EmployeeGetDto)(added)

        Return ApiResponse(Of EmployeeGetDto).Success(201, newDto)
    End Function

    Public Async Function GetAll() As Task(Of ApiResponse(Of IEnumerable(Of EmployeeGetDto))) Implements IEmployeeBs.GetAll
        Dim repoResponse = Await _repo.GetAllAsync()
        Dim dtoList = _mapper.Map(Of IEnumerable(Of EmployeeGetDto))(repoResponse)

        Return ApiResponse(Of IEnumerable(Of EmployeeGetDto)).Success(200, dtoList)
    End Function

    Public Async Function Delete(id As Long) As Task(Of ApiResponse(Of NoData)) Implements IEmployeeBs.Delete
        Dim result = Await _repo.DeleteAsync(id)
        If result Then
            Return ApiResponse(Of NoData).Success(200)
        Else
            Return ApiResponse(Of NoData).Fail(404, "Item not found")
        End If
    End Function

    Public Async Function Update(id As Long, dto As EmployeePutDto) As Task(Of ApiResponse(Of EmployeeGetDto)) Implements IEmployeeBs.Update
        Dim existingItem = Await _repo.GetByIdAsync(id)
        If existingItem Is Nothing Then
            Return ApiResponse(Of EmployeeGetDto).Fail(404, "Item not found")
        End If

        _mapper.Map(dto, existingItem)
        Await _repo.UpdateAsync(existingItem)

        Dim updatedDto = _mapper.Map(Of EmployeeGetDto)(existingItem)

        Return ApiResponse(Of EmployeeGetDto).Success(200, updatedDto)
    End Function


    'Public Async Function SearchByBirthdateAndLastname(birthdate As Date, lastname As String) As Task(Of ApiResponse(Of IEnumerable(Of EmployeeGetDto))) Implements IEmployeeBs.SearchByBirthdateAndLastname
    '    If birthdate = Nothing OrElse String.IsNullOrWhiteSpace(lastname) Then
    '        Return ApiResponse(Of IEnumerable(Of EmployeeGetDto)).Fail(400, "Both birthdate and lastname must be provided")
    '    End If

    '    Dim filteredEmployees = Await _repo.SearchByBirthdateAndLastnameProcedure(birthdate, lastname)

    '    If Not filteredEmployees.Any() Then
    '        Return ApiResponse(Of IEnumerable(Of EmployeeGetDto)).Fail(404, "No matching employees found")
    '    End If

    '    Dim dtoList = _mapper.Map(Of IEnumerable(Of EmployeeGetDto))(filteredEmployees)
    '    Return ApiResponse(Of IEnumerable(Of EmployeeGetDto)).Success(200, dtoList)
    'End Function

    Public Async Function GetEmployeeReport(departmentId As Long?, startDate As Date?, endDate As Date?) As Task(Of ApiResponse(Of IEnumerable(Of EmployeeGetDto))) Implements IEmployeeBs.GetEmployeeReport
        Dim employees = Await _repo.GetEmployeeReport(departmentId, startDate, endDate)
        Dim employeeDtos = _mapper.Map(Of IEnumerable(Of EmployeeGetDto))(employees)

        Return ApiResponse(Of IEnumerable(Of EmployeeGetDto)).Success(200, employeeDtos)
    End Function

    Public Async Function GetAnnualLeave(id As Long, dto As EmployeePutDto) As Task(Of ApiResponse(Of EmployeePutDto)) Implements IEmployeeBs.GetAnnualLeave
        ' Veritabanından kişiyi id'ye göre al
        Dim employee = Await _repo.GetByIdAsync(id)

        ' Eğer kişi bulunamazsa hata döndür
        If employee Is Nothing Then
            Return ApiResponse(Of EmployeePutDto).Fail(404, "Employee not found")
        End If

        ' Hiredate değeri null ise hata döndür
        If employee.Hiredate.Equals(Nothing) Then
            Return ApiResponse(Of EmployeePutDto).Fail(400, "Hiredate is null")
        End If

        ' Daha önce hesaplanmış yıllık izin var mı kontrol et
        If Not employee.Annualleave.HasValue Then
            ' Hiredate tarihinden bugünkü tarih çıkarılarak çalışma süresi hesaplanır
            Dim workDuration As TimeSpan = DateTime.Now - employee.Hiredate

            ' Elde edilen toplam gün sayısını yıl olarak hesaplamak için 365.25 kullanılır (artık yılları da hesaba katmak için)
            Dim yearsWorked As Integer = CInt(Math.Floor(workDuration.TotalDays / 365.25))

            Dim total As Integer = employee.Annualleave

            ' Yıllık izni hesapla
            If yearsWorked >= 1 AndAlso yearsWorked <= 5 Then
                total += 14
            ElseIf yearsWorked > 5 AndAlso yearsWorked <= 15 Then
                total += 24
            ElseIf yearsWorked > 15 Then
                total += 26
            Else
                ' Hesaplama yapılamazsa hata döndür
                Return ApiResponse(Of EmployeePutDto).Fail(500, "Error calculating annual leave")
            End If

            ' Veritabanındaki ANNUALLEAVE sütununu güncelle
            employee.Annualleave = total ' Varsayılan olarak Employee varlığınızdaki yıllık izin özelliği

            ' Değişiklikleri veritabanına kaydet
            Await _repo.UpdateAsync(employee)
        End If

        ' Hesaplanan yıllık izinle ApiResponse'ı döndür
        Return ApiResponse(Of EmployeePutDto).Success(200, _mapper.Map(Of EmployeePutDto)(employee))
    End Function

    Public Async Function GetByPosition(positionId As Long) As Task(Of ApiResponse(Of IEnumerable(Of EmployeeGetDto))) Implements IEmployeeBs.GetByPosition
        Dim includeList As New List(Of String)
        includeList.Add("Position")
        Dim repoResponse = Await _repo.GetListAsync(Function(e) e.Positionid = positionId, includeList)
        Dim dtoList = _mapper.Map(Of IEnumerable(Of EmployeeGetDto))(repoResponse)
        Return ApiResponse(Of IEnumerable(Of EmployeeGetDto)).Success(200, dtoList)
    End Function

    Public Async Function SearchEmployeesByLastNameAndBirthdate(lastname As String, birthdate As Date) As Task(Of ApiResponse(Of IEnumerable(Of EmployeeGetDto))) Implements IEmployeeBs.SearchEmployeesByLastNameAndBirthdate
        ' Repository'den veriyi al
        Dim employees = Await _repo.SearchEmployeesByLastNameAndBirthdate(lastname, birthdate)

        ' Employee nesnelerini EmployeeGetDto'ya dönüştür
        Dim employeeDtos = _mapper.Map(Of IEnumerable(Of EmployeeGetDto))(employees)

        ' ApiResponse ile sonucu döndür
        Return ApiResponse(Of IEnumerable(Of EmployeeGetDto)).Success(200, employeeDtos)
    End Function
End Class

