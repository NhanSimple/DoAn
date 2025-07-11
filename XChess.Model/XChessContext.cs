using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XChess.Model.Common;
using XChess.Model.Entities;

namespace XChess.Model
{
    public class XChessContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Move> Moves { get; set; }
        public DbSet<ChessMatch> ChessMatchs { get; set; }
        public DbSet<MatchPlayer> MatchPlayers { get; set; }
        public DbSet<MatchResult> MatchResults { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public XChessContext() : base("name=XChessConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MatchPlayer>()
       .HasKey(x => new { x.MatchId, x.UserId }); // Composite key     
            modelBuilder.Entity<MatchResult>()
   .HasKey(x => new { x.MatchId, x.UserId }); // Composite key     
            modelBuilder.Entity<MatchPlayer>()
    .HasRequired(mp => mp.User)
    .WithMany()
    .HasForeignKey(mp => mp.UserId)
    .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
      .HasIndex(u => u.Email)
      .IsUnique();

            modelBuilder.Entity<MatchPlayer>()
                .HasRequired(mp => mp.Match)
                .WithMany()
                .HasForeignKey(mp => mp.MatchId)
                .WillCascadeOnDelete(true);
            // Quan hệ: User - UserRole

            modelBuilder.Entity<MatchPlayer>()
            .HasKey(mp => new { mp.MatchId, mp.UserId });

            // MatchResult: composite key
            modelBuilder.Entity<MatchResult>()
                .HasKey(mr => new { mr.MatchId, mr.UserId });

            // UserRole: composite key
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            // Quan hệ: User - UserRole
            modelBuilder.Entity<UserRole>()
                .HasRequired(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .WillCascadeOnDelete(true);

            // Quan hệ: Role - UserRole
            modelBuilder.Entity<UserRole>()
                .HasRequired(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Move>()
    .HasRequired(m => m.Match)
    .WithMany(c => c.Moves)
    .HasForeignKey(m => m.MatchId)
    .WillCascadeOnDelete(true);

            // Quan hệ Move → User (tùy chọn: AI thì không có User)
            modelBuilder.Entity<Move>()
                .HasOptional(m => m.User)
                .WithMany(u => u.Moves)
                .HasForeignKey(m => m.UserId)
                .WillCascadeOnDelete(false);
        }

        public override int SaveChanges()
        {
            var now = DateTime.Now < new DateTime(1753, 1, 1)
                ? new DateTime(1753, 1, 1)
                : DateTime.Now;

            string identityName = Thread.CurrentPrincipal.Identity?.Name ?? "System";
            long? userId = Users.FirstOrDefault(u => u.UserName == identityName)?.Id;

            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditableEntity &&
                       (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (IAuditableEntity)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedBy = identityName;
                    entity.CreatedDate = now;
                    entity.CreatedID = userId;
                    entity.CreatedAt = now;
                }
                else
                {
                    Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                    Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                }

                entity.UpdatedBy = identityName;
                entity.UpdatedDate = now;
                entity.UpdatedID = userId;

                if (entity.UpdatedAt.HasValue && entity.UpdatedAt < new DateTime(1753, 1, 1))
                    entity.UpdatedAt = new DateTime(1753, 1, 1);
                if (entity.DeletedAt.HasValue && entity.DeletedAt < new DateTime(1753, 1, 1))
                    entity.DeletedAt = new DateTime(1753, 1, 1);
            }

            return base.SaveChanges();
        }

    }
}