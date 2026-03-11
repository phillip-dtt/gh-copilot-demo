namespace albums_api.Models
{
    public record Album(int Id, string Title, string Artist, decimal Price, string Image_url)
    {
        public static List<Album> GetAll()
        {
            return new List<Album>()
            {
                new Album(1, "You, Me and an App Id", "Daprize", 10.99m, "https://aka.ms/albums-daprlogo"),
                new Album(2, "Seven Revision Army", "The Blue-Green Stripes", 13.99m, "https://aka.ms/albums-containerappslogo"),
                new Album(3, "Scale It Up", "KEDA Club", 13.99m, "https://aka.ms/albums-kedalogo"),
                new Album(4, "Lost in Translation", "MegaDNS", 12.99m, "https://aka.ms/albums-envoylogo"),
                new Album(5, "Lock Down Your Love", "V is for VNET", 12.99m, "https://aka.ms/albums-vnetlogo"),
                new Album(6, "Sweet Container O' Mine", "Guns N Probeses", 14.99m, "https://aka.ms/albums-containerappslogo")
            };
        }

        public static Album? GetById(int id)
        {
            return GetAll().FirstOrDefault(a => a.Id == id);
        }
    }
}
