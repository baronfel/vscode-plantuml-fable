module Fable.Import.Node.PlantUML

open Fable.Import.Node.Stream
open Fable.Core

type GenerateResponse = { out : Readable<string> }

[<Import("generate", "node-plantuml")>]
let generate (inputFile: string): GenerateResponse = jsNative