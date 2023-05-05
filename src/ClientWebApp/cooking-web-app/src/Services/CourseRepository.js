import { get_api, delete_api, post_api, post_image_api ,put_api } from "./Methods"

export function getCourses(
        pageSize = 6,
        pageNumber = 1,){
    return get_api(`https://localhost:7029/api/courses?PublishedOnly=true&PageSize=${pageSize}&PageNumber=${pageNumber}`);
}

export function getCourseBySlug(slug){
    return get_api(`https://localhost:7029/api/courses/${slug}`)
}

export function getCourseById(id){
    return get_api(`https://localhost:7029/api/courses/${id}`)
}

export function getPopularCourse(){
    return get_api(`https://localhost:7029/api/courses/popular/4`);
}

export function getFilter(){
    return get_api(`https://localhost:7029/api/courses/get-filter`)
}

export function getCoursesFilter(
    keyword = '',
    demandId = '',
    priceId = '',
    numberOfSessionsId = '',
    year = '',
    month = '',
    pageSize = 10,
    pageNumber = 1,
    sortColumn = '',
    sortOrder = ''){
        let url = new URL(`https://localhost:7029/api/courses/get-courses-filter`);
        keyword !== '' && url.searchParams.append('Keyword', keyword);
        demandId !== '' && url.searchParams.append('DemandId', demandId);
        priceId !== '' && url.searchParams.append('PriceId', priceId);
        numberOfSessionsId !== '' && url.searchParams.append('NumberOfSessionsId', numberOfSessionsId);
        month !== '' && url.searchParams.append('CreateMonth', month);
        year !== '' && url.searchParams.append('CreateYear', year);
        sortColumn !== '' && url.searchParams.append('SortColumn', sortColumn);
        sortOrder !== '' && url.searchParams.append('SortOrder', sortOrder);
        url.searchParams.append('PageSize', pageSize);
        url.searchParams.append('PageNumber', pageNumber);
        return get_api(url.href);
    }

    export function deleteCourse(id = 0){
        return delete_api(`https://localhost:7029/api/courses/${id}`)
    }
    
    export function toggleStatus(id = 0){
        return get_api(`https://localhost:7029/api/courses/toggle-status/${id}`)
    }

    export function addCourse(course){
        return post_api(`https://localhost:7029/api/courses`, course)
    }

    export function updateCourse(courseId, course){
        return put_api(`https://localhost:7029/api/courses/${courseId}`, course)
    }

    export function updateImage(courseId, formData){
        return post_image_api(`https://localhost:7029/api/courses/${courseId}/picture`, formData)
    }