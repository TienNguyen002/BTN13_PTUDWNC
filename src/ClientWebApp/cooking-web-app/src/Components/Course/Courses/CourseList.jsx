import React, { useState, useEffect} from "react";
import { Link } from "react-router-dom";
import "./Course.scss"
import { useQuery } from "../../../Utils/Utils";
import { getCourses } from "../../../Services/CourseRepository"

const CourseList = () => {
    const[courseList, setCourseList] = useState([]);
    
    let query = useQuery(),
        p = query.get('p') ?? 1,
        ps = query.get('ps') ?? 6;

    useEffect(() => {
        getCourses(ps, p).then(data => {
            if(data){
                setCourseList(data.items)
            }
            else
                setCourseList([])
        })
    }, [p, ps])

    return(
      <>
        <div className="container">
          <div className="wrapper row">
            {courseList.map((item, index) => (
              <div className="col-6 course" key={index}>
                <div className="course-image">
                  <Link to={`/khoa-hoc/${item.urlSlug}`} className="course-image">
                    <img src={`https://localhost:7029/${item.imageUrl}`} alt={item.urlSlug}/>
                  </Link>
                </div>
                <div className="course-title">
                  <Link to={`/khoa-hoc/${item.urlSlug}`} className="course-title">
                    {item.title}
                  </Link>
                </div>
              </div>
            ))}
          </div>
        </div>
      </>
    )
}

export default CourseList;