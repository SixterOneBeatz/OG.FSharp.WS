namespace OG.FSharp.Infrastructure.Repositories

open OG.FSharp.Application.Common.Repositories
open OG.FSharp.Infrastructure.Context
open OG.FSharp.Domain.Models.Person
open System.Linq

type PersonRepository(ctx: SchoolContext) = 
    interface IPersonRepository with

        member _.Get(id : int) = ctx.People.FirstOrDefault(fun p -> p.PersonId = id)

        member _.GetAll() = 
            List.ofSeq(ctx.People.ToList())

        member _.Add(person: Person) = 
            ctx.People.Add(person) |> ignore
            ctx.SaveChanges() |> ignore
            ctx.People.First(fun p -> p = person)

        member _.Delete(person: Person) = 
            ctx.People.Remove(person) |> ignore
            ctx.SaveChanges() |> ignore
            person

        member _.Update(person: Person) = 
            ctx.People.Update(person) |> ignore
            ctx.SaveChanges() |> ignore
            person

        member _.Exists(id: int) = 
            ctx.People.Any(fun x -> x.PersonId = id)
       
    