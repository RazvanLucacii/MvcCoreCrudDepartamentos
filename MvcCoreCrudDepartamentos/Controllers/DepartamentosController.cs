using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MvcCoreCrudDepartamentos.Models;
using MvcCoreCrudDepartamentos.Repositories;

namespace MvcCoreCrudDepartamentos.Controllers
{
    public class DepartamentosController : Controller
    {
        RepositoryDepartamentos repoDept;

        public DepartamentosController()
        {
            this.repoDept = new RepositoryDepartamentos();
        }
        public async Task<IActionResult> Index()
        {
            List<Departamento> departamentos = await this.repoDept.GetDepartamentosAsync();
            return View(departamentos);
        }

        public async Task<IActionResult> Detalles(int id)
        {
            Departamento departamento = await this.repoDept.FindDepartamentoAsync(id);
            return View(departamento);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string nombre, string localidad)
        {
            await this.repoDept.InsertDepartamentos(nombre, localidad);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Departamento departamento = await this.repoDept.FindDepartamentoAsync(id);
            return View(departamento);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Departamento departamento)
        {
            await this.repoDept.UpdateDepartamentoAsync(departamento.IdDepartamento, departamento.Nombre, departamento.Localidad);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int iddepartamento)
        {
            await this.repoDept.DeleteDepartamentoAsync(iddepartamento);
            return RedirectToAction("Index");
        }
    }
}
