using AllinOne.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScholarshipPlatform.Data;


[Authorize(Roles = "Admin")] 
public class ScholarshipsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ScholarshipsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // =========================================
    //  عرض كل المنح
    // =========================================
    public async Task<IActionResult> Index()
    {
        var scholarships = await _context.Scholarships.ToListAsync();
        return View(scholarships);
    }

    // =========================================
    //  إنشاء منحة جديدة
    // =========================================
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Scholarship scholarship)
    {
        if (ModelState.IsValid)
        {
            // توليد Slug تلقائي من العنوان
            scholarship.Slug = scholarship.Title.ToLower().Replace(" ", "-");

            _context.Scholarships.Add(scholarship);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(scholarship);
    }

    // =========================================
    //  تعديل منحة
    // =========================================
    public async Task<IActionResult> Edit(int id)
    {
        var scholarship = await _context.Scholarships.FindAsync(id);
        if (scholarship == null) return NotFound();

        return View(scholarship);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Scholarship scholarship)
    {
        if (id != scholarship.Id) return NotFound();

        if (ModelState.IsValid)
        {
            // تحديث Slug تلقائي عند تعديل العنوان
            scholarship.Slug = scholarship.Title.ToLower().Replace(" ", "-");

            _context.Update(scholarship);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(scholarship);
    }

    // =========================================
    //  حذف منحة
    // =========================================
    public async Task<IActionResult> Delete(int id)
    {
        var scholarship = await _context.Scholarships.FindAsync(id);
        if (scholarship == null) return NotFound();

        return View(scholarship);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var scholarship = await _context.Scholarships.FindAsync(id);
        if (scholarship != null)
        {
            _context.Scholarships.Remove(scholarship);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}