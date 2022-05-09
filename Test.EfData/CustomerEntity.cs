namespace Test.EfData
{
    public class Customer
    {
        public string IdCard { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Id { get; set; }

        public virtual Balance Balance { get; set; }
    }
}
