import React, {useEffect} from "react";

const KitchenTips = () => {
    useEffect(() => {
        document.title = 'Mẹo nhà bếp';
    }, []);

    return(
        <h1>Đây là trang Mẹo nhà bếp</h1>
    )
}

export default KitchenTips;