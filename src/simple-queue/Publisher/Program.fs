open System.Text
open System
open RabbitMQ.Client

[<EntryPoint>]
let main argv =

    let factory = ConnectionFactory(HostName = "localhost")
    let connection = factory.CreateConnection()
    let channel = connection.CreateModel()

    channel.QueueDeclare("hello",false,false,false, null) |> ignore

    let message = argv |> String.concat ","
    let body = Encoding.UTF8.GetBytes message

    let properties = channel.CreateBasicProperties();
    channel.BasicPublish("", "hello", properties, body)
    
    printfn " Press [enter] to exit."
    Console.ReadLine() |> ignore

    0 // exit code