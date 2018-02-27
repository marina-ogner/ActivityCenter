using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EntityProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EntityProject.Controllers
{
    public class DashboardController : Controller
    {
        private EntityProjectContext _context;

        public DashboardController(EntityProjectContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("dashboard")]
        public IActionResult Index()
        {
            int? id = HttpContext.Session.GetInt32("id");
            List<User> user = _context.users.Where(u => u.UserId == id).ToList();
            if (user.Count > 0)
            {
                ViewBag.User = user[0];
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }

            List<Activity> activities = _context.activities.Include(a => a.Participants).Include(a => a.User).Where(a => a.Datetime > DateTime.Now).OrderBy(a => a.Datetime).ToList();
            ViewBag.Activities = activities;
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }
            return View();
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("add")]
        public IActionResult Add()
        {
            int? id = HttpContext.Session.GetInt32("id");
            List<User> user = _context.users.Where(u => u.UserId == id).ToList();
            if (user.Count > 0)
            {
                ViewBag.User = user[0];
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateActivity(ActivityViewModel model)
        {
            if (ModelState.IsValid)
            {
                int? Id = HttpContext.Session.GetInt32("id");
                User user = _context.users.SingleOrDefault(u => u.UserId == Id);
                Activity NewActivity = new Activity
                {
                    Title = model.Title,
                    Datetime = model.Date + model.Time,
                    Duration = model.Duration * model.Units,
                    Description = model.Description,
                    UserId = (int)Id,
                    Address = model.Address,
                };
                _context.activities.Add(NewActivity);
                _context.SaveChanges();
                Activity lastActivity = _context.activities.Last();
                int activityId = lastActivity.ActivityId;
                return RedirectToAction("Activity", new { ActivityId = activityId });
            }
            return View("Add", model);
        }

        [HttpGet]
        [Route("activity/{ActivityId}")]
        public IActionResult Activity(int ActivityId)
        {
            int? id = HttpContext.Session.GetInt32("id");
            List<User> user = _context.users.Where(u => u.UserId == id).ToList();
            if (user.Count > 0)
            {
                ViewBag.User = user[0];
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }

            Activity activity = _context.activities.Include(a => a.User).Include(a => a.Participants).ThenInclude(g => g.User).SingleOrDefault(a => a.ActivityId == ActivityId);
            ViewBag.Activity = activity;
            string escapedAddress = System.Uri.EscapeUriString(activity.Address);
            string src = $"https://maps.googleapis.com/maps/api/staticmap?center={escapedAddress}&zoom=14&size=400x400&key=AIzaSyAVUDHkEhq0sDvcLh22ZWaQ6rC732hvxt4";
            ViewBag.src = src;



            return View();
        }

        [HttpGet]
        [Route("join/{ActivityId}")]
        public IActionResult Join(int ActivityId)
        {
            Boolean flag = true;
            Activity joiningActivity = _context.activities.SingleOrDefault(a => a.ActivityId == ActivityId);
            User user = _context.users.Include(u => u.Activities).ThenInclude(a => a.Activity).SingleOrDefault(u => u.UserId == (int)HttpContext.Session.GetInt32("id"));
            foreach (var par in user.Activities)
            {
                if (par.Activity.Datetime.AddMinutes(par.Activity.Duration) < joiningActivity.Datetime || joiningActivity.Datetime.AddMinutes(joiningActivity.Duration) < par.Activity.Datetime)
                {

                }
                else{
                    flag=false;
                }
            }
            if (flag == false)
            {
                TempData["Error"] = "Can not join the activity because the activity you have already joined has a conflicting time (this includes duration)! ";
            }
            else
            {
                Participant participant = new Participant();
                participant.UserId = (int)HttpContext.Session.GetInt32("id");
                participant.ActivityId = ActivityId;
                _context.participants.Add(participant);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("leave/{ActivityId}")]
        public IActionResult Leave(int ActivityId)
        {

            int UserId = (int)HttpContext.Session.GetInt32("id");

            Participant RetrievedParticipant = _context.participants.Where(p => p.ActivityId == ActivityId).SingleOrDefault(p => p.UserId == UserId);
            _context.participants.Remove(RetrievedParticipant);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("delete/{ActivityId}")]
        public IActionResult Delete(int ActivityId)
        {
            List<Participant> participants = _context.participants.Where(p => p.ActivityId == ActivityId).ToList();
            foreach (var participant in participants)
            {
                _context.participants.Remove(participant);
            }
            Activity activity = _context.activities.SingleOrDefault(a => a.ActivityId == ActivityId);
            _context.activities.Remove(activity);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}