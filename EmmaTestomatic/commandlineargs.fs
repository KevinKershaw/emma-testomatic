module commandlineargs

type Browser =
    | Firefox
    | Chrome
    | IE

type CommandLineArgs () = class
    member val browser : Browser = Firefox with get, set
    member val pressEnter : bool = false with get, set
    member val warmUp : bool = true with get, set
    member val showUsage : bool = false with get, set
    member val isValid : bool = true with get, set
    member val isDevMode : bool = false with get, set
end

let parseCommandLine (args: string[]) =
    let cla = new CommandLineArgs()
    for item in args do
        match item.ToLower() with
            | "-firefox" -> cla.browser <- Firefox
            | "-chrome" -> cla.browser <- Chrome
            | "-ie" -> cla.browser <- IE
            | "-pressenter" -> cla.pressEnter <- true
            | "-nopressenter" -> cla.pressEnter <- false
            | "-warmup" -> cla.warmUp <- true
            | "-nowarmup" -> cla.warmUp <- false
            | "-dev" -> cla.isDevMode <- true
            | "-help" | "-?" -> cla.showUsage <- true
            | other -> cla.isValid <- false
    cla

