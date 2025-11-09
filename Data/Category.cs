using System.ComponentModel.DataAnnotations;

namespace BlazorEcom.Data
{

    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter name...")]
        public string Name { get; set; }
    }
}