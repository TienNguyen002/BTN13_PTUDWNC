import React from "react";
import PopularCourse from "../Course/PopularCourse/PopularCourse"
import CourseList from "../Course/Courses/CourseList";
import { Link } from "react-router-dom";
import "./style/index.scss"
import Chef from "../Chef/Chef";
import DailyPost from "../Post/DailyPost";
import Recipe from "../Recipe/Recipe";

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
            <div className="title">
            <Link to={`/hoc-nau-an`} className="title"><h3>HỌC NẤU ĂN</h3></Link>
            <p className="description">Những lớp học chuyên nghiệp và chất lượng hàng đầu và được xem là đang “hot nhất” tại Việt Nam như: món Âu, món Á, món Nhật, món Việt, món Hoa, các lớp theo yêu cầu… có nội dung đa dạng, cung cấp đầy đủ các kiến thức và kỹ năng nấu nướng cần thiết dành cho đầu bếp chuyên nghiệp, cập nhật các xu hướng ẩm thực đang thịnh hành.</p>
          </div>
                <CourseList/>
            </div>
            <div className="chef">
                <Chef/>
            </div>
            <div className="daily-post">
                <div className="title">
                    <h3 className="title">MÓN ĂN MỖI NGÀY</h3>
                </div>
                <DailyPost/>
            </div>
            <div className="recipe">
                <div className="title">
                    <h3 className="title">BÍ QUYẾT NẤU ĂN NGON</h3>
                </div>
                <Recipe/>
            </div>
            <div className="image text-center">
                <img src="https://nghebep.com/wp-content/uploads/2017/06/banner-qc.jpg" alt="hinh-gioi-thieu"/>
            </div>
        </>
        
    )
}

export default Content;