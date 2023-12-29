
Imports HRMS.Model.Models
Imports Microsoft.AspNetCore.Http

Public Class ImageGetDto
    Public Property Id As Long

    Public Property Imagename As String

    Public Property Imagepath As String

    Public Property Imagetype As String

    Public Property Isactive As Boolean

    Public Property Employeeid As Long?

    Public Property Candİdateid As Long?

    Public Overridable Property Candİdate As Candidate

    Public Overridable Property Employee As Employee
End Class

Public Class ImageUploadDto





    Public Property Employeeid As Long?

    Public Property Candİdateid As Long?


    Public Property File As IFormFile
End Class


Public Class ImagePostDto

    Public Property Imagename As String

    Public Property Imagepath As String

    Public Property Imagetype As String

    Public Property Isactive As Boolean

    Public Property Employeeid As Long?

    Public Property Candİdateid As Long?

    Public Property File As IFormFile
End Class

Public Class ImagePutDto
    Public Property Id As Long

    Public Property Imagename As String

    Public Property Imagepath As String

    Public Property Imagetype As String

    Public Property Isactive As Boolean

    Public Property Employeeid As Long?

    Public Property Candİdateid As Long?

    Public Overridable Property Candİdate As Candidate

    Public Overridable Property Employee As Employee
    Public Property File As IFormFile
End Class