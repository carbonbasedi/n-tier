using Business.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
	public interface ICategoryService
	{
		Task<List<CategoryListItemVM>> GetAllAsync();
		Task<bool> CreateAsync(CategoryCreateVM model);
		Task<bool> DeleteAsync(int id);
		Task<CategoryUpdateVM> UpdateAsync(int id);
		Task<bool> UpdateAsync(CategoryUpdateVM model,int id);
	}
}
