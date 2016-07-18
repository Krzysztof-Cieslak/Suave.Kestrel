module Main

open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting

open Suave.Kestrel

type Startup() =
    member this.Configure (app : IApplicationBuilder) : unit=
        app.UseMiddleware<SuaveMiddleware>(App.endpoint) |> ignore

[<EntryPoint>]
let main argv =
    WebHostBuilder()
        .UseKestrel()
        .UseStartup<Startup>()
        .Build()
        .Run()


    0 // return an integer exit code
