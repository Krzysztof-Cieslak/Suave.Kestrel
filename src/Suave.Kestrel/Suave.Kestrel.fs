module Suave.Kestrel

open System
open System.IO
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Http.Extensions
open Microsoft.Extensions.Primitives
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
        let res = ctx |> toSuaveContext |> endpoint |> Async.RunSynchronously
        let ctx', body  =
            match res with
            | None -> setSuaveResponse ctx Suave.Http.HttpResult.empty
            | Some r -> setSuaveResponse ctx r.response
        ctx'.Response.WriteAsync body