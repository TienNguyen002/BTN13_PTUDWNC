import React, { useState, useEffect} from "react";
import { Link } from "react-router-dom";
import "./DailyPost.scss"
import { useQuery } from "../../Utils/Utils"
import { getPosts } from "../../Services/PostRepository"

const DailyPost = () => {
    const[postList, setPostList] = useState([]);
    
    let query = useQuery(),
        p = query.get('p') ?? 1,
        ps = query.get('ps') ?? 4;

    useEffect(() => {
        getPosts(ps, p).then(data => {
            if(data){
                setPostList(data.items)
            }
            else
                setPostList([])
        })
    }, [p, ps])

    return(
      <>
        <div className="container">
          
          <div className="wrapper row">
            {postList.map((item, index) => (
              <div className="col-4 post" key={index}>
                <div className="post-image">
                  <Link to={`/thuc-don/${item.urlSlug}`} className="post-image">
                    <img src={`https://localhost:7029/${item.imageUrl}`} alt="IMG" className="post-image"/>
                  </Link>
                </div>
                <div className="post-title">
                  <Link to={`/thuc-don/${item.urlSlug}`} className="course-title">
                    {item.title}
                  </Link>
                </div>
                <div className="post-description">
                    {item.shortDescription}
                </div>
              </div>
            ))}
          </div>
        </div>
      </>
    )
}

export default DailyPost;