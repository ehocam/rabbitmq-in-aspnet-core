using Consumer.Services;

namespace Consumer.Messages.Order;

public class BaseOrderMessage
{
    public string CorrelationId { get; set; }
    public string OrderId { get; set; }
    public IList<BasketItem> BasketItems { get; set; }
    public DateTime CreateDateTimeDate { get; set; }
}