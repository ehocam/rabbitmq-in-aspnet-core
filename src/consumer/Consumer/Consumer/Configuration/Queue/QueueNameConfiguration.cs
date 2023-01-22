using MassTransit;

namespace Consumer.Configuration.Queue;

public class ConsumerEnvironmentSetup : IEntityNameFormatter
{
    private readonly string _queueName;

    public ConsumerEnvironmentSetup(string QueueName)
    {
        _queueName = QueueName;
    }
    
    public string FormatEntityName<T>()
    {
        return "Q_" + _queueName;
    }
}