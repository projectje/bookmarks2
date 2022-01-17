using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edje.Bookmarks.Db.Db;

public class Owner
{
    //public Owner()
    //{
    //    Taxonomy = new HashSet<Taxonomy>();
   // }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(Order = 1)]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    //[InverseProperty("Owner")]
    public virtual ICollection<Taxonomy> Taxonomy { get; set; }

}
