namespace SE_training.Server;

using SE_training.Core;

public static class SeedMaterial
    {
    
        public static async Task<IHost> SeedAsync(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                
                await SeedMaterialAsync(context);

                SearchEngine.INSTANCE = new SearchEngine(new UserRepository(context), new MaterialRepository(context), new TagRepository(context), new CommentRepository(context), new RatingRepository(context));

            }
            return host;
        }

        private static async Task SeedMaterialAsync(DatabaseContext context)
        {   
            //Fake data to use for seeding our database

            await context.Database.MigrateAsync();

            if (!await context.Materials.AnyAsync())
            {

            //Teachers 
            var TeacherRasmus = new Teacher{Name = "Rasmus C# Guro", Email = "RasmusMail@itu.dk"};
            var TeacherPavlo = new Teacher{Name ="Pavlo Software Enginere",Email = "PavloMail@itu.dk"};

            //Students
            var StudentMads = new Student{Name ="Mads Cornelius", Email = "mads@itu.dk"};
            var StudentJack = new Student{Name ="Jack Jensen",Email ="jack@itu.dk"};
            var StudentRasmus = new Student{Name ="Rasmus Balthazar",Email = "rasmus@itu.dk"};
            var StudentAnton = new Student{Name ="Anton Grilles",Email = "anton@itu.dk"};

            context.Users.AddRange(TeacherRasmus,TeacherPavlo,StudentJack,StudentAnton,StudentRasmus,StudentMads);

            //Tags
                var Docker = new Tag{TagName = "Docker"};
                var Cs = new Tag(){TagName = "C#"};
                var SE = new Tag(){TagName = "Software Enginere"};
                var UML = new Tag(){TagName = "UML Diagrams"};
                var JavaFX = new Tag(){TagName = "JavaFX"};
                var JSON = new Tag(){TagName = "JSON"};
                var Pdf = new Tag(){TagName = "Pdf"};
                var LINQ = new Tag(){TagName = "LINQ"};
            

            var FillDescription = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
            var path = "MaterialsData/";

            context.Materials.AddRange(
                    new Material{AuthorId = 1,Name = "Lecture03 Lambdas",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"Lecture03.Pdf"},
                    new Material{AuthorId = 1,Name = "Lecture04 Data Access",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"Lecture04.Pdf"},
                    new Material{AuthorId = 1,Name = "Lecture05 Dependency Injection",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"Lecture05.Pdf"},   
                    new Material{AuthorId = 1,Name = "Lecture06 Asynchronous",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"Lecture06.Pdf"},  
                    new Material{AuthorId = 1,Name = "Lecture07 JSON and Rest",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"Lecture07.Pdf"},   
                    new Material{AuthorId = 1,Name = "Lecture08 ASP.NET Core Web API",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"Lecture08.Pdf"},
                    new Material{AuthorId = 1,Name = "Lecture09 Web Applications",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"Lecture09.Pdf"},
                    new Material{AuthorId = 1,Name = "Lecture10 Mobile Applications",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"Lecture10.Pdf"},      
                    new Material{AuthorId = 1,Name = "Lecture11 Security",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"Lecture11.Pdf"},    
                    new Material{AuthorId = 2,Name = "BDSA01 Software Enginere",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"BDSA21_01.Pdf"}, 
                    new Material{AuthorId = 2,Name = "BDSA02 Requirements Engineering",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"BDSA21_02.Pdf"}, 
                    new Material{AuthorId = 2,Name = "BDSA03 UML Diagrams",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"BDSA21_03.Pdf"},        
                    new Material{AuthorId = 2,Name = "BDSA05 SOLID Principles",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"BDSA21_05.Pdf"},   
                    new Material{AuthorId = 2,Name = "BDSA07 Architectural and Object Oriented Design Part 1",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"BDSA21_07_P1.Pdf"},  
                    new Material{AuthorId = 2,Name = "BDSA07 Architectural and Object Oriented Design Part 2",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"BDSA21_07_P2.Pdf"},     
                    new Material{AuthorId = 2,Name = "BDSA07 Architectural and Object Oriented Design Part 3",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"BDSA21_07_P3.Pdf"},      
                    new Material{AuthorId = 2,Name = "TEST DOKUMENT",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"Test.Pdf"},    
                    new Material{AuthorId = 1,Name = "The Gilded Rose Assignment",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"The_Gilded_Rose.Pdf"});

            context.Tags.AddRange(
            PutTogether(
                MakeTags(1, "C#", "LINQ", "Pdf"),
                MakeTags(2, "C#", "LINQ", "Pdf"),
                MakeTags(3, "C#", "LINQ", "Pdf"),
                MakeTags(4, "C#", "LINQ", "Pdf"),
                MakeTags(5, "C#", "LINQ", "Pdf", "JSON"),
                MakeTags(6, "C#", "LINQ", "Pdf", "JavaFX"),
                MakeTags(7, "C#", "LINQ", "Pdf", "JavaFX"),
                MakeTags(8, "C#", "LINQ", "Pdf"),
                MakeTags(9, "C#", "LINQ", "Pdf"),

                MakeTags(10, "Software Enginere", "Pdf"),
                MakeTags(11, "Software Enginere", "Pdf"),
                MakeTags(12, "Software Enginere", "Pdf", "UML"),
                MakeTags(13, "Software Enginere", "Pdf"),
                MakeTags(14, "Software Enginere", "Pdf"),
                MakeTags(15, "Software Enginere", "Pdf"),
                MakeTags(16, "Software Enginere", "Pdf"),
                MakeTags(17, "Software Enginere", "Pdf"),
                MakeTags(18, "Software Enginere", "Pdf")
            ));

            await context.SaveChangesAsync();
            }
    }

    public static IEnumerable<Tag> MakeTags(int materialId, params string[] tags)
    {
        foreach (var tag in tags)
        {
            yield return new Tag(){MaterialId = materialId, TagName = tag};
        }
    }

    public static IEnumerable<T> PutTogether<T>(params IEnumerable<T>[] enumerables)
    {
        foreach (var enumerable in enumerables)
        {
            foreach (var item in enumerable)
            {
                yield return item;
            }
        }
    }
}
