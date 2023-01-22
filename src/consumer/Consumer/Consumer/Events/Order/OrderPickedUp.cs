using Consumer.Services;

namespace Consumer.Events.Order;

public class OrderPickedUp : BaseEvent
{
    public string CouierId { get; set; }
    public DateTime PickedUpDateTime { get; set; }
}