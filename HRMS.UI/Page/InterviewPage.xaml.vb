Imports System.Collections.ObjectModel
Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Text
Imports System.ServiceModel

Public Class InterviewPage : Implements IPage
    Private httpclient As HttpClient
    Public Property Interviews As ObservableCollection(Of Interview)

    Public Sub New()
        InitializeComponent()
        httpclient = New HttpClient()
        httpclient.BaseAddress = New Uri("https://localhost:5030/")
        Interviews = New ObservableCollection(Of Interview)()
        LoadInterview()
    End Sub
    Public Class ApiResponse(Of T)
        Public Property Data As List(Of T)
    End Class
    Public Async Function LoadInterview() As Task

        Dim response = Await httpclient.GetAsync("Interview/GetAll")
        If response.IsSuccessStatusCode Then
            Dim apiResponse = Await response.Content.ReadAsAsync(Of ApiResponse(Of Interview))()

            Interviews = New ObservableCollection(Of Interview)(apiResponse.Data)
            interviewGridControl.ItemsSource = Interviews
        End If

        ' API'den tüm Candidate listesini al
        Dim candidateResponse = Await httpclient.GetAsync("Candidate/GetAll")
        If candidateResponse.IsSuccessStatusCode Then
            ' API yanıtını deserialize et
            Dim candidateApiResponse = Await candidateResponse.Content.ReadAsAsync(Of ApiResponse(Of Candidate))()

            ' ComboBox için varsayılan değer ekle (opsiyonel)
            Dim defaultCandidate As New Candidate With {.Id = "-1", .Firstname = "Seçim", .Lastname = "Yapılmadı"}
            Dim candidates = candidateApiResponse.Data.ToList()
            candidates.Insert(0, defaultCandidate)

            ' candidateComboBox'ı Candidate listesiyle doldur
            candidateComboBox.DisplayMemberPath = "FullName"
            candidateComboBox.SelectedValuePath = "Id"
            candidateComboBox.ItemsSource = candidates

            ' Varsayılan olarak Seçim Yapılmadı seçeneğini seçili hale getir (opsiyonel)
            candidateComboBox.SelectedValue = "-1"
        End If
    End Function

    Public Async Function Add() As Task Implements IPage.Add
        ' Collect data from the user interface
        Dim interviewDateValue As Date? = interviewDate.SelectedDate
        Dim interviewNotesValue As String = txtInterviewNotes.Text
        Dim interviewOutcomeValue As String = txtInterviewOutcome.Text
        Dim interviewerIdValue As Long? = 0  ' Update this based on your logic for selecting interviewer
        Dim candidateIdValue As Long? = Convert.ToInt64(candidateComboBox.SelectedValue)

        ' Check if the required fields are provided
        If interviewDateValue Is Nothing OrElse candidateIdValue Is Nothing OrElse candidateIdValue = -1 Then
            MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
            Return
        End If

        ' Create the object for the HTTP request
        Dim interviewData As New With {
        Key .InterviewDate = interviewDateValue,
        Key .InterviewNotes = interviewNotesValue,
        Key .InterviewOutcome = interviewOutcomeValue,
        Key .InterviewerId = interviewerIdValue,
        Key .CandidateId = candidateIdValue,
        Key .Operation = "ADD"
    }

        Dim jsonContent = JsonConvert.SerializeObject(interviewData)
        Dim content = New StringContent(jsonContent, Encoding.UTF8, "application/json")

        Try
            ' Send the HTTP request
            Dim response As HttpResponseMessage = Await httpclient.PostAsync("https://localhost:5030/Interview/Schedule", content)
            If response.IsSuccessStatusCode Then
                MessageBox.Show("Mülakat başarı ile eklendi.", "Success", MessageBoxButton.OK, MessageBoxImage.Information)
                Await LoadInterview()
            Else
                MessageBox.Show("Mülakat eklenirken problem oldu: " & response.StatusCode.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Function

    Public Async Function Delete() As Task Implements IPage.Delete
        Dim selected As Interview = TryCast(interviewGridControl.SelectedItem, Interview)

        If selected Is Nothing Then
            MessageBox.Show("Lütfen silinecek bir mülakat seçin.", "Hata", MessageBoxButton.OK, MessageBoxImage.Error)
            Return
        End If

        ' Onay kutusu göster
        Dim messageBoxResult As MessageBoxResult = MessageBox.Show("Bu mülakatı silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButton.YesNo, MessageBoxImage.Warning)

        ' Eğer kullanıcı Evet'i seçerse, silme işlemini gerçekleştir
        If messageBoxResult = MessageBoxResult.Yes Then
            Try
                ' HttpClient nesnesini sınıf seviyesinde kullanın
                Dim response As HttpResponseMessage = Await httpclient.DeleteAsync($"https://localhost:5030/Interview/{selected.Id}")
                If response.IsSuccessStatusCode Then
                    MessageBox.Show("Aday başarıyla silindi.", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information)
                    Await LoadInterview()
                Else
                    MessageBox.Show("Bir hata oluştu: " & response.StatusCode.ToString(), "Hata", MessageBoxButton.OK, MessageBoxImage.Error)
                End If
            Catch ex As Exception
                MessageBox.Show("Hata: " & ex.Message, "Hata", MessageBoxButton.OK, MessageBoxImage.Error)
            End Try
        End If
    End Function

    Public Async Function Update() As Task Implements IPage.Update
        Dim selectedInterview As Interview = TryCast(interviewGridControl.SelectedItem, Interview)



        If selectedInterview IsNot Nothing Then
            ' Güncellenmiş mülakat bilgilerini al
            Dim updatedInterview As New With {
            Key .Id = selectedInterview.Id,
            Key .Interviewdate = interviewDate.SelectedDate,
            Key .Interviewnotes = txtInterviewNotes.Text,
            Key .Interviewoutcome = txtInterviewOutcome.Text,
            Key .Interviewerid = 0,
            Key .CandidateId = Convert.ToInt64(candidateComboBox.SelectedValue),
            Key .Operation = "UPDATE"
        }

            ' JSON içeriğini hazırla ve gönder
            Dim jsonContent = JsonConvert.SerializeObject(updatedInterview)
            Dim content = New StringContent(jsonContent, Encoding.UTF8, "application/json")

            ' API'ye güncelleme isteğini gönder
            Dim response As HttpResponseMessage = Await httpclient.PostAsync("https://localhost:5030/Interview/Manage", content)

            If response.StatusCode = Net.HttpStatusCode.OK Then
                MessageBox.Show("Mülakat başarıyla güncellendi.", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information)
                ' Mülakat verilerini yeniden yükle
                Await LoadInterview()
            Else
                MessageBox.Show("Bir hata oluştu: " & response.StatusCode.ToString(), "Hata", MessageBoxButton.OK, MessageBoxImage.Error)
            End If
        Else
            MessageBox.Show("Lütfen güncellenecek bir mülakat seçin.", "Hata", MessageBoxButton.OK, MessageBoxImage.Error)
        End If
    End Function


    Private Sub candidateComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)

    End Sub

    Private Sub OnInterviewSelectionChanged(sender As Object, e As DevExpress.Xpf.Grid.SelectedItemChangedEventArgs)
        Dim selectedInterview As Interview = TryCast(interviewGridControl.SelectedItem, Interview)
        If selectedInterview IsNot Nothing Then
            interviewDate.SelectedDate = selectedInterview.Interviewdate
            txtInterviewNotes.Text = selectedInterview.Interviewnotes
            txtInterviewOutcome.Text = selectedInterview.Interviewoutcome
            candidateComboBox.SelectedValue = selectedInterview.Candidateid

        End If
    End Sub


End Class
