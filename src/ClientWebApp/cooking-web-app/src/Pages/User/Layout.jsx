import Navbar from "../../Components/Shared/Navbar";
import { Outlet } from "react-router-dom";
import RegisterCourse from '../../Components/Shared/RegisterCourse'
import Footer from '../../Components/Shared/Footer/Footer'
import "./style/User.scss"

const Layout = () => {
    return(
        <>
            <Navbar/>
            <div>
                <Outlet />
            </div>
            <div className='register'>
                <RegisterCourse/>
            </div>
            <div className='footer'>
                <Footer/>
            </div>
        </>
    )
}

export default Layout;