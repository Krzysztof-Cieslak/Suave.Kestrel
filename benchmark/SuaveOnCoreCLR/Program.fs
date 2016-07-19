﻿
open Suave                 // always open suave
open Suave.Successful      // for OK-result
open Suave.Web             // for config\
open Suave.Http

[<EntryPoint>]
let main argv =
    let config = {defaultConfig with bindings = [HttpBinding.mkSimple HTTP "127.0.0.1" 8081]}
    startWebServer config (OK "Hello World from CoreCLR!")
    0
