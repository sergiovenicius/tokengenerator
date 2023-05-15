using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tokengenerator.Database
{
    public class DBCard
    {
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "The CustomerId must be a positive integer")]
        public int CustomerId { get; set; }
        [Required]
        [Range(1000, 9999999999999999, ErrorMessage = "The CardNumber must have between 4 and 16 digits")]
        public long CardNumber { get; set; }
        [Required]
        [Range(1, 99999, ErrorMessage = "The CVV must have between 1 and 5 digits")]
        public int CVV { get; set; }
        public long Token { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CardId { get; set; }
    }
}
