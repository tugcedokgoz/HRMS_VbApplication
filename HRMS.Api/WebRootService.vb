Imports HRMS.Business
Imports Microsoft.AspNetCore.Hosting


Public Class WebRootProvider : Implements IWebRootProvider
    Private ReadOnly _webHostEnvironment As IWebHostEnvironment

    Public Sub New(webHostEnvironment As IWebHostEnvironment)
        _webHostEnvironment = webHostEnvironment
    End Sub

    Public Function GetWebRootPath() As String Implements IWebRootProvider.GetWebRootPath
        Return _webHostEnvironment.ContentRootPath
    End Function
End Class


Public Class WebRootConfig
    Public Property WebRootPath As String
End Class
