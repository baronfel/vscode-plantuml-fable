namespace VSCode.PlantUML.Fable

open Ionide.VSCode.Helpers
open Fable.Import

module Previewer = 

    let renderPreview _ = 
        printfn "rendering plantuml preview!"
        unbox ()

    let activate _context =
        vscode.commands.registerCommand(
            "plantuml.preview",
            renderPreview
        ) |> ignore
        printfn "previewer activated"