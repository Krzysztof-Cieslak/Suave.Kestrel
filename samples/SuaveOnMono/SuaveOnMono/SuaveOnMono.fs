
open Suave                 // always open suave
open Suave.Successful      // for OK-result
open Suave.Web             // for config

[<EntryPoint>]
let main argv =

    startWebServer defaultConfig (OK "Hello World from Mono!")
    0
