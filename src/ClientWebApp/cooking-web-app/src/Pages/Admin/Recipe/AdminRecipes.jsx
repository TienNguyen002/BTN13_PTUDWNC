import React, { useState, useEffect} from "react";
import Table from "react-bootstrap/Table";
import { Link } from "react-router-dom";
import { getRecipes } from "../../../Services/RecipeRepository";
import Loading from "../../../Components/Shared/Loading"
import "../Admin.scss"
import RecipeFilterPane from "../../../Components/Admin/Recipe/RecipeFilterPane";

const AdminRecipe = () => {
    const [recipesList, setRecipesList] = useState([]);
    const [isVisibleLoading, setIsVisibleLoading] = useState(true); 

    let p = 1, ps = 10;

    useEffect(() => {
        document.title = "Danh sách công thức";

        getRecipes( ps, p).then(data => {
            if(data)
                setRecipesList(data.items);
            else
                setRecipesList([]);
            setIsVisibleLoading(false);
        })
    }, [ p, ps]);

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

export default AdminRecipe;