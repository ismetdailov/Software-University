namespace MoiteRecepti.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using MoiteRecepti.Data;
    using MoiteRecepti.Data.Common.Repositories;
    using MoiteRecepti.Data.Models;

    [Area("Administration")]
    public class CategoriesController : Controller
    {
        private readonly IDeletableEntityRepository<Category> entityRepository1;

        public CategoriesController(IDeletableEntityRepository<Category> entityRepository)
        {
            this.entityRepository1 = entityRepository;
        }

        // GET: Administration/Categories
        public async Task<IActionResult> Index()
        {
            return this.View(await this.entityRepository1.All().ToListAsync());
        }

        // GET: Administration/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = await this.entityRepository1.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        // GET: Administration/Categories/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Category category)
        {
            if (this.ModelState.IsValid)
            {
                this.entityRepository1.AddAsync(category);
                await this.entityRepository1.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }
            return this.View(category);
        }

        // GET: Administration/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category =  this.entityRepository1.All().FirstOrDefault(x=>x.Id==id);
            if (category == null)
            {
                return this.NotFound();
            }
            return this.View(category);
        }

        // POST: Administration/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Category category)
        {
            if (id != category.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.entityRepository1.Update(category);
                    await this.entityRepository1.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.CategoryExists(category.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }
            return this.View(category);
        }

        // GET: Administration/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = await this.entityRepository1.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        // POST: Administration/Categories/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category =this.entityRepository1.All().FirstOrDefault(x=>x.Id==id);
            this.entityRepository1.Delete(category);
            await this.entityRepository1.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool CategoryExists(int id)
        {
            return this.entityRepository1.All().Any(e => e.Id == id);
        }
    }
}
