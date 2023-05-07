using Mapster;
using CookingWeb.Core.Entities;
using CookingWeb.WebApi.Models.Course;
using CookingWeb.WebApi.Models.Recipe;

namespace CookingWeb.WebApi.Mapsters
{
    public class MapsterConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Course, CourseDto>();
            config.NewConfig<Course, CourseDetail>();

            config.NewConfig<Recipe, RecipeDto>();
            config.NewConfig<Recipe, RecipeDetail>();
        }
    }
}
