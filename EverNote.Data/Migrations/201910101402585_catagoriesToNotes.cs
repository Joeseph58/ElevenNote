namespace EverNote.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class catagoriesToNotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Note", "CatagoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Note", "CatagoryId");
            AddForeignKey("dbo.Note", "CatagoryId", "dbo.Catagory", "CatagoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Note", "CatagoryId", "dbo.Catagory");
            DropIndex("dbo.Note", new[] { "CatagoryId" });
            DropColumn("dbo.Note", "CatagoryId");
        }
    }
}
