using Business.Services.Abstract;
using Business.ViewModels.Category;
using Common.Entities;
using DataAccess.Repositiories.Abstract;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository _categoryRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ModelStateDictionary _modelState;

		public CategoryService(
								ICategoryRepository categoryRepository,
								IUnitOfWork unitOfWork,
								IActionContextAccessor actionContextAccessor)
		{
			_categoryRepository = categoryRepository;
			_unitOfWork = unitOfWork;
			_modelState = actionContextAccessor.ActionContext.ModelState;
		}
		public async Task<bool> CreateAsync(CategoryCreateVM model)
		{
			if (!_modelState.IsValid) return false;

			var category = await _categoryRepository.GetByTitleAsync(model.Title);
			if (category != null)
			{
				_modelState.AddModelError("Title", "Category under this Title exists");
				return false;
			}

			category = new Category
			{
				Title = model.Title,
				CreatedAt = DateTime.Now,
			};

			await _categoryRepository.CreateAsync(category);
			await _unitOfWork.CommitAsync();
			return true;
		}
		public async Task<CategoryUpdateVM> UpdateAsync(int id)
		{
			var category = await _categoryRepository.GetAsync(id);
			if (category is null) return null;

			var model = new CategoryUpdateVM
			{
				Title = category.Title,
			};
			return model;
		}

		public async Task<bool> UpdateAsync(CategoryUpdateVM model, int id)
		{
			if (!_modelState.IsValid) return false;

			var category = await _categoryRepository.GetByTitleAsync(model.Title);
			if (category != null)
			{
				_modelState.AddModelError("Title", "Category under this Title exists");
				return false;
			}

			category = await _categoryRepository.GetAsync(id);
			if (category is null)
			{
				_modelState.AddModelError("CategoryNotFound", "Category doesn't exist");
				return false;
			}

			category.Title = model.Title;
			category.UpdatedAt = DateTime.Now;

			_categoryRepository.Update(category);
			await _unitOfWork.CommitAsync();
			return true;
		}

		public async Task<List<CategoryListItemVM>> GetAllAsync()
		{
			var dbCategories = await _categoryRepository.GetAllAsync();

			var model = new List<CategoryListItemVM>();
			foreach(var dbCategory in dbCategories)
			{
				model.Add(new CategoryListItemVM
				{
					Id = dbCategory.Id,
					Title = dbCategory.Title
				});
			}
			return model;
		}
		public async Task<bool> DeleteAsync(int id)
		{
			var category = await _categoryRepository.GetAsync(id);
			if(category is null)
			{
				_modelState.AddModelError("CategoryNotFound", "Category doesn't exist");
				return false;
			}

			_categoryRepository.Delete(category);
			await _unitOfWork.CommitAsync();

			return true;
		}

	}
}
