Imports DocuSign.eSign.Model
Imports HRMS.UI.DepartmentPage
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Collections.ObjectModel
Imports System.Net.Http
Imports System.Text

Public Class EmployeePage : Implements IPage

    Public Async Function Add() As Task Implements IPage.Add
        Try
            Dim name = firstname.Text
            Dim lastna = lastname.Text
            Dim birth = birthdate.SelectedDate
            Dim gend = gender.SelectedValue
            Dim hire = hiredate.SelectedDate
            Dim mail = email.Text
            Dim phone = phonenumber.Text
            Dim position = positionId.SelectedValue
            Dim department = departmenId.SelectedValue
            Dim manager = manegerId.SelectedValue
            Dim annual = annualleave.Text


            Dim _httpClient As New HttpClient

            Dim postObject As New Employee With {
            .Firstname = name,
            .Lastname = lastna,
            .Birthdate = birth,
            .Gender = gend,
            .Hiredate = hire,
            .Email = mail,
            .Phonenumber = phone,
            .Positionid = position,
            .Departmanid = department,
            .Managerid = manager,
            .Annualleave = annual
        }

            Dim jsonContent = JsonConvert.SerializeObject(postObject)
            Dim content = New StringContent(jsonContent, Encoding.UTF8, "application/json")

            Dim response As HttpResponseMessage = Await _httpClient.PostAsync("https://localhost:5030/Employee", content)

            If response.IsSuccessStatusCode Then
                ' Handle successful response if needed
                MessageBox.Show("Çalışan başarılı bir şekilde eklendi.", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information)
            Else
                ' Display an error message for unsuccessful response
                Dim errorMessage = Await response.Content.ReadAsStringAsync()
                MessageBox.Show($"Error: {response.StatusCode}, {errorMessage}", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
            End If
        Catch ex As Exception
            ' Handle exceptions
            MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Function


    Private Async Sub searchEmployeeButton_Click(sender As Object, e As RoutedEventArgs)
        Using client As New HttpClient()
            Dim response As HttpResponseMessage
            Dim selected = birthdate.SelectedDate

            If Not lastname.Text = "Soyad Giriniz:" Then
                If selected Is Nothing Then
                    selected = DateTime.MinValue
                End If

                ' birthdate'i "yyyy-MM-dd" formatına çevir
                Dim formattedBirthdate As String = selected?.ToString("yyyy-MM-dd")

                response = Await client.GetAsync($"https://localhost:5030/Employee/SearchByLastNameAndBirthdate?lastname={lastname.Text}&birthdate={formattedBirthdate}")

                If response.IsSuccessStatusCode Then
                    Dim json As String = Await response.Content.ReadAsStringAsync()
                    Dim wrapper = JsonConvert.DeserializeObject(Of DataWrapper)(json)
                    employeeGrid.ItemsSource = wrapper.Data ' Bu satırı değiştirin
                Else
                    ' Hata durumunu ele alın
                    employeeGrid.ItemsSource = New ObservableCollection(Of Employee)() ' Hata durumunda boş bir koleksiyon atayın
                End If
            Else
                response = Await client.GetAsync("https://localhost:5030/Employee/GetAll")
                If response.IsSuccessStatusCode Then
                    Dim json As String = Await response.Content.ReadAsStringAsync()
                    Dim wrapper = JsonConvert.DeserializeObject(Of DataWrapper)(json)
                    employeeGrid.ItemsSource = wrapper.Data ' Bu satırı değiştirin
                Else
                    ' Hata durumunu ele alın
                    employeeGrid.ItemsSource = New ObservableCollection(Of Employee)() ' Hata durumunda boş bir koleksiyon atayın
                End If
            End If
        End Using
    End Sub


    Public Class DataWrapper
        Public Property Data As ObservableCollection(Of Employee)
    End Class

    Private Sub employeeGrid_SelectedItemChanged(sender As Object, e As DevExpress.Xpf.Grid.SelectedItemChangedEventArgs)
        Dim selected As Employee = TryCast(employeeGrid.SelectedItem, Employee)
        If selected IsNot Nothing Then
            firstname.Text = selected.Firstname
            lastname.Text = selected.Lastname
            birthdate.SelectedDate = selected.Birthdate
            gender.SelectedValue = selected.Gender
            hiredate.SelectedDate = selected.Hiredate
            email.Text = selected.Email
            phonenumber.Text = selected.Phonenumber
            positionId.SelectedValue = selected.Positionid
            departmenId.SelectedValue = selected.Departmanid
            manegerId.SelectedValue = selected.Managerid
            annualleave.Text = selected.Annualleave

            If selected.Gender IsNot Nothing Then
                gender.SelectedItem = selected.Gender
            Else
                gender.SelectedIndex = -1 ' Hiçbir şey seçili değil
            End If
        End If
    End Sub
    Private Async Sub EmployeeControl_Loaded(sender As Object, e As RoutedEventArgs)
        Using client As New HttpClient()
            Dim response As HttpResponseMessage = Await client.GetAsync("https://localhost:5030/Employee/GetByPosition?positionId=3")

            If response.IsSuccessStatusCode Then
                Dim json As String = Await response.Content.ReadAsStringAsync()
                Dim wrapper = JsonConvert.DeserializeObject(Of DataWrapper(Of Employee))(json)

                ' Seçim yapılmadı seçeneğini ekle
                Dim defaultEmployee As New Employee With {.Id = -1, .Firstname = "Seçim", .Lastname = "Yapılmadı"}
                wrapper.Data.Insert(0, defaultEmployee)

                ' Combobox'ı verilerle doldur
                manegerId.DisplayMemberPath = "FullName" ' Combobox'ta görünen metni belirtir
                manegerId.SelectedValuePath = "Id" ' Combobox'ta seçilen değeri belirtir
                manegerId.ItemsSource = wrapper.Data

                ' Varsayılan olarak Seçim Yapılmadı seçeneğini seçili hale getir
                manegerId.SelectedValue = defaultEmployee.Id
            Else
                ' Hata durumunu ele al
            End If
        End Using

        Using client As New HttpClient()
            Dim response As HttpResponseMessage = Await client.GetAsync("https://localhost:5030/Department/GetAll")
            If response.IsSuccessStatusCode Then
                Dim json As String = Await response.Content.ReadAsStringAsync()
                Dim wrapper = JsonConvert.DeserializeObject(Of DataWrapper(Of Department))(json)

                Dim defaultdepartman As New Department With {.Id = -1, .Departmentname = "Seçim Yapılmadı"}
                wrapper.Data.Insert(0, defaultdepartman)

                ' Combobox'ı verilerle doldur
                departmenId.DisplayMemberPath = "Departmentname" ' Combobox'ta görünen metni belirtir
                departmenId.SelectedValuePath = "Id" ' Combobox'ta seçilen değeri belirtir
                departmenId.ItemsSource = wrapper.Data

                ' Varsayılan olarak Seçim Yapılmadı seçeneğini seçili hale getir
                departmenId.SelectedValue = defaultdepartman.Id
            Else
                ' Hata durumunu ele al
            End If
        End Using


        Dim genders As New List(Of String) From {"Kadın", "Erkek"}

        ' ComboBox'ı temizleyin
        gender.Items.Clear()

        ' Veriyi ComboBox'a ekleyin
        For Each genderr In genders
            gender.Items.Add(genderr)
        Next


    End Sub



    Public Async Function Delete() As Task Implements IPage.Delete
        Using client As New HttpClient()
            Dim selected As Employee = TryCast(employeeGrid.SelectedItem, Employee)

            If selected Is Nothing Then
                MessageBox.Show("Lütfen silinecek bir çalışan seçin.", "Hata", MessageBoxButton.OK, MessageBoxImage.Error)
                Return
            End If

            ' Onay kutusu göster
            Dim messageBoxResult As MessageBoxResult = MessageBox.Show("Bu çalışanı silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButton.YesNo, MessageBoxImage.Warning)

            ' Eğer kullanıcı Evet'i seçerse, silme işlemini gerçekleştir
            If messageBoxResult = MessageBoxResult.Yes Then
                Try
                    ' HttpClient nesnesini her seferinde yeniden oluştur
                    Dim response As HttpResponseMessage = Await client.DeleteAsync($"https://localhost:5030/Employee/{selected.Id}")
                    If response.IsSuccessStatusCode Then
                        MessageBox.Show("Çalışan başarıyla silindi.", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information)
                    Else
                        MessageBox.Show($"Bir hata oluştu: {response.StatusCode}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error)
                    End If
                Catch ex As Exception
                    MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error)
                End Try
            End If
        End Using
    End Function


    Public Function Update() As Task Implements IPage.Update
        Throw New NotImplementedException()
    End Function
    Private Sub LastnameTextBox_GotFocus(sender As Object, e As RoutedEventArgs)
        If lastname.Text = "Soyad Giriniz:" Then
            lastname.Text = ""
        End If
    End Sub
    Private Sub TextBox_GotFocus(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub LastnameTextBox_LostFocus(sender As Object, e As RoutedEventArgs)
        If lastname.Text = "" Then
            lastname.Text = "Soyad Giriniz:"
        End If
    End Sub

    Private Async Sub departmenId_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Using client As New HttpClient()
            ' Varsayılan olarak departmanId.SelectedItem, seçilen departmanı temsil eden bir nesnedir
            Dim selectedDepartment As Department = CType(departmenId.SelectedItem, Department)

            ' Bir departmanın seçilip seçilmediğini kontrol et
            If selectedDepartment IsNot Nothing Then
                ' URL veya istek parametrelerinde departman ID'sini kullan
                Dim positionsUrl As String = $"https://localhost:5030/Department/ListPositions/{selectedDepartment.Id}"

                ' İkinci API isteğini yap
                Dim response As HttpResponseMessage = Await client.GetAsync(positionsUrl)

                If response.IsSuccessStatusCode Then
                    ' API'dan gelen JSON verisini oku
                    Dim json As String = Await response.Content.ReadAsStringAsync()

                    ' JSON verisini uygun tipe dönüştür
                    Dim wrapper = JsonConvert.DeserializeObject(Of DataWrapper(Of Position))(json)



                    Dim defaultposition As New Position With {.Id = -1, .PositionTitle = "Seçim Yapılmadı"}
                    wrapper.Data.Insert(0, defaultposition)

                    ' Combobox'ı verilerle doldur
                    positionId.DisplayMemberPath = "PositionTitle" ' Combobox'ta görünen metni belirtir
                    positionId.SelectedValuePath = "Id" ' Combobox'ta seçilen değeri belirtir
                    positionId.ItemsSource = wrapper.Data

                    ' Varsayılan olarak Seçim Yapılmadı seçeneğini seçili hale getir
                    positionId.SelectedValue = defaultposition.Id


                Else
                    ' Hata durumunu ele al
                End If
            Else
                ' Hiçbir departmanın seçilmediği durumu ele al
            End If
        End Using
    End Sub
End Class
