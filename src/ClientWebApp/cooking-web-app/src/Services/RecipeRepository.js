import { get_api } from "./Methods"

export function getRecipes(
        pageSize = 4,
        pageNumber = 1,){
    return get_api(`https://localhost:7029/api/recipes?PageSize=${pageSize}&PageNumber=${pageNumber}`);
}

export function getRecipeBySlug(slug){
    return get_api(`https://localhost:7029/api/recipes/${slug}`)
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