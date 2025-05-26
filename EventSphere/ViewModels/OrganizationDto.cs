// ViewModels/OrganizationDto.cs
using System.ComponentModel.DataAnnotations;

namespace EventSphere.ViewModels
{
    public class OrganizationDto
    {
        public int OrganizationId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        public string? ContactEmail { get; set; }

        public string? Phone { get; set; }

        [Url]
        public string? Website { get; set; }

        public string? SocialMedia { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? CreatedDate { get; set; }
    }
}

