module OG.FSharp.InfrastructureServiceRegistration

open Microsoft.Extensions.DependencyInjection
open OG.FSharp.Infrastructure.Context
open OG.FSharp.Application.Common.Repositories
open Microsoft.Extensions.Configuration
open Microsoft.EntityFrameworkCore
open OG.FSharp.Infrastructure.Repositories

type IServiceCollection with
    member x.AddInfrastructure(configuration: IConfiguration) : IServiceCollection =
           
           x.AddTransient<IPersonRepository, PersonRepository>() |> ignore

           x.AddDbContext<SchoolContext>(fun options -> 
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")) |> ignore) 