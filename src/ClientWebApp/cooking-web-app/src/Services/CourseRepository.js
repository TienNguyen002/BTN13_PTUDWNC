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

export function getFilter(){
    return get_api(`https://localhost:7029/api/courses/get-filter`)
}

export function getCoursesFilter(keyword = '',
    demandId = '',
    priceId = '',
    numberofsessionsId = '',
    year = '',
    month = '',
    pageSize = 10,
    pageNumber = 1,
    sortColumn = '',
    sortOrder = ''){
        let url = new URL(`https://localhost:7029/api/courses/get-courses-filter`);
        keyword !== '' & url.searchParams.append('Keyword', keyword);
        demandId !== '' & url.searchParams.append('DemandId', demandId);
        priceId !== '' & url.searchParams.append('PriceId', priceId);
        numberofsessionsId !== '' && url.searchParams.append('NumberOfSessionsId', numberofsessionsId);
        year !== '' && url.searchParams.append('CreateYear', year);
        month !== '' && url.searchParams.append('CreateMonth', month);
        sortColumn !== '' && url.searchParams.append('SortColumn', sortColumn);
        sortOrder !== '' && url.searchParams.append('SortOrder', sortOrder);
        url.searchParams.append('PageSize', pageSize);
        url.searchParams.append('PageNumber', pageNumber);
        return get_api(url.href);
    }