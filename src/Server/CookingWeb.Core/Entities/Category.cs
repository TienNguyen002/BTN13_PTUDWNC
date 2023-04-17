using CookingWeb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Core.Entities
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlSlug { get; set; }
        public bool ShowOnMenu { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        public IList<Post> Posts { get; set; }
    }
}
