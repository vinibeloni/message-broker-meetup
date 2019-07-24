open RabbitMQ.Client.Events
open RabbitMQ.Client
open System.Text
open System

[<EntryPoint>]
let main argv =
    let factory = ConnectionFactory(HostName = "localhost")
    use connection = factory.CreateConnection()
    use channel = connection.CreateModel()
    
    channel.QueueDeclare("hello", false, false, false, null) |> ignore
    
    let consumer = EventingBasicConsumer(channel)
    
    consumer.Received.Add(fun message ->
        message.Body
        |> Encoding.UTF8.GetString 
        |> printfn " Receive a message: %s"
    )
    
    channel.BasicConsume("hello", true, consumer) |> ignore

    printfn " Press [enter] to exit."
    Console.ReadLine() |> ignore

    0 // exit code