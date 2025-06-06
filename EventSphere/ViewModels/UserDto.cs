﻿namespace EventSphere.ViewModels
{
    public class UserDto
    {
        public int UserId { get; set; }  
        public string Username { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
    }
}
