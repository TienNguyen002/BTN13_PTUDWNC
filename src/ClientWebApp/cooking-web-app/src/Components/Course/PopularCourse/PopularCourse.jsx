import React, { useState, useEffect } from "react"
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import Slider from "react-slick";
import { Link } from "react-router-dom";
import { getPopularCourse } from "../../../Services/CourseRepository"
import { isEmptyOrSpaces } from "../../../Utils/Utils"
import "./PopularCourse.scss"

const PopularCourse = ({ courseItem }) => {
    const courseSetting = {
        dots: false,
        infinite: true,
        speed: 500,
        slidesToShow: 3,
        slidesToScroll: 1,

        autoplay: {
            delay: 3000,
            disableOnInteraction: false,
        },
    }

    let imageUrl = !courseItem || isEmptyOrSpaces(courseItem.imageUrl)
        ? process.env.PUBLIC_URL + "/images/nopicture.png"
        : `https://localhost:7029/${courseItem.imageUrl}`;


    const [popularCourse, setPopularCourse] = useState([]);

    useEffect(() => {
        getPopularCourse().then((data) => {
            if (data) {
                setPopularCourse(data);
            }
            else {
                setPopularCourse([]);
            }
        });
    }, [])

    return (
        <>
            <div className="container">
                <div className="course-header row pt-5 pb-3">
                    <div className="col">
                        <h3 className="course-header-title text-center">Các khóa học phổ biến</h3>
                    </div>
                    <Slider {...courseSetting}>
                        {popularCourse.map((value, index) => {
                            return (
                                <>
                                    <div className="container" key={index}>
                                        <div className="course-header">
                                            <div className="course-header-item text-center">
                                                <Link to={`/hoc-nau-an/${value.urlSlug}`} className="course-header-item">
                                                    <img
                                                        src={imageUrl}
                                                        alt={value.name}
                                                        className="course-header-item-img"
                                                    />
                                                    <h5 className="course-header-item-title">{value.title}</h5>
                                                </Link>
                                            </div>
                                        </div>
                                    </div>
                                </>
                            );
                        })}
                    </Slider>
                </div>
            </div>
        </>
    )
}

export default PopularCourse;