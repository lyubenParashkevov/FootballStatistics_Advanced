using FootballStatistics.Services.Contracts;
using FootballStatistics.ViewModels.League;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballStatistics.Controllers
{
    [Authorize]
    public class LeaguesController : Controller
    {
        private readonly ILeagueService leagueService;

        public LeaguesController(ILeagueService leagueService)
        {
            this.leagueService = leagueService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var leagues = await leagueService.GetAllAsync();
            return View(leagues);
        }
   
        [HttpGet]
        public IActionResult Create()
        {
            return View(new LeagueFormModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeagueFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await leagueService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var model = await leagueService.GetDetailsAsync(id);
            if (model == null) return NotFound();

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await leagueService.GetEditModelAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeagueFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool updated = await leagueService.UpdateAsync(id, model);
            if (!updated)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await leagueService.GetEditModelAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                bool deleted = await leagueService.DeleteAsync(id);

                if (!deleted)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                var model = await leagueService.GetEditModelAsync(id);
                if (model == null)
                {
                    return NotFound();
                }

                return View("Delete", model);
            }
        }
    }
}
