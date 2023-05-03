import React, {useEffect} from "react";

const Food = () => {
    useEffect(() => {
        document.title = 'Món ăn ngon';
    }, []);

    return(
        <h1>Đây là trang Món ăn ngon</h1>
    )
}

export default Food;