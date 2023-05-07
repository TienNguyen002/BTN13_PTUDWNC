import {
    faEnvelope,
    faGlobe,
    faPhone,
 } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React from "react";
import "../style/Footer.scss"
import { Link } from "react-router-dom";

const About = () => {
    return(
        <div className="about-footer">
            <h4>LIÊN HỆ</h4>
            <div>
                <p>Nghề Bếp Á Âu đơn vị đào tạo nghề bếp dẫn đầu tại Việt Nam. Tham gia các khóa học tại đây bạn sẽ nắm được các công thức nấu ăn, những mẹo vặt làm cho món ăn ngon hơn. Từ cách chọn, chế biến nguyên liệu, cho đến lúc trang trí để tạo ra một món ăn ngon.</p>
                <div className="footer-address">
                    <FontAwesomeIcon icon={faGlobe} className="footer-fa-icon fa-address" />
                    Địa chỉ: 1 Đường Phù Đổng Thiên Vương, Phường 8, Thành phố Đà Lạt, Lâm Đồng
                </div>
                <div className="footer-hotline">
                    <FontAwesomeIcon icon={faPhone} className="footer-fa-icon fa-phone" />
                    Hotline: 
                    <Link to={"tel:0819104319"} className="footer-hotline-call">
                        0819104319
                    </Link>
                </div>
                <div>
                    <FontAwesomeIcon icon={faEnvelope} className="footer-fa-icon fa-envelope" />
                    Email:
                    <Link to={"mailto:2015749@dlu.edu.vn"} className="footer-email-send">
                        2015749@dlu.edu.vn
                    </Link>
                </div>
            </div>
            <div >
                <Link to={`/gioi-thieu`} className="about-page">Giới thiệu</Link>
            </div>
        </div>
    )
}

export default About;