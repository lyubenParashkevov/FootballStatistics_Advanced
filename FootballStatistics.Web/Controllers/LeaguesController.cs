using FootballStatistics.Common;
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var leagues = await leagueService.GetAllAsync();
            return View(leagues);
        }

        [Authorize(Roles = ApplicationRoles.Administrator)]
        [HttpGet]
        public IActionResult Create()
        {


            return View(new LeagueFormModel());
        }

        [Authorize(Roles = ApplicationRoles.Administrator)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeagueFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await leagueService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

       
        public async Task<IActionResult> Details(int id)
        {
            var model = await leagueService.GetDetailsAsync(id);
            if (model == null) return NotFound();

            return View(model);
        }

        [Authorize(Roles = ApplicationRoles.Administrator)]
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

        [Authorize(Roles = ApplicationRoles.Administrator)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeagueFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //bool updated = await leagueService.UpdateAsync(id, model);
            //if (!updated)
            //{
            //    return NotFound();
            //}

            //return RedirectToAction(nameof(Index));

            try
            {
                bool updated = await leagueService.UpdateAsync(id, model);
                if (!updated)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        [Authorize(Roles = ApplicationRoles.Administrator)]
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

        [Authorize(Roles = ApplicationRoles.Administrator)]
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

        [HttpGet]
        public async Task<IActionResult> Standings(int id)
        {
            var model = await leagueService.GetStandingsAsync(id);
            return View(model);
        }
    }
}
