using Microsoft.EntityFrameworkCore;
using StudentsApi.Models;

namespace StudentsApi.Data;
public class StudentContext : DbContext
{
    public StudentContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Student> Students {get;set;}

}