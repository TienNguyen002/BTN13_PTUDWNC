import React, {useEffect} from "react";

const DailyMenu = () => {
    useEffect(() => {
        document.title = 'Thực đơn mỗi ngày';
    }, []);

    return(
        <h1>Đây là trang Thực đơn mỗi ngày</h1>
    )
}

export default DailyMenu;