import React from "react";
import { Navbar as Nb, Nav, NavItem } from "react-bootstrap";
import { Link } from "react-router-dom";
import logo from "./images/cooking-logo.png"
import "./style/index.scss"
import SearchBar from "./SearchBar";

const Navbar = () => {
    return(
        <Nb collapseOnSelect variant="light"
        className="border-bottom shadow navbar">
            <div className="container-fluid">
                <Nb.Brand href="/">
                    <img 
                        alt="Dạy nấu ăn"
                        src={logo}
                        width="80"
                        height="50"/>
                </Nb.Brand>
                <Nb.Toggle aria-controls="responsive-navbar-nav"/>
                <Nb.Collapse id="responsive-navbar-nav" className="d-sm-inline-flex
                justify-content-between">
                    <Nav className="mr-auto flex-grow-1">
                        <NavItem>
                            <Link to='/hoc-nau-an' className="navitem">
                                HỌC NẤU ĂN
                            </Link>
                        </NavItem>
                        <NavItem>
                            <Link to='/mon-an-ngon' className="navitem">
                                MÓN ĂN NGON
                            </Link>
                        </NavItem>
                        <NavItem>
                            <Link to='/thuc-don-moi-ngay' className="navitem">
                                THỰC ĐƠN MỖI NGÀY
                            </Link>
                        </NavItem>
                        <NavItem>
                            <Link to='/meo-nha-bep' className="navitem">
                                MẸO NHÀ BẾP
                            </Link>
                        </NavItem>
                        <NavItem>
                            <Link to='/tin-tuc' className="navitem">
                                TIN TỨC
                            </Link>
                        </NavItem>
                    </Nav>
                </Nb.Collapse>
            </div>
            <div className="search-bar">
                <SearchBar/>
            </div>
        </Nb>
    )
}

export default Navbar;