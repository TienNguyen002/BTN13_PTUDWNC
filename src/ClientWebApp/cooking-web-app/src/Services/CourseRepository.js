import { get_api, delete_api, post_api, post_image_api ,put_api } from "./Methods"
import axios from "axios";

export function getCourses(
        pageSize = 6,
        pageNumber = 1,){
    return get_api(`https://localhost:7029/api/courses?PublishedOnly=true&PageSize=${pageSize}&PageNumber=${pageNumber}`);
}

export function getAllCourses(){
  return get_api(`https://localhost:7029/api/courses/allcourses`);
}

export function getCourseBySlug(slug){
    return get_api(`https://localhost:7029/api/courses/byslug/${slug}`)
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

    export async function addCourse(course) {
        try {
          const res = await axios.post(`https://localhost:7029/api/courses`, course);
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

      export async function updateCourse(courseId, course) {
        try {
          const res = await axios.put(`https://localhost:7029/api/courses/${courseId}`, course);
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

      export async function updateImage(courseId, image) {
        try {
          const formData = new FormData();
          formData.append("file", image);
          const res = await axios.post(`https://localhost:7029/api/courses/${courseId}/picture`, formData);
      
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