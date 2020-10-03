namespace DotNetAppSqlDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Todoes", "FirstName", c => c.String(nullable: false));
            AddColumn("dbo.Todoes", "LastName", c => c.String(nullable: false));
            AddColumn("dbo.Todoes", "FullAddress", c => c.String(nullable: false));
            AddColumn("dbo.Todoes", "MailingAddress", c => c.String());
            AddColumn("dbo.Todoes", "AsAboveAddress", c => c.Boolean(nullable: false));
            AddColumn("dbo.Todoes", "EmailAddress", c => c.String(nullable: false));
            AddColumn("dbo.Todoes", "PhoneNumber", c => c.Long(nullable: false));
            AddColumn("dbo.Todoes", "CitizenStatus", c => c.String(nullable: false));
            AddColumn("dbo.Todoes", "EmploymentStartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Todoes", "EmploymentType", c => c.String(nullable: false));
            AddColumn("dbo.Todoes", "PositionTitle", c => c.String(nullable: false));
            AddColumn("dbo.Todoes", "EmergencyContactName", c => c.String(nullable: false));
            AddColumn("dbo.Todoes", "EmergencyContactRelationship", c => c.String(nullable: false));
            AddColumn("dbo.Todoes", "EmergencyContactPhoneNumber", c => c.Long(nullable: false));
            AddColumn("dbo.Todoes", "EmployeeSignature", c => c.Binary());
            DropColumn("dbo.Todoes", "Description");
            DropColumn("dbo.Todoes", "CreatedDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Todoes", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Todoes", "Description", c => c.String());
            DropColumn("dbo.Todoes", "EmployeeSignature");
            DropColumn("dbo.Todoes", "EmergencyContactPhoneNumber");
            DropColumn("dbo.Todoes", "EmergencyContactRelationship");
            DropColumn("dbo.Todoes", "EmergencyContactName");
            DropColumn("dbo.Todoes", "PositionTitle");
            DropColumn("dbo.Todoes", "EmploymentType");
            DropColumn("dbo.Todoes", "EmploymentStartDate");
            DropColumn("dbo.Todoes", "CitizenStatus");
            DropColumn("dbo.Todoes", "PhoneNumber");
            DropColumn("dbo.Todoes", "EmailAddress");
            DropColumn("dbo.Todoes", "AsAboveAddress");
            DropColumn("dbo.Todoes", "MailingAddress");
            DropColumn("dbo.Todoes", "FullAddress");
            DropColumn("dbo.Todoes", "LastName");
            DropColumn("dbo.Todoes", "FirstName");
        }
    }
}
