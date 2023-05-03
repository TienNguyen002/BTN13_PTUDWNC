import { get_api } from "./Methods"

export function getCourses(
        pageSize = 6,
        pageNumber = 1,){
    return get_api(`https://localhost:7029/api/courses?PageSize=${pageSize}&PageNumber=${pageNumber}`);
}

export function getCourseBySlug(slug){
    return get_api(`https://localhost:7029/api/courses/${slug}`)
}

export function getPopularCourse(){
    return get_api(`https://localhost:7029/api/courses/popular/4`);
}