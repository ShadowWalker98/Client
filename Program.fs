// For more information see https://aka.ms/fsharp-console-apps
module ClientTester

open System.Net
open System.Net.Sockets

[<Literal>]
let REMOTE_IP = "192.168.0.143"

[<Literal>]
let LOCAL_IP = "192.168.0.164"

let message = "Hello, I'm Satyajit!"

let messageBytes = System.Text.Encoding.ASCII.GetBytes(message)

let clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)

let endPoint = IPEndPoint(IPAddress.Parse(LOCAL_IP), 3314)

let remoteEndPoint = IPEndPoint(IPAddress.Parse(REMOTE_IP), 1234)

clientSocket.Bind(endPoint)
clientSocket.Connect(remoteEndPoint)
printfn $"Is it connected to the remote endpoint?: %s{clientSocket.Connected.ToString()}"
let bytesSentResponse = clientSocket.Send(messageBytes)
printfn $"Number of bytes in the message: %d{messageBytes.Length}"
printfn $"Number of bytes sent: %d{bytesSentResponse}"

let bytesResponse = [|for i in 0..256 -> byte(i)|]
let responseBytes = clientSocket.Receive(bytesResponse)
printfn $"Number of bytes received from server: %d{responseBytes}"
printfn $"Message from server: %s{System.Text.Encoding.ASCII.GetString bytesResponse[0 .. responseBytes - 1]}"





