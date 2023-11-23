using AutoMapper;
using MC_Projekt_wypozyczalni.Data;
using MC_Projekt_wypozyczalni.Models;
using MC_Projekt_wypozyczalni.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System;

namespace MC_Projekt_wypozyczalni.Controllers
{
    public class VehicleController : Controller
    {

        private readonly IRepositoryService<Vehicle> repository;
        private readonly IMapper _mapper;

        public VehicleController(IRepositoryService<Vehicle> repository, IMapper mapper)
        {
            this.repository = repository;
            this._mapper = mapper;
        }

        // GET: VehicleController
        public ActionResult Index()
        {
            var model = repository.GetAllRecords();

            List<VehicleItemViewModel> collection = new List<VehicleItemViewModel>();

            foreach (var item in model)
            {
                var viewModel = _mapper.Map<VehicleItemViewModel>(item);
                collection.Add(viewModel);
            }

            return View(collection);
        }

        // GET: VehicleController/Details/5
        public ActionResult Details(int id)
        {
            var vehicle = repository.GetSingle(id);
            return View(_mapper.Map<VehicleDetailViewModel>(vehicle));
        }

        // GET: VehicleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            var vehicle = new Vehicle {name = collection["name"], color = collection["color"], range = Double.Parse(collection["range"]) };
            repository.Add(vehicle);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VehicleController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(repository.GetSingle(id));
        }

        // POST: VehicleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            var vehicle = repository.GetSingle(id);

            vehicle.name = collection["name"];
            vehicle.color = collection["color"];
            vehicle.range = Double.Parse(collection["range"]);

            repository.Edit(vehicle);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VehicleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(repository.GetSingle(id));
        }

        // POST: VehicleController/Delete/5
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
