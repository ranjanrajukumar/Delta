using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Delta.Application.DTOs.Student
{
    public class StudentDto
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public DateTime DOB { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }

        public string FatherName { get; set; }
        public string Relation { get; set; }
        public string FatherOccupation { get; set; }

        public string MotherName { get; set; }
        public string MotherOccupation { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }

        public decimal Income { get; set; }
        public int Status { get; set; }


        public string BloodGroup { get; set; }
        public string PAN { get; set; }
        public string ApaarID { get; set; }
        public string BirthID { get; set; }
        public string AadharNo { get; set; }
        public string PassportNo { get; set; }

        public string PresentAddress { get; set; }
        public string PerCity { get; set; }
        public string PerState { get; set; }
        public string PerPIN { get; set; }
        public string PerPhone { get; set; }
        public string PerCountry { get; set; }

        public int CityID { get; set; }
        public int DistID { get; set; }
        public int ReligionID { get; set; }
        public int QuotaID { get; set; }
        public int PH { get; set; }

        public byte[] Photo { get; set; }
    }

}
