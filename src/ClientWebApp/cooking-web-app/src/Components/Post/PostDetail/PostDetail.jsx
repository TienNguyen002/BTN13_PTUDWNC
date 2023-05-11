import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { isEmptyOrSpaces } from "../../../Utils/Utils"
import { getPostBySlug } from "../../../Services/PostRepository"
import "./PostDetail.scss"
import { faCalendar } from "@fortawesome/free-regular-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";   

const PostDetail = () => {
    const params = useParams();
    const [post, setPost] = useState(null);
    const { slug } = params;

    let imageUrl = !post || isEmptyOrSpaces(post.imageUrl)
    ? process.env.PUBLIC_URL + "/images/nopicture.png"
    : `https://localhost:7029/${post.imageUrl}`;

    useEffect(() => {
        document.title = "Chi tiết khóa học";
        getPostBySlug(slug).then((data) => {
            window.scroll(0, 0);
            if(data){
                setPost(data);
            }
            else
            setPost({})
        });
    }, [slug]);

    if(post){
        return(
            <div className="post-content container">
                <div className="post-content-title">
                    <h1 className="post-content-title text-center">{post.title}</h1>
                </div>
                <div className="post-content-date">
                    <FontAwesomeIcon icon={faCalendar}/>{post.createDate}
                </div>
                <div className="post-content-container">
                    <div className="post-content-img">
                        <img className="post-content-img" src={imageUrl} alt={post.urlSlug}/>
                    </div>
                    <div className="post-content-shortDescription">
                        {post.shortDescription}
                    </div>
                </div>
                <div className="post-content-description">
                    {post.description}
                </div>
                <div className="post-content-author">
                    <p><b className="color">Tác giả:</b> {post.author.fullName}</p>
                    <p>{post.author.description}</p>
                </div>
            </div>   
        )
    }
}

export default PostDetail;