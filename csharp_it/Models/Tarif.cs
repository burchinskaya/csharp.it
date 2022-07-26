﻿using System;
using System.Collections.Generic;

namespace csharp_it.Models
{
	public class Tarif
	{
		public Guid Id { get; set; }
		public int CourseId { get; set; }
		public virtual Course Course { get; set; }
		public double Price { get; set; }
		// 0 - USD
		// 1 - UAH
		// 2 - EUR
		public int Currency { get; set; }
		public string Description { get; set; }

		public virtual List<UserCourse> UserCourses { get; set; }
		public virtual List<TarifAccess> TarifAccesses { get; set; }

		public Tarif()
        {
			UserCourses = new List<UserCourse>();
			TarifAccesses = new List<TarifAccess>();
        }
	}
}

