namespace OG.FSharp.API
#nowarn "20"
open Microsoft.AspNetCore.Builder
open Microsoft.EntityFrameworkCore;
open Microsoft.Extensions.Configuration;
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open OG.FSharp.InfrastructureServiceRegistration
open OG.FSharp.ApplicationServiceRegistration

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let builder = WebApplication.CreateBuilder(args)

        let configuration = builder.Configuration

        builder.Services.AddControllers()
        builder.Services.AddApplication()
        builder.Services.AddInfrastructure(configuration)

        let app = builder.Build()

        app.UseHttpsRedirection()
        app.UseCors(fun cors -> 
                        cors.AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin() 
                        |> ignore)
        app.UseAuthorization()
        app.MapControllers()

        app.Run()

        exitCode
