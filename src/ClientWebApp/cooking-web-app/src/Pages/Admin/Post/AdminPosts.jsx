import React, { useState, useEffect} from "react";
import Table from "react-bootstrap/Table";
import { Link } from "react-router-dom";
import { getPostsFilter } from "../../../Services/PostRepository";
import Loading from "../../../Components/Shared/Loading"
import "../Admin.scss"
import PostFilterPane from "../../../Components/Admin/Post/PostFilterPane";
import { useSelector } from "react-redux";

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

export default AdminPost;