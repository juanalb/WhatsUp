namespace WhatsUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChattoChatIdinMessage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "Chat_Id", "dbo.Chats");
            DropIndex("dbo.Messages", new[] { "Chat_Id" });
            AlterColumn("dbo.Messages", "Chat_Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "Chat_Id", c => c.Int());
            CreateIndex("dbo.Messages", "Chat_Id");
            AddForeignKey("dbo.Messages", "Chat_Id", "dbo.Chats", "Id");
        }
    }
}
