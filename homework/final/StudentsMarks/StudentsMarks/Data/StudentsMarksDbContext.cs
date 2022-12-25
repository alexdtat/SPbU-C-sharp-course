using Microsoft.EntityFrameworkCore;

namespace StudentsMarks.Data;

public class StudentsMarksDbContext: DbContext
{
    public StudentsMarksDbContext(
        DbContextOptions<StudentsMarksDbContext> options)
        : base(options)
    {
    }
    public DbSet<StudentMark> StudentsMarks => Set<StudentMark>();
}
