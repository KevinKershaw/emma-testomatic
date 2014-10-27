module testmain
open canopy
open canopy.configuration
open runner
open etconfig
open commandlineargs
open warmup
open System

let usage = "usage: EmmaTestomatic args\n" +
            "where args is a combination of the following:\n" +
            "   -Firefox (use firefox browser*)\n" +
            "   -Chrome (use chrome browser)\n" +
            "   -IE (use internet explorer browser)\n" +
            "   -PressEnter (require entering return key at end of tests)\n" +
            "   -NoPressEnter (do NOT require entering return key at end of tests*)\n" +
            "   -WarmUp (run prepare both emma and dataport for testing*)\n" +
            "   -NoWarmUp (do NOT prepare both emma and dataport for testing)\n" +
            "   -Dev (run only the dev test suite)\n" +
            "all parameters are not case sensitive\n" +
            "* default"

[<EntryPoint>]
let main argv =
    Console.WriteLine("EmmmaTestomatic version " + versioninfo.versionString)
    chromeDir <- (AppDomain.CurrentDomain.BaseDirectory + @"\BrowserSupport")
    ieDir <- (AppDomain.CurrentDomain.BaseDirectory + @"\BrowserSupport")
    let cla = parseCommandLine argv
    if cla.showUsage || not cla.isValid then
        System.Console.WriteLine(usage)
        -1
    else
        if cla.isDevMode then
            devtests.all ()
        else
            publicportaltests.all ()
            submissiontests.all ()
        match cla.browser with
            | Firefox -> start firefox
            | Chrome -> start chrome
            | IE -> start ie
        if cla.warmUp then
            warmupEmma ()
            warmupDataport ()
        run()
        if cla.pressEnter then
            printfn "press [enter] to exit"
            System.Console.ReadLine() |> ignore
        quit()
        0
