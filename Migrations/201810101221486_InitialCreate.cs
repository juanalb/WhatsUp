namespace WhatsUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MobileNumber = c.Int(nullable: false),
                        Username = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account1_Id = c.Int(),
                        Account2_Id = c.Int(),
                        Account_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account1_Id)
                .ForeignKey("dbo.Accounts", t => t.Account2_Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .Index(t => t.Account1_Id)
                .Index(t => t.Account2_Id)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MobileNumber = c.Int(nullable: false),
                        OwnerAccountId = c.Int(nullable: false),
                        ContactAccountId = c.Int(),
                        Account_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.ContactAccountId)
                .ForeignKey("dbo.Accounts", t => t.OwnerAccountId, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .Index(t => t.OwnerAccountId)
                .Index(t => t.ContactAccountId)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MessageText = c.String(),
                        TimeOfMessage = c.DateTime(nullable: false),
                        Chat_Id = c.Int(),
                        Receiver_Id = c.Int(),
                        Sender_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chats", t => t.Chat_Id)
                .ForeignKey("dbo.Accounts", t => t.Receiver_Id)
                .ForeignKey("dbo.Accounts", t => t.Sender_Id)
                .Index(t => t.Chat_Id)
                .Index(t => t.Receiver_Id)
                .Index(t => t.Sender_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Sender_Id", "dbo.Accounts");
            DropForeignKey("dbo.Messages", "Receiver_Id", "dbo.Accounts");
            DropForeignKey("dbo.Messages", "Chat_Id", "dbo.Chats");
            DropForeignKey("dbo.Contacts", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Contacts", "OwnerAccountId", "dbo.Accounts");
            DropForeignKey("dbo.Contacts", "ContactAccountId", "dbo.Accounts");
            DropForeignKey("dbo.Chats", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Chats", "Account2_Id", "dbo.Accounts");
            DropForeignKey("dbo.Chats", "Account1_Id", "dbo.Accounts");
            DropIndex("dbo.Messages", new[] { "Sender_Id" });
            DropIndex("dbo.Messages", new[] { "Receiver_Id" });
            DropIndex("dbo.Messages", new[] { "Chat_Id" });
            DropIndex("dbo.Contacts", new[] { "Account_Id" });
            DropIndex("dbo.Contacts", new[] { "ContactAccountId" });
            DropIndex("dbo.Contacts", new[] { "OwnerAccountId" });
            DropIndex("dbo.Chats", new[] { "Account_Id" });
            DropIndex("dbo.Chats", new[] { "Account2_Id" });
            DropIndex("dbo.Chats", new[] { "Account1_Id" });
            DropTable("dbo.Messages");
            DropTable("dbo.Contacts");
            DropTable("dbo.Chats");
            DropTable("dbo.Accounts");
        }
    }
}
