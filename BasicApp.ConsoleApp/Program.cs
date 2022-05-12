// See https://aka.ms/new-console-template for more information
using System.Collections;
using IBM.WMQ;

Console.WriteLine("Hello, World!");

var counter = 0;

var max = args.Length != 0 ? Convert.ToInt32(args[0]) : -1;

while (max == -1 || counter < max)
{
    string message = $"Counter: {++counter}";
    Console.WriteLine(message);
    AddMessageToQueue(message);
    await Task.Delay(TimeSpan.FromMilliseconds(1_000));
}

void AddMessageToQueue(string message)
{
    try
    {
        var properties = new Hashtable
        {
            { MQC.HOST_NAME_PROPERTY, "host.minikube.internal" },
            { MQC.PORT_PROPERTY, 1414 },
            { MQC.CHANNEL_PROPERTY, "DEV.APP.SVRCONN" },
            { MQC.TRANSPORT_PROPERTY, MQC.TRANSPORT_MQSERIES_MANAGED },
            { MQC.CONNECT_OPTIONS_PROPERTY, MQC.MQCNO_RECONNECT_Q_MGR },
            { MQC.USER_ID_PROPERTY, "mqdev" },
            { MQC.PASSWORD_PROPERTY, "Pass1234" },
        };

        using var queueManager = new MQQueueManager("QM1", properties);
        using (var queue = queueManager.AccessQueue("DEV.QUEUE.1", MQC.MQOO_OUTPUT + MQC.MQOO_FAIL_IF_QUIESCING))
        {
            var sendmsg = new MQMessage
            {
                Format = MQC.MQFMT_STRING,
                MessageType = MQC.MQMT_DATAGRAM,
                MessageId = MQC.MQMI_NONE,
                CorrelationId = MQC.MQCI_NONE
            };

            sendmsg.WriteString(message);
            queue.Put(sendmsg);
            Console.WriteLine("Put: " + message);
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.ToString());
    }
}