using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;

namespace WeddingPlanner.Controllers
{
    public class WeddingPlanController : Controller
    {
        private ProjectContext DbContext;
        public WeddingPlanController(ProjectContext context)
        {
            DbContext = context;
        }

        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            if(HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            ViewBag.UserId = HttpContext.Session.GetInt32("UserID");
            List<WeddingPlan> plans = DbContext.WeddingPlans.Include(w=>w.Guests).ToList();
            return View(plans);
        }
        [HttpGet]
        [Route("create")]
        public IActionResult PlanWedding()
        {
            if(HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            ViewBag.UserId = HttpContext.Session.GetInt32("UserID");
            ViewBag.users = DbContext.Users;
            return View();
        }
        [HttpPost]
        [Route("submitplan")]
        public IActionResult SubmitPlan(WeddingPlan newplan)
        {
            if (ModelState.IsValid)
            {
                ViewBag.UserId = HttpContext.Session.GetInt32("UserID");
                DbContext.WeddingPlans.Add(newplan);
                DbContext.SaveChanges();
                return RedirectToAction("Dashboard", "WeddingPlan");
            }
            ViewBag.UserId = HttpContext.Session.GetInt32("UserID");
            ViewBag.users = DbContext.Users;
            return View("PlanWedding"); 
        }

        [HttpGet]
        [Route("deleteplan/{id}")]
        public IActionResult DeletePlan(int id)
        {
            if(HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            WeddingPlan deleteitem = DbContext.WeddingPlans
                .SingleOrDefault(p => p.PlanId == id);
            DbContext.WeddingPlans.Remove(deleteitem);
            DbContext.SaveChanges();
            return RedirectToAction("Dashboard", "WeddingPlan");
        }

        [HttpGet]
        [Route("detail/{Planid}")]
        public IActionResult WeddingDetail(int PlanId)
        {
            if(HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            WeddingPlan CurrentWedding = DbContext.WeddingPlans
                .Include(w => w.Guests)
                .ThenInclude(g => g.User)
                .SingleOrDefault(w => w.PlanId == PlanId);
            return View(CurrentWedding);
        }

        [HttpGet]
        [Route("RSVP/{PlanId}")]
        public IActionResult RSVP(int PlanId)
        {
            if(HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            User CurrentUser = DbContext.Users
                .SingleOrDefault(u => u.UserId == HttpContext.Session
                .GetInt32("UserID"));
            WeddingPlan CurrentWedding = DbContext.WeddingPlans
                .Include(w => w.Guests)
                .ThenInclude(g => g.User)
                .SingleOrDefault(w => w.PlanId == PlanId);
            WeddingGuest thisguest = DbContext.WeddingGuests.Where(w => w.PlanId == PlanId && w.UserId == CurrentUser.UserId).FirstOrDefault();
            if (thisguest != null)
            {
                CurrentWedding.Guests.Remove(thisguest);
            }
            else
            {
                WeddingGuest NewGuest = new WeddingGuest {
                    UserId = CurrentUser.UserId,
                    PlanId = CurrentWedding.PlanId,
                };
                CurrentWedding.Guests.Add(NewGuest);
            }
            DbContext.SaveChanges();
            return RedirectToAction("Dashboard", "WeddingPlan");
        }

    }
}