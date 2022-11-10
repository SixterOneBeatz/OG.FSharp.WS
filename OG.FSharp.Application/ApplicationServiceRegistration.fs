module OG.FSharp.ApplicationServiceRegistration

open Microsoft.Extensions.DependencyInjection

type IServiceCollection with
    member x.AddApplication() : IServiceCollection =
           x