using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Delta.Application.DTOs.Utilities
{
    public class ICity
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
    }
}
