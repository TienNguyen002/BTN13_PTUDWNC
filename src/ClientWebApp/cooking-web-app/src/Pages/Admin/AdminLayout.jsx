import { Outlet } from "react-router-dom";
import Sidebar from "../../Components/Admin/Shared/Sidebar/Sidebar";

const AdminLayout = () => {
    return(
        <>
            <Sidebar/>
            <div>
                <Outlet/>
            </div>
        </>
    )
}

export default AdminLayout;