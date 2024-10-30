using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace QuanLyThuVien.Models
{
    public partial class QLTV1Context : DbContext
    {
        public QLTV1Context()
        {
        }

        public QLTV1Context(DbContextOptions<QLTV1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<NhaXuatBan> NhaXuatBans { get; set; }
        public virtual DbSet<Phat> Phats { get; set; }
        public virtual DbSet<PhieuMuon> PhieuMuons { get; set; }
        public virtual DbSet<Sach> Saches { get; set; }
        public virtual DbSet<TacGium> TacGia { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LUCIUS\\SQLEXPRESS01;Initial Catalog=QLTV1;Integrated Security=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.MaSv)
                    .HasName("PK__NguoiDun__2725081AEA3225AD");

                entity.ToTable("NguoiDung");

                entity.Property(e => e.MaSv)
                    .ValueGeneratedNever()
                    .HasColumnName("MaSV");

                entity.Property(e => e.HoTen).HasColumnName("hoTen");

                entity.Property(e => e.Mk).HasColumnName("mk");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(10)
                    .HasColumnName("SDT");

                entity.Property(e => e.Tk)
                    .HasMaxLength(50)
                    .HasColumnName("tk");

                entity.HasOne(d => d.TkNavigation)
                    .WithMany(p => p.NguoiDungs)
                    .HasForeignKey(d => d.Tk)
                    .HasConstraintName("FK__NguoiDung__SDT__267ABA7A");
            });

            modelBuilder.Entity<NhaXuatBan>(entity =>
            {
                entity.HasKey(e => e.MaNxb)
                    .HasName("PK__NhaXuatB__3A19482C524EDEE2");

                entity.ToTable("NhaXuatBan");

                entity.Property(e => e.MaNxb)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MaNXB")
                    .IsFixedLength(true);

                entity.Property(e => e.TenNxb).HasColumnName("TenNXB");
            });

            modelBuilder.Entity<Phat>(entity =>
            {
                entity.HasKey(e => e.Idphat)
                    .HasName("PK__Phat__8A9575FAF1A1FB12");

                entity.ToTable("Phat");

                entity.Property(e => e.Idphat)
                    .ValueGeneratedNever()
                    .HasColumnName("IDPhat");

                entity.Property(e => e.MaSv).HasColumnName("MaSV");

                entity.HasOne(d => d.MaSvNavigation)
                    .WithMany(p => p.Phats)
                    .HasForeignKey(d => d.MaSv)
                    .HasConstraintName("FK__Phat__MaSV__34C8D9D1");
            });

            modelBuilder.Entity<PhieuMuon>(entity =>
            {
                entity.ToTable("PhieuMuon");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.MaSach)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.MaSv).HasColumnName("MaSV");

                entity.Property(e => e.NgayMuon).HasColumnType("date");

                entity.Property(e => e.NgayTra).HasColumnType("date");

                entity.HasOne(d => d.MaSachNavigation)
                    .WithMany(p => p.PhieuMuons)
                    .HasForeignKey(d => d.MaSach)
                    .HasConstraintName("FK__PhieuMuon__MaSac__31EC6D26");

                entity.HasOne(d => d.MaSvNavigation)
                    .WithMany(p => p.PhieuMuons)
                    .HasForeignKey(d => d.MaSv)
                    .HasConstraintName("FK__PhieuMuon__MaSV__30F848ED");
            });

            modelBuilder.Entity<Sach>(entity =>
            {
                entity.HasKey(e => e.MaSach)
                    .HasName("PK__Sach__B235742D942ED806");

                entity.ToTable("Sach");

                entity.Property(e => e.MaSach)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.MaNxb)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MaNXB")
                    .IsFixedLength(true);

                entity.Property(e => e.MaTacGia)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.NamXb).HasColumnName("NamXB");

                entity.HasOne(d => d.MaNxbNavigation)
                    .WithMany(p => p.Saches)
                    .HasForeignKey(d => d.MaNxb)
                    .HasConstraintName("FK__Sach__MaNXB__2D27B809");

                entity.HasOne(d => d.MaTacGiaNavigation)
                    .WithMany(p => p.Saches)
                    .HasForeignKey(d => d.MaTacGia)
                    .HasConstraintName("FK__Sach__MaTacGia__2E1BDC42");
            });

            modelBuilder.Entity<TacGium>(entity =>
            {
                entity.HasKey(e => e.MaTacGia)
                    .HasName("PK__TacGia__F24E6756E6CDB1D4");

                entity.Property(e => e.MaTacGia)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.HasKey(e => e.Tk)
                    .HasName("PK__TaiKhoan__3213E048783856C6");

                entity.ToTable("TaiKhoan");

                entity.Property(e => e.Tk)
                    .HasMaxLength(50)
                    .HasColumnName("tk");

                entity.Property(e => e.Loaitk)
                    .HasMaxLength(10)
                    .HasColumnName("loaitk");

                entity.Property(e => e.Mk)
                    .HasMaxLength(50)
                    .HasColumnName("mk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
