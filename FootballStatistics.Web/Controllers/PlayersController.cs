using FootballStatistics.Services.Contracts;
using FootballStatistics.Web.ViewModels.ViewModels.Player;
using Microsoft.AspNetCore.Mvc;

namespace FootballStatistics.Web.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IPlayerService playerService;

        public PlayersController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await playerService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await playerService.GetDetailsAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await playerService.GetCreateModelAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlayerFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Teams = (await playerService.GetCreateModelAsync()).Teams;
                return View(model);
            }

            await playerService.CreateAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await playerService.GetEditModelAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PlayerFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Teams = (await playerService.GetCreateModelAsync()).Teams;
                return View(model);
            }

            bool result = await playerService.UpdateAsync(id, model);

            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await playerService.GetDetailsAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool result = await playerService.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
