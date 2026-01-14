using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Domain.Common
{
    public abstract class BaseEntity
    {
        // 🔹 Who Actions
        public string? AuthAdd { get; set; }        // Created By
        public string? AuthLstEdit { get; set; }     // Last Edited By
        public string? AuthDel { get; set; }     // Deleted By

        // 🔹 When Actions
        public DateTime AddOnDt { get; set; }        // Created Date
        public DateTime? EditOnDt { get; set; }      // Edited Date
        public DateTime? DelOnDt { get; set; }       // Deleted Date

        // 🔹 Soft Delete
        public int DelStatus { get; set; }          // Is Deleted
    }
}
