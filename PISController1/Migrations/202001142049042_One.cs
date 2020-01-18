namespace PISController11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class One : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ModelClients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FIO = c.String(),
                        Passport = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ModelContractClients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AgentId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        Sum = c.Double(nullable: false),
                        DateStart = c.DateTime(nullable: false),
                        DateEnd = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ModelContractAgents", t => t.AgentId, cascadeDelete: true)
                .ForeignKey("dbo.ModelClients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.AgentId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.ModelContractAgents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AgentId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Commission = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ModelUsers", t => t.AgentId, cascadeDelete: true)
                .Index(t => t.AgentId);
            
            CreateTable(
                "dbo.ModelUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FIO = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ModelDirectories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(nullable: false),
                        ContractId = c.Int(nullable: false),
                        Data = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ModelContractClients", t => t.ContractId, cascadeDelete: true)
                .ForeignKey("dbo.ModelServices", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.ServiceId)
                .Index(t => t.ContractId);
            
            CreateTable(
                "dbo.ModelServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameService = c.String(),
                        Price = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ModelStatements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Period = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ModelStatementsTabs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatementId = c.Int(nullable: false),
                        AgentId = c.Int(nullable: false),
                        Sum = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ModelContractAgents", t => t.AgentId, cascadeDelete: true)
                .ForeignKey("dbo.ModelStatements", t => t.StatementId, cascadeDelete: true)
                .Index(t => t.StatementId)
                .Index(t => t.AgentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ModelStatementsTabs", "StatementId", "dbo.ModelStatements");
            DropForeignKey("dbo.ModelStatementsTabs", "AgentId", "dbo.ModelContractAgents");
            DropForeignKey("dbo.ModelDirectories", "ServiceId", "dbo.ModelServices");
            DropForeignKey("dbo.ModelDirectories", "ContractId", "dbo.ModelContractClients");
            DropForeignKey("dbo.ModelContractClients", "ClientId", "dbo.ModelClients");
            DropForeignKey("dbo.ModelContractClients", "AgentId", "dbo.ModelContractAgents");
            DropForeignKey("dbo.ModelContractAgents", "AgentId", "dbo.ModelUsers");
            DropIndex("dbo.ModelStatementsTabs", new[] { "AgentId" });
            DropIndex("dbo.ModelStatementsTabs", new[] { "StatementId" });
            DropIndex("dbo.ModelDirectories", new[] { "ContractId" });
            DropIndex("dbo.ModelDirectories", new[] { "ServiceId" });
            DropIndex("dbo.ModelContractAgents", new[] { "AgentId" });
            DropIndex("dbo.ModelContractClients", new[] { "ClientId" });
            DropIndex("dbo.ModelContractClients", new[] { "AgentId" });
            DropTable("dbo.ModelStatementsTabs");
            DropTable("dbo.ModelStatements");
            DropTable("dbo.ModelServices");
            DropTable("dbo.ModelDirectories");
            DropTable("dbo.ModelUsers");
            DropTable("dbo.ModelContractAgents");
            DropTable("dbo.ModelContractClients");
            DropTable("dbo.ModelClients");
        }
    }
}
