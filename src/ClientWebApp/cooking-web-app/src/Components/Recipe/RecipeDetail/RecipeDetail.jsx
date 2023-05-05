import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { isEmptyOrSpaces } from "../../../Utils/Utils"
import { getRecipeBySlug } from "../../../Services/RecipeRepository"
import "./RecipeDetail.scss"
import { faCalendar } from "@fortawesome/free-regular-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";   

const RecipeDetail = () => {
    const params = useParams();
    const [recipe, setRecipe] = useState(null);
    const { slug } = params;

    let imageUrl = !recipe || isEmptyOrSpaces(recipe.imageUrl)
    ? process.env.PUBLIC_URL + "/images/nopicture.png"
    : `https://localhost:7029/${recipe.imageUrl}`;

    useEffect(() => {
        document.title = "Công thức";
        getRecipeBySlug(slug).then((data) => {
            window.scroll(0, 0);
            if(data){
                setRecipe(data);
            }
            else
                setRecipe({})
        });
    }, [slug]);

    if(recipe){
        return(
            <div className="recipe-content container">
                <div className="recipe-content-title">
                    <h1 className="recipe-content-title text-center">{recipe.title}</h1>
                </div>
                <div className="recipe-content-date">
                    <FontAwesomeIcon icon={faCalendar}/>{recipe.createDate}
                </div>
                <div className="recipe-content-container">
                    <div className="recipe-content-img">
                        <img className="recipe-content-img" src={imageUrl} alt={recipe.urlSlug}/>
                    </div>
                    <div className="recipe-content-shortDescription">
                        {recipe.shortDescription}
                    </div>
                </div>
                <div className="recipe-content-description">
                    {recipe.description}
                </div>
                <div className="recipe-content-ingredient">
                    <h4 className="recipe-content-ingredient-title">Nguyên liệu</h4>
                    {recipe.ingredient}
                </div>
                <div className="recipe-content-step">
                <h4 className="recipe-content-step-title">Các bước thực hiện</h4>
                    {recipe.step}
                </div>
                <div className="recipe-content-author">
                    <p><b className="color">Tác giả:</b> {recipe.author.fullName}</p>
                    <p>{recipe.author.description}</p>
                </div>
            </div>   
        )
    }
}

export default RecipeDetail;