using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using shopapp.business.Abstract;
using shopapp.entity;
using shopapp.webui.Models;

namespace shopapp.webui.Controllers
{
    public class AdminController:Controller
    {
        private IProductService _productService;
        public AdminController(IProductService productService)
        {
            this._productService=productService;
        }

        public IActionResult ProductList()
        {
            return View(new ProductListViewModel()
            {
                Products= _productService.GetAll(),//ana yerden efcoregenericten geliyor

            });
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {//formu çağırdığım an bura get gelir. Gönderdiğim an postla gider oda aşağıda
            return View();            
        }
        [HttpPost]
        public IActionResult CreateProduct(ProductModel model)
        {//formu çağırdığım an bura get gelir. Gönderdiğim an postla gider
          var entity = new Product
          {
              Name=model.Name,
              Url=model.Url,
              Price=model.Price,
              Description=model.Description,
              ImageUrl=model.ImageUrl,
          };
          
           _productService.Create(entity);
           //TempData["message"]=$"{entity.Name} isimli ürün eklendi."; //bu mesajı view admin productlistte gösteririz.çünkü o sayfaya gidiyoruz.ViewData yapsaydım olmazdı çünkü o sayfa yeni bir sayfa.Direk layoulta yazdımki belki diğer sayfalardada kullanırız diye
           var msg= new AlertMesaage()
           {
               Message=$"{entity.Name} isimli ürün eklendi.",
               AlertType="success"
           };
           TempData["message"]=JsonConvert.SerializeObject(msg);
           return RedirectToAction("ProductList");//postlayınca productlist sayfası gelsin
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var entity=_productService.GetById((int)id);
             if(entity==null)
            {
                return NotFound();
            }
            var model=new ProductModel()
            {
                ProductId=entity.ProductId,
                Name=entity.Name,
                Url=entity.Url,
                Price=entity.Price,
                ImageUrl=entity.ImageUrl,
                Description=entity.Description
            };
            return View(model);            
        }

        [HttpPost]
        public IActionResult Edit(ProductModel model)
        {
            var entity=_productService.GetById(model.ProductId);//modelin içindekini alıyoki kullanıcı girmesin
            if(entity==null)
            {
                return NotFound();
            }
            entity.Name=model.Name;
            entity.Price=model.Price;
            entity.Url=model.Url;
            entity.ImageUrl=model.ImageUrl;
            entity.Description=model.Description;
            _productService.Update(entity);
           // TempData["message"]=$"{entity.Name} isimli ürün güncellendi.";
           var msg= new AlertMesaage()
           {
               Message=$"{entity.Name} isimli ürün güncelle.",
               AlertType="primary"
           };
           TempData["message"]=JsonConvert.SerializeObject(msg);
            return RedirectToAction("ProductList");         
        }
        
        [HttpPost]
        public IActionResult DeleteProduct(int productId)
        {
            var entity=_productService.GetById(productId);
            if(entity!=null)
            {
                _productService.Delete(entity);
            }
            //TempData["message"]=$"{entity.Name} isimli ürün silindi.";
            var msg= new AlertMesaage()
           {
               Message=$"{entity.Name} isimli ürün silindi.",
               AlertType="danger"
           };
           TempData["message"]=JsonConvert.SerializeObject(msg);
            return RedirectToAction("ProductList");         
        }


    }
}