using Consumer.Services;

namespace Consumer.Events.Order;

public class OrderReceived : BaseEvent
{
    public DateTime CheckoutDate { get; set; }
}