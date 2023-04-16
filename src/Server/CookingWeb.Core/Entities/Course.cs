using CookingWeb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Core.Entities
{
    public class Course : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string UrlSlug { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int DemandId { get; set; }
        public string AgeToLearn { get; set; }
        public string Price { get; set; }
        public string NumberOfSessions { get; set; }
        public bool Published { get; set; }
        public int RegisterCount { get; set; }
        public int ChefId { get; set; }
        public int StudentId { get; set; }
        public Demand Demand { get; set; }
        public IList<Recipe> Recipes { get; set; }
        public IList<Chef> Chefs { get; set; }
        public IList<Student> Students { get; set; }
    }
}
