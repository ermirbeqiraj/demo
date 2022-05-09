using System;

namespace BusinessLogic.Exceptions
{
    public abstract class DomainException : Exception
    {
        public abstract string DomainMessage { get; protected set; }
    }

    public class CustomerNotFoundException : DomainException
    {
        public override string DomainMessage { get; protected set; }
        public CustomerNotFoundException() : base()
        {
            DomainMessage = "The customer you're asking for is not here, or he never was \\--/";
        }
    }

    public class DepositMinimumException : DomainException
    {
        public override string DomainMessage { get; protected set; }
        public DepositMinimumException() : base()
        {
            DomainMessage = "The minimum amount you've to deposit is 1 - go champ :D";
        }
    }

    public class FundsOutOfRange : DomainException
    {
        public override string DomainMessage { get; protected set; }
        public FundsOutOfRange(decimal balance) : base()
        {
            DomainMessage = $"Max you can withdraw is {balance}";
        }
    }
}
