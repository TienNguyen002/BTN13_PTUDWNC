import { get_api, delete_api } from "./Methods"

export function getPosts(
        pageSize = 4,
        pageNumber = 1,){
    return get_api(`https://localhost:7029/api/posts?PublishedOnly=true&PageSize=${pageSize}&PageNumber=${pageNumber}`);
}

export function getPostBySlug(slug){
    return get_api(`https://localhost:7029/api/posts/${slug}`)
}

export function getFilter(){
    return get_api(`https://localhost:7029/api/posts/get-filter`)
}

export function getPostsFilter(
    keyword = '',
    authorId = '',
    categoryId = '',
    year = '',
    month = '',
    pageSize = 10,
    pageNumber = 1,
    sortColumn = '',
    sortOrder = ''){
        let url = new URL(`https://localhost:7029/api/posts/get-posts-filter`);
        keyword !== '' && url.searchParams.append('Keyword', keyword);
        authorId !== '' && url.searchParams.append('AuthorId', authorId);
        categoryId !== '' && url.searchParams.append('CategoryId', categoryId);
        month !== '' && url.searchParams.append('CreateMonth', month);
        year !== '' && url.searchParams.append('CreateYear', year);
        sortColumn !== '' && url.searchParams.append('SortColumn', sortColumn);
        sortOrder !== '' && url.searchParams.append('SortOrder', sortOrder);
        url.searchParams.append('PageSize', pageSize);
        url.searchParams.append('PageNumber', pageNumber);
        return get_api(url.href);
    }

    export function deletePost(id = 0){
        return delete_api(`https://localhost:7029/api/posts/${id}`)
    }
    
    export function toggleStatus(id = 0){
        return get_api(`https://localhost:7029/api/posts/toggle-status/${id}`)
    }