namespace OG.FSharp.API.Controllers

open Microsoft.AspNetCore.Mvc
open OG.FSharp.API.Context
open OG.FSharp.API.Domain.SchoolDomain
open System.Linq

[<ApiController>]
[<Route("[controller]")>]
type PersonController (ctx: SchoolContext) = 
    inherit ControllerBase()

    [<HttpGet>]
    member this.Get() =
        let persons =  ctx.People

        if (box persons = null)
        then this.NotFound() :> IActionResult
        else this.Ok persons :> IActionResult

    [<HttpPost>]
    member this.Post(person: Person) : IActionResult = 

        let addPerson(person: Person) : Person = 
            ctx.People.Add(person) |> ignore
            ctx.SaveChanges() |> ignore
            ctx.People.First(fun p -> p = person)

        match person.PersonId with 
        | 0 -> addPerson person |> this.Ok :> IActionResult
        | _ -> this.BadRequest() :> IActionResult

    [<HttpPut>]
    [<Route("{id}")>]
    member this.Put(id: int, [<FromBody>] person: Person) : IActionResult = 

        let updatePerson () : Person = 
            ctx.People.Update(person) |> ignore
            ctx.SaveChanges() |> ignore
            person

        match person.PersonId with 
        | 0 -> this.BadRequest() :> IActionResult
        | _ ->
            if ctx.People.Any(fun p -> p.PersonId = id)
            then updatePerson() |> this.Ok  :> IActionResult
            else this.Conflict person :> IActionResult

    [<HttpDelete>]
    [<Route("{id}")>]
    member this.Delete(id: int) : IActionResult = 

        let deletePerson(person: Person) : Person = 
            ctx.People.Remove(person) |> ignore
            ctx.SaveChanges() |> ignore
            person

        let person = this.GetPerson id

        match id with 
        | 0 -> this.BadRequest() :> IActionResult
        | _ ->
            match box(person) with
            | null -> this.Conflict person :> IActionResult        
            | _ -> deletePerson person |> this.Ok :> IActionResult
                
    member this.GetPerson(id: int) : Person = 
        ctx.People.FirstOrDefault(fun p -> p.PersonId = id)