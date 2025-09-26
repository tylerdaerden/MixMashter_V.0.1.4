using Microsoft.EntityFrameworkCore;
using MixMashter.Models.Entities;
using MixMashter.Models.Enums;

namespace MixMashter.DAL.Db
{
    public class MixMashterDbContext : DbContext
    {
        public MixMashterDbContext(DbContextOptions<MixMashterDbContext> options)
            : base(options)
        {

        }

        //préparation de mon DB context pour après ma migration et EF Core , ici encore je force le non nullable avec null! par sécurité

        
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Artist> Artists { get; set; } = null!;
        public DbSet<Song> Songs { get; set; } = null!;
        public DbSet<Mashup> Mashups { get; set; } = null!;
        public DbSet<Playlist> Playlists { get; set; } = null!;
        public DbSet<Favorites> Favorites { get; set; } = null!;
        public DbSet<Mashup_Song> MashupSongs { get; set; } = null!;
        public DbSet<Playlist_Mashup> PlaylistMashups { get; set; } = null!;


        // ici je configure mes relations entre mes entités du MPD , sur base de la foncion de la doc officielle de EF Core
        // chinée direct sur le site de Microsoft

        /// <summary>
        /// méthode pour configurer les relations entre les entités, native de l'orm EF Core , donc je fais au plus simple , à noter que ce fichier est celui
        /// que j'ai du modifier à plusurs reprises pour régler des soucis de relations , de clés composites etc... petite joie de EF Core, mais au final c'est une façon
        /// bien centralisée pour gérer ma DB, première utilisation exercices de tehnofutur donc j'apprends au fur et à mesure
        /// https://learn.microsoft.com/en-us/dotnet/api/system.data.entity.dbcontext.onmodelcreating?view=entity-framework-6.2.0
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //ici je configure mes relations entre mes entités de mon piti MPD maison 

            //User
            modelBuilder.Entity<User>()
                .HasMany(u => u.Mashups)
                .WithOne(m => m.User)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Playlists)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //Conversion de l'énum Role en string pour stockage en base de données, beaucoup plus lisible qu'un int (ici que 3 , mais dans projets plus gros faudra que j'attrape ce bon réflexe, sinon dur dur)
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>()
                .HasMaxLength(100) 
                .IsRequired()
                .HasDefaultValue(Role.User); //Définit User par défaut pour éviter les stuuuts


            //Mashup
            modelBuilder.Entity<Mashup>()
                .HasMany(m => m.Favorites)
                .WithOne(f => f.Mashup)
                .HasForeignKey(f => f.MashupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Mashup>()
                .HasMany(m => m.MashupSongs)
                .WithOne(ms => ms.Mashup)
                .HasForeignKey(ms => ms.MashupId)
                .OnDelete(DeleteBehavior.Cascade);

            //Song
            modelBuilder.Entity<Song>()
                .HasMany(s => s.MashupSongs)
                .WithOne(ms => ms.Song)
                .HasForeignKey(ms => ms.SongId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Song>()
                .HasOne(s => s.Artist)
                .WithMany(a => a.Songs)
                .HasForeignKey(s => s.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);

            //Playlist
            modelBuilder.Entity<Playlist>()
                .HasMany(p => p.PlaylistMashups)
                .WithOne(pm => pm.Playlist)
                .HasForeignKey(pm => pm.PlaylistId)
                .OnDelete(DeleteBehavior.Cascade);

            // Favorites (clé composite d'où les FK multiples)
            modelBuilder.Entity<Favorites>()
                .HasKey(f => new { f.UserId, f.MashupId });

            modelBuilder.Entity<Favorites>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict); // ⚠️ éviter le multiple cascade path (erreur lors de ma première migration , correction trouvée avec aide IA car je bloquais)

            modelBuilder.Entity<Favorites>()
                .HasOne(f => f.Mashup)
                .WithMany(m => m.Favorites)
                .HasForeignKey(f => f.MashupId)
                .OnDelete(DeleteBehavior.Cascade);


            //Mashup_Song (clé composite d'où les FK multiples)
            modelBuilder.Entity<Mashup_Song>()
                .HasKey(ms => new { ms.MashupId, ms.SongId });

            //Playlist_Mashup (clé composite d'où les FK multiples , encore , on change pas une équipe qui gagne)
            modelBuilder.Entity<Playlist_Mashup>()
                .HasKey(pm => new { pm.PlaylistId, pm.MashupId });

            modelBuilder.Entity<Playlist_Mashup>()
                .HasOne(pm => pm.Playlist)
                .WithMany(p => p.PlaylistMashups)
                .HasForeignKey(pm => pm.PlaylistId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Playlist_Mashup>()
                .HasOne(pm => pm.Mashup)
                .WithMany(m => m.PlaylistMashups)
                .HasForeignKey(pm => pm.MashupId)
                .OnDelete(DeleteBehavior.Restrict); // évite le multiple cascade path


        }
    }
}
