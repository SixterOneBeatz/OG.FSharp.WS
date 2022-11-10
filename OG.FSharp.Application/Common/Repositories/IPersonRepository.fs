namespace OG.FSharp.Application.Common.Repositories

open OG.FSharp.Domain.Models.Person

type IPersonRepository =
    abstract member Get : int -> Person
    abstract member GetAll : unit -> List<Person>
    abstract member Add : Person -> Person
    abstract member Update : Person -> Person
    abstract member Delete: Person -> Person
    abstract member Exists: int -> bool