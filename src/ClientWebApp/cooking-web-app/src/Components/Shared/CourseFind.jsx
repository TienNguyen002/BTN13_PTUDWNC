import React, { useEffect, useState } from "react";
import Form from 'react-bootstrap/Form'
import Button from 'react-bootstrap/Button'
import "./style/index.scss"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faMagnifyingGlass } from "@fortawesome/free-solid-svg-icons";
import { getFilter } from "../../Services/CourseRepository"

const ItemFind = () => {
    const 
    [filter, setFilter] = useState({
        demandList: [],
        priceList: [],
        numberOfSessionsList: [],
    })

    useEffect(() => {
        getFilter().then(data => {
            if(data){
                setFilter({
                    demandList: data.demandList,
                    priceList: data.priceList,
                    numberOfSessionsList: data.numberOfSessionsList,
                });
            } else{
                setFilter({
                    demandList: [],
                    priceList: [],
                    numberOfSessionsList: [],
                });
            }
        })
    }, [])

    // const handleSubmit = (e) => {
    //     e.preventDefault();
    // };

    return(
        <div className="container course-find">
            <div className="row">
                <div className="col">
                    <Form.Group className='col-auto'>
                        <Form.Label className='visually-hidden'>
                            DemandId
                        </Form.Label>
                        <Form.Select
                            name='demandId'
                            title='Demand Id'>
                            <option value=''>-- Chọn Nhu cầu --</option>
                            {filter.demandList.length > 0 &&
                                        filter.demandList.map((item, index) => 
                                        <option key={index} value={item.value}>{item.text}</option>)}
                        </Form.Select>
                    </Form.Group>
                </div>
                <div className="col">
                    <Form.Group className='col-auto'>
                        <Form.Label className='visually-hidden'>
                            PriceId
                        </Form.Label>
                        <Form.Select
                            name='priceId'
                            title='Price Id'>
                            <option value=''>-- Chọn Giá tiền --</option>
                            {filter.priceList.length > 0 &&
                                        filter.priceList.map((item, index) => 
                                        <option key={index} value={item.value}>{item.text}</option>)}
                        </Form.Select>
                    </Form.Group>
                </div>
                <div className="col">
                    <Form.Group className='col-auto'>
                        <Form.Label className='visually-hidden'>
                            SessionsId
                        </Form.Label>
                        <Form.Select
                            name='sessionsId'
                            title='Sessions Id'>
                            <option value=''>-- Chọn Số buổi --</option>
                            {filter.numberOfSessionsList.length > 0 &&
                                        filter.numberOfSessionsList.map((item, index) => 
                                        <option key={index} value={item.value}>{item.text}</option>)}
                        </Form.Select>
                    </Form.Group>   
                </div>
            </div>
            <div className="button">
                <Button variant='danger' type='submit'>
                    Tìm kiếm khóa học <FontAwesomeIcon icon={faMagnifyingGlass}/>
                </Button>
            </div>
        </div>
    )
}

export default ItemFind;
