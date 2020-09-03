using System.ComponentModel.DataAnnotations;

namespace shopapp.webui.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }  
        [Display(Name="Name",Prompt="Enter product name")]
        public string Name { get; set; }       
        public string Url { get; set; }       
        public double? Price { get; set; } 
        public string Description { get; set; }         
        public string ImageUrl { get; set; }
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }
    }//burada ve viewde istediğin gibi oynarsın en sonda gönderirsin zaten controllerda bakarsan tüm değişiklik yapılmış modee gitmiş ve product.csy atılmış şekilde görürsün.
}