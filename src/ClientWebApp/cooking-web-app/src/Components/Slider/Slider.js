import React from "react";
import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import "./style/index.scss"
import img1 from "./images/trung-tam-dao-tao-nghe-dau-bep.jpg"
import img2 from "./images/thoi-gian-hoc-thuc-hanh.jpg"
import img3 from "./images/phong-thuc-hanh-hien-dai.jpg"
import img4 from "./images/nhieu-chi-nhanh-tai-viet-nam.jpg"
import img5 from "./images/co-hoi-viec-lam-on-dinh-thu-nhap-cao.jpg"

const Slide = () => {
    const setting = {
        dots: false,
        infinite: true,
        speed: 500,
        slidesToShow: 1,
        slidesToScroll: 1,
        autoplay: {
            delay: 5000,
            disableOnInteraction: false,
        }
    }

    return (
        <div className="image_banner">
          <Slider {...setting}>
            <div>
              <img src={img1} alt="anh1" className="image-slide" />
            </div>
            <div>
              <img src={img2} alt="anh2" className="image-slide" />
            </div>
            <div>
              <img src={img3} alt="anh3" className="image-slide" />
            </div>
            <div>
              <img src={img4} alt="anh4" className="image-slide" />
            </div>
            <div>
              <img src={img5} alt="anh5" className="image-slide" />
            </div>
          </Slider>
        </div>
      );
}

export default Slide