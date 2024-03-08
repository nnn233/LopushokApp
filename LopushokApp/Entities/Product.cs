namespace LopushokApp.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public int? TypeId { get; set; }
        public string Title { get; set; }
        public string? Image { get; set; }
        public string Article { get; set; }
        public decimal Cost { get; set; }
        public int? WorkShopNumber { get; set; }
        public int? PersonCount { get; set; }
        public string? Description { get; set; }
    }
}
