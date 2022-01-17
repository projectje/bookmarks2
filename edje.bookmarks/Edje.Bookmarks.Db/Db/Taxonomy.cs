using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edje.Bookmarks.Db.Db;

public class Taxonomy
{
    //public Taxonomy()
    //{
    //    TermTaxonomy = new HashSet<TermTaxonomy>();
    //}

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(Order = 1)]
    public int Id { get; set; }

    [ForeignKey(nameof(Owner))]
    public int OwnerId { get; set; }
    public Owner Owner { get; set; }

    public string Name { get; set; } = string.Empty;

    // foregin keys

    //[ForeignKey(nameof(OwnerId))]
    //[InverseProperty("Taxonomy")]
    //public virtual Owner? Owner { get; set; }

    // inverse properties

    //[InverseProperty("Taxonomy")]
    //public virtual ICollection<TermTaxonomy>? TermTaxonomy { get; set; }
}
