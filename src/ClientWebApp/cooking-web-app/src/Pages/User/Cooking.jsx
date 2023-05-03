import React, {useEffect} from "react";

const Cooking = () => {
    useEffect(() => {
        document.title = 'Học nấu ăn';
    }, []);

    return(
        <div>
            <h1>HỌC NẤU ĂN</h1>
            <img src="https://nghebep.com/wp-content/uploads/2019/05/hoc-nghe-nau-an.jpg"/>
        </div>
    )
}

export default Cooking;