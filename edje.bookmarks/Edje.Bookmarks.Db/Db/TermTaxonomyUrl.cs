using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edje.Bookmarks.Db.Db;

public class TermTaxonomyUrl
{
    [Key]
    [Column(Order = 1)]
    public int UrlId { get; set; }

    //[ForeignKey(nameof(UrlId))]
    //[InverseProperty("Id")]
    //public Url? Url { get; set; }

    [Key]
    [Column(Order = 2)]
    public int TermTaxonomyId { get; set; }

    //[ForeignKey(nameof(TermTaxonomyId))]
    //[InverseProperty("TermTaxonomyUrl")]
    //public TermTaxonomy? TermTaxonomy { get; set; }

    public int Rank { get; set; }
}
