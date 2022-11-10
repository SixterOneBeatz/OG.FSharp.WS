namespace OG.FSharp.Domain.Models

open System

module rec Person =

    [<CLIMutable>]
    type Person = {
        PersonId: int
        Discriminator: string
        EnrollmentDate: DateTime option
        FirstName: string
        HireDate: DateTime option
        LastName: string
    }