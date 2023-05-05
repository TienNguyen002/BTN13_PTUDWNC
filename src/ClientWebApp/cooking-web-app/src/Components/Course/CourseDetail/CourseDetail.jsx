import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { isEmptyOrSpaces } from "../../../Utils/Utils"
import { getCourseBySlug } from "../../../Services/CourseRepository"
import "./CourseDetail.scss"
import Table from "react-bootstrap/Table"
import { faCalendar } from "@fortawesome/free-regular-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { format } from "date-fns";

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
                <div className="course-content-date">
                    <FontAwesomeIcon icon={faCalendar}/>{course.updateDate}
                </div>
                <div className="course-content-chef">
                    <img src={course.chef.imageUrl}/>
                    <p><b className="course-content-chef">Giảng viên:</b> {course.chef.fullName}</p>
                </div>
                <div className="course-content-container">
                    <div className="course-content-img">
                        <img className="course-content-img" src={imageUrl} alt={course.urlSlug}/>
                    </div>
                    <div className="course-content-shortDescription">
                        {course.shortDescription}
                    </div>
                </div>
                <div className="course-content-description">
                        {course.description}
                </div>
                <div className="cooking-table">
                <h4 className="cooking-table-header text-center">LỊCH HỌC</h4>
                <Table>
                    <thead>
                        <tr className="table-header text-center">
                            <td className="table-header-content">STT</td>
                            <td className="table-header-content">Ngày học</td>
                            <td className="table-header-content">Sáng</td>
                            <td className="table-header-content">Chiều</td>
                            <td className="table-header-content">Tối</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr className="text-center">
                            <td>1</td>
                            <td>Thứ 2 – 4 – 6</td>
                            <td>08h30 – 11h30</td>
                            <td>13h30 – 16h30</td>
                            <td>18h00 – 21h00</td>
                        </tr>   
                        <tr className="text-center">
                            <td>2</td>
                            <td>Thứ 3 – 5 – 7</td>
                            <td>09h30 – 12h30</td>
                            <td>14h30 – 15h30</td>
                            <td>17h00 – 22h00</td>
                        </tr>
                    </tbody>
                </Table>              
            </div>
            </div>   
        )
    }
}

export default CourseDetail;