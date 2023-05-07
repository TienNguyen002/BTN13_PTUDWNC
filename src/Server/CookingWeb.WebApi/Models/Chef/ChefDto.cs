﻿namespace CookingWeb.WebApi.Models.Chef
{
    public class ChefDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime JoinedDate { get; set; }
        public string UrlSlug { get; set; }
    }
}