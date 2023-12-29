Imports Microsoft.AspNetCore.Hosting

Public Module AppConfig

    Private ReadOnly _webHost As IWebHostEnvironment



    Function GetRoot() As String
        Return _webHost.WebRootPath
    End Function


End Module
