namespace VintageGroupCar.Models
{
    public class Raduno
    {
        public int Id { get; set; }
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public DateTime DataEvento { get; set; }
        public byte[] Immagine { get; set; }
    }
}
