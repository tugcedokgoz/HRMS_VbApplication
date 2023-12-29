
Imports System.Collections.ObjectModel
Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json

Partial Public Class DepartmentPage : Implements IPage

    Public Async Function Add() As Task Implements IPage.Add
        Dim name = departmentName.Text

        Using _httpClient = HttpClientFactory.Create()



            Dim postObject As New With {
           Key .departmentName = name,
           Key .operation = "ADD"
            }

            Dim jsonContent = JsonConvert.SerializeObject(postObject)
            Dim content = New StringContent(jsonContent, Encoding.UTF8, "application/json")

            Dim response As HttpResponseMessage = Await _httpClient.PostAsync("Department/ManageDepartment", content)

            If response.StatusCode = Net.HttpStatusCode.OK Then
                Await LoadDepartments()
            Else
                MessageBox.Show("Bir hata oluştu: " & response.StatusCode.ToString(), "Hata", MessageBoxButton.OK, MessageBoxImage.Error)
            End If
        End Using
    End Function

    Private Async Sub searchButton_Click(sender As Object, e As RoutedEventArgs)
        Using client = HttpClientFactory.Create()
            Dim response As HttpResponseMessage
            Dim selected = managerComboBox.SelectedValue

            If Not departmentName.Text = "Departman Adı Giriniz:" Then
                If selected Is Nothing Then
                    selected = 0
                End If
                response = Await client.GetAsync($"Department/Search?departmentName={departmentName.Text}&departmentManager={selected}")

                If response.IsSuccessStatusCode Then
                    Dim json As String = Await response.Content.ReadAsStringAsync()
                    Dim wrapper = JsonConvert.DeserializeObject(Of DataWrapper(Of Department))(json)
                    departmentGrid.ItemsSource = wrapper.Data ' Bu satırı değiştirin
                Else
                    ' Hata durumunu ele alın
                    departmentGrid.ItemsSource = New ObservableCollection(Of Department)() ' Hata durumunda boş bir koleksiyon atayın
                End If
            Else
                response = Await client.GetAsync("Department/GetAll")
                If response.IsSuccessStatusCode Then
                    Dim json As String = Await response.Content.ReadAsStringAsync()
                    Dim wrapper = JsonConvert.DeserializeObject(Of DataWrapper(Of Department))(json)
                    departmentGrid.ItemsSource = wrapper.Data ' Bu satırı değiştirin
                Else
                    ' Hata durumunu ele alın
                    departmentGrid.ItemsSource = New ObservableCollection(Of Department)() ' Hata durumunda boş bir koleksiyon atayın
                End If
            End If
        End Using
    End Sub

    Public Class DataWrapper(Of T)
        Public Property Data As ObservableCollection(Of T)
    End Class

    Private Sub departmentName_GotFocus(sender As Object, e As RoutedEventArgs)
        If departmentName.Text = "Departman Adı Giriniz:" Then
            departmentName.Text = ""
        End If
    End Sub

    Private Sub TextBox_GotFocus(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub departmentName_LostFocus(sender As Object, e As RoutedEventArgs)
        If departmentName.Text = "" Then
            departmentName.Text = "Departman Adı Giriniz:"
        End If
    End Sub

    Private Async Sub UserControl_Loaded(sender As Object, e As RoutedEventArgs)
        Using client = HttpClientFactory.Create()
            Dim response As HttpResponseMessage = Await client.GetAsync("Employee/GetByPosition?positionId=3")
            If response.IsSuccessStatusCode Then
                Dim json As String = Await response.Content.ReadAsStringAsync()
                Dim wrapper = JsonConvert.DeserializeObject(Of DataWrapper(Of Employee))(json)
                Dim defaultManager = New Employee With {.Id = -1, .Firstname = "Seçim", .Lastname = "Yapılmadı"}
                wrapper.Data.Insert(0, defaultManager)

                ' İşlenmiş listeyi bir UI elementine bağla, örneğin bir ListBox'a
                ' Bu örnekte, 'managersListBox' isminde bir ListBox varsayılmıştır.
                managerComboBox.ItemsSource = wrapper.Data
            Else
                ' Hata durumunu ele al
            End If
            Await LoadDepartments()
        End Using
    End Sub

    Private Sub departmentGrid_SelectedItemChanged(sender As Object, e As DevExpress.Xpf.Grid.SelectedItemChangedEventArgs)
        Dim selected As Department = TryCast(departmentGrid.SelectedItem, Department)
        If selected IsNot Nothing Then
            departmentName.Text = selected.Departmentname
            If selected.Manager IsNot Nothing Then
                managerComboBox.SelectedValue = selected.Manager.Id
            Else
                managerComboBox.SelectedIndex = -1 ' Hiçbir şey seçili değil
            End If
        End If
    End Sub

    Public Async Function Delete() As Task Implements IPage.Delete
        Dim selected As Department = TryCast(departmentGrid.SelectedItem, Department)

        Using _httpClient = HttpClientFactory.Create()


            Dim postObject As New With {
           Key .id = selected.Id,
           Key .departmentName = selected.Departmentname,
           Key .operation = "DELETE"
        }

            Dim jsonContent = JsonConvert.SerializeObject(postObject)
            Dim content = New StringContent(jsonContent, Encoding.UTF8, "application/json")

            Dim response As HttpResponseMessage = Await _httpClient.PostAsync("Department/ManageDepartment", content)

            If response.StatusCode = Net.HttpStatusCode.OK Then
                Await LoadDepartments()
            Else
                MessageBox.Show("Bir hata oluştu: " & response.StatusCode.ToString(), "Hata", MessageBoxButton.OK, MessageBoxImage.Error)
            End If

        End Using

    End Function

    Public Async Function Update() As Task Implements IPage.Update
        Dim selected As Department = TryCast(departmentGrid.SelectedItem, Department)

        Using _httpClient = HttpClientFactory.Create()

            Dim postObject = New With {
                  Key .id = selected.Id,
                  Key .departmentName = departmentName.Text,
                  Key .managerId = managerComboBox.SelectedValue,
                  Key .operation = "UPDATE"
               }


            Dim jsonContent = JsonConvert.SerializeObject(postObject)
            Dim content = New StringContent(jsonContent, Encoding.UTF8, "application/json")

            Dim response As HttpResponseMessage = Await _httpClient.PostAsync("Department/ManageDepartment", content)

            If response.StatusCode = Net.HttpStatusCode.OK Then
                Await LoadDepartments()
            Else
                MessageBox.Show("Bir hata oluştu: " & response.StatusCode.ToString(), "Hata", MessageBoxButton.OK, MessageBoxImage.Error)
            End If
        End Using

    End Function

    Public Async Function LoadDepartments() As Task
        Using client = HttpClientFactory.Create()
            Dim response As HttpResponseMessage

            response = Await client.GetAsync("Department/GetAll")
            If response.IsSuccessStatusCode Then
                Dim json As String = Await response.Content.ReadAsStringAsync()
                Dim wrapper = JsonConvert.DeserializeObject(Of DataWrapper(Of Department))(json)
                departmentGrid.ItemsSource = wrapper.Data ' Bu satırı değiştirin
            Else
                ' Hata durumunu ele alın
                departmentGrid.ItemsSource = New ObservableCollection(Of Department)() ' Hata durumunda boş bir koleksiyon atayın
            End If
        End Using
    End Function
End Class


