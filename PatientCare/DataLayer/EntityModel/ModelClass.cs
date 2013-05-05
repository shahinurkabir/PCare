using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.EntityModel
{
    [Table("tDoctor")]
    public class tDoctor
    {
        public tDoctor()
        {
            Patient = new List<tPatient>();
        }
        [Key]
        public int DoctorId { get; set; }
        [Required]
        [MaxLength(30)]
        [Column(TypeName="VarChar")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30)]
        [Column(TypeName = "VarChar")]
        public string LastName { get; set; }
        public List<tPatient> Patient { get; set; }
    }

    [Table("tPatient")]
    public class tPatient
    {
        [Key]
        public int PatientId { get; set; }
        [Required]
        [MaxLength(30)]
        [Column(TypeName = "VarChar")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30)]
        [Column(TypeName = "VarChar")]
        public string LastName { get; set; }
        [Required]
         public int DoctorId { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        [MaxLength(30)]
        [Column(TypeName = "VarChar")]
        public string SSN { get; set; }
        [ForeignKey("DoctorId")]
        public tDoctor Doctor { get; set; }
    }

    [Table("tAddress")]
    public class tAddress
    {
        [Key]
        public int PatientId { get; set; }
        [Required]
        [MaxLength(255)]
        [Column(TypeName = "VarChar")]
        public string Address1 { get; set; }
        [MaxLength(255)]
        [Column(TypeName = "VarChar")]
        public string Address2 { get; set; }
        [Required]
        [MaxLength(30)]
        [Column(TypeName = "VarChar")]
        public string PostalCode { get; set; }
        [Required]
        [MaxLength(100)]
        [Column(TypeName = "VarChar")]
        public string City { get; set; }
        [ForeignKey("PatientId")]
        public virtual tPatient Patient { get; set; }
    }
    [Table("tUser")]
    public class tUser
    {
        [Key]
        [MaxLength(10)]
        [Column(TypeName = "VarChar")]
        public string UserId { get; set; }
        [Required]
        [MaxLength(20)]
        [Column(TypeName = "VarChar")]
        public string Password { get; set; }
    }
}
