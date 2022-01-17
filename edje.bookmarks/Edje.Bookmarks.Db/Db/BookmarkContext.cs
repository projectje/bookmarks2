using Microsoft.EntityFrameworkCore;

namespace Edje.Bookmarks.Db.Db;

/*
 * run from the root of the solution:
 *
 * dotnet ef migrations add InitialCreate --project Edje.Bookmarks.Db --startup-project Edje.Bookmarks
 *
 * to init, then to update to create/update the database
 *
 * dotnet ef database update --project Edje.Bookmarks.Db --startup-project Edje.Bookmarks
 */
#nullable disable

public partial class BookmarkContext : DbContext
{
    public BookmarkContext(DbContextOptions<BookmarkContext> options)
             : base(options)
    {
    }

    public virtual DbSet<Url> Url { get; set; }
    public virtual DbSet<Owner> Owner { get; set; }
    public virtual DbSet<Taxonomy> Taxonomy { get; set; }
    public virtual DbSet<Term> Term { get; set; }
    public virtual DbSet<TermTaxonomy> TermTaxonomy { get; set; }
    public virtual DbSet<TermTaxonomyUrl> TermTaxonomiyUrl { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

        modelBuilder.Entity<TermTaxonomyUrl>()
            .HasKey(c => new
            {
                c.UrlId,
                c.TermTaxonomyId
            });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

/*
public class BookmarkContextFactory : IDesignTimeDbContextFactory<BookmarkContext>
{
    public BookmarkContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(System.AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

        DbContextOptionsBuilder<BookmarkContext>? optionsBuilder = new DbContextOptionsBuilder<BookmarkContext>();
        optionsBuilder.UseSqlite(configuration.GetConnectionString("BookmarkDb"));

        return new BookmarkContext(optionsBuilder.Options);
    }
}*/