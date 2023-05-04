import { get_api } from "./Methods"

export function getRecipes(
        pageSize = 4,
        pageNumber = 1,){
    return get_api(`https://localhost:7029/api/recipes?PageSize=${pageSize}&PageNumber=${pageNumber}`);
}

export function getRecipeBySlug(slug){
    return get_api(`https://localhost:7029/api/recipes/${slug}`)
}