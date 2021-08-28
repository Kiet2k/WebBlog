using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace razorweb.Models
{
    public class Acticle
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [DisplayName("Tiêu Đề")]
        public string Title { get; set; }

        [DataType(DataType.Date)]
         [DisplayName("Ngày đăng")]
        public DateTime PublishDate { get; set; }
         [DisplayName("Nội dung")]
        public string Content {set; get;}
    }
}