module ClientTester.SocketConnector

open System
open System.Net
open System.Net.Sockets
open ClientTester.InputHandler

[<Literal>]
let REMOTE_IP = "192.168.0.143"

[<Literal>]
let LOCAL_IP = "192.168.0.164"

let sendMessage (clientSocket : Socket) (message: string) : unit =
   let messageBytes = System.Text.Encoding.ASCII.GetBytes(message.Replace(char(7), char(21)))
   printfn $"%c{char(7)}"
   let bytesSentResponse = clientSocket.Send(messageBytes)
   printfn $"Number of bytes sent: %d{bytesSentResponse}"
   
let receiveMessage (clientSocket : Socket) : string =
   let bytesResponse = [|for i in 0..256 -> byte(i)|]
   let responseBytes = clientSocket.Receive(bytesResponse)
   printfn $"Message from server: %s{System.Text.Encoding.ASCII.GetString bytesResponse[0 .. responseBytes - 1]}"
   let messageFromServer = System.Text.Encoding.ASCII.GetString bytesResponse[0 .. responseBytes - 1]
   messageFromServer
   
let serverConnectHandshake (clientSocket : Socket): bool =
   let initMessage = receiveMessage clientSocket
   initMessage.Equals "Hello!"

let socketSetup : Socket =
   let clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
   let endPoint = IPEndPoint(IPAddress.Loopback, 3314)
   let remoteEndPoint = IPEndPoint(IPAddress.Loopback, 1234)
   clientSocket.Bind(endPoint)
   clientSocket.Connect(remoteEndPoint)
   printfn $"Is it connected to the remote endpoint?: %s{clientSocket.Connected.ToString()}"
   clientSocket
   
let socketHandler =
   let clientSocket = socketSetup
   if serverConnectHandshake clientSocket then
      printfn $"Server acknowledged, proceeding to operations!"
   else
      printfn $"Server didn't respond correctly, exiting..."
      clientSocket.Close()
      
   // keep the connection open until the user asks to disconnect
   
   let mutable closeConnection : bool = false
   let mutable command = ""
   while closeConnection = false do
      command <- inputHandler operationPrompt
      sendMessage clientSocket command
      let message = receiveMessage clientSocket
      printfn "End of loop"
      
