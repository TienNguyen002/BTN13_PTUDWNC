import React from "react";
import "./Chef.scss"
import ChefData from "../../Data/ChefData"
import { Link } from "react-router-dom";

const Chef = () => {
    return (
        <>
            <div className="container chef">
                <div className="title">
                    <h3 className="title">GIẢNG VIÊN</h3>
                    <p className="description">Đội ngũ giảng viên tại trung tâm dạy nấu ăn sẽ “cầm tay chỉ nghề”, rèn luyện những kỹ năng từ nhỏ nhất đến chuyên sâu nhất. “Truyền nghề” luôn là mục đích hướng đến trong chương trình dạy nấu ăn của Nghề Bếp Á Âu</p>
                </div>
                <div className="wrapper row">
                    {ChefData.map((item, index) => (
                        <div className="col-3 chef-item" key={index}>
                            <div className="chef-item-image">
                                <Link to={``} className="chef-item-image">
                                    <img src={item.image} />
                                </Link>
                            </div>
                            <div className="chef-name">
                                <Link to={``} className="chef-item-name">
                                    {item.name}
                                </Link>
                            </div>
                            <div className="chef-item-des">
                                {item.description}
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        </>
    )
}

export default Chef;