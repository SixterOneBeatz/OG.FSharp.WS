namespace OG.FSharp.Infrastructure.Context

open Microsoft.EntityFrameworkCore
open EntityFrameworkCore.FSharp.Extensions
open OG.FSharp.Domain.Models.Person

type SchoolContext =
    inherit DbContext

    new() = { inherit DbContext() }
    new(options : DbContextOptions<SchoolContext>) = { inherit DbContext(options) }

    [<DefaultValue>] val mutable private _People : DbSet<Person>
    member this.People
        with get() = this._People
        and set v = this._People <- v


    override this.OnModelCreating(modelBuilder: ModelBuilder) =
        base.OnModelCreating(modelBuilder)

        modelBuilder.Entity<Person>(fun entity ->

            entity.ToTable("Person")
                |> ignore

            entity.Property(fun e -> e.PersonId)
                .HasDefaultValue(0)
                .HasColumnName("PersonID")
                .HasColumnOrder(0)
                |> ignore

            entity.Property(fun e -> e.Discriminator)
                .HasMaxLength(50)
                .HasColumnOrder(5)
                |> ignore

            entity.Property(fun e -> e.EnrollmentDate)
                .HasColumnType("datetime")
                .HasColumnOrder(4)
                |> ignore

            entity.Property(fun e -> e.FirstName)
                .HasMaxLength(50)
                .HasColumnOrder(2)
                |> ignore

            entity.Property(fun e -> e.HireDate)
                .HasColumnType("datetime")
                .HasColumnOrder(3)
                |> ignore

            entity.Property(fun e -> e.LastName)
                .HasMaxLength(50)
                .HasColumnOrder(1)
                |> ignore
        ) |> ignore

        modelBuilder.RegisterOptionTypes()
