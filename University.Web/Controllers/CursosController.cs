using Microsoft.AspNetCore.Mvc;
using University.Application.Interfaces;
using University.Application.ViewModels;

namespace University.Web.Controllers
{
    public class CursosController : Controller
    {
        private readonly ICursoAppService _service;

        public CursosController(ICursoAppService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var cursos = await _service.GetAllAsync();
            return View(cursos);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CursoViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            await _service.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var curso = await _service.GetByIdAsync(id);
            if (curso == null) return NotFound();
            return View(curso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CursoViewModel vm)
        {
            if (id != vm.Id) return BadRequest();
            if (!ModelState.IsValid) return View(vm);

            await _service.UpdateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var curso = await _service.GetByIdAsync(id);
            if (curso == null) return NotFound();
            return View(curso);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
