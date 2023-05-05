import React, { useState, useEffect} from "react";
import Table from "react-bootstrap/Table";
import { Link } from "react-router-dom";
import { getPostsFilter, toggleStatus, deletePost } from "../../../Services/PostRepository";
import Loading from "../../../Components/Shared/Loading"
import "../Admin.scss"
import PostFilterPane from "../../../Components/Admin/Post/PostFilterPane";
import { useSelector } from "react-redux";
import { faTrash, faEye, faEyeSlash } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";


const AdminPost = () => {
    const [postsList, setPostsList] = useState([]),
    [isVisibleLoading, setIsVisibleLoading] = useState(true),
    postFilter = useSelector(state => state.postFilter);

    let p = 1, ps = 10;

    useEffect(() => {
        document.title = "Danh sách bài viết";

        getPostsFilter(postFilter.keyword,
            postFilter.authorId,
            postFilter.categoryId,
            postFilter.year,
            postFilter.month,
            ps, p).then(data => {
            if(data)
                setPostsList(data.items);
            else
                setPostsList([]);
            setIsVisibleLoading(false);
        })
    }, [postFilter, p, ps]);

    const handleDelete = (e, id) => {
        e.preventDefault();
        window.location.reload(false);
        DeletePost(id);

        async function DeletePost(id){
            if(window.confirm("Xóa bài viết này?")){
                const response = await deletePost(id);
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
            <h1>Danh sách bài viết</h1>
            <PostFilterPane/>
            {isVisibleLoading ? <Loading/> :
                <Table striped responsive bordered>
                    <thead className="table text-center">
                        <tr className="table-title">
                            <th>Tiêu đề</th>
                            <th>Tác giả</th>
                            <th>Thuộc chủ đề</th>
                            <th>Trạng thái</th>
                            <th>Xóa</th>
                        </tr>
                    </thead>
                    <tbody>
                        {postsList.length > 0 ? postsList.map((item, index) =>
                            <tr key={index}>
                                <td>
                                    <Link to={`/admin/post/edit/${item.id}`}>
                                        {item.title}
                                    </Link>
                                    <p className="shortDescription">{item.shortDescription}</p>
                                </td>
                                <td className="text-center">{item.author.fullName}</td>
                                <td className="text-center">{item.category.name}</td>
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

export default AdminPost;