import React, { useState, useEffect} from "react";
import Table from "react-bootstrap/Table";
import { Link, useParams } from "react-router-dom";
import { getCoursesFilter } from "../../../Services/CourseRepository";
import Loading from "../../../Components/Shared/Loading"
import "../Admin.scss"
import CourseFilterPane from "../../../Components/Admin/Course/CourseFilterPane";
import { isInteger } from "../../../Utils/Utils"
import { useSelector } from "react-redux";

const AdminCourse = () => {
    const [coursesList, setCoursesList] = useState([]),
    [isVisibleLoading, setIsVisibleLoading] = useState(true),
    courseFilter = useSelector(state => state.courseFilter);

    let {id} = useParams(), 
    p = 1, ps = 10;

    useEffect(() => {
        document.title = "Danh sách khóa học";

        getCoursesFilter( courseFilter.keyword,
            courseFilter.demandId,
            courseFilter.priceId,
            courseFilter.numberOfSessionsId,
            courseFilter.year,
            courseFilter.month,
            ps, p).then(data => {
            if(data)
                setCoursesList(data.items);
            else
                setCoursesList([]);
            setIsVisibleLoading(false);
        })
    }, [courseFilter, p, ps]);

    return(
        <>
            <h1>Danh sách khóa học</h1>
            <CourseFilterPane/>
            {isVisibleLoading ? <Loading/> :
                <Table striped responsive bordered>
                    <thead className="table text-center">
                        <tr className="table-title">
                            <th>Tiêu đề</th>
                            <th>Đầu bếp</th>
                            <th>Loại nhu cầu</th>
                            <th>Giá</th>
                            <th>Số buổi</th>
                            <th>Trạng thái</th>
                        </tr>
                    </thead>
                    <tbody>
                        {coursesList.length > 0 ? coursesList.map((item, index) =>
                            <tr key={index}>
                                <td>
                                    <Link to={`/admin/courses/edit/${item.id}`}>
                                        {item.title}
                                    </Link>
                                    <p className="shortDescription">{item.shortDescription}</p>
                                </td>
                                <td className="text-center">{item.chef.fullName}</td>
                                <td className="text-center">{item.demand.name}</td>
                                <td className="text-center">{item.price.name}</td>
                                <td className="text-center">{item.numberOfSessions.name}</td>
                                <td className="text-center">{item.published ? "Có" : "Không"}</td>
                            </tr>
                        ) : 
                        <tr>
                            <td>
                                <h4>Không tìm thấy khóa học nào</h4>
                            </td>
                        </tr>
                    }
                    </tbody>
                </Table>
            }
        </>
    )
}

export default AdminCourse;