namespace WhatsUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeOfChatAccountIds : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Chats", "Account1_Id", "dbo.Accounts");
            DropForeignKey("dbo.Chats", "Account2_Id", "dbo.Accounts");
            DropIndex("dbo.Chats", new[] { "Account1_Id" });
            DropIndex("dbo.Chats", new[] { "Account2_Id" });
            AlterColumn("dbo.Chats", "Account1_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Chats", "Account2_Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Chats", "Account2_Id", c => c.Int());
            AlterColumn("dbo.Chats", "Account1_Id", c => c.Int());
            CreateIndex("dbo.Chats", "Account2_Id");
            CreateIndex("dbo.Chats", "Account1_Id");
            AddForeignKey("dbo.Chats", "Account2_Id", "dbo.Accounts", "Id");
            AddForeignKey("dbo.Chats", "Account1_Id", "dbo.Accounts", "Id");
        }
    }
}
