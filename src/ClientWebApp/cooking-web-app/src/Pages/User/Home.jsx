import React, { useEffect, useState } from 'react'
import Slide from "../../Components/Slider/Slider"
import CourseFind from '../../Components/Shared/CourseFind'
import Content from '../../Components/Shared/Content'

const Home = () => {
    useEffect(() => {
        document.title = "Trang chủ"
    }, [])

    return(
        <div>
            <div className='slider'>
                <Slide/>
                <CourseFind/>
            </div>
            <div className='main-contain'>
                <Content/>
            </div>
            
        </div>
    )
}

export default Home;