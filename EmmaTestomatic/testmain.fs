module testmain
open canopy
open runner
open etconfig
open warmup
open System

[<EntryPoint>]
let main argv =
    publicportaltests.all ()
    submissiontests.all ()
    start firefox
    warmupEmma ()
    warmupDataport ()
    run()
    printfn "press [enter] to exit"
    System.Console.ReadLine() |> ignore
    quit()
    0
