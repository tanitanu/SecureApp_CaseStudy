using DXC.BlogConnect.WebAPI.Data;
using DXC.BlogConnect.WebAPI.Models.Domain;
using DXC.BlogConnect.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DXC.BlogConnect.WebAPI.Repositories
{
    /*Created by Prabu Elavarasan*/
    public class TagRepository : ITagRepository
    {
        private readonly BlogConnectDbcontext bloggieDbContext;

        public TagRepository(BlogConnectDbcontext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }


        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            var tags = await bloggieDbContext.Tags.ToListAsync();

            return tags.DistinctBy(x => x.Name.ToLower());
        }
    }
}
