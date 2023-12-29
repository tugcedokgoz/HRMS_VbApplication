Imports System.Collections.ObjectModel
Imports System.Net.Http
Imports System.Text
Imports DevExpress.XtraRichEdit.Model
Imports DocuSign.eSign.Client
Imports DocuSign.eSign.Model
Imports Newtonsoft.Json

Public Class LeavePage : Implements IPage
    Private httpclient As HttpClient
    Public Property Leaveies As ObservableCollection(Of Leave)

    Public Sub New()
        InitializeComponent()
        httpclient = New HttpClient()
        httpclient.BaseAddress = New Uri("https://localhost:5030/")
        Leaveies = New ObservableCollection(Of Leave)()
        LoadLeave()
    End Sub

    Public Async Function LoadLeave() As Task
        Try
            Dim response = Await httpclient.GetAsync("Leave")
            If response.IsSuccessStatusCode Then
                Dim apiResponse = Await response.Content.ReadAsAsync(Of ApiResponse(Of Leave))()
                Leaveies = New ObservableCollection(Of Leave)(apiResponse.Data)
                leaveGridControl.ItemsSource = Leaveies
            Else
                MessageBox.Show("Bir hata oluştu: " & response.StatusCode.ToString(), "Hata", MessageBoxButton.OK, MessageBoxImage.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Bir hata oluştu: " & ex.Message, "Hata", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try

        'employee combobox
        Dim employeeResponse = Await httpclient.GetAsync("Employee/GetAll")
        If employeeResponse.IsSuccessStatusCode Then
            Dim employeeApiResponse = Await employeeResponse.Content.ReadAsAsync(Of ApiResponse(Of Employee))()

            Dim defaultEmployee As New Employee With {.Id = "-1", .Firstname = "Seçim", .Lastname = "Yapılmadı"}
            Dim employeeies = employeeApiResponse.Data.ToList()
            employeeies.Insert(0, defaultEmployee)

            employeeCombobox.DisplayMemberPath = "FullName"
            employeeCombobox.SelectedValuePath = "Id"
            employeeCombobox.ItemsSource = employeeies

            employeeCombobox.SelectedValue = "-1"
        End If

        'LeaveType combobox
        Dim leaveTypeResponse = Await httpclient.GetAsync("LeaveTypes/GetAll")
        If leaveTypeResponse.IsSuccessStatusCode Then
            Dim leaveTypeApiResponse = Await leaveTypeResponse.Content.ReadAsAsync(Of ApiResponse(Of LeaveType))()
            Dim defaultLeaveType As New LeaveType With {.Id = "-1", .Typename = "Seçim Yapılmadı"}
            Dim leaveTypeies = leaveTypeApiResponse.Data.ToList()
            leaveTypeies.Insert(0, defaultLeaveType)

            leaveTypeCombobox.DisplayMemberPath = "Typename"
            leaveTypeCombobox.SelectedValuePath = "Id"
            leaveTypeCombobox.ItemsSource = leaveTypeies

            leaveTypeCombobox.SelectedValue = "-1"
        End If

    End Function
    Public Class ApiResponse(Of T)
        Public Property Data As List(Of T)
    End Class
    Public Async Function Add() As Task Implements IPage.Add
        Dim leaveStartDateValue As Date? = leaveStartDate.SelectedDate
        Dim leaveEndDateValue As Date? = leaveEndDate.SelectedDate
        Dim employeeComboboxValue As Long? = Convert.ToInt64(employeeCombobox.SelectedValue)
        Dim leaveTypeComboboxValue As Long? = Convert.ToInt64(leaveTypeCombobox.SelectedValue)
        Dim leaveTextBoxValue As String = leaveTextBox.Text
        Dim leaveStatusValue As String = leaveStatus.Text


        Dim leaveData As New With {
            Key .LeaveStartDate = leaveStartDateValue,
            Key .LeaveEndDate = leaveEndDateValue,
            Key .EmployeeId = employeeComboboxValue,
            Key .LeaveTypeId = leaveTypeComboboxValue,
            Key .Description = leaveTextBoxValue,
            Key .Status = leaveStatusValue,
            Key .Operation = "ADD"
            }
        Dim jsonContent = JsonConvert.SerializeObject(leaveData)
        Dim content = New StringContent(jsonContent, Encoding.UTF8, "application/json")
        Try
            ' Send the HTTP request
            Dim response As HttpResponseMessage = Await httpclient.PostAsync("https://localhost:5030/Leave/ManageLeave", content)
            If response.IsSuccessStatusCode Then
                MessageBox.Show("Veri başarı ile eklendi.", "Success", MessageBoxButton.OK, MessageBoxImage.Information)
                Await LoadLeave()
            Else
                MessageBox.Show("Veri eklenirken problem oldu: " & response.StatusCode.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Function

    Public Async Function Delete() As Task Implements IPage.Delete
        Dim selected As Leave = TryCast(leaveGridControl.SelectedItem, Leave)
        If selected Is Nothing Then
            MessageBox.Show("Lütfen silinecek bir veri seçin.", "Hata", MessageBoxButton.OK, MessageBoxImage.Error)
            Return
        End If
        Dim messageBoxResult As MessageBoxResult = MessageBox.Show("Bu veriyi silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButton.YesNo, MessageBoxImage.Warning)
        If messageBoxResult = MessageBoxResult.Yes Then
            Try
                Dim response As HttpResponseMessage = Await httpclient.DeleteAsync($"https://localhost:5030/Leave/{selected.Id}")
                If response.IsSuccessStatusCode Then
                    MessageBox.Show("Veri Başarıyla Silindi.", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information)
                    Await LoadLeave()
                Else
                    MessageBox.Show("Bir hata oluştu:" & response.StatusCode.ToString(), "Hata", MessageBoxButton.OK, MessageBoxImage.Error)

                End If
            Catch ex As Exception
                MessageBox.Show("Hata:" & ex.Message, "Hata", MessageBoxButton.OK, MessageBoxImage.Error)
            End Try
        End If
    End Function

    Public Function Update() As Task Implements IPage.Update
        Throw New NotImplementedException()
    End Function



End Class
