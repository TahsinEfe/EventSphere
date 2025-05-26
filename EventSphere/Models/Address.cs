using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventSphere.Models
{
    public partial class Address
    {
        [Key]
        [Column("AddressID")]
        public int AddressId { get; set; }

        [StringLength(255)]
        public string? Street { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? District { get; set; }

        [StringLength(20)]
        public string? PostalCode { get; set; }

        [StringLength(100)]
        public string? Country { get; set; }

        // Navigation property: Bu adrese bağlı etkinlikler
        [InverseProperty("Address")]
        public ICollection<Event> Events { get; set; } = new List<Event>();

    }
}
