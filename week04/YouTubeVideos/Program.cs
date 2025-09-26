using System;
using System.Linq.Expressions;

class Program
{
    static void Main(string[] args)
    {
        var v1 = new Video("Intro to C# Classes", "CodeByAna", 540);
        v1.AddComment(new Comment("David", "Muy claro el ejemplo gracias."));
        v1.AddComment(new Comment("Karla", "Puedes explicarme contructores sobrecargados?"));
        v1.AddComment(new Comment("Luis", "Me puedes ayudar con la tarea."));

        var v2 = new Video("Abstraction in OPP", "DevEnEspañol", 620);
        v2.AddComment(new Comment("Maria", "Excelente explicacion de responsabilidad"));
        v2.AddComment(new Comment("Tom", "Tienes el codigo en GitHub"));
        v2.AddComment(new Comment("Sofia", "Buen ritmo y ejemplo"));

        var v3 = new Video("List and Collections", "CSharpBasics", 480);
        v3.AddComment(new Comment("Rafa", "Diferencia entre List y Array?"));
        v3.AddComment(new Comment("Ana", "Los ejemplos fueron directos"));
        v3.AddComment(new Comment("Leo", "Podria añadir LINQ en otros videos"));

        var v4 = new Video("Unit Testing, Fundamentals", "TestCraft", 700);
        v4.AddComment(new Comment("Luz", "Ahora entiendo asserts"));
        v4.AddComment(new Comment("Pablo", "Ejemplos con xUnits"));
        v4.AddComment(new Comment("Nati", "Bien estructurado."));

        var videos = new List<Video> { v1, v2, v3, v4 };

        foreach (var video in videos)
        {
            Console.WriteLine($"Title : {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.LengthSeconds} seconds");
            Console.WriteLine($"Comment: {video.GetCommentCount()}");

            foreach (var c in video.GetComments())
            {
                Console.WriteLine($"- {c.Author}; {c.Text}");
            }

            Console.WriteLine(new string('-', 40));
        }
    }
}