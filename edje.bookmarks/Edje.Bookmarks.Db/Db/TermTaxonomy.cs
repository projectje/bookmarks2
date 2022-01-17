using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edje.Bookmarks.Db.Db;

public class TermTaxonomy
{
    //public TermTaxonomy()
    //{
    //    TermTaxonomyUrl = new HashSet<TermTaxonomyUrl>();
    //    TermTaxonomyChilds = new HashSet<TermTaxonomy>();
    //}

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(Order = 1)]
    public int Id { get; set; }

    public int TermTaxonomyId { get; set; }
  
    /// <summary>
    /// FK to Term Id
    /// </summary>
    public int TermId { get; set; }
  
    /// <summary>
    /// FK To Taxonomy Id
    /// </summary>
    public int TaxonomyId { get; set; }
   
    public int Rank { get; set; }

    //  foreign keys

    //[ForeignKey(nameof(TermTaxonomyId))]
    //[InverseProperty("TermTaxonomyParent")]
    //public virtual TermTaxonomy? TermTaxonomyParent { get; set; }

    //[ForeignKey(nameof(TermId))]
    //[InverseProperty("TermTaxonomy")]
    //public virtual Term? Term { get; set; }

    //[ForeignKey(nameof(TaxonomyId))]
    //[InverseProperty("TermTaxonomy")]
    //public virtual Taxonomy? Taxonomy { get; set; }

    //  inverse properties

    //[InverseProperty("TermTaxonomy")]
    //public virtual ICollection<TermTaxonomyUrl>? TermTaxonomyUrl { get; set; }

   //[InverseProperty("TermTaxonomyChilds")]
   //public virtual TermTaxonomy Child { get; set; }
   //public virtual ICollection<TermTaxonomy>? TermTaxonomyChilds { get; set; }
}
