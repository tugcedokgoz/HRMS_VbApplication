Imports System.Text
Imports DevExpress.Pdf.Native
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Core

Namespace HRMS.UI
    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Partial Public Class MainWindow
        Inherits ThemedWindow

        Private Property _selector As Integer

        Public Property _page As IPage

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub DataGrid_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)

        End Sub
        Private Sub DepartmanlarButton_Click(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            _selector = 1
            ' Öncelikle mevcut içeriği temizleyin
            MainContentPanel.Children.Clear()

            ' DepartmentPage UserControl'ünü oluşturun
            Dim departmentPage As New DepartmentPage()

            _page = departmentPage

            ' UserControl'ü ana içerik paneline ekleyin
            MainContentPanel.Children.Add(departmentPage)
        End Sub
        Private Sub EmployeeButton_Click(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            _selector = 1
            MainContentPanel.Children.Clear()
            Dim employeePage As New EmployeePage()
            _page = employeePage
            MainContentPanel.Children.Add(employeePage)
        End Sub

        Private Sub BarButtonItem_ItemClick(sender As Object, e As ItemClickEventArgs)
            If (_selector = 1) Then
                _page.Add()
            End If
        End Sub

        Private Sub btnDelete_ItemClick(sender As Object, e As ItemClickEventArgs)
            _page.Delete()
        End Sub
        Private Sub PositionButton_Click(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            _selector = 1

            MainContentPanel.Children.Clear()
            Dim positionPage As New PositionPage()
            _page = positionPage
            MainContentPanel.Children.Add(positionPage)
        End Sub

        Private Sub btnUpdate_ItemClick(sender As Object, e As ItemClickEventArgs)
            _page.Update()
        End Sub

        Private Sub CandidateButton_Click(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            _selector = 1
            MainContentPanel.Children.Clear()
            Dim canidatePage As New CandidatePage()
            _page = canidatePage
            MainContentPanel.Children.Add(canidatePage)
        End Sub

        Private Sub InterviewButton_Click(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            _selector = 1
            MainContentPanel.Children.Clear()
            Dim interviewPage As New InterviewPage()
            _page = interviewPage
            MainContentPanel.Children.Add(interviewPage)
        End Sub

        Private Sub LeaveButton_Click(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            _selector = 1
            MainContentPanel.Children.Clear()
            Dim leavePage As New LeavePage()
            _page = leavePage
            MainContentPanel.Children.Add(leavePage)
        End Sub

        Private Sub SalaryButton_Click(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            '_selector = 1
            MainContentPanel.Children.Clear()
            Dim salaryPage As New SalaryPage()
            '_page = salaryPage
            MainContentPanel.Children.Add(salaryPage)
        End Sub

    End Class
End Namespace
