module ClientTester.InputHandler
    
    [<Literal>]
    let operationPrompt = "Please enter operation followed by operands as arguments below: "
    
    let inputHandler (message: string) : string =
        printfn $"%s{message}"
        let input = System.Console.ReadLine()
        input
        
        
        
        
        
        