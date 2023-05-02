using CookingWeb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Core.Entities
{
    public class Recipe : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Metadata { get; set; }
        public string ImageUrl { get; set; }
        public string UrlSlug { get; set; }
        public string Ingredient { get; set; }
        public string Step { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool Published { get; set; }
        public int ViewCount { get; set; }
        public int AuthorId { get; set; }
        public int CourseId { get; set; }
        public Author Author { get; set; }
        public Course Course { get; set; }
    }
}
