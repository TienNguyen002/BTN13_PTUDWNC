import React from "react";
import PopularCourse from "../Course/PopularCourse/PopularCourse"
import CourseList from "../Course/Courses/CourseList";
import "./style/index.scss"

const Content = () => {
    return(
        <>
            <div className="popular">
                <PopularCourse/>  
            </div>
            <div className='more-info'>
                Học viên Nghề Bếp Á Âu được lĩnh hội giá trị nghề và năng lực nghiệp vụ chất lượng bằng phương pháp đào tạo sát thực tiễn trong môi trường học tập hiện đại. Với hơn 100.000 học viên trên khắp các tỉnh thành được đào tạo mỗi năm thuộc đa ngành Nhà hàng – Khách sạn, Bếp trưởng, Pha chế, Làm bánh, Quản lý và Kinh doanh ẩm thực…
            </div>
            <div className="course-list">
                <CourseList/>
            </div>
        </>
        
    )
}

export default Content;