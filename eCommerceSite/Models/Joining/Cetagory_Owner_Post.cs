using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;



namespace eCommerceSite.Models.Joining
{
    public class Cetagory_Owner_Post
    {
        [Key]
        public int ID { set; get; }

        [ForeignKey("Post")]
        public int PID { set; get; }

        public Post.Post Post { set; get; }

        [ForeignKey("Owner")]
        public int OID { set; get; }

        public Owner.Owner Owner { set; get; }

        [ForeignKey("Cetagory")]
        public int CID { set; get; }

        [MaxLength(4)]
        [Column(TypeName = "varchar(4)")]
        public String Approve { set; get; }

        public Cetagory.Cetagory Cetagory { set; get; }

    }
}
