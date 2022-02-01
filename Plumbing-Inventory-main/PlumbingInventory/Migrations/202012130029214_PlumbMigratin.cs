namespace PlumbingInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlumbMigratin : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "ItemCat_ItemCat_ID", "dbo.ItemCats");
            DropForeignKey("dbo.ItemRecords", "Item_Item_ID", "dbo.Items");
            DropForeignKey("dbo.ItemRecords", "Job_Job_ID", "dbo.Jobs");
            DropIndex("dbo.Items", new[] { "ItemCat_ItemCat_ID" });
            DropIndex("dbo.ItemRecords", new[] { "Item_Item_ID" });
            DropIndex("dbo.ItemRecords", new[] { "Job_Job_ID" });
            RenameColumn(table: "dbo.Items", name: "ItemCat_ItemCat_ID", newName: "ItemCat_ID");
            RenameColumn(table: "dbo.ItemRecords", name: "Item_Item_ID", newName: "Item_ID");
            RenameColumn(table: "dbo.ItemRecords", name: "Job_Job_ID", newName: "Job_ID");
            AlterColumn("dbo.Items", "ItemCat_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.ItemRecords", "Item_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.ItemRecords", "Job_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Items", "ItemCat_ID");
            CreateIndex("dbo.ItemRecords", "Item_ID");
            CreateIndex("dbo.ItemRecords", "Job_ID");
            AddForeignKey("dbo.Items", "ItemCat_ID", "dbo.ItemCats", "ItemCat_ID", cascadeDelete: true);
            AddForeignKey("dbo.ItemRecords", "Item_ID", "dbo.Items", "Item_ID", cascadeDelete: true);
            AddForeignKey("dbo.ItemRecords", "Job_ID", "dbo.Jobs", "Job_ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemRecords", "Job_ID", "dbo.Jobs");
            DropForeignKey("dbo.ItemRecords", "Item_ID", "dbo.Items");
            DropForeignKey("dbo.Items", "ItemCat_ID", "dbo.ItemCats");
            DropIndex("dbo.ItemRecords", new[] { "Job_ID" });
            DropIndex("dbo.ItemRecords", new[] { "Item_ID" });
            DropIndex("dbo.Items", new[] { "ItemCat_ID" });
            AlterColumn("dbo.ItemRecords", "Job_ID", c => c.Int());
            AlterColumn("dbo.ItemRecords", "Item_ID", c => c.Int());
            AlterColumn("dbo.Items", "ItemCat_ID", c => c.Int());
            RenameColumn(table: "dbo.ItemRecords", name: "Job_ID", newName: "Job_Job_ID");
            RenameColumn(table: "dbo.ItemRecords", name: "Item_ID", newName: "Item_Item_ID");
            RenameColumn(table: "dbo.Items", name: "ItemCat_ID", newName: "ItemCat_ItemCat_ID");
            CreateIndex("dbo.ItemRecords", "Job_Job_ID");
            CreateIndex("dbo.ItemRecords", "Item_Item_ID");
            CreateIndex("dbo.Items", "ItemCat_ItemCat_ID");
            AddForeignKey("dbo.ItemRecords", "Job_Job_ID", "dbo.Jobs", "Job_ID");
            AddForeignKey("dbo.ItemRecords", "Item_Item_ID", "dbo.Items", "Item_ID");
            AddForeignKey("dbo.Items", "ItemCat_ItemCat_ID", "dbo.ItemCats", "ItemCat_ID");
        }
    }
}
