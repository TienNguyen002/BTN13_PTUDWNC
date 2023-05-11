import React from "react";
import "./style/Footer.scss"
import About from "./Content/About"
import Social from "./Content/Social"

const Footer = () => {
    return(
        <div className="footer">
            <div className="row">
                <div className="col-6">
                    <About/>
                </div>
                <div className="col-6">
                    <Social/>
                </div>
                <div className="container-fluid text-center footer-bottom">
                    &copy; 2023 - Trung tâm dạy nấu ăn
                </div>
            </div>
        </div>    
    )
}

export default Footer;