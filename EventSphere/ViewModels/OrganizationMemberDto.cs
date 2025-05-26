namespace EventSphere.ViewModels
{
    public class OrganizationMemberDto
    {
        public int MemberId { get; set; }
        public int OrganizationId { get; set; }
        public int UserId { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsAdmin { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}