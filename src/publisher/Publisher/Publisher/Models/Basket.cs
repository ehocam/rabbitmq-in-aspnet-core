using System.Net.Http.Headers;

namespace Publisher.Model;

public class Basket
{
    // common basket properties
    public IList<Product> Products { get; set; }
}