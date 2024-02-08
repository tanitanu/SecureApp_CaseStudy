using ElectronicStoreAPI.Data;
using ElectronicStoreAPI.Models.Domain;
using ElectronicStoreAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;

//by Srinivasan

namespace ElectronicStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ElecStoreDBContext dbContext;

        public ProductController(ElecStoreDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Get All products
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var prod = await dbContext.ProductMstr.ToListAsync();

            return Ok(prod);
        }

        //Get product by ModelNO
        [HttpGet]
        [Route("{model}")]
        public async Task<IActionResult> GetProd([FromRoute] string model)
        {
            var prod = await dbContext.ProductMstr.FirstOrDefaultAsync(a => a.ProdModelNo== model);

            if (prod == null)
            {
                return NotFound();
            }

            return Ok(prod);
        }

        //Add new Product
        [HttpPost]
        public async Task<IActionResult> AddProd([FromBody] ProductDTO prodDTO)
        {
            //map DTO to domain model
            var prodDomainMdl = new Product
            {
                ProdCat = prodDTO.ProdCat,
                ProdBrand = prodDTO.ProdBrand,
                ProdModelNo = prodDTO.ProdModelNo,
                ProdDescription = prodDTO.ProdDescription,
                ProdMRP = prodDTO.ProdMRP,
                ProdPrice = prodDTO.ProdPrice,
                ProdQty = prodDTO.ProdQty
            };
            //var cat = dbContext.CategoryMstr.Find(prodDomainMdl.ProdCat);
            //var brd = dbContext.BrandMstr.Find(prodDomainMdl.ProdBrand);
            //dbContext.CategoryMstr.Attach(cat);  //.State = EntityState.Unchanged;
            //dbContext.BrandMstr.Attach(brd);
            
            await dbContext.ProductMstr.AddAsync(prodDomainMdl);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProd), new {model=prodDTO.ProdModelNo}, prodDTO );
        }

        //Update existing Product
        [HttpPut]
        [Route("{model}")]
        public async Task<IActionResult> AddProd([FromRoute] string model, [FromBody] ProductDTO prodDTO)
        {
            var prod = await dbContext.ProductMstr.FirstOrDefaultAsync( a => a.ProdModelNo == model);

            if (prod == null)
            {
                return NotFound();

            }

            //search found, map DTO to domain and update
            prod.ProdCat = prodDTO.ProdCat;
            prod.ProdBrand = prodDTO.ProdBrand;
            prod.ProdModelNo = prodDTO.ProdModelNo;
            prod.ProdDescription = prodDTO.ProdDescription;
            prod.ProdMRP = prodDTO.ProdMRP;
            prod.ProdPrice = prodDTO.ProdPrice; 
            prod.ProdQty = prodDTO.ProdQty;

            await dbContext.SaveChangesAsync();

            //convert domain back to DTO and return
            var prdDTO = new ProductDTO
            {
                ProdCat = prod.ProdCat,
                ProdBrand = prod.ProdBrand,
                ProdModelNo = prod.ProdModelNo,
                ProdDescription = prod.ProdDescription,
                ProdMRP = prod.ProdMRP,
                ProdPrice = prod.ProdPrice,
                ProdQty = prod.ProdQty
            };
            return Ok(prdDTO);
        }

        //delete product
        [HttpDelete]
        [Route("{model}")]
        public async Task<IActionResult> DelProd([FromRoute] string model)
        {
            var prod = await dbContext.ProductMstr.FirstOrDefaultAsync(a => a.ProdModelNo == model);

            if (prod == null) { return NotFound();}

            //delete the product
            dbContext.ProductMstr.Remove(prod);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
