namespace FunFactAPI.Models
{
    public class FunFact
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Fact { get; set; }
        public string Source { get; set; }
        public string Character { get; set; }
        public int Votes { get; set; } = 0;
    }
}
