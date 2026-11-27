using System.Security.Claims;
using CrudApplication.Data;
using CrudApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudApplication.Controllers;

[Authorize]
public class ProductsController : Controller
{
    private readonly AppDbContext _db;

    public ProductsController(AppDbContext db)
    {
        _db = db;
    }

    private string GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
    }

    public async Task<IActionResult> Index()
    {
        var userId = GetUserId();

        var list = await _db.Products
            .Where(p => p.OwnerId == userId)
            .OrderBy(p => p.Id)
            .ToListAsync();

        return View(list);
    }

    public IActionResult Create()
    {
        return View(new ProductForm());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductForm form)
    {
        if (!ModelState.IsValid)
        {
            return View(form);
        }

        var product = new Product
        {
            Name = form.Name.Trim(),
            Description = form.Description,
            Price = form.Price,
            OwnerId = GetUserId()
        };

        _db.Products.Add(product);
        await _db.SaveChangesAsync();

        TempData["Message"] = "Product added.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var userId = GetUserId();

        var product = await _db.Products
            .FirstOrDefaultAsync(p => p.Id == id && p.OwnerId == userId);

        if (product == null)
        {
            return NotFound();
        }

        ViewData["Id"] = product.Id;

        return View(new ProductForm
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProductForm form)
    {
        var userId = GetUserId();

        var product = await _db.Products
            .FirstOrDefaultAsync(p => p.Id == id && p.OwnerId == userId);

        if (product == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            ViewData["Id"] = id;
            return View(form);
        }

        product.Name = form.Name.Trim();
        product.Description = form.Description;
        product.Price = form.Price;

        await _db.SaveChangesAsync();

        TempData["Message"] = "Product updated.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var userId = GetUserId();

        var product = await _db.Products
            .FirstOrDefaultAsync(p => p.Id == id && p.OwnerId == userId);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var userId = GetUserId();

        var product = await _db.Products
            .FirstOrDefaultAsync(p => p.Id == id && p.OwnerId == userId);

        if (product == null)
        {
            return NotFound();
        }

        _db.Products.Remove(product);
        await _db.SaveChangesAsync();

        TempData["Message"] = "Product deleted.";
        return RedirectToAction(nameof(Index));
    }
}
