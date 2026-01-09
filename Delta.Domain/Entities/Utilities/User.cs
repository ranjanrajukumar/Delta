using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Delta.Domain.Entities.Utilities
{
    [Table("tblUser")] // Maps to the actual table name in the database
    public class User
    {
        [Key] // Defines UserId as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? MasterID { get; set; }
        public int? RoleID { get; set; }
        public int? IsAdmin { get; set; }
        public string? Category { get; set; }
        public string? Email { get; set; }
        public string? MobileNo { get; set; }
        public string? FullName { get; set; }
        public string? ProfileImage { get; set; }
        public DateTime? LastLogin { get; set; }
        public int? LoginAttempts { get; set; }
        public int? IsLocked { get; set; }
        public string? CreatedBy { get; set; } = null;
        public string? UpdatedBy { get; set; } = null;
        public string? IPAddress { get; set; } = null;
        public string? UserType { get; set; }
        public string? Gender { get; set; }
        public string? Status { get; set; }
        public string? AuthAdd { get; set; }
        public string? AuthLstEdt { get; set; }
        public string? AuthDel { get; set; }
        public DateTime? AddOnDt { get; set; }
        public DateTime? EditOnDt { get; set; }
        public DateTime? DelOnDt { get; set; }
        public int? DelStatus { get; set; }
    }
}
