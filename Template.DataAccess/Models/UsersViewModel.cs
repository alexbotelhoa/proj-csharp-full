using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace Template.DataAccess.Models
{
    [DataContract]
    [Table("users")]
    public class UsersViewModel
    {
        [DataMember]
        [Column("id")]
        public int Id { get; set; }
        
        [DataMember]
        [Column("name")]
        public string Name { get; set; }
        
        [DataMember]
        [Column("email")]
        public string Email { get; set; }
    }
}
