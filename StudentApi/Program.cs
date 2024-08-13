using System.Data.Common;
using Microsoft.OpenApi.Models;
using StudentsApi.Data;
using StudentsApi.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(); //add open API support
builder.Services.AddEndpointsApiExplorer(); //Enable end point exploration
builder.Services.AddSwaggerGen( c=>

{
    c.SwaggerDoc("v1", new OpenApiInfo());
} ); //enable automatic swagger generation

builder.Services.AddSqlite<StudentContext>("Data Source = Students.db");

var app = builder.Build();

app.UseSwagger(); //enable swagger in the app itself
app.UseSwaggerUI(); //enable swagger web UI

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/students", (StudentContext context) => context.Students.ToArray()).WithName("Get all students");

app.MapGet("/student/{id}",(StudentContext context, int id) => context.Students.Find(id));

app.MapPost("/student/create", (StudentContext context,Student student) =>
{
    context.Students.Add(student);
    context.SaveChanges();
}  );

app.MapPut("/student/update/{id}", (StudentContext context,Student newStudent, int id) =>
{
    var student = context.Students.Find(id);
    if (student == null) return Results.NotFound();
    student.FirstName = newStudent.FirstName;
    student.LastName = newStudent.LastName;
    student.Grade = newStudent.Grade;
    context.SaveChanges();
    return Results.Ok();
}  );

app.MapDelete("/student/delete/{id}", (StudentContext context,int id) =>
{
    var student = context.Students.Find(id);
    if (student==null) return Results.NotFound();
    context.Students.Remove(student);
    context.SaveChanges();
    return Results.Ok();

}  );


app.UseHttpsRedirection();   





app.Run();


