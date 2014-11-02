module etreporter
open canopy
open reporters
open System

type LogFileReporter (logFileTemplate : string) =
    
    let openOutputStream fileTemplate =
        let dtString = DateTime.Now.ToString "yyyy-MM-dd-HHmm"
        let outputFileName = logFileTemplate.Replace("{dt}", dtString)
        let outputFileInfo = new IO.FileInfo (outputFileName)
        if System.IO.Directory.Exists (outputFileInfo.Directory.FullName) = false then
            System.IO.Directory.CreateDirectory (outputFileInfo.Directory.FullName) |> ignore
        new System.IO.StreamWriter (outputFileInfo.FullName)

    let tw = openOutputStream logFileTemplate

    new() = LogFileReporter (@".\logs\run-{dt}.log")

    interface IReporter with               
        member this.pass () = tw.WriteLine " PASSED"

        member this.fail ex id ss =
            tw.WriteLine " FAILED"
            tw.WriteLine ("    " + ex.Message)
            tw.WriteLine "    stack:"
            ex.StackTrace.Split([| "\r\n"; "\n" |], StringSplitOptions.None)
            |> Array.iter (fun trace -> 
                if trace.Contains(".FSharp.") || trace.Contains("canopy.core") || trace.Contains("OpenQA.Selenium") 
                    || trace.Contains("canopy.runner") then
                    ()
                else
                    tw.WriteLine ("    " + trace)
                )

        member this.describe d = tw.WriteLine d
          
        member this.contextStart c = tw.WriteLine ("context \"" + c + "\"")

        member this.contextEnd c = ()

        member this.summary minutes seconds passed failed =
            tw.WriteLine ()
            tw.WriteLine ("{0} minutes {1} seconds to execute", minutes, seconds)
            tw.WriteLine ("{0} passed", passed)
            tw.WriteLine ("{0} failed", failed)
        
        member this.write w = tw.WriteLine ("    " + w)
        
        member this.suggestSelectors selector suggestions = ()

        member this.testStart id = tw.Write ("test \"" + id + "\"")
            
        member this.testEnd id = ()

        member this.quit () =
            tw.Close ()
            tw.Dispose ()
        
        member this.suiteBegin () = ()

        member this.suiteEnd () = ()

        member this.coverage url ss = ()

        member this.todo () = ()

        member this.skip () = ()


type TeeReporter(reporter1 : IReporter, reporter2 : IReporter) =
    let _reporter1 = reporter1
    let _reporter2 = reporter2

    interface IReporter with               
        member this.pass () =
            _reporter1.pass ()
            _reporter2.pass ()

        member this.fail ex id ss =
            _reporter1.fail ex id ss
            _reporter2.fail ex id ss

        member this.describe d = 
            _reporter1.describe d
            _reporter2.describe d
          
        member this.contextStart c = 
            _reporter1.contextStart c
            _reporter2.contextStart c

        member this.contextEnd c = 
            _reporter1.contextEnd c
            _reporter2.contextEnd c

        member this.summary minutes seconds passed failed =  
            _reporter1.summary minutes seconds passed failed
            _reporter2.summary minutes seconds passed failed
        
        member this.write w = 
            _reporter1.write w
            _reporter2.write w
        
        member this.suggestSelectors selector suggestions = 
            _reporter1.suggestSelectors selector suggestions
            _reporter2.suggestSelectors selector suggestions

        member this.testStart id = 
            _reporter1.testStart id
            _reporter2.testStart id
            
        member this.testEnd id = 
            _reporter1.testEnd id
            _reporter2.testEnd id

        member this.quit () = 
            _reporter1.quit ()
            _reporter2.quit ()
        
        member this.suiteBegin () = 
            _reporter1.suiteBegin ()
            _reporter2.suiteBegin ()

        member this.suiteEnd () = 
            _reporter1.suiteEnd ()
            _reporter2.suiteEnd ()

        member this.coverage url ss = 
            _reporter1.coverage url ss
            _reporter2.coverage url ss

        member this.todo () = 
            _reporter1.todo ()
            _reporter2.todo ()

        member this.skip () = 
            _reporter1.skip ()
            _reporter2.skip ()

