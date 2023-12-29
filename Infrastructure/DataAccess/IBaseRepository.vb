Imports System.Linq.Expressions
Imports System.Runtime.InteropServices
Imports System.Threading.Tasks

Public Interface IBaseRepository(Of T, TKey As Structure)
    Function GetAllAsync() As Task(Of IEnumerable(Of T))
    Function GetByIdAsync(id As TKey) As Task(Of T)
    Function GetAsync(Optional ByVal predicate As Expression(Of Func(Of T, Boolean)) = Nothing, Optional includeList As List(Of String) = Nothing) As Task(Of T)
    Function GetListAsync(Optional ByVal predicate As Expression(Of Func(Of T, Boolean)) = Nothing, Optional includeList As List(Of String) = Nothing) As Task(Of IEnumerable(Of T))
    Function AddAsync(entity As T) As Task(Of T)
    Function UpdateAsync(entity As T) As Task(Of T)
    Function DeleteAsync(id As TKey) As Task(Of Boolean)
End Interface
