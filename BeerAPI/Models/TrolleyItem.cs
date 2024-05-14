namespace BeerAPI.Models
{
    public class TrolleyItem
    {
        public int BeerId { get; set; }
        public int Quantity { get; set; }
        public Beer Beer {  get; set; }
    }
}
