namespace WhatsUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeOfSenderndReceiverId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "Receiver_Id", "dbo.Accounts");
            DropForeignKey("dbo.Messages", "Sender_Id", "dbo.Accounts");
            DropIndex("dbo.Messages", new[] { "Receiver_Id" });
            DropIndex("dbo.Messages", new[] { "Sender_Id" });
            AlterColumn("dbo.Messages", "Receiver_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Messages", "Sender_Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "Sender_Id", c => c.Int());
            AlterColumn("dbo.Messages", "Receiver_Id", c => c.Int());
            CreateIndex("dbo.Messages", "Sender_Id");
            CreateIndex("dbo.Messages", "Receiver_Id");
            AddForeignKey("dbo.Messages", "Sender_Id", "dbo.Accounts", "Id");
            AddForeignKey("dbo.Messages", "Receiver_Id", "dbo.Accounts", "Id");
        }
    }
}
