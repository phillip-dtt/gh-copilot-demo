namespace albums_api.Models
{
    public record CartItem(int AlbumId, string Title, string Artist, double Price, string ImageUrl);

    public record Cart(string CartId, List<CartItem> Items)
    {
        public double Total => Items.Sum(i => i.Price);
    }
}
