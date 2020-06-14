using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CounterIntelligenceCommand.Domain.Core;
using CounterIntelligenceCommand.Domain.Services;
using CounterIntelligenceCommand.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace CounterIntelligenceCommand.Web.Controllers
{
    public class StaffController : Controller
    {
        private readonly ILogger<StaffController> _logger;
        private readonly IStaffService _staffService;
        private readonly IStateService _stateService;


        public StaffController(IStaffService staffService, IStateService stateService, ILogger<StaffController> logger)
        {
            _logger = logger;
            _staffService = staffService;
            _stateService = stateService;
        }

        [Route("/staff", Name = "staffList")]
        [HttpGet]
        public async Task<IActionResult> Index(int page, int limit)
        {
            page = Math.Max(page, 1);
            ViewBag.Page = page;
            limit = limit > 0 ? limit : 30;
            ViewBag.Limit = limit;
            var staffList = await _staffService.GetStaffsAsync(page, limit);
            return View(staffList);
        }

       [HttpGet]
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var states = await _stateService.GetStates();
            ViewBag.States = new SelectList(states, "Key", "Value");
            var ranks = RankExtensions.GetRanks();
            ViewBag.Ranks = new SelectList(ranks, "Key", "Value");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StaffViewModel model)
        {
            var states = await _stateService.GetStates();
            var ranks = RankExtensions.GetRanks();

            if (ModelState.IsValid)
            {
                var response = await _staffService.AddStaff(model.FirstName, model.LastName, model.MiddleName,
                    model.ArmyNumber, model.PhoneNumber, model.Address, model.Rank, model.State, model.BirthDate,
                    model.RetirementDate);
                ViewBag.Error = !response.Status;
                ViewBag.Message = response.Message;
                ViewBag.States = new SelectList(states, "Key", "Value");
                ViewBag.Ranks = new SelectList(ranks, "Key", "Value");
                return View(new StaffViewModel());
            }
            else
            {
                ViewBag.Error = true;
                ViewBag.Message = "Kindly fill the form correctly!";
                ViewBag.States = new SelectList(states, "Key", "Value", model.State);
                ViewBag.Ranks = new SelectList(ranks, "Key", "Value", model.Rank);
                return View(model);
            }

            
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var staff = await _staffService.GetStaffAsync(id);

            if (staff == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var states = await _stateService.GetStates();
                var ranks = RankExtensions.GetRanks();
                ViewBag.States = new SelectList(states, "Key", "Value", staff.StateId);
                ViewBag.Ranks = new SelectList(ranks, "Key", "Value", staff.RankId);

                var viewModel = new StaffViewModel
                {
                    Rank = staff.RankId, LastName = staff.LastName, FirstName = staff.FirstName,
                    State = staff.StateId, ArmyNumber = staff.ArmyNumber, PhoneNumber = staff.PhoneNumber,
                    BirthDate = staff.BirthDate, RetirementDate = staff.RetirementDate,
                    Address = staff.Address, MiddleName = staff.MiddleName, Id = staff.Id
                };
                return View(viewModel);
            }
           
         
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StaffViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _staffService.UpdateStaff(id, model.FirstName, model.LastName, model.MiddleName,
                    model.ArmyNumber, model.PhoneNumber, model.Address, model.Rank, model.State, model.BirthDate,
                    model.RetirementDate);
                ViewBag.Error = !response.Status;
                ViewBag.Message = response.Message;
            }
            else
            {
                ViewBag.Error = true;
                ViewBag.Message = "Kindly fill the form correctly!";
            }

            var states = await _stateService.GetStates();
            ViewBag.States = new SelectList(states, "Key", "Value", model.State);
            var ranks = RankExtensions.GetRanks();
            ViewBag.Ranks = new SelectList(ranks, "Key", "Value", model.Rank);
            model.Id = id;
            return View(model);
        }

        // GET: Staff/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Staff/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}