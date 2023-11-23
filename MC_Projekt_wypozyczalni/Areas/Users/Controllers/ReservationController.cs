using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MC_Projekt_wypozyczalni.Models;
using MC_Projekt_wypozyczalni.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System;
using System.Data;

namespace MC_Projekt_wypozyczalni.Areas.Users.Controllers
{
    [Authorize(Roles = "User")]
    public class ReservationController : Controller
    {
        private readonly IRepositoryService<Reservation> _reservationRepository;
        private readonly IRepositoryService<Vehicle> _vehicleRepository;
        private readonly IMapper _mapper;
        private IValidator<Reservation> _validator;

        public ReservationController(IRepositoryService<Reservation> rRepository, IRepositoryService<Vehicle> vRepository, IMapper mapper, IValidator<Reservation> validator)
        {
            _reservationRepository = rRepository;
            _vehicleRepository = vRepository;
            _mapper = mapper;
            _validator = validator;
        }

		[Route("user/reservation/")]
		// GET: ReservationController
		public ActionResult Index()
        {
            var model = _vehicleRepository.GetAllRecords();

            return View(model);
        }

		[Route("user/reservation/reserve/")]
		// GET: ReservationController
		public ActionResult Reserve(int id)
        {
            var model = _reservationRepository.FindBy(x => x.vehicleID == id && x.userName == User.Identity.Name);

            ViewData["id"] = id;

            return View(model);
        }

		[Route("user/reservation/details/")]
		// GET: ReservationController/Details/5
		public ActionResult Details(int id)
        {
            return View(_reservationRepository.GetSingle(id));
        }

		[Route("user/reservation/create/")]
		// GET: ReservationController/Create
		public ActionResult Create(int id)
        {
            ViewData["id"] = id;
            return View();
        }

		[Route("user/reservation/create/")]
		// POST: ReservationController/Create
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(IFormCollection collection)
        {
            var user = User.Identity.Name;
            if (user == null)
                user = "unknown";

            var reservation = new Reservation { reservationStart = DateTime.Parse(collection["reservationStart"]),
                reservationEnd = DateTime.Parse(collection["reservationEnd"]),
                userName = user,
                vehicleID = Int32.Parse(collection["vehicleID"])
            };

            ValidationResult result = await _validator.ValidateAsync(reservation);

            if (!result.IsValid)
            {
                result.AddToModelState(ModelState);

                return View("Create", reservation);
            }

            _reservationRepository.Add(reservation);

            return RedirectToAction("Index");
        }

		[Route("user/reservation/edit/")]
		// GET: ReservationController/Edit/5
		public ActionResult Edit(int id)
        {
            return View(_reservationRepository.GetSingle(id));
        }

		[Route("user/reservation/edit/")]
		// POST: ReservationController/Edit/5
		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            var reservation = _reservationRepository.GetSingle(id);

            reservation.reservationStart = DateTime.Parse(collection["reservationStart"]);
            reservation.reservationEnd = DateTime.Parse(collection["reservationEnd"]);

            _reservationRepository.Edit(reservation);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

		[Route("user/reservation/delete/")]
		// GET: ReservationController/Delete/5
		public ActionResult Delete(int id)
        {
            return View();
        }

		[Route("user/reservation/delete/")]
		// POST: ReservationController/Delete/5
		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var reservation = _reservationRepository.GetSingle(id);

            _reservationRepository.Delete(reservation);

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
