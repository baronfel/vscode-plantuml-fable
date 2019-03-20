module VSCode.PlantUML.Fable.Extension

open VSCode.PlantUML.Fable

let activate context = 
    Previewer.activate context
    printfn "VSCode.PlantUML.Fable activated"