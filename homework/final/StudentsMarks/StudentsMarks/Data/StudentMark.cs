using System.ComponentModel.DataAnnotations;

namespace StudentsMarks;

public class StudentMark
{
    public int StudentMarkId { get; set; }
    public string Name { get; set; } = "";
    public string Course { get; set; } = "";
    [Range(0, 10, ErrorMessage = "Grade must be in range 0 - 10.")]
    public int Mark { get; set; }
}
