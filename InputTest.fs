module ClientTester.InputTest

    open System

    let inputHandler =
        printfn "Testing console input!"
        printfn "Please enter a number below: "
        System.Console.Title <- "Input Testing"
        System.Console.ReadLine