using MC_Projekt_wypozyczalni.Models;
using MC_Projekt_wypozyczalni.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MC_Projekt_wypozyczalni.Controllers
{
    public class LoaningPointController : Controller
    {

        private readonly IRepositoryService<LoaningPoint> repository;

        public LoaningPointController(IRepositoryService<LoaningPoint> repository)
        {
            this.repository = repository;
        }

        // GET: LoaningPointController
        public ActionResult Index()
        {
            var model = repository.GetAllRecords();
            return View(model);
        }

        // GET: LoaningPointController/Details/5
        public ActionResult Details(int id)
        {
            return View(repository.GetSingle(id));
        }

        // GET: LoaningPointController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaningPointController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            var loaningPoint = new LoaningPoint { name = collection["name"], address = collection["address"] };
            repository.Add(loaningPoint);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoaningPointController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(repository.GetSingle(id));
        }

        // POST: LoaningPointController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            var loaningPoint = repository.GetSingle(id);

            loaningPoint.name = collection["name"];
            loaningPoint.address = collection["address"];

            repository.Edit(loaningPoint);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoaningPointController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(repository.GetSingle(id));
        }

        // POST: LoaningPointController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var vehicle = repository.GetSingle(id);

            repository.Delete(vehicle);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
