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
    [Authorize(Roles = "Admin")]
    public class AdminReservationController : Controller
    {
        private readonly IRepositoryService<Reservation> _reservationRepository;
        private readonly IMapper _mapper;
        private IValidator<Reservation> _validator;

        public AdminReservationController(IRepositoryService<Reservation> rRepository, IMapper mapper, IValidator<Reservation> validator)
        {
            this._reservationRepository = rRepository;
            _mapper = mapper;
            _validator = validator;
        }

		[Route("admin/reservation/")]
		// GET: AdminReservationController
		public ActionResult Index()
        {
            var model = _reservationRepository.GetAllRecords();

            return View(model);
        }

		[Route("admin/reservation/changestatus/")]
		public ActionResult ChangeStatus(int id)
        {
            return View(_reservationRepository.GetSingle(id));
        }

        [Route("admin/reservation/changestatus/")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeStatus(int id, string status)
        {
            var reservation = _reservationRepository.GetSingle(id);

            reservation.status = status;

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


        [Route("admin/reservation/edit/")]
		// GET: ReservationController/Edit/5
		public ActionResult Edit(int id)
        {
            return View(_reservationRepository.GetSingle(id));
        }

		[Route("admin/reservation/edit/")]
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

        [Route("admin/reservation/details/")]
        // GET: ReservationController/Details/5
        public ActionResult Details(int id)
        {
            return View(_reservationRepository.GetSingle(id));
        }

        [Route("admin/reservation/delete/")]
		// GET: ReservationController/Delete/5
		public ActionResult Delete(int id)
        {
            return View();
        }

		[Route("admin/reservation/delete/")]
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
