import { get_api, delete_api } from "./Methods"
import axios from "axios";

export function getRecipes(
        pageSize = 4,
        pageNumber = 1,){
    return get_api(`https://localhost:7029/api/recipes?PublishedOnly=true&PageSize=${pageSize}&PageNumber=${pageNumber}`);
}

export function getRecipeById(id){
    return get_api(`https://localhost:7029/api/recipes/${id}`)
}

export function getRecipeBySlug(slug){
    return get_api(`https://localhost:7029/api/recipes/byslug/${slug}`)
}

export function getFilter(){
    return get_api(`https://localhost:7029/api/recipes/get-filter`)
}

export function getRecipesFilter(
    keyword = '',
    authorId = '',
    courseId = '',
    year = '',
    month = '',
    pageSize = 10,
    pageNumber = 1,
    sortColumn = '',
    sortOrder = ''){
        let url = new URL(`https://localhost:7029/api/recipes/get-recipes-filter`);
        keyword !== '' && url.searchParams.append('Keyword', keyword);
        authorId !== '' && url.searchParams.append('AuthorId', authorId);
        courseId !== '' && url.searchParams.append('CourseId', courseId);
        month !== '' && url.searchParams.append('CreateMonth', month);
        year !== '' && url.searchParams.append('CreateYear', year);
        sortColumn !== '' && url.searchParams.append('SortColumn', sortColumn);
        sortOrder !== '' && url.searchParams.append('SortOrder', sortOrder);
        url.searchParams.append('PageSize', pageSize);
        url.searchParams.append('PageNumber', pageNumber);
        return get_api(url.href);
    }

    export function deleteRecipe(id = 0){
        return delete_api(`https://localhost:7029/api/recipes/${id}`)
    }
    
    export function toggleStatus(id = 0){
        return get_api(`https://localhost:7029/api/recipes/toggle-status/${id}`)
    }

    export async function addRecipe(recipe) {
        try {
          const res = await axios.post(`https://localhost:7029/api/recipes`, recipe);
          const data = res.data;
          if (data.isSuccess) {
            return data.result;
          } else {
            console.log(data);
            return null;
          }
        } catch (error) {
          console.log('Error', error);
          return null;
        }
      }

      export async function updateRecipe(recipeId, recipe) {
        try {
          const res = await axios.put(`https://localhost:7029/api/recipes/${recipeId}`, recipe);
          const data = res.data;
          if (data.isSuccess) {
            return data.result;
          } else {
            return null;
          }
        } catch (error) {
          console.log('Error', error);
          return null;
        }
      }

      export async function updateImage(recipeId, image) {
        try {
          const formData = new FormData();
          formData.append("file", image);
          const res = await axios.post(`https://localhost:7029/api/recipes/${recipeId}/picture`, formData);
      
          const data = res.data;
          if (data.isSuccess) {
            return data;
          } else {
            return null;
          }
        } catch (error) {
          console.log('Error', error);
          return null;
        }
      }