import React, { useState, useEffect} from "react";
import Table from "react-bootstrap/Table";
import { Link } from "react-router-dom";
import { getRecipesFilter, deleteRecipe, toggleStatus } from "../../../Services/RecipeRepository";
import Loading from "../../../Components/Shared/Loading"
import "../Admin.scss"
import RecipeFilterPane from "../../../Components/Admin/Recipe/RecipeFilterPane";
import { useSelector } from "react-redux";
import { faTrash, faEye, faEyeSlash } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const AdminRecipe = () => {
    const [recipesList, setRecipesList] = useState([]),
    [isVisibleLoading, setIsVisibleLoading] = useState(true),
    recipeFilter = useSelector(state => state.recipeFilter);

    let p = 1, ps = 10;

    useEffect(() => {
        document.title = "Danh sách công thức";

        getRecipesFilter(recipeFilter.keyword,
            recipeFilter.authorId,
            recipeFilter.courseId,
            recipeFilter.year,
            recipeFilter.month,
             ps, p).then(data => {
            if(data)
                setRecipesList(data.items);
            else
                setRecipesList([]);
            setIsVisibleLoading(false);
        })
    }, [recipeFilter, p, ps]);

    const handleDelete = (e, id) => {
        e.preventDefault();
        window.location.reload(false);
        DeleteRecipe(id);

        async function DeleteRecipe(id){
            if(window.confirm("Xóa công thức này?")){
                const response = await deleteRecipe(id);
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
            <h1>Danh sách công thức</h1>
            <RecipeFilterPane/>
            {isVisibleLoading ? <Loading/> :
                <Table striped responsive bordered>
                    <thead className="table text-center">
                        <tr className="table-title">
                            <th>Tiêu đề</th>
                            <th>Tác giả</th>
                            <th>Thuộc khóa học</th>
                            <th>Trạng thái</th>
                            <th>Xóa</th>
                        </tr>
                    </thead>
                    <tbody>
                        {recipesList.length > 0 ? recipesList.map((item, index) =>
                            <tr key={index}>
                                <td>
                                    <Link to={`/admin/recipe/edit/${item.id}`}>
                                        {item.title}
                                    </Link>
                                    <p className="shortDescription">{item.shortDescription}</p>
                                </td>
                                <td className="text-center">{item.author.fullName}</td>
                                <td className="text-center">{item.course.title}</td>
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
                                <h4>Không tìm thấy công thức nào</h4>
                            </td>
                        </tr>
                    }
                    </tbody>
                </Table>
            }
        </>
    )
}

export default AdminRecipe;