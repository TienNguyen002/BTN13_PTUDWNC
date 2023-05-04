import { get_api } from "./Methods"

export function getPosts(
        pageSize = 4,
        pageNumber = 1,){
    return get_api(`https://localhost:7029/api/posts?PageSize=${pageSize}&PageNumber=${pageNumber}`);
}

export function getPostBySlug(slug){
    return get_api(`https://localhost:7029/api/posts/${slug}`)
}