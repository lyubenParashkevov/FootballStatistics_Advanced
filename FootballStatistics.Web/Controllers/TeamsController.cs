using FootballStatistics.Services.Contracts;
using FootballStatistics.ViewModels.Team;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballStatistics.Controllers
{
    [Authorize]
    public class TeamsController : Controller
    {
        private readonly ITeamService teamService;

        public TeamsController(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<TeamListItemModel> teams = await teamService.GetAllAsync();
            return View(teams);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            TeamFormModel model = await teamService.GetCreateModelAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamFormModel model)
        {
            if (!ModelState.IsValid)
            {          
                model.Leagues = (await teamService.GetCreateModelAsync()).Leagues;
                return View(model);
            }

            await teamService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            TeamFormModel? model = await teamService.GetEditModelAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeamFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Leagues = (await teamService.GetCreateModelAsync()).Leagues;
                return View(model);
            }

            bool updated = await teamService.UpdateAsync(id, model);

            if (!updated)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            TeamFormModel? model = await teamService.GetEditModelAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                bool deleted = await teamService.DeleteAsync(id);

                if (!deleted)
                {
                    return NotFound();
                }
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                var model = await teamService.GetEditModelAsync(id);
                return View("Delete", model);
            }

            return RedirectToAction(nameof(Index));

        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var model = await teamService.GetDetailsAsync(id);
            if (model == null) return NotFound();

            return View(model);
        }
    }
}
