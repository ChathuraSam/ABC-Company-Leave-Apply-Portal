namespace ABC_Leave_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeCode = c.String(nullable: false, maxLength: 10),
                        EmployeeName = c.String(maxLength: 50),
                        LeavePackage = c.String(maxLength: 10),
                        SupervisorCode_EmployeeCode = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.EmployeeCode)
                .ForeignKey("dbo.Employees", t => t.SupervisorCode_EmployeeCode)
                .Index(t => t.SupervisorCode_EmployeeCode);
            
            CreateTable(
                "dbo.LeaveAllocations",
                c => new
                    {
                        EmployeeID = c.String(nullable: false, maxLength: 10),
                        Year = c.DateTime(nullable: false),
                        LeaveType = c.String(maxLength: 128),
                        EntitledAmount = c.Int(nullable: false),
                        UtilizedAmount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmployeeID, t.Year })
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.LeaveTypes", t => t.LeaveType)
                .Index(t => t.EmployeeID)
                .Index(t => t.LeaveType);
            
            CreateTable(
                "dbo.LeaveTypes",
                c => new
                    {
                        LeaveTypeCode = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.LeaveTypeCode);
            
            CreateTable(
                "dbo.LeaveEntries",
                c => new
                    {
                        EmployeeCode = c.String(nullable: false, maxLength: 10),
                        LeaveTypeId = c.String(nullable: false, maxLength: 128),
                        RequestedDate = c.DateTime(nullable: false),
                        CheckByName = c.String(maxLength: 10),
                        CheckDate = c.DateTime(nullable: false),
                        Status = c.String(),
                        CommentsEmployee = c.String(),
                        CommentsApprover = c.String(),
                        AutoNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmployeeCode, t.LeaveTypeId, t.RequestedDate })
                .ForeignKey("dbo.Employees", t => t.EmployeeCode, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.CheckByName)
                .ForeignKey("dbo.LeaveTypes", t => t.LeaveTypeId, cascadeDelete: true)
                .Index(t => t.EmployeeCode)
                .Index(t => t.LeaveTypeId)
                .Index(t => t.CheckByName);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LeaveEntries", "LeaveTypeId", "dbo.LeaveTypes");
            DropForeignKey("dbo.LeaveEntries", "CheckByName", "dbo.Employees");
            DropForeignKey("dbo.LeaveEntries", "EmployeeCode", "dbo.Employees");
            DropForeignKey("dbo.LeaveAllocations", "LeaveType", "dbo.LeaveTypes");
            DropForeignKey("dbo.LeaveAllocations", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Employees", "SupervisorCode_EmployeeCode", "dbo.Employees");
            DropIndex("dbo.LeaveEntries", new[] { "CheckByName" });
            DropIndex("dbo.LeaveEntries", new[] { "LeaveTypeId" });
            DropIndex("dbo.LeaveEntries", new[] { "EmployeeCode" });
            DropIndex("dbo.LeaveAllocations", new[] { "LeaveType" });
            DropIndex("dbo.LeaveAllocations", new[] { "EmployeeID" });
            DropIndex("dbo.Employees", new[] { "SupervisorCode_EmployeeCode" });
            DropTable("dbo.LeaveEntries");
            DropTable("dbo.LeaveTypes");
            DropTable("dbo.LeaveAllocations");
            DropTable("dbo.Employees");
        }
    }
}
