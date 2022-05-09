namespace DataTransferObjects
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 
    /// </summary>
    public class Customer
    {
        #region Fields

        /// <summary>
        /// Gets or sets the identifier card.
        /// </summary>
        /// <value>
        /// The identifier card.
        /// </value>
        public String IdCard { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the surname.
        /// </summary>
        /// <value>
        /// The surname.
        /// </value>
        public String Surname { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Int32 Id { get; set; }

        #endregion
    }

    public class CustomerFundsDto
    {
        [Required]
        public decimal Funds { get; set; }
    }

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