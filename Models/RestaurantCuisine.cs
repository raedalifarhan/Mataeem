namespace Mataeem.Models
{
    public class RestaurantCuisine
    {
        public Guid RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        public Guid CuisineId { get; set; }
        public Cuisine Cuisine { get; set; }
    }
}
