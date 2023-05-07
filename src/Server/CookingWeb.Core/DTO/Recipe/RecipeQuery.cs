﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Core.DTO.Recipe
{
    public class RecipeQuery
    {
        public string Keyword { get; set; }
        public int? AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSlug { get; set; }
        public int? CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseSlug { get; set; }
        public int? CreateMonth { get; set; }
        public int? CreateYear { get; set; }
        public bool PublishedOnly { get; set; }
        public bool NotPublished { get; set; }
    }
}