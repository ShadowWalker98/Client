// For more information see https://aka.ms/fsharp-console-apps
module ClientConnections
open ClientTester.SocketConnector
open ClientTester
open ClientTester.InputTest

[<EntryPoint>]
let main args =
   printfn "In the main function!"
   socketConnector
   0







