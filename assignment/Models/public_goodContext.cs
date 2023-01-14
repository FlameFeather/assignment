using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace assignment.Models
{
    public partial class public_goodContext : DbContext
    {
        public public_goodContext()
        {
        }

        public public_goodContext(DbContextOptions<public_goodContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Good> Goods { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Record> Records { get; set; }
        public virtual DbSet<TempUser> TempUsers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=public_good;user id=root;password=123456", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Good>(entity =>
            {
                entity.HasKey(e => e.Gid)
                    .HasName("PRIMARY");

                entity.ToTable("good");

                entity.HasIndex(e => e.Gid, "goods_gid_uindex")
                    .IsUnique();

                entity.Property(e => e.Gid)
                    .HasMaxLength(15)
                    .HasColumnName("gid");

                entity.Property(e => e.BeginTime)
                    .HasMaxLength(50)
                    .HasColumnName("begin_time");

                entity.Property(e => e.Gcontext)
                    .HasColumnType("text")
                    .HasColumnName("gcontext");

                entity.Property(e => e.Gstate)
                    .HasColumnName("gstate")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Itemid)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("itemid");

                entity.Property(e => e.KeepTime)
                    .HasColumnName("keep_time")
                    .HasDefaultValueSql("'-1'");

                entity.Property(e => e.Uid)
                    .HasMaxLength(15)
                    .HasColumnName("uid");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("item");

                entity.HasIndex(e => e.Itemid, "item_itemid_uindex")
                    .IsUnique();

                entity.Property(e => e.Itemid)
                    .HasMaxLength(15)
                    .HasColumnName("itemid");

                entity.Property(e => e.AvailableQuantity).HasColumnName("available_quantity");

                entity.Property(e => e.BorrowedQuantity).HasColumnName("borrowed_quantity");

                entity.Property(e => e.Iname)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("iname");

                entity.Property(e => e.Itime)
                    .HasColumnName("itime")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.UnavailableQuantity).HasColumnName("unavailable_quantity");
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.HasKey(e => e.Mid)
                    .HasName("PRIMARY");

                entity.ToTable("manager");

                entity.HasIndex(e => e.Mid, "manager_mid_uindex")
                    .IsUnique();

                entity.Property(e => e.Mid)
                    .HasMaxLength(15)
                    .HasColumnName("mid");

                entity.Property(e => e.Mpwd)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("mpwd");
            });

            modelBuilder.Entity<Record>(entity =>
            {
                entity.HasKey(e => e.Rid)
                    .HasName("PRIMARY");

                entity.ToTable("record");

                entity.HasIndex(e => e.Rid, "record_rid_uindex")
                    .IsUnique();

                entity.Property(e => e.Rid).HasColumnName("rid");

                entity.Property(e => e.BeginTime)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("begin_time");

                entity.Property(e => e.EndTime)
                    .HasMaxLength(50)
                    .HasColumnName("end_time");

                entity.Property(e => e.Gid)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("gid");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasDefaultValueSql("'2'");

                entity.Property(e => e.Uid)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("uid");
            });

            modelBuilder.Entity<TempUser>(entity =>
            {
                entity.HasKey(e => e.TempUid)
                    .HasName("PRIMARY");

                entity.ToTable("temp_user");

                entity.HasIndex(e => e.TempUid, "temp_user_temp_uid_uindex")
                    .IsUnique();

                entity.Property(e => e.TempUid)
                    .HasMaxLength(15)
                    .HasColumnName("temp_uid");

                entity.Property(e => e.TempContext)
                    .HasColumnType("text")
                    .HasColumnName("temp_context");

                entity.Property(e => e.TempDate)
                    .HasMaxLength(50)
                    .HasColumnName("temp_date");

                entity.Property(e => e.TempGender)
                    .IsRequired()
                    .HasColumnName("temp_gender")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.TempUpwd)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("temp_upwd");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PRIMARY");

                entity.ToTable("user");

                entity.HasIndex(e => e.Uid, "user_uid_uindex")
                    .IsUnique();

                entity.Property(e => e.Uid)
                    .HasMaxLength(15)
                    .HasColumnName("uid");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("gender")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Ucontext)
                    .HasColumnType("text")
                    .HasColumnName("ucontext");

                entity.Property(e => e.Udate)
                    .HasMaxLength(50)
                    .HasColumnName("udate");

                entity.Property(e => e.Upwd)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("upwd");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
