namespace Bankomatas
{
    public class Card
    {
        public Guid ID { get; set; }
        public string Pin { get; set; }
        public double Money { get; set; }
        public int CashOutSum { get; set; }
        public int CashOutAttemps { get; set; }
        public List<double> Transactions { get; set; }
    }
}
