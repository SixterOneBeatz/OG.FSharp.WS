namespace OG.FSharp.API.Controllers

open Microsoft.AspNetCore.Mvc
open OG.FSharp.Domain.Models.Person
open OG.FSharp.Application.Common.Repositories

[<ApiController>]
[<Route("[controller]")>]
type PersonController (repo: IPersonRepository) = 
    inherit ControllerBase()

    [<HttpGet>]
    member this.GetAll() =
        let persons =  repo.GetAll()

        if (box persons = null)
        then this.NotFound() :> IActionResult
        else this.Ok persons :> IActionResult

    [<HttpGet>]
    [<Route("{id}")>]
    member this.Get(id: int) =
        match id with 
        | 0 -> this.BadRequest() :> IActionResult
        | _ ->
            if repo.Exists id
            then repo.Get id |> this.Ok  :> IActionResult
            else this.Conflict id :> IActionResult

    [<HttpPost>]
    member this.Post(person: Person) : IActionResult = 

        match person.PersonId with 
        | 0 -> repo.Add person |> this.Ok :> IActionResult
        | _ -> this.BadRequest() :> IActionResult

    [<HttpPut>]
    [<Route("{id}")>]
    member this.Put(id: int, [<FromBody>] person: Person) : IActionResult = 
        match person.PersonId with 
        | 0 -> this.BadRequest() :> IActionResult
        | _ ->
            if repo.Exists id
            then repo.Update person |> this.Ok  :> IActionResult
            else this.Conflict person :> IActionResult

    [<HttpDelete>]
    [<Route("{id}")>]
    member this.Delete(id: int) : IActionResult = 
        let person = repo.Get id

        match id with 
        | 0 -> this.BadRequest() :> IActionResult
        | _ ->
            match box(person) with
            | null -> this.Conflict person :> IActionResult        
            | _ -> repo.Delete person |> this.Ok :> IActionResult