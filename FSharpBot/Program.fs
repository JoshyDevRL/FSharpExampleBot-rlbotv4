namespace global
open System
namespace FSHARPBOT
type Program() =
    static member Main(args : string[]) = 
        let mutable (port : int) = Unchecked.defaultof<int>
        try
            port <- int (args.[0])
        with
            :? Exception as e -> 
                let mutable currentColor = Console.ForegroundColor
                Console.ForegroundColor <- ConsoleColor.Red
                Console.WriteLine ("Could not get port from arguments to C# bot!\n" + "If you're reading this message, it means that the C# bot did not receive a valid port in the command line " + "arguments.\n" + "If you have configured auto-run, the port should be given to the bot automatically. Otherwise, you'll " + "need to run the bot with the port every time (e.g. Bot.exe 45031). Note that this port should match the " + "one in PythonAgent/PythonAgent.py.\n" + "If you're trying to run the bot without auto-run in an IDE, see this source file " + "(CSharpBot/Bot/Program.cs) for IDE instructions.")
                Console.ForegroundColor <- currentColor
                reraise ()
        let mutable (botManager : RLBotDotNet.BotManager<Bot>) = new RLBotDotNet.BotManager<Bot>(0)
        botManager.Start (port)
module Program__run =
    [<EntryPoint>]
    let main args = 
        Program.Main (args)
        0