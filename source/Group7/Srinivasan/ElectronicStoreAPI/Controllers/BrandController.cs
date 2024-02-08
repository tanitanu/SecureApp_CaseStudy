using ElectronicStoreAPI.Data;
using ElectronicStoreAPI.Models.Domain;
using ElectronicStoreAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//by Srinivasan
namespace ElectronicStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly ElecStoreDBContext dbContext;

        public BrandController(ElecStoreDBContext dbContext )
        {
            this.dbContext = dbContext;
        }

        //Get All Brands
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var brand = await dbContext.BrandMstr.ToListAsync();

            return Ok(brand);
        }

        //Get selected Brand by ID
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetId([FromRoute] Guid id)
        {
            //var brand = dbContext.BrandMstr.Find(id);

            var brand = await dbContext.BrandMstr.FirstOrDefaultAsync(a => a.BrandCode == id);

            if (brand == null)
            {
                return NotFound();
            }

            return Ok(brand);
        }

        //Get selected Brand by brand name
        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> GetId([FromRoute] string name)
        {
            var brand = await dbContext.BrandMstr.FirstOrDefaultAsync(a => a.BrandName == name);

            if (brand == null)
            {
                return NotFound();
            }

            return Ok(brand);
        }

        //Add New Brand
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddBrandDTO addBrand)
        {
            //DTO to domain model
            var brandDomainMdl = new Brand
            {
                BrandName = addBrand.BrandName
            };

            //Create Brand
            await dbContext.BrandMstr.AddAsync(brandDomainMdl);
            await dbContext.SaveChangesAsync();

            //Convert domain model back to DTO 
            var brandDTO = new BrandDTO
            {
                BrandCode = brandDomainMdl.BrandCode,
                BrandName = brandDomainMdl.BrandName
            };
            return CreatedAtAction(nameof(GetId), new { id = brandDTO.BrandCode },  brandDTO);
        }

        //Update existing Brand name
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AddBrandDTO updBrandDTO)
        {
            var brandDomainMdl = await dbContext.BrandMstr.FirstOrDefaultAsync(a => a.BrandCode == id);

            if (brandDomainMdl == null)
            {
                return NotFound();
            }

            //seach found, map DTO to domain model and update 
            brandDomainMdl.BrandName = updBrandDTO.BrandName;

            await dbContext.SaveChangesAsync();

            //Convet domain model back to DTO and return result
            var brDTO = new BrandDTO
            {
                BrandCode = brandDomainMdl.BrandCode,
                BrandName = brandDomainMdl.BrandName
            };

            return Ok(brDTO);
        }

        //Delete selected Brand by brand name
        [HttpDelete]
        [Route("{name}")]
        public async Task<IActionResult> Delete([FromRoute] string name) 
        {
            var brandDomainMdl = await dbContext.BrandMstr.FirstOrDefaultAsync(a => a.BrandName == name);

            if (brandDomainMdl == null)
            {
                return NotFound();
            }

            //delete the brand
            dbContext.BrandMstr.Remove(brandDomainMdl);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        
    }
}
