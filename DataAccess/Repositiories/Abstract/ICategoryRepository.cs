﻿using Common.Entities;
using DataAccess.Repositiories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositiories.Abstract
{
	public interface ICategoryRepository : IRepository<Category>
	{
		Task<Category> GetByTitleAsync(string title);
	}
}
