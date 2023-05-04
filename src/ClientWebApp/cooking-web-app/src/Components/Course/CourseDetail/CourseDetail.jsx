import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { isEmptyOrSpaces } from "../../../Utils/Utils"
import { getCourseBySlug } from "../../../Services/CourseRepository"
import "./CourseDetail.scss"

const CourseDetail = () => {
    const params = useParams();
    const [course, setCourse] = useState(null);
    const { slug } = params;

    let imageUrl = !course || isEmptyOrSpaces(course.imageUrl)
    ? process.env.PUBLIC_URL + "/images/nopicture.png"
    : `https://localhost:7029/${course.imageUrl}`;

    useEffect(() => {
        document.title = "Chi tiết khóa học";
        getCourseBySlug(slug).then((data) => {
            window.scroll(0, 0);
            if(data){
                setCourse(data);
            }
            else
                setCourse({})
        });
    }, [slug]);

    if(course){
        return(
            <div className="course-content container">
                <div className="course-content-title">
                    <h1 className="course-content-title text-center">{course.title}</h1>
                </div>
                <div className="course-content-shortDescription">
                    {course.shortDescription}
                </div>
                <div className="course-content-img">
                    <img className="course-content-img" src={imageUrl} alt={course.urlSlug}/>
                </div>
                <div className="course-content-description">
                    {course.description}
                </div>
            </div>   
        )
    }
}

export default CourseDetail;