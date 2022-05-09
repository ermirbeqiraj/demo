namespace DataTransferObjects
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CustomerFundsDto
    {
        [Required]
        public decimal Funds { get; set; }
    }
}