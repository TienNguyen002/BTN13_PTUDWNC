﻿using CookingWeb.WebApi.Models.Author;
using CookingWeb.WebApi.Models.Category;

namespace CookingWeb.WebApi.Models.Post
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string UrlSlug { get; set; }
        public string ImageUrl { get; set; }
        public bool Published { get; set; }
        public int ViewCount { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public AuthorDto Author { get; set; }
        public CategoryDto Category { get; set; }
    }
}