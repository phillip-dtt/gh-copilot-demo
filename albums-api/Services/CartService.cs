using albums_api.Models;

namespace albums_api.Services
{
    public class CartService
    {
        private readonly Dictionary<string, List<int>> _carts = new();

        public Cart GetCart(string cartId)
        {
            var albumIds = GetOrCreateCart(cartId);
            var allAlbums = Album.GetAll();
            var items = albumIds
                .Select(id => allAlbums.FirstOrDefault(a => a.Id == id))
                .Where(a => a != null)
                .Select(a => new CartItem(a!.Id, a.Title, a.Artist, a.Price, a.Image_url))
                .ToList();

            return new Cart(cartId, items);
        }

        public Cart AddItem(string cartId, int albumId)
        {
            var albumIds = GetOrCreateCart(cartId);
            if (!albumIds.Contains(albumId))
                albumIds.Add(albumId);

            return GetCart(cartId);
        }

        public Cart RemoveItem(string cartId, int albumId)
        {
            var albumIds = GetOrCreateCart(cartId);
            albumIds.Remove(albumId);
            return GetCart(cartId);
        }

        private List<int> GetOrCreateCart(string cartId)
        {
            if (!_carts.TryGetValue(cartId, out var list))
            {
                list = new List<int>();
                _carts[cartId] = list;
            }
            return list;
        }
    }
}
