using ElectronicStoreAPI.Data;
using ElectronicStoreAPI.Models.Domain;
using ElectronicStoreAPI.Models.DTO;
using ElectronicStoreAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

//by Srinivasan

namespace ElectronicStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ElecStoreDBContext dbContext;
        private readonly ICategoryRepository catRepository;

        public CategoryController(ElecStoreDBContext dbContext, ICategoryRepository catRepository)
        {
            this.dbContext = dbContext;
            this.catRepository = catRepository;
        }

        //Get All Category
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           // var cat = await dbContext.CategoryMstr.ToListAsync();
           var cat = await catRepository.GetAll();

            return Ok(cat);
        }

        //Get selected Category
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetId([FromRoute] Guid id)
        {
            var cat = dbContext.CategoryMstr.FirstOrDefault(a => a.CatCode == id);

            if (cat == null)
            {
                return NotFound();
            }

            return Ok(cat);
        }

        //Add New Category
        [HttpPost]
        public IActionResult Add([FromBody] AddCategoryDTO addCategory)
        {
            //Map DTO to domain model
            var catDomainMdl = new Category
            {
                CatDescription = addCategory.CatDescription
            };

            //Create Category
            dbContext.CategoryMstr.Add(catDomainMdl);
            dbContext.SaveChanges();

            //Convert domain model back to DTO 
            var catDTO = new CategoryDTO
            {
                CatCode = catDomainMdl.CatCode,
                CatDescription = catDomainMdl.CatDescription
            };
            return CreatedAtAction(nameof(GetId), new { id = catDTO.CatCode }, catDTO);
        }

        //Delete Category based on Name
        [HttpDelete]
        [Route("{name}")]
        public IActionResult Delete([FromRoute] string name)
        {
            var catDomainMdl = dbContext.CategoryMstr.FirstOrDefault(a => a.CatDescription == name);

            if (catDomainMdl == null)
            {
                return NotFound();
            }

            //delete the category
            dbContext.CategoryMstr.Remove(catDomainMdl);
            dbContext.SaveChanges();
            return Ok();
        }

        //Update selected Category
        [HttpPut]
        [Route("{id}")]

        public IActionResult UpdateCat([FromRoute] Guid id, [FromBody] AddCategoryDTO updCatDTO)
        {
            var catDomainMdl = dbContext.CategoryMstr.FirstOrDefault(a => a.CatCode == id);

            if (catDomainMdl == null)
            {
                return NotFound(id);
            }

            //update
            catDomainMdl.CatDescription= updCatDTO.CatDescription;

            dbContext.SaveChanges();

            //convert domain model back to DTO and retun
            var categoryDTO = new CategoryDTO
            {
                CatCode = catDomainMdl.CatCode,
                CatDescription = updCatDTO.CatDescription
            };
            return Ok(categoryDTO);
        }
    }
}
