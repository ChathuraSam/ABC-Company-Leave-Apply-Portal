using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ABC_Leave_Portal.Models
{
    public class LeavePortalContext: DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveAllocation> LeaveAlloations { get; set; }
        public DbSet<LeaveEntry> LeaveEntries { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }

    }
}