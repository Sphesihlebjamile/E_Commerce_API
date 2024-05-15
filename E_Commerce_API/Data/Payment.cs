using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_API.Data
{
    public class Payment
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Method { get; set; }
        public double Amount { get; set; }
        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
