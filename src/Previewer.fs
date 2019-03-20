namespace VSCode.PlantUML.Fable

open Fable.Core
open Fable.Import
open Fable.Import.Node
open Fable.Import.Node.Stream
open Fable.Import.vscode
open Fable.Import.Browser
open System

module Stream =
    let waitForEnd (stream: Readable<string>) = 
        Promise.create (fun resolve reject ->
            stream.on("end", fun _ -> resolve ()) |> ignore
            stream.on("error", fun error -> reject error) |> ignore
        )

module Previewer = 

    let makeViewerForFile dataUri = 
        sprintf """
        <html>
          <body>
          <img src="%s"/>
          </body
        </html>
        """ dataUri

    let openFileInPreview filePath = 
        let previewPanel = vscode.window.createWebviewPanel(
                                "plantumlPreview",
                                sprintf "PlantUML preview - %s" filePath,
                                U2.Case1 vscode.ViewColumn.Two
                                )
        let fileContent = 
            filePath
            |> fs.readFileSync
            |> Globals.Buffer.from
            |> fun b -> b.toString "base64"
        
        let dataUri = 
            sprintf "data:image/png;base64,%s" fileContent

        previewPanel.webview.html <- makeViewerForFile dataUri                        
        ()

    let writeFile fileName content = 
        Promise.create (fun resolve reject -> 
            fs.writeFile(fileName, content, 
                fun error -> 
                    if isNull error 
                    then resolve ()  
                    else reject error
            )
        )

    let renderPreview _ = 
        printfn "rendering plantuml preview!"
        let fileName = vscode.window.activeTextEditor.document.fileName
        let response = PlantUML.generate fileName
        let targetfileName = "/tmp/plantuml.preview.png"
        let writeFileStream = fs.createWriteStream targetfileName
        printfn "starting stream"
        response.out.pipe (unbox writeFileStream) |> ignore
        promise {
            do! Stream.waitForEnd response.out
            printfn "stream done"
            openFileInPreview targetfileName
        }
        |> Promise.catch (fun error -> printfn "ERR: %A" error)
        |> box

    let activate _context =
        vscode.commands.registerCommand(
            "plantuml.preview",
            renderPreview
        ) |> ignore
        printfn "previewer activated"