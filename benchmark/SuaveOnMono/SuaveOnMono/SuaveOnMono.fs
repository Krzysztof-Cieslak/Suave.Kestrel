
open Suave                 // always open suave
open Suave.Successful      // for OK-result
open Suave.Web             // for config

[<EntryPoint>]
let main argv =
    let config = {defaultConfig with bindings = [HttpBinding.mkSimple HTTP "127.0.0.1" 8083]}
  
    startWebServer config (OK "Hello World from Mono!")
    0
