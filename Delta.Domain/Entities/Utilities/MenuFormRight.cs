using Delta.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Delta.Domain.Entities.Utilities
{

    [Table("tblMenuFormRight")] // Maps to the actual table name in the database
    public class MenuFormRight:BaseEntity
    {

        [Key] // Defines UserId as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCode { get; set; }        // Unique permission key
        public int MenuId { get; set; }        // STUDENT_ENTRY / PROFESSION
        public string ButtonId { get; set; }      // SAVE / DELETE / STATUS
        public string ButtonText { get; set; }    // Save / Delete
        public string DisplayName { get; set; }   // Save Student Profile
        public int Tab { get; set; }               // 0=Form, 1=Tab, 2=Report
    }
}
