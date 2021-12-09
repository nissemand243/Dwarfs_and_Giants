namespace SE_training.Server;

public static class SeedMaterial
    {
    
        public static async Task<IHost> SeedAsync(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        

                await SeedMaterialAsync(context);
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
                    var PDF = new Tag(){TagName = "PDF"};
                    var LINQ = new Tag(){TagName = "LINQ"};
                

                var FillDescription = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
                var path = "MaterialsData/";

                context.Materials.AddRange(
                    new Material{AuthorId = 1,Name = "Lecture03 Lambdas",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"Lecture03.pdf", Tags = new[] {Cs,LINQ,PDF}},
                    new Material{AuthorId = 1,Name = "Lecture04 Data Access",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"Lecture04.pdf", Tags = new[] {Cs,LINQ,PDF}},
                    new Material{AuthorId = 1,Name = "Lecture05 Dependency Injection",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"Lecture05.pdf", Tags = new[] {Cs,LINQ,PDF}},
                    new Material{AuthorId = 1,Name = "Lecture06 Asynchronous",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"Lecture06.pdf", Tags = new[] {Cs,LINQ,PDF}},
                    new Material{AuthorId = 1,Name = "Lecture07 JSON and Rest",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"Lecture07.pdf", Tags = new[] {Cs,LINQ,PDF,JSON}},
                    new Material{AuthorId = 1,Name = "Lecture08 ASP.NET Core Web API",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"Lecture08.pdf", Tags = new[] {Cs,LINQ,PDF,JavaFX}},
                    new Material{AuthorId = 1,Name = "Lecture09 Web Applications",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"Lecture09.pdf", Tags = new[] {Cs,LINQ,PDF,JavaFX}},
                    new Material{AuthorId = 1,Name = "Lecture10 Mobile Applications",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"Lecture10.pdf", Tags = new[] {Cs,LINQ,PDF}},
                    new Material{AuthorId = 1,Name = "Lecture11 Security",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"Lecture11.pdf", Tags = new[] {Cs,LINQ,PDF}},

                    new Material{AuthorId = 2,Name = "BDSA01 Software Enginere",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"BDSA21_01.pdf", Tags = new[] {SE,PDF}},
                    new Material{AuthorId = 2,Name = "BDSA02 Requirements Engineering",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"BDSA21_02.pdf", Tags = new[] {SE,PDF}},
                    new Material{AuthorId = 2,Name = "BDSA03 UML Diagrams",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"BDSA21_03.pdf", Tags = new[] {SE,UML,PDF}},
                    new Material{AuthorId = 2,Name = "BDSA05 SOLID Principles",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"BDSA21_05.pdf", Tags = new[]  {SE,PDF}},
                    new Material{AuthorId = 2,Name = "BDSA07 Architectural and Object Oriented Design Part 1",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"BDSA21_07_P1.pdf", Tags = new[]{SE,PDF}},
                    new Material{AuthorId = 2,Name = "BDSA07 Architectural and Object Oriented Design Part 2",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"BDSA21_07_P2.pdf", Tags = new[]{SE,PDF}},
                    new Material{AuthorId = 2,Name = "BDSA07 Architectural and Object Oriented Design Part 3",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"BDSA21_07_P3.pdf", Tags = new[]{SE,PDF}},
                    new Material{AuthorId = 2,Name = "TEST DOKUMENT",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"Test.pdf", Tags = new[] {SE,PDF}},
                    new Material{AuthorId = 1,Name = "The Gilded Rose Assignment",Description = FillDescription, FileType = FileType.Pdf,FilePath = path+"The_Gilded_Rose.pdf", Tags = new[] {SE,PDF}}
                    );


                await context.SaveChangesAsync();
             }

            
            
                


        }
}



