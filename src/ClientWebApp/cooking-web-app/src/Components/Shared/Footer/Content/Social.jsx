import React from "react";
import "../style/Footer.scss"
import { Link } from "react-router-dom";
import facebook from "../../images/facebook.png"
import youtube from "../../images/ytb.png"

const Social = () => {
    return(
        <div className="social col-4">
            <h4>MẠNG XÃ HỘI</h4>
            <div className="row">
                <div className="facebook">
                    <Link to={`https://www.facebook.com/Junn.Nguy3n`}>
                        <img src={facebook} className="facebook"/>
                    </Link>
                </div>
                <div className="youtube">
                    <Link to={``}>
                        <img src={youtube} className="youtube"/>
                    </Link>
                </div>
                <div>
                    <iframe title="map"
                        src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3903.2877902405253!2d108.44201621412589!3d11.95456563961217!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x317112d959f88991%3A0x9c66baf1767356fa!2zVHLGsOG7nW5nIMSQ4bqhaSBI4buNYyDEkMOgIEzhuqF0!5e0!3m2!1svi!2s!4v1633261535076!5m2!1svi!2s"></iframe>
                </div>
            </div>
        </div>
    )
}

export default Social;