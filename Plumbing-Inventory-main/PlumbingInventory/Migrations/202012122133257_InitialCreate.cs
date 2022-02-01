namespace PlumbingInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemCats",
                c => new
                    {
                        ItemCat_ID = c.Int(nullable: false, identity: true),
                        ItemCat_Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ItemCat_ID);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Item_ID = c.Int(nullable: false, identity: true),
                        Item_Name = c.String(maxLength: 50),
                        Item_Qty = c.Int(nullable: false),
                        ItemCat_ItemCat_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Item_ID)
                .ForeignKey("dbo.ItemCats", t => t.ItemCat_ItemCat_ID)
                .Index(t => t.ItemCat_ItemCat_ID);
            
            CreateTable(
                "dbo.ItemRecords",
                c => new
                    {
                        ItemRecord_ID = c.Int(nullable: false, identity: true),
                        ItemRecord_QtyUsed = c.Int(nullable: false),
                        ItemRecord_Status = c.String(),
                        Item_Item_ID = c.Int(),
                        Job_Job_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ItemRecord_ID)
                .ForeignKey("dbo.Items", t => t.Item_Item_ID)
                .ForeignKey("dbo.Jobs", t => t.Job_Job_ID)
                .Index(t => t.Item_Item_ID)
                .Index(t => t.Job_Job_ID);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Job_ID = c.Int(nullable: false, identity: true),
                        Job_Name = c.String(nullable: false, maxLength: 100),
                        Job_Date = c.String(),
                    })
                .PrimaryKey(t => t.Job_ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ItemRecords", "Job_Job_ID", "dbo.Jobs");
            DropForeignKey("dbo.ItemRecords", "Item_Item_ID", "dbo.Items");
            DropForeignKey("dbo.Items", "ItemCat_ItemCat_ID", "dbo.ItemCats");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ItemRecords", new[] { "Job_Job_ID" });
            DropIndex("dbo.ItemRecords", new[] { "Item_Item_ID" });
            DropIndex("dbo.Items", new[] { "ItemCat_ItemCat_ID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Jobs");
            DropTable("dbo.ItemRecords");
            DropTable("dbo.Items");
            DropTable("dbo.ItemCats");
        }
    }
}
