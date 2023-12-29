Imports System.ComponentModel

Public Class Interview
    Implements INotifyPropertyChanged
    Public Property Id As Long
    Public Property Interviewdate As Date

    Public Property Interviewnotes As String

    Public Property Interviewoutcome As String

    Public Property Candidateid As Long

    Public Property Interviewerid As Long

    Public Overridable Property Candidate As Candidate

    Public Overridable Property Interviewer As Employee

    'Private _candidateName As String
    'Public Property CandidateName As String
    '    Get
    '        Return _candidateName
    '    End Get
    '    Set(value As String)
    '        _candidateName = value
    '        OnPropertyChanged("CandidateName")
    '    End Set
    'End Property




    ' PropertyChanged olayını tetikleyen yardımcı metod
    Protected Sub OnPropertyChanged(ByVal name As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
    End Sub

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

End Class
