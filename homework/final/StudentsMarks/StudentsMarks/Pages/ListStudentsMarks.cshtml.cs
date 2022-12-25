using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsMarks.Data;

namespace StudentsMarks.Pages;

public class ListStudentsMarksModel : PageModel
{
    private readonly StudentsMarksDbContext _context;
    public ListStudentsMarksModel(StudentsMarksDbContext context)
        => _context = context;
    public IList<StudentMark> StudentMarks { get; private set; } = new List<StudentMark>();
    public void OnGet()
    {
        StudentMarks = _context.StudentsMarks.OrderBy(studentMark => studentMark.StudentMarkId).ToList();
    }
}
