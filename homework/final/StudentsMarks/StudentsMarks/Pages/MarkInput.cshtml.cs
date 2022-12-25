using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsMarks.Data;

namespace StudentsMarks.Pages;

[BindProperties]
public class MarkInputModel : PageModel
{
    private readonly StudentsMarksDbContext _context;
    public MarkInputModel(StudentsMarksDbContext context)
        => _context = context;
    public StudentMark StudentMark { get; set; } = new();

    public async Task<IActionResult> OnPostAsync()
    {
        _context.StudentsMarks.Add(StudentMark);
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}
