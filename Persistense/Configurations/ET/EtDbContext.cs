using Application.Interfaces;
using Domain.Entities.ET;
using Microsoft.EntityFrameworkCore;

namespace Persistense;

public partial class CleanDbContext : DbContext, ICleanDbContext
{
    public DbSet<Evaluation> evaluations { get; set; }
    public DbSet<EvaluationDetail> evaluationsDetail { get; set; }
    public DbSet<EvaluationMaster> evaluationMasters { get; set; }
    public DbSet<EvaluationMasterDetail> evaluationMasterDetails { get; set; }
    public DbSet<SkillMatrix> skillMatrices { get; set; }
    public DbSet<SkillMatrixMaster> skillMatrixMasters { get; set; }
    public DbSet<SkillMatrixMasterDetail> skillMatrixMasterDetails { get; set; }
}
