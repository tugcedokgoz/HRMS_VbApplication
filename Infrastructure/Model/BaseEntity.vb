Public Class BaseEntity(Of TKey As {Structure, IEquatable(Of TKey)})
    Public Property Id As TKey
    Public Property Isactive As Boolean

End Class
