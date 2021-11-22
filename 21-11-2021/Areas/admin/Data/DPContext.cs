using _21_11_2021.Areas.admin.Models;
using _21_11_2021.Areas.admin.Models.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _21_11_2021.Areas.admin.Data
{
    public class DPContext : IdentityDbContext<User>
    {
        public DPContext(DbContextOptions<DPContext> options)
           : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());

        }
        public DbSet<ChiTietHoaDon> chiTietHoaDons { get; set; }
        public DbSet<DanhGia> danhGias { get; set; }
        public DbSet<DanhMuc> danhMucs { get; set; }
        public DbSet<DonHang> donHangs { get; set; }
        public DbSet<KhuyenMai> khuyenMais { get; set; }
        public DbSet<LoaiSanPham> loaiSanPhams { get; set; }
        public DbSet<SanPham> sanPhams { get; set; }
        public DbSet<Slideshows> slideshows { get; set; }
        public DbSet<SlideshowFoot> slideshowFoots { get; set; }
        public DbSet<TinTuc> tinTucs { get; set; }
        public DbSet<LienHe> lienHes { get; set; }
        public DbSet<User> users { get; set; }

    }
}
