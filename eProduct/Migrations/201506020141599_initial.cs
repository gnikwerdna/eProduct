namespace eP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        ProductGroup = c.String(),
                        ProntoPartNumber = c.String(),
                        ProductManager = c.Int(nullable: false),
                        ProductComplianceSpecialist = c.Int(nullable: false),
                        StandardReferenceNumber = c.String(),
                        Regulation = c.String(),
                        TestHouseName = c.String(),
                        AccreditationAgencyName = c.String(),
                        Licenses = c.String(),
                        SupplierID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.Compliances",
                c => new
                    {
                        ComplianceID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        Description = c.String(),
                        subID = c.Int(nullable: false),
                        level = c.Int(nullable: false),
                        grp = c.Int(nullable: false),
                        order = c.Int(nullable: false),
                        ComplianceFormId = c.Int(),
                        compliance_ComplianceID = c.Int(),
                    })
                .PrimaryKey(t => t.ComplianceID)
                .ForeignKey("dbo.Compliances", t => t.compliance_ComplianceID)
                .ForeignKey("dbo.ComplianceForms", t => t.ComplianceFormId)
                .Index(t => t.ComplianceFormId)
                .Index(t => t.compliance_ComplianceID);
            
            CreateTable(
                "dbo.ComplianceForms",
                c => new
                    {
                        ComplianceFormId = c.Int(nullable: false, identity: true),
                        FormName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ComplianceFormId);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        fileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.fileId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ComplianceProducts",
                c => new
                    {
                        Compliance_ComplianceID = c.Int(nullable: false),
                        Product_ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Compliance_ComplianceID, t.Product_ProductID })
                .ForeignKey("dbo.Compliances", t => t.Compliance_ComplianceID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_ProductID, cascadeDelete: true)
                .Index(t => t.Compliance_ComplianceID)
                .Index(t => t.Product_ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ComplianceProducts", "Product_ProductID", "dbo.Products");
            DropForeignKey("dbo.ComplianceProducts", "Compliance_ComplianceID", "dbo.Compliances");
            DropForeignKey("dbo.Compliances", "ComplianceFormId", "dbo.ComplianceForms");
            DropForeignKey("dbo.Compliances", "compliance_ComplianceID", "dbo.Compliances");
            DropIndex("dbo.ComplianceProducts", new[] { "Product_ProductID" });
            DropIndex("dbo.ComplianceProducts", new[] { "Compliance_ComplianceID" });
            DropIndex("dbo.Files", new[] { "ProductId" });
            DropIndex("dbo.Compliances", new[] { "compliance_ComplianceID" });
            DropIndex("dbo.Compliances", new[] { "ComplianceFormId" });
            DropTable("dbo.ComplianceProducts");
            DropTable("dbo.Files");
            DropTable("dbo.ComplianceForms");
            DropTable("dbo.Compliances");
            DropTable("dbo.Products");
        }
    }
}
