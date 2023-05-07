import React, { useState, useEffect} from "react";
import Table from "react-bootstrap/Table";
import { Link, useParams } from "react-router-dom";
import { getCoursesFilter, toggleStatus, deleteCourse } from "../../../Services/CourseRepository";
import Loading from "../../../Components/Shared/Loading"
import "../Admin.scss"
import CourseFilterPane from "../../../Components/Admin/Course/CourseFilterPane";
import { isInteger } from "../../../Utils/Utils"
import { useSelector } from "react-redux";
import { faTrash, faEye, faEyeSlash } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

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

    const handleDelete = (e, id) => {
        e.preventDefault();
        window.location.reload(false);
        DeleteCourse(id);

        async function DeleteCourse(id){
            if(window.confirm("Xóa khóa học này?")){
                const response = await deleteCourse(id);
                if(response)
                    alert("Xóa thành công!");
                else
                    alert("Lỗi!!");
            }
        }
    };

    const handleToggleStatus = (e, id) => {
        e.preventDefault();
        window.location.reload(false);
        ChangePublished(id);

        async function ChangePublished(id){
            const response = await toggleStatus(id);
            if(response)
                console.log(response);
            else
                console.log("Thay đổi không thành công");
        }
    }

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
                            <th>Xóa</th>
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
                                <td>
                                    <div className="text-center"
                                        onClick={(e) => handleToggleStatus(e, item.id)}>
                                            {item.published 
                                            ? <div className="published">
                                                <FontAwesomeIcon icon={faEye}/> Có
                                            </div>
                                            : <div className="not-published">
                                                <FontAwesomeIcon icon={faEyeSlash}/> Không
                                            </div>}
                                    </div> 
                                </td>
                                <td>
                                    <div className="text-center delete"
                                        onClick={(e) => handleDelete(e, item.id)}>
                                        <FontAwesomeIcon icon={faTrash}/>
                                    </div>
                                </td>
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