using FootballStatistics.Common;
using FootballStatistics.Services.Contracts;
using FootballStatistics.Web.ViewModels.ViewModels.Stadium;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballStatistics.Web.Controllers
{
    [Authorize]
    public class StadiumsController : Controller
    {
        private readonly IStadiumService stadiumService;

        public StadiumsController(IStadiumService stadiumService)
        {
            this.stadiumService = stadiumService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var model = await stadiumService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await stadiumService.GetDetailsAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Authorize(Roles = ApplicationRoles.Administrator)]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await stadiumService.GetCreateModelAsync();
            return View(model);
        }

        [Authorize(Roles = ApplicationRoles.Administrator)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StadiumFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Teams = (await stadiumService.GetCreateModelAsync()).Teams;
                return View(model);
            }

            try
            {
                await stadiumService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                model.Teams = (await stadiumService.GetCreateModelAsync()).Teams;
                return View(model);
            }
        }

        [Authorize(Roles = ApplicationRoles.Administrator)]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await stadiumService.GetEditModelAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Authorize(Roles = ApplicationRoles.Administrator)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StadiumFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Teams = (await stadiumService.GetCreateModelAsync()).Teams;
                return View(model);
            }

            bool result = await stadiumService.UpdateAsync(id, model);

            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index), new { id });
        }



        [Authorize(Roles = ApplicationRoles.Administrator)]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await stadiumService.GetDetailsAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Authorize(Roles = ApplicationRoles.Administrator)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool result = await stadiumService.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
