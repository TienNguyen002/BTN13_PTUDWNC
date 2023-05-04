import React, { useState, useEffect} from "react";
import { Link } from "react-router-dom";
import "./Recipe.scss"
import { useQuery } from "../../Utils/Utils"
import { getRecipes } from "../../Services/RecipeRepository"

const Recipe = () => {
    const[recipeList, setRecipeList] = useState([]);
    
    let query = useQuery(),
        p = query.get('p') ?? 1,
        ps = query.get('ps') ?? 4;

    useEffect(() => {
        getRecipes(ps, p).then(data => {
            if(data){
                setRecipeList(data.items)
            }
            else
            setRecipeList([])
        })
    }, [p, ps])

    return(
      <>
        <div className="container">
          <div className="wrapper row">
            {recipeList.map((item, index) => (
              <div className="col-4 recipe" key={index}>
                <div className="recipe-image">
                  <Link to={`/cong-thuc/${item.urlSlug}`} className="recipe-image">
                    <img src={`https://localhost:7029/${item.imageUrl}`} alt="IMG" className="recipe-image"/>
                  </Link>
                </div>
                <div className="recipe-title">
                  <Link to={`/cong-thuc/${item.urlSlug}`} className="recipe-title">
                    {item.title}
                  </Link>
                </div>
                <div className="recipe-description">
                    {item.shortDescription}
                </div>
              </div>
            ))}
          </div>
        </div>
      </>
    )
}

export default Recipe;