namespace OG.FSharp.API.Domain

open System

module rec SchoolDomain =

    [<CLIMutable>]
    type Person = {
        PersonId: int
        mutable Discriminator: string
        mutable EnrollmentDate: DateTime option
        mutable FirstName: string
        mutable HireDate: DateTime option
        mutable LastName: string
    }

