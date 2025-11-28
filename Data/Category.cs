using System.ComponentModel.DataAnnotations;

namespace HamburgueriaBlazor.Data
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please Enter Name...")]
        public string Name { get; set; }
    }
}
