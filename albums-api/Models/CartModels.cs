namespace albums_api.Models
{
    public record CartItem(int AlbumId, string Title, string Artist, decimal Price, string ImageUrl);

    public record Cart(string CartId, List<CartItem> Items)
    {
        public decimal Total => Items.Sum(i => i.Price);
    }
}
