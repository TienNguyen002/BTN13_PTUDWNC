using CookingWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Core.DTO.Author;

public class AuthorItem
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string UrlSlug { get; set; }
    public DateTime JoinedDate { get; set; }
    public IList<Recipe> Recipes { get; set; }
    public IList<Post> Posts { get; set; }
}