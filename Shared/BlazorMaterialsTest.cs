namespace SE_training.Shared;

using System.Collections.ObjectModel;
using SE_training.Core;
using static SE_training.Core.FileType;

    public class BlazorMaterialsTest
    {
        
        public static async Task<DetailsMaterialDTO> Show_a_specifik_Material(string id)
        {
            await Task.Delay(2000);
            
            var Id =  Int32.Parse(id);
            var AuthorId = 0;
            var Name = "This is a UI Test";
            var Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
            var FileType = Pdf;
            var FilePath = "MaterialsData/test.pdf";

       
       
            var Tags = new Collection<TagDTO>(){
                {new TagDTO(2,2,"Docker")},
                {new TagDTO(1,2,"Test")},
                 {new TagDTO(33,2,"xunit")}
            };
            var Comment = new Collection<CommentDTO>();
            var Rating = 4.2;
       

           var material = new DetailsMaterialDTO(Id, AuthorId, Name, Description, FileType, FilePath,Tags,Comment,Rating);

           return material;
        }

          public static  async Task<MaterialDTO[]> Shows_a_list_of_Search_Relatede_Material()
        {
           await Task.Delay(1000);
           var MaterialList = new MaterialDTO[4];
            
            var Id =  0;
            var AuthorId = 0;
            var Name = "This is a UI Test";
            var Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
            var FileType = Pdf;
            var FilePath = "MaterialsData/test.pdf";
     
          
           var material = new MaterialDTO(Id, AuthorId, Name, Description, FileType, FilePath);
            
           MaterialList[0]=(material);
            Id =  1;
            AuthorId = 1;
            Name = "THIS IS THE WORLD";
            Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
            FileType = Pdf;
            FilePath = "MaterialsData/test.pdf";
     

           material = new MaterialDTO(Id, AuthorId, Name, Description, FileType, FilePath);
           MaterialList[1]=(material);
          
          
            Id =  3;
            AuthorId = 2;
            Name = "ASIUJBDIUBSIB asbd Docker??";
            Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
            FileType = Pdf;
            FilePath = "MaterialsData/test.pdf";
     
        

           material = new MaterialDTO(Id, AuthorId, Name, Description, FileType, FilePath);
           MaterialList[2]=(material);
           
            Id =  4;
            AuthorId = 0;
            Name = "How to read up 1day before exam";
            Description = "Do not sleep in 48 hours";
            FileType = Pdf;
            FilePath = "MaterialsData/test.pdf";

           material = new MaterialDTO(Id, AuthorId, Name, Description, FileType, FilePath);
            MaterialList[3]=(material);
           return MaterialList;
        }

         public static  async Task<List<CommentDTO>> Show_a_list_of_Comments()
        {
           await Task.Delay(500); 

            var CommentList = new List<CommentDTO>(){
                {new CommentDTO(1024,2,2,"Lorem Ipsum is simply dummy text of the printing and typesetting industry.")},
                {new CommentDTO(123,2,23,"Lorem Ipsum is simply dummy text of the printing and typesetting industry.")},
                {new CommentDTO(10324,2,244,"Lorem Ipsum is simply dummy text of the printing and typesetting industry.")},
                {new CommentDTO(1023424,2,52,"Lorem Ipsum is simply dummy text of the printing and typesetting industry.")},
                {new CommentDTO(10224,2,232,"HA BSH sadn asnd asjd kasj hsand")}
            };
            return CommentList;
        }
    }

