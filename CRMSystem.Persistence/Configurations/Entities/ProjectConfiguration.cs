using CRMSystem.Domain.Enums;
using CRMSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Persistence.Configurations.Entities
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasData(
                 new Project
                 {
                     Id = 1,
                     Status = ProjectStatus.InProgress,
                     Title = "Test Admin Project",
                     Deadline = new DateTime(2024, 12, 31),
                     Description = "Test Description",
                     UserId = 1
                 },
                 new Project
                 {
                     Id = 2,
                     Status = ProjectStatus.Completed,
                     Title = "Test Client Project",
                     Deadline = new DateTime(2024, 12, 31),
                     Description = "Test Description",
                     UserId = 2
                 }
             );
        }
    }
}
