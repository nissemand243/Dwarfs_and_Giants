namespace SE_training.Client.Shared;
using SE_training.Infrastructure;

    public class SpecifikMaterialSetup
    {
        public static async Task<Material> setUptestasync(string id)
        {
            await Task.Delay(2000);
            var material = new Material();
            material.Id =  Int32.Parse(id);
            material.Name = "This is a UI Test";
            material.Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
        

            return material;


        }
    }

