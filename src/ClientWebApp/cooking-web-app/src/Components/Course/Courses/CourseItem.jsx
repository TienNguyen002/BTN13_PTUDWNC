import React from "react";
import { Link } from "react-router-dom";
import "./Course.scss"
import { isEmptyOrSpaces } from "../../../Utils/Utils"
import Card from 'react-bootstrap/Card';

const CourseItem = (courseItem) => {
    let imageUrl = !courseItem || isEmptyOrSpaces(courseItem.imageUrl)
        ? process.env.PUBLIC_URL + "/images/nopicture.png"
        : `https://localhost:7029/${courseItem.imageUrl}`;

    return(
        <>
            <article className='blog-entry mb-4'>
            <Card>
                <div className='row g-0'>
                    <div className='col-md-4'>
                        <Card.Img variant='top' src={imageUrl} alt={courseItem.title}/>
                    </div>
                    <div className='col-md-8'>
                        <Card.Body>
                            <Card.Title>
                                <Link className="text-decoration-none"
                                    to={``}>
                                        {courseItem.title}
                                </Link>
                            </Card.Title>
                            
                            <div className='text-end'>
                                <Link
                                to={``}
                                className='btn btn-primary'
                                title={courseItem.title}>
                                    Xem chi tiết
                                </Link>
                            </div>
                        </Card.Body>
                    </div>
                </div>
            </Card>
            </article>
        </>
    )
}

export default CourseItem;