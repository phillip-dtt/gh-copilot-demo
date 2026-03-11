using System.Collections.Concurrent;
using albums_api.Models;

namespace albums_api.Services
{
    public class CartService
    {
        // Thread-safe outer dictionary; inner HashSet guarded by lock per cart
        private readonly ConcurrentDictionary<string, HashSet<int>> _carts = new();

        // Album lookup cached once at startup — avoids repeated GetAll() calls
        private static readonly Dictionary<int, Album> _albumCache =
            Album.GetAll().ToDictionary(a => a.Id);

        public Cart GetCart(string cartId)
        {
            var albumIds = GetOrCreateCart(cartId);
            List<CartItem> items;
            lock (albumIds)
            {
                items = albumIds
                    .Where(id => _albumCache.ContainsKey(id))
                    .Select(id => _albumCache[id])
                    .Select(a => new CartItem(a.Id, a.Title, a.Artist, a.Price, a.Image_url))
                    .ToList();
            }
            return new Cart(cartId, items);
        }

        // Returns null when albumId does not exist in the catalog
        public Cart? AddItem(string cartId, int albumId)
        {
            if (!_albumCache.ContainsKey(albumId))
                return null;

            var albumIds = GetOrCreateCart(cartId);
            lock (albumIds)
            {
                albumIds.Add(albumId); // HashSet silently ignores duplicates
            }
            return GetCart(cartId);
        }

        public Cart RemoveItem(string cartId, int albumId)
        {
            var albumIds = GetOrCreateCart(cartId);
            lock (albumIds)
            {
                albumIds.Remove(albumId);
            }
            return GetCart(cartId);
        }

        private HashSet<int> GetOrCreateCart(string cartId) =>
            _carts.GetOrAdd(cartId, _ => new HashSet<int>());
    }
}
