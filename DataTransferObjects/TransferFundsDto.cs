namespace DataTransferObjects
{
    using System.ComponentModel.DataAnnotations;

    public class TransferFundsDto
    {
        [Required]
        public int From { get; set; }
        [Required]
        public int To { get; set; }
        [Required]
        public decimal Funds { get; set; }
    }
}