namespace SE_training.Shared;

using System.Collections.ObjectModel;
using SE_training.Core;

    public class MaterialSetup
    {
        
        public static async Task<DetailsMaterialDTO> setUptestasync(string id)
        {
            await Task.Delay(2000);
            
            var Id =  Int32.Parse(id);
            var AuthorId = 0;
            var Name = "This is a UI Test";
            var Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
            var FileType ="PDF";
            var FilePath = "MaterialsData/test.pdf";

       
       
            var Tags = new Collection<TagDTO>(){
                {new TagDTO(2,2,"Docker")},
                {new TagDTO(1,2,"Test")},
                 {new TagDTO(33,2,"xunit")}
            };
            var Comment = new Collection<CommentDTO>();
        
        
            var Rating = 2.3;

           var material = new DetailsMaterialDTO(Id, AuthorId, Name, Description, FileType, FilePath,Tags,Comment,Rating);
           return material;
        }

          public static  async Task<List<DetailsMaterialDTO>> SetUpTestMaterialasync()
        {
           await Task.Delay(2000);
           var MaterialList = new List<DetailsMaterialDTO>();
            
            var Id =  0;
            var AuthorId = 0;
            var Name = "This is a UI Test";
            var Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
            var FileType ="PDF";
            var FilePath = "MaterialsData/test.pdf";
     
            var Tags = new Collection<TagDTO>(){
                {new TagDTO(2,2,"Docker")},
                {new TagDTO(1,2,"Test")},
                {new TagDTO(33,2,"xunit")}
            };
            var Comment = new Collection<CommentDTO>();
        
            var Rating = 4.6;

           var material = new DetailsMaterialDTO(Id, AuthorId, Name, Description, FileType, FilePath,Tags,Comment,Rating);
           MaterialList.Add(material);
            Id =  1;
            AuthorId = 1;
            Name = "THIS IS THE SECOUND WORLD";
            Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
            FileType ="PDF";
            FilePath = "MaterialsData/test.pdf";
     
            Tags = new Collection<TagDTO>(){
                {new TagDTO(2,2,"C#")},
                {new TagDTO(1,2,"Swing")},
                 {new TagDTO(33,2,"Junit")}
            };
            Comment = new Collection<CommentDTO>();
        
            Rating = 3.7;

           material = new DetailsMaterialDTO(Id, AuthorId, Name, Description, FileType, FilePath,Tags,Comment,Rating);
           MaterialList.Add(material);
          
          
            Id =  3;
            AuthorId = 2;
            Name = "ASIUJBDIUBSIB asbd Docker??";
            Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
            FileType ="PDF";
            FilePath = "MaterialsData/test.pdf";
     
            Tags = new Collection<TagDTO>(){
                {new TagDTO(2,2,"C#")},
                {new TagDTO(1,2,"JavaFX")},
                 {new TagDTO(33,2,"Junit")}
            };
            Comment = new Collection<CommentDTO>();
        
            Rating = 4.2;

           material = new DetailsMaterialDTO(Id, AuthorId, Name, Description, FileType, FilePath,Tags,Comment,Rating);
           MaterialList.Add(material);
           
            Id =  4;
            AuthorId = 0;
            Name = "NEJ NEJ NEJ NEJ NEJ NEJ NEJ NEJ NEJ NEJ";
            Description = "neeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeej";
            FileType ="PDF";
            FilePath = "MaterialsData/test.pdf";
     
            Tags = new Collection<TagDTO>(){
                {new TagDTO(2,2,"Nein")},
                {new TagDTO(1,2,"No")},
                 {new TagDTO(33,2,"Kampai")}
            };
            Comment = new Collection<CommentDTO>();
        
            Rating = 1.8;

           material = new DetailsMaterialDTO(Id, AuthorId, Name, Description, FileType, FilePath,Tags,Comment,Rating);
           MaterialList.Add(material);
           return MaterialList;
        }

        public static int GetRatingsAvg(IDictionary<string, int> Ratings)
        {
            var averageRating = 0; 
            var numOfRatings = 0; 
            Ratings.GetEnumerator();
         
            foreach (var key in Ratings.Keys)
            {
                averageRating += Ratings[key];
                numOfRatings++;

            }
            averageRating /= numOfRatings;
            return averageRating;
        }
    }

