using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Data;
using HospitalManagement.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization; 

namespace HospitalManagement.Controllers
{
   
    [Authorize]
    public class PatientsController : Controller
    {
       
        private readonly ApplicationDbContext _context;

        
        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            
            var allPatients = await _context.Patients.ToListAsync();
            
            return View(allPatients);
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound(); 
            }
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
            {
                return NotFound(); 
            }
            return View(patient);
        }

        public IActionResult Create()
        {
        
            if (!User.IsInRole("Doctor"))
            {
                return Forbid(); 
            }
            return View();
        }

        
        [HttpPost]
        
        public async Task<IActionResult> Create(Patient patient)
        {
            if (!User.IsInRole("Doctor"))
            {
                return Forbid();
            }
            if (ModelState.IsValid)
            {
                _context.Add(patient); 
                await _context.SaveChangesAsync(); 
                return RedirectToAction(nameof(Index)); 
            }
            return View(patient);

        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (!User.IsInRole("Doctor"))
            {
                return Forbid();
            }

            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

       
        [HttpPost]
        
        public async Task<IActionResult> Edit(int id, Patient patient)
        {
            if (!User.IsInRole("Doctor"))
            {
                return Forbid();
            }

            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
        
                    _context.Update(patient); 
                    await _context.SaveChangesAsync(); 
                
            }
            return View(patient);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.IsInRole("Doctor"))
            {
                return Forbid();
            }

            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

       
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!User.IsInRole("Doctor"))
            {
                return Forbid();
            }

            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient); 
            }

            await _context.SaveChangesAsync(); // Save the change (delete the record).
            return RedirectToAction(nameof(Index));
        }
    }
}