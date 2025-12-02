using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.Application.Interfaces;
using University.Application.ViewModels;

namespace University.Web.Controllers
{
    public class AlunosController : Controller
    {
        private readonly IAlunoAppService _alunoService;
        private readonly ICursoAppService _cursoService;

        public AlunosController(IAlunoAppService alunoService, ICursoAppService cursoService)
        {
            _alunoService = alunoService;
            _cursoService = cursoService;
        }

        private async Task CarregarCursosAsync(int? cursoSelecionadoId = null)
        {
            var cursos = await _cursoService.GetAllAsync();
            ViewBag.Cursos = new SelectList(cursos, "Id", "Nome", cursoSelecionadoId);
        }

        public async Task<IActionResult> Index()
        {
            var alunos = await _alunoService.GetAllAsync();
            return View(alunos);
        }

        public async Task<IActionResult> Create()
        {
            await CarregarCursosAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlunoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                await CarregarCursosAsync(vm.CursoId);
                return View(vm);
            }

            await _alunoService.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var aluno = await _alunoService.GetByIdAsync(id);
            if (aluno == null) return NotFound();

            await CarregarCursosAsync(aluno.CursoId);
            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AlunoViewModel vm)
        {
            if (id != vm.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                await CarregarCursosAsync(vm.CursoId);
                return View(vm);
            }

            await _alunoService.UpdateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var aluno = await _alunoService.GetByIdAsync(id);
            if (aluno == null) return NotFound();

            return View(aluno);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var aluno = await _alunoService.GetByIdAsync(id);
            if (aluno == null) return NotFound();

            return View(aluno);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _alunoService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // Busca dinâmica com AJAX
        public async Task<IActionResult> Search(string term)
        {
            var alunos = await _alunoService.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(term))
            {
                term = term.ToLower();
                alunos = alunos
                    .Where(a =>
                        a.Nome.ToLower().Contains(term) ||
                        a.Email.ToLower().Contains(term) ||
                        a.CursoNome.ToLower().Contains(term))
                    .ToList();
            }

            return PartialView("_AlunosTablePartial", alunos);
        }
    }
}
