using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskRESTAPI.Models;

namespace TaskRESTAPI.Data
{
    public class TaskRESTAPIContext : DbContext
    {
        public TaskRESTAPIContext (DbContextOptions<TaskRESTAPIContext> options)
            : base(options)
        {
        }

        public DbSet<TaskRESTAPI.Models.TaskModel> TaskModel { get; set; } = default!;
    }
}
