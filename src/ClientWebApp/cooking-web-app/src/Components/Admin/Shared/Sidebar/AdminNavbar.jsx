import React, { useState } from 'react'
import { Link } from 'react-router-dom'
import { faBars, faXmark } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { SidebarData } from "./SidebarData"
import "./sidebar.scss"

const AdminNavbar = () => {
    const [sidebar, setSidebar] = useState(false)

    const showSidebar = () => setSidebar(!sidebar)
  return (
    <div>
        <div className="admin-navbar">
            <Link to="#" className='menu-bars'>
                <FontAwesomeIcon icon={faBars} onClick={showSidebar}/> 
            </Link>
            <h1 className='admin-hello'>Admin Page</h1>
        </div>
        <nav className={sidebar ? 'nav-menu active' : 'nav-menu'}>
            <ul className='nav-menu-items' onClick={showSidebar}>
                <li className='admin-navbar-toggle'>
                    <Link to="#" className='menu-bars'>
                        <FontAwesomeIcon icon={faXmark}/>
                    </Link>
                </li>
                {SidebarData.map((item, index) => {
                    return(
                        <li key={index} className={item.cName}>
                            <Link to={item.path}>
                                {item.icon}
                                <span>{item.title}</span>
                            </Link>
                        </li>
                    )
                })}
            </ul>
        </nav>
    </div>
  )
}

export default AdminNavbar