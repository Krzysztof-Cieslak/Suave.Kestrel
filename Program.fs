module Main

open System
open System.IO
open System.Text
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Http.Extensions
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.Primitives

module Helpers  =
    open Suave.Cookie

    type SuaveContext = Suave.Http.HttpContext
    let empty = Suave.Http.HttpContext.empty

    let toSuaveContext (ctx : HttpContext) : SuaveContext =
        let headers =
            ctx.Request.Headers
            |> Seq.map (fun h -> h.Key, h.Value |> Seq.map (fun s -> s) |> String.concat " ")
            |> Seq.toList

        let form =
            use ms = new MemoryStream()
            ctx.Request.Body.CopyTo ms
            ms.ToArray ()

        let req = {
            Suave.Http.HttpRequest.empty with
                httpVersion = "HTTP/1.1"
                url = Uri <| ctx.Request.GetEncodedUrl()
                host = ctx.Request.Host.Value
                ``method`` = Suave.Http.HttpMethod.parse <| ctx.Request.Method
                headers = headers
                rawForm = form
                rawQuery = ctx.Request.QueryString.Value

        }
        { empty with request = req }

    let setSuaveResponse (ctx : HttpContext) (resp : Suave.Http.HttpResult) =

        resp.headers
        |> Seq.iter (fun (k,v) -> ctx.Response.Headers.Add(k,  StringValues v))
        ctx.Response.Headers.Add("Suave", "OK" |> StringValues)

        ctx.Response.StatusCode <- resp.status.code
        let body =
            match resp.content with
            | Suave.Http.Bytes  b ->
                ctx.Response.Headers.["Content-Length"] <- StringValues (string b.Length)
                ctx.Response.Headers.["Content-Type"] <- StringValues "text/plain"
                System.Text.Encoding.UTF8.GetString b

            | _ -> ""


        ctx, body

type SuaveMiddleware (next : RequestDelegate, endpoint : Suave.Http.WebPart) =
    member this.Invoke(ctx : HttpContext) =
        let res = ctx |> Helpers.toSuaveContext |> endpoint |> Async.RunSynchronously
        let ctx', body  =
            match res with
            | None -> Helpers.setSuaveResponse ctx Suave.Http.HttpResult.empty
            | Some r -> Helpers.setSuaveResponse ctx r.response
        ctx'.Response.WriteAsync body


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
