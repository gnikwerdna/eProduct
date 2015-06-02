namespace eProduct.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
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
                        ProductCompliance_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ComplianceID)
                .ForeignKey("dbo.Compliances", t => t.compliance_ComplianceID)
                .ForeignKey("dbo.ComplianceForms", t => t.ComplianceFormId)
                .ForeignKey("dbo.ProductCompliances", t => t.ProductCompliance_ID)
                .Index(t => t.ComplianceFormId)
                .Index(t => t.compliance_ComplianceID)
                .Index(t => t.ProductCompliance_ID);
            
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
                "dbo.ProductCompliances",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        ProductId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductsToCategoryModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Category_CategoryID = c.Int(),
                        Product_ProductID = c.Int(),
                        Supplier_SupplierID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryID)
                .ForeignKey("dbo.Products", t => t.Product_ProductID)
                .ForeignKey("dbo.Suppliers", t => t.Supplier_SupplierID)
                .Index(t => t.Category_CategoryID)
                .Index(t => t.Product_ProductID)
                .Index(t => t.Supplier_SupplierID);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        CompanyContact = c.String(),
                    })
                .PrimaryKey(t => t.SupplierID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Fname = c.String(),
                        Lname = c.String(),
                        UserTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.UserTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TypeDesc = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductCompliance1",
                c => new
                    {
                        Product_ProductID = c.Int(nullable: false),
                        Compliance_ComplianceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_ProductID, t.Compliance_ComplianceID })
                .ForeignKey("dbo.Products", t => t.Product_ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Compliances", t => t.Compliance_ComplianceID, cascadeDelete: true)
                .Index(t => t.Product_ProductID)
                .Index(t => t.Compliance_ComplianceID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductsToCategoryModels", "Supplier_SupplierID", "dbo.Suppliers");
            DropForeignKey("dbo.ProductsToCategoryModels", "Product_ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductsToCategoryModels", "Category_CategoryID", "dbo.Categories");
            DropForeignKey("dbo.ProductCompliances", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Compliances", "ProductCompliance_ID", "dbo.ProductCompliances");
            DropForeignKey("dbo.Files", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductCompliance1", "Compliance_ComplianceID", "dbo.Compliances");
            DropForeignKey("dbo.ProductCompliance1", "Product_ProductID", "dbo.Products");
            DropForeignKey("dbo.Compliances", "ComplianceFormId", "dbo.ComplianceForms");
            DropForeignKey("dbo.Compliances", "compliance_ComplianceID", "dbo.Compliances");
            DropIndex("dbo.ProductCompliance1", new[] { "Compliance_ComplianceID" });
            DropIndex("dbo.ProductCompliance1", new[] { "Product_ProductID" });
            DropIndex("dbo.ProductsToCategoryModels", new[] { "Supplier_SupplierID" });
            DropIndex("dbo.ProductsToCategoryModels", new[] { "Product_ProductID" });
            DropIndex("dbo.ProductsToCategoryModels", new[] { "Category_CategoryID" });
            DropIndex("dbo.ProductCompliances", new[] { "ProductId" });
            DropIndex("dbo.Files", new[] { "ProductId" });
            DropIndex("dbo.Compliances", new[] { "ProductCompliance_ID" });
            DropIndex("dbo.Compliances", new[] { "compliance_ComplianceID" });
            DropIndex("dbo.Compliances", new[] { "ComplianceFormId" });
            DropTable("dbo.ProductCompliance1");
            DropTable("dbo.UserTypes");
            DropTable("dbo.Users");
            DropTable("dbo.Suppliers");
            DropTable("dbo.ProductsToCategoryModels");
            DropTable("dbo.ProductCompliances");
            DropTable("dbo.Files");
            DropTable("dbo.Products");
            DropTable("dbo.ComplianceForms");
            DropTable("dbo.Compliances");
            DropTable("dbo.Categories");
        }
    }
}
