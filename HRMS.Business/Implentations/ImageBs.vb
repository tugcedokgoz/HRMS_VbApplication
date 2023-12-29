Imports System.IO
Imports AutoMapper
Imports HRMS.Model
Imports HRMS.Model.Models
Imports HRMS.Repository
Imports Infrastructure
Imports Infrastructure.Infrastructure.Utilities.ApiResponses
Imports Microsoft.AspNetCore.Hosting

Public Class ImageBs
    Implements IImageBs

    Private ReadOnly _repo As IImageRepository
    Private ReadOnly _mapper As IMapper
    Private ReadOnly _webRootService As IWebRootProvider
    Private ReadOnly _hostRoot As String

    Public Sub New(repo As IImageRepository, mapper As IMapper, webRootService As IWebRootProvider)
        _repo = repo
        _mapper = mapper
        _webRootService = webRootService
        _hostRoot = _webRootService.GetWebRootPath()
    End Sub

    Public Async Function GetById(id As Long) As Task(Of ApiResponse(Of ImageGetDto)) Implements IImageBs.GetById
        Dim image = Await _repo.GetByIdAsync(id)
        Dim mappedImage = _mapper.Map(Of ImageGetDto)(image)

        Return ApiResponse(Of ImageGetDto).Success(200, mappedImage)
    End Function

    ' ImageBs sınıfında
    Public Async Function Add(dto As ImageUploadDto) As Task(Of ApiResponse(Of ImageGetDto)) Implements IImageBs.Add
        Dim postObj As New ImagePostDto
        If dto.File IsNot Nothing AndAlso dto.File.Length > 0 Then
            Try

                ' Dosya türünü kontrol et (Örneğin, sadece .jpg ve .png kabul edilsin)
                If Not (dto.File.ContentType = "image/jpeg" OrElse dto.File.ContentType = "image/png") Then
                    Return ApiResponse(Of ImageGetDto).Fail(400, "Unsupported file format. Only .jpg and .png are allowed.")
                End If

                ' Dosya boyutunu kontrol et (Örneğin, maksimum 5MB kabul edilsin)
                If dto.File.Length > 5 * 1024 * 1024 Then ' 5MB
                    Return ApiResponse(Of ImageGetDto).Fail(400, "File size exceeds the maximum limit of 5MB.")
                End If

                ' Dosyayı sunucuya kaydet
                Dim filePath = Path.Combine($"{_hostRoot}\Images\", dto.File.FileName)
                Using stream = New FileStream(filePath, FileMode.Create)
                    Await dto.File.CopyToAsync(stream)
                End Using

                ' DTO'daki ImagePath'i güncelle
                postObj.Imagepath = filePath


                postObj.Imagename = dto.File.FileName
                postObj.Imagetype = dto.File.ContentType
                postObj.Isactive = True
                postObj.Employeeid = dto.Employeeid
                postObj.Candİdateid = dto.Candİdateid

                postObj.File = dto.File


            Catch ex As Exception
                ' Dosya yükleme sırasında bir hata oluşursa
                Return ApiResponse(Of ImageGetDto).Fail(500, $"An error occurred while uploading the file: {ex.Message}")
            End Try
        Else
            Return ApiResponse(Of ImageGetDto).Fail(400, "No file provided.")
        End If

        ' Automapper ile DTO'dan Entity'ye dönüştürme
        Dim newItem = _mapper.Map(Of Image)(postObj)

        ' Veritabanına yeni öğeyi ekleme
        Dim added = Await _repo.AddAsync(newItem)
        Dim newDto = _mapper.Map(Of ImageGetDto)(added)

        ' Başarılı yanıt döndür
        Return ApiResponse(Of ImageGetDto).Success(201, newDto)
    End Function


    Public Async Function Update(id As Long, dto As ImagePutDto) As Task(Of ApiResponse(Of ImageGetDto)) Implements IImageBs.Update
        Dim existingItem = Await _repo.GetByIdAsync(id)
        If existingItem Is Nothing Then
            Return ApiResponse(Of ImageGetDto).Fail(404, "Item not found")
        End If

        ' Eğer yeni bir dosya yüklenmişse, mevcut dosyayı güncelle
        If dto.File IsNot Nothing AndAlso dto.File.Length > 0 Then
            Try
                ' Dosya türünü kontrol et
                ' Dosya boyutunu kontrol et

                ' Mevcut dosyayı yeni dosya ile değiştir



                Dim filePath = Path.Combine($"{_hostRoot}\Images\", dto.File.FileName)
                Using stream = New FileStream(filePath, FileMode.Create)
                    Await dto.File.CopyToAsync(stream)
                End Using

                ' DTO'daki ImagePath'i güncelle
                dto.ImagePath = filePath
            Catch ex As Exception
                Return ApiResponse(Of ImageGetDto).Fail(500, $"An error occurred while updating the file: {ex.Message}")
            End Try
        End If

        ' Automapper ile DTO'dan Entity'e dönüştürme ve veritabanında güncelleme
        _mapper.Map(dto, existingItem)
        Await _repo.UpdateAsync(existingItem)

        ' Güncellenmiş DTO'yu döndür
        Dim updatedDto = _mapper.Map(Of ImageGetDto)(existingItem)
        Return ApiResponse(Of ImageGetDto).Success(200, updatedDto)
    End Function

    Public Async Function Delete(id As Long) As Task(Of ApiResponse(Of NoData)) Implements IImageBs.Delete
        Dim existingItem = Await _repo.GetByIdAsync(id)
        If existingItem Is Nothing Then
            Return ApiResponse(Of NoData).Fail(404, "Item not found")
        End If

        Try
            ' Eğer resmin bir dosyası varsa, bu dosyayı da sistemden silin
            If Not String.IsNullOrEmpty(existingItem.ImagePath) Then
                Dim filePath = existingItem.ImagePath
                If File.Exists(filePath) Then
                    File.Delete(filePath)
                End If
            End If

            ' Veritabanından resmi sil
            Await _repo.DeleteAsync(id)
            Return ApiResponse(Of NoData).Success(200)
        Catch ex As Exception
            Return ApiResponse(Of NoData).Fail(500, $"An error occurred while deleting the item: {ex.Message}")
        End Try
    End Function
    Public Async Function GetAll() As Task(Of ApiResponse(Of IEnumerable(Of ImageGetDto))) Implements IImageBs.GetAll
        Dim repoResponse = Await _repo.GetAllAsync()
        Dim dtoList = _mapper.Map(Of IEnumerable(Of ImageGetDto))(repoResponse)

        Return ApiResponse(Of IEnumerable(Of ImageGetDto)).Success(200, dtoList)
    End Function

    'Public Async Function Delete(id As Long) As Task(Of ApiResponse(Of NoData)) Implements IImageBs.Delete
    '    Dim result = Await _repo.DeleteAsync(id)
    '    If result Then
    '        Return ApiResponse(Of NoData).Success(200)
    '    Else
    '        Return ApiResponse(Of NoData).Fail(404, "Item not found")
    '    End If
    'End Function

    'Public Async Function Update(id As Long, dto As ImagePutDto) As Task(Of ApiResponse(Of ImageGetDto)) Implements IImageBs.Update
    '    Dim existingItem = Await _repo.GetByIdAsync(id)
    '    If existingItem Is Nothing Then
    '        Return ApiResponse(Of ImageGetDto).Fail(404, "Item not found")
    '    End If

    '    _mapper.Map(dto, existingItem)
    '    Await _repo.UpdateAsync(existingItem)

    '    Dim updatedDto = _mapper.Map(Of ImageGetDto)(existingItem)

    '    Return ApiResponse(Of ImageGetDto).Success(200, updatedDto)
    'End Function
End Class
