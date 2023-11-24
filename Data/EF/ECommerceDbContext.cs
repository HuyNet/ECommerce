using Data.Configurations;
using Data.Entities;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EF
{
    public class ECommerceDbContext : IdentityDbContext<AppUser,AppRole,Guid>
    {
        public ECommerceDbContext(DbContextOptions options) :base(options) 
        { 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppConfigConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductInCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryTranslationConfiguration());
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTranslationConfiguration());
            modelBuilder.ApplyConfiguration(new PromotionConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new ProductImageConfiguration());


            //tablet identity
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new {x.UserId,x.RoleId});
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x=>x.UserId);
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x=>x.UserId);
            //base.OnModelCreating(modelBuilder);

            //data seeding
            modelBuilder.Entity<AppConfig>().HasData(
                new AppConfig() { Key = "HomeTitle" , Value = "This is home page"},
                new AppConfig() { Key = "HomeKeyword", Value = "This is keyword" }
                );

            modelBuilder.Entity<Language>().HasData(
                new Language() { LanguageId = "vi-VN",Name="Tiếng Việt", IsDefault = true},
                new Language() { LanguageId = "en-US", Name = "English", IsDefault = true }
                );



            var ADMIN_ID =new Guid("30658B70-9F61-40B8-A6A8-C0E6EF85B2D7");
            var ROLE_ID = new Guid("4C83DC56-907F-444A-86F8-EB9549984B41");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id =ROLE_ID,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator Role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = ADMIN_ID,
                UserName = "sa",
                NormalizedUserName = "sa",
                Email = "raiberkit131415@gmail.com",
                NormalizedEmail = "sraiberkit131415@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Hc0811!@#"),
                SecurityStamp = string.Empty,
                FisrtName = "A",
                LastName = "Kai",
                BOD = new DateTime(1990,01,01)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AppConfig> AppConfigs { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ProductTranslation> ProductTranslations { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<ProductInCategory> ProductInCategories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }



        //tablet IdentityService4
        public DbSet<ApiResourceProperty> ApiResourcePropertys { get; set; }
        public DbSet<IdentityResourceProperty> IdentityResourcePropertys { get; set; }
        public DbSet<ApiResourceSecret> ApiSecrets { get; set; }
        public DbSet<ApiScopeClaim> ApiScopeClaims { get; set; }
        public DbSet<IdentityResourceClaim> IdentityClaims { get; set; }
        public DbSet<ApiResourceClaim> ApiResourceClaims { get; set; }
        public DbSet<ClientGrantType> ClientGrantTypes { get; set; }
        public DbSet<ClientScope> ClientScopes { get; set; }
        public DbSet<ClientSecret> ClientSecrets { get; set; }
        public DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }
        public DbSet<ClientIdPRestriction> ClientIdPRestrictions { get; set; }
        public DbSet<ClientRedirectUri> ClientRedirectUris { get; set; }
        public DbSet<ClientProperty> ClientPropertys { get; set; }
        public DbSet<ApiScopeProperty> ApiScopePropertys { get; set; }
        public DbSet<ApiResourceScope> ApiResourceScopes { get; set; }


    }
}
