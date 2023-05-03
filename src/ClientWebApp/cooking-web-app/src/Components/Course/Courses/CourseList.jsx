import React, { useState, useEffect} from "react";
import { Link } from "react-router-dom";
import "./Course.scss"
import { useQuery } from "../../../Utils/Utils";
import { getCourses } from "../../../Services/CourseRepository"
import CourseItem from "./CourseItem"

const CourseList = () => {
    const[courseList, setCourseList] = useState([]);
    
    let query = useQuery(),
        p = query.get('p') ?? 1,
        ps = query.get('ps') ?? 10;

    useEffect(() => {
        getCourses(ps, p).then(data => {
            if(data){
                setCourseList(data.items)
            }
            else
                setCourseList([])
        })
    }, [p, ps])
    // return(
    //     <>
    //         <div className="container">
    //             <div className="col">
    //                 <Link to="/hoc-nau-an" className="title"><h3>HỌC NẤU ĂN</h3></Link>
    //                 <p>Những lớp học chuyên nghiệp và chất lượng hàng đầu và được xem là đang “hot nhất” tại Việt Nam như: món Âu, món Á, món Nhật, món Việt, món Hoa, các lớp theo yêu cầu… có nội dung đa dạng, cung cấp đầy đủ các kiến thức và kỹ năng nấu nướng cần thiết dành cho đầu bếp chuyên nghiệp, cập nhật các xu hướng ẩm thực đang thịnh hành.</p>
    //             </div>
    //         </div>
    //     </>
    if(courseList.length > 0)
        return (
          <div className="p-4">
            {courseList.map((item, index) => {
              return (
                <CourseItem courseItem={item} key={index}/>
              );
            })}
          </div>
        );
      else return(
        <></>
      );   
}

export default CourseList;