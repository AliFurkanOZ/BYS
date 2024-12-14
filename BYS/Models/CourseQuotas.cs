using BYS.Models;
using System.ComponentModel.DataAnnotations;

namespace BYS.Models
{
    public class CourseQuota
    {
        [Key]
        public int CourseId { get; set; }
        public int Quota { get; set; }
        public int RemainingQuota { get; set; }


        public virtual Course Course { get; set; }
    }

}