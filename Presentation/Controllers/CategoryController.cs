using Business.Services.Abstract;
using Business.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ICategoryService _categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[HttpGet]
		public async Task<IActionResult> List()
		{
			var model = await _categoryService.GetAllAsync();
			return View(model);
		}

		#region Create
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(CategoryCreateVM model)
		{
			var isSucceeded = await _categoryService.CreateAsync(model);
			if (isSucceeded) return RedirectToAction(nameof(Index));

			return View(model);
		}
		#endregion

		#region Update

		[HttpGet]
		public async Task<IActionResult> Update(int id)
		{
			var model = await _categoryService.UpdateAsync(id);
			if (model is null) return NotFound();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Update(CategoryUpdateVM model,int id)
		{
			var isSucceded = await _categoryService.UpdateAsync(model,id);
			if (isSucceded) return RedirectToAction(nameof(Index));

			return View(model);
		}

		#endregion

		#region Delete
		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var isSucceeded = await _categoryService.DeleteAsync(id);
			if(isSucceeded) return RedirectToAction(nameof(Index));

			return NotFound("Category not found");
		}
		#endregion
	}
}
