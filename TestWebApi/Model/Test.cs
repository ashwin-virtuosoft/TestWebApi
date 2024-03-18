using System.ComponentModel.DataAnnotations;

namespace TestWebApi.Model
{
    public class Test
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string NAME { get; set; }
        public int ROLL_NO { get; set; }
    }
}
