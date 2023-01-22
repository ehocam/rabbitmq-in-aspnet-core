using MongoDB.Bson.Serialization.Attributes;

namespace Publisher.Model;

public class Order
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }
    public Customer Customer { get; set; }
    public Basket Basket { get; set; }
    public Payment Payment { get; set; }
    public DateTime CreatedDate { get; set; }
}