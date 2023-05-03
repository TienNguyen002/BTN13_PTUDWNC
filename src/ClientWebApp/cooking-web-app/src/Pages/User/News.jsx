import React, {useEffect} from "react";

const News = () => {
    useEffect(() => {
        document.title = 'Tin tức';
    }, []);

    return(
        <h1>Đây là trang Tin tức</h1>
    )
}

export default News;