using FootballStatistics.Services.Contracts;
using FootballStatistics.ViewModels.Match;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballStatistics.Controllers
{
    [Authorize]
    public class MatchesController : Controller
    {
        private readonly IMatchService matchService;

        public MatchesController(IMatchService matchService)
        {
            this.matchService = matchService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var matches = await matchService.GetAllAsync();
            return View(matches);
        }
     
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await matchService.GetCreateModelAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MatchFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Teams = (await matchService.GetCreateModelAsync()).Teams;
                return View(model);
            }

            try
            {
                await matchService.CreateAsync(model);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                model.Teams = (await matchService.GetCreateModelAsync()).Teams;
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
      
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await matchService.GetEditModelAsync(id);
            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MatchFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Teams = (await matchService.GetCreateModelAsync()).Teams;
                return View(model);
            }

            try
            {
                bool updated = await matchService.UpdateAsync(id, model);
                if (!updated)
                    return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                model.Teams = (await matchService.GetCreateModelAsync()).Teams;
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await matchService.GetEditModelAsync(id);
            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool deleted = await matchService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
