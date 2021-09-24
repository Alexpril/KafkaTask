namespace KafkaAPI
{
    public enum Categories
    {
        Electronics,
        Grocery,
        Snacks,
        Drinks,
        Furniture
    }

    public enum Results 
    {
        Success,
        Failed
    }
    public class Product
    {
        public string Name { get; set; }
        public Categories Category { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public Results Result { get; set; }
    }
}
