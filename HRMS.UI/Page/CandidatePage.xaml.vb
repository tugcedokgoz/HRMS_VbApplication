Imports System.Collections.ObjectModel
Imports System.Net.Http
Imports System.Text
Imports DocuSign.eSign.Client
Imports Microsoft.VisualBasic.ApplicationServices
Imports Newtonsoft.Json

Public Class CandidatePage : Implements IPage

    Private httpclient As HttpClient
    Public Property Candidates As ObservableCollection(Of Candidate)
    Public Property Positions As ObservableCollection(Of Position)
    Public Sub New()
        InitializeComponent()
        httpclient = New HttpClient()
        httpclient.BaseAddress = New Uri("https://localhost:5030/")
        Candidates = New ObservableCollection(Of Candidate)()
        Positions = New ObservableCollection(Of Position)()
        LoadCandidate()
    End Sub
    Public Class ApiResponse(Of T)
        Public Property Data As List(Of T)
    End Class
    Public Async Function LoadCandidate() As Task

        Dim response = Await httpclient.GetAsync("Candidate/GetAll")
        If response.IsSuccessStatusCode Then
            Dim apiResponse = Await response.Content.ReadAsAsync(Of ApiResponse(Of Candidate))()
            Candidates = New ObservableCollection(Of Candidate)(apiResponse.Data)
            candidateGridControl.ItemsSource = Candidates
        End If
    End Function
    Public Async Sub UserControl_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Dim comboResponse = Await httpclient.GetAsync("Position/GetAll")
            If comboResponse.IsSuccessStatusCode Then
                Dim positionsApiResponse = Await comboResponse.Content.ReadAsAsync(Of ApiResponse(Of Position))()
                ' Data tipinin List(Of Position) olduğundan emin olun.
                Positions = New ObservableCollection(Of Position)(positionsApiResponse.Data)
                ' ComboBox'a Positions koleksiyonunu atayın.
                positionComboBox.ItemsSource = Positions
            Else

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub candidateFirstName_GotFocus(sender As Object, e As RoutedEventArgs)
        If firstName.Text = "Adı Giriniz..." Then
            firstName.Text = ""
        End If
    End Sub
    Private Sub candidateFirstName_LostFocus(sender As Object, e As RoutedEventArgs)
        If firstName.Text = "" Then
            firstName.Text = "Adı Giriniz..."
        End If
    End Sub

    Private Sub candidateLastName_GotFocus(sender As Object, e As RoutedEventArgs)
        If lastName.Text = "Soyad Giriniz..." Then
            lastName.Text = ""
        End If
    End Sub
    Private Sub candidateLastName_LostFocus(sender As Object, e As RoutedEventArgs)
        If lastName.Text = "" Then
            lastName.Text = "Soyad Giriniz..."
        End If
    End Sub

    Private Sub candidateResumeLink_GotFocus(sender As Object, e As RoutedEventArgs)
        If resumeLink.Text = "Özgeçmiş Bağlantısını Giriniz..." Then
            resumeLink.Text = ""
        End If
    End Sub
    Private Sub candidateResumeLink_LostFocus(sender As Object, e As RoutedEventArgs)
        If resumeLink.Text = "" Then
            resumeLink.Text = "Özgeçmiş Bağlantısını Giriniz..."
        End If
    End Sub

    Public Async Function AddCandidate() As Task Implements IPage.Add
        ' Kullanıcı arayüzünden verileri al
        Dim firstNameValue As String = firstName.Text
        Dim lastNameValue As String = lastName.Text
        Dim resumeLinkValue As String = resumeLink.Text
        Dim applicationDateValue As Date? = birthdate.SelectedDate
        Dim appliedPositionIdValue As Long? = CType(positionComboBox.SelectedValue, Long?)

        ' HTTP isteği için nesne oluştur
        Dim candidateData As New With {
        Key .FirstName = firstNameValue,
        Key .LastName = lastNameValue,
        Key .ResumeLink = resumeLinkValue,
        Key .ApplicationDate = applicationDateValue,
        Key .AppliedPositionId = appliedPositionIdValue,
        Key .Operation = "ADD"
    }
        Dim jsonContent = JsonConvert.SerializeObject(candidateData)
        Dim content = New StringContent(jsonContent, Encoding.UTF8, "application/json")

        Try
            Dim response As HttpResponseMessage = Await httpclient.PostAsync("https://localhost:50099/Candidate/Procedure", content)
            If response.IsSuccessStatusCode Then
                MessageBox.Show("Aday başarıyla eklendi.", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information)
                Await LoadCandidate()
            Else
                MessageBox.Show("Aday eklenirken bir hata oluştu: " & response.StatusCode.ToString(), "Hata", MessageBoxButton.OK, MessageBoxImage.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Hata: " & ex.Message, "Hata", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Function
    Public Async Function Delete() As Task Implements IPage.Delete
        Dim selected As Candidate = TryCast(candidateGridControl.SelectedItem, Candidate)

        If selected Is Nothing Then
            MessageBox.Show("Lütfen silinecek bir aday seçin.", "Hata", MessageBoxButton.OK, MessageBoxImage.Error)
            Return
        End If

        ' Onay kutusu göster
        Dim messageBoxResult As MessageBoxResult = MessageBox.Show("Bu adayı silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButton.YesNo, MessageBoxImage.Warning)

        ' Eğer kullanıcı Evet'i seçerse, silme işlemini gerçekleştir
        If messageBoxResult = MessageBoxResult.Yes Then
            Try
                ' HttpClient nesnesini sınıf seviyesinde kullanın
                Dim response As HttpResponseMessage = Await httpclient.DeleteAsync($"Candidate/{selected.Id}")
                If response.IsSuccessStatusCode Then
                    MessageBox.Show("Aday başarıyla silindi.", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information)
                    Await LoadCandidate()
                Else
                    MessageBox.Show("Bir hata oluştu: " & response.StatusCode.ToString(), "Hata", MessageBoxButton.OK, MessageBoxImage.Error)
                End If
            Catch ex As Exception
                MessageBox.Show("Hata: " & ex.Message, "Hata", MessageBoxButton.OK, MessageBoxImage.Error)
            End Try
        End If
    End Function


    Public Function Update() As Task Implements IPage.Update
        Throw New NotImplementedException()
    End Function

    Private Sub OnCandidateSelectionChanged(sender As Object, e As DevExpress.Xpf.Grid.SelectedItemChangedEventArgs)
        Dim selectedCandidate As Candidate = TryCast(candidateGridControl.SelectedItem, Candidate)
        If selectedCandidate IsNot Nothing Then
            firstName.Text = selectedCandidate.Firstname
            lastName.Text = selectedCandidate.Lastname
            resumeLink.Text = selectedCandidate.Resumelink
            birthdate.SelectedDate = selectedCandidate.Applicationdate

        End If
    End Sub
End Class
