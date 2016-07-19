
open Suave                 // always open suave
open Suave.Successful      // for OK-result
open Suave.Web             // for config
open Suave.LibUv
[<EntryPoint>]
let main argv =
    let config = {defaultConfig 
                    with 
                        bindings = [HttpBinding.mkSimple HTTP "127.0.0.1" 8084]
                        tcpServerFactory = LibUvServerFactory()
                    }
    startWebServer config (OK "Hello World from Mono LibUv!")
    0
