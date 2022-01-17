using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edje.Bookmarks.Db.Db;

public class Term
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(Order = 1)]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;


    //[InverseProperty("Term")]
    //public virtual ICollection<TermTaxonomy>? TermTaxonomy { get; set; }
}
