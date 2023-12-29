Imports System.Linq.Expressions
Imports System.Threading.Tasks
Imports Microsoft.EntityFrameworkCore

Public Class BaseRepository(Of T As BaseEntity(Of TKey), TKey As {Structure, IEquatable(Of TKey)}, TContext As DbContext)
    Implements IBaseRepository(Of T, TKey)


    Private ReadOnly _context As TContext
    Private ReadOnly _dbSet As DbSet(Of T)

    Public Sub New(context As TContext)
        _context = context
        _dbSet = context.Set(Of T)
    End Sub

    Public Async Function AddAsync(entity As T) As Task(Of T) Implements IBaseRepository(Of T, TKey).AddAsync
        Dim newEntity = Await _context.Set(Of T)().AddAsync(entity)
        newEntity.Entity.Isactive = True
        Await _context.SaveChangesAsync()
        Return newEntity.Entity
    End Function

    Public Async Function DeleteAsync(id As TKey) As Task(Of Boolean) Implements IBaseRepository(Of T, TKey).DeleteAsync
        Dim entity = Await _context.Set(Of T)().Where(Function(e) e.Isactive = True).FirstOrDefaultAsync(Function(e) e.Id.Equals(id))
        If entity IsNot Nothing Then
            entity.Isactive = False
            Await _context.SaveChangesAsync()
            Return True
        End If
        Return False
    End Function

    Public Async Function GetAllAsync() As Task(Of IEnumerable(Of T)) Implements IBaseRepository(Of T, TKey).GetAllAsync
        Return Await _context.Set(Of T)().Where(Function(e) e.Isactive = True).ToListAsync()
    End Function

    Public Async Function GetByIdAsync(id As TKey) As Task(Of T) Implements IBaseRepository(Of T, TKey).GetByIdAsync
        Return Await _context.Set(Of T)().FirstOrDefaultAsync(Function(e) e.Isactive AndAlso e.Id.Equals(id))

    End Function

    Public Async Function UpdateAsync(entity As T) As Task(Of T) Implements IBaseRepository(Of T, TKey).UpdateAsync
        Dim updated = _context.Set(Of T)().Update(entity)
        Await _context.SaveChangesAsync()
        Return updated.Entity
    End Function

    Public Async Function GetAsync(Optional predicate As Expression(Of Func(Of T, Boolean)) = Nothing, Optional includeList As List(Of String) = Nothing) As Task(Of T) Implements IBaseRepository(Of T, TKey).GetAsync
        Dim dbSet = _context.Set(Of T)()
        Dim queryable As IQueryable(Of T) = dbSet

        If includeList IsNot Nothing Then
            For Each include In includeList
                queryable = queryable.Include(include)
            Next
        End If

        If predicate IsNot Nothing Then
            queryable = queryable.Where(predicate)
        End If

        Return Await queryable.FirstOrDefaultAsync()
    End Function

    Public Async Function GetListAsync(Optional predicate As Expression(Of Func(Of T, Boolean)) = Nothing, Optional includeList As List(Of String) = Nothing) As Task(Of IEnumerable(Of T)) Implements IBaseRepository(Of T, TKey).GetListAsync
        Dim dbSet = _context.Set(Of T)()
        Dim queryable As IQueryable(Of T) = dbSet

        If predicate IsNot Nothing Then
            queryable = queryable.Where(predicate)
        End If
        If includeList IsNot Nothing Then
            For Each include In includeList
                queryable = queryable.Include(include)
            Next
        End If
        Return Await queryable.ToListAsync()
    End Function
End Class
