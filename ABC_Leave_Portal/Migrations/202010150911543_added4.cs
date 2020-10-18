namespace ABC_Leave_Portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "SupervisorCode_EmployeeCode", "dbo.Employees");
            DropIndex("dbo.Employees", new[] { "SupervisorCode_EmployeeCode" });
            AddColumn("dbo.Employees", "SupervisorCode", c => c.String(maxLength: 10));
            DropColumn("dbo.Employees", "SupervisorCode_EmployeeCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "SupervisorCode_EmployeeCode", c => c.String(maxLength: 10));
            DropColumn("dbo.Employees", "SupervisorCode");
            CreateIndex("dbo.Employees", "SupervisorCode_EmployeeCode");
            AddForeignKey("dbo.Employees", "SupervisorCode_EmployeeCode", "dbo.Employees", "EmployeeCode");
        }
    }
}
