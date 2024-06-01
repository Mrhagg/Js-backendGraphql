using CourseProvider.Infrastructure.Models;
using CourseProvider.Infrastructure.Services;

namespace CourseProvider.Infrastructure.GrapQl;

public class Query(ICourseService courseService)
{
    private readonly ICourseService _courseService = courseService;

    [GraphQLName("getAllCourses")]

    public async Task<IEnumerable<Course>> GetCoursesAsync()
    {
        return await _courseService.GetCoursesAsync();
    }

    [GraphQLName("getCoursesById")]

    public async Task<Course> GetCourseByIdAsync(string id)
    {
      return await _courseService.GetCourseById(id); 
    }
}
