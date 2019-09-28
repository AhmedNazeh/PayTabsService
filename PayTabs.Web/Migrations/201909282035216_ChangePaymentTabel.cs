namespace PayTabs.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePaymentTabel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPaymentInfoes", "IsPayed", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserPaymentInfoes", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserPaymentInfoes", "InvoiceId", c => c.String());
            AddColumn("dbo.UserPaymentInfoes", "TransactionId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserPaymentInfoes", "TransactionId");
            DropColumn("dbo.UserPaymentInfoes", "InvoiceId");
            DropColumn("dbo.UserPaymentInfoes", "CreationDate");
            DropColumn("dbo.UserPaymentInfoes", "IsPayed");
        }
    }
}
