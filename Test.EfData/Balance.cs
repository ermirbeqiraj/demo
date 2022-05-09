namespace Test.EfData
{
    public class Balance
    {
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
