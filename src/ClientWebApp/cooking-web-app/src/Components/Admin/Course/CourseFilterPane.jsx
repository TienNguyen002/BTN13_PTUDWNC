import { useEffect, useState } from 'react'
import Form from 'react-bootstrap/Form'
import Button from 'react-bootstrap/Button'
import { Link } from 'react-router-dom'
import { getFilter } from '../../../Services/CourseRepository'
import { useSelector, useDispatch } from 'react-redux'
import {
    reset,
    updateKeyword,
    updateDemandId,
    updatePriceId,
    updateNumberOfSessionsId,
    updateMonth,
    updateYear
} from '../../../Redux/Reducer'

const CourseFilterPane = () => {
    const courseFilter = useSelector(state => state.courseFilter),
        dispatch = useDispatch(),
        [filter, setFilter] = useState({
            demandList: [],
            priceList: [],
            numberofsessionsList: [],
            monthList: []
        });

    const handleReset = (e) => {
        dispatch(reset());
    };

    const handleSubmit = (e) => {
        e.preventDefault();
    };

    useEffect(() => {
        getFilter().then(data => {
            if (data) {
                setFilter({
                    demandList: data.demandList,
                    priceList: data.priceList,
                    numberofsessionsList: data.numberofsessionsList,
                    monthList: data.monthList
                });
            } else {
                setFilter({
                    demandList: [],
                    priceList: [],
                    numberofsessionsList: [],
                    monthList: []
                });
            }
        })
    }, [])

    return (
        <Form method='get'
            onReset={handleReset}
            className='row gy-2 gx-3 align-items-center p-2'>
            <Form.Group className='col-auto'>
                <Form.Label className='visually-hidden'>
                    Keyword
                </Form.Label>
                <Form.Control
                    type='text'
                    placeholder='Nhập từ khóa ...'
                    name='keyword'
                    value={courseFilter.keyword}
                    onChange={e => dispatch(updateKeyword(e.target.value))} />
            </Form.Group>
            <Form.Group className='col-auto'>
                <Form.Label className='visually-hidden'>
                    DemandId
                </Form.Label>
                <Form.Select
                    name='demandId'
                    value={courseFilter.demandId}
                    onChange={e => dispatch(updateDemandId(e.target.value))}
                    title='Demand Id'>
                    <option value=''>-- Chọn nhu cầu --</option>
                    {filter.demandList.map((item, index) =>
                        <option key={index} value={item.value}>{item.text}</option>)}
                </Form.Select>
            </Form.Group>
            <Form.Group className='col-auto'>
                <Form.Label className='visually-hidden'>
                    CategoryId
                </Form.Label>
                <Form.Select
                    name='categoryId'
                    value={courseFilter.categoryId}
                    onChange={e => dispatch(updatePriceId(e.target.value))}
                    title='Category Id'>
                    <option value=''>-- Chọn giá --</option>
                    {filter.priceList.map((item, index) =>
                        <option key={index} value={item.value}>{item.text}</option>)}
                </Form.Select>
            </Form.Group>
            {/* <Form.Group className='col-auto'>
                <Form.Label className='visually-hidden'>
                    NumberOfSessionsId
                </Form.Label>
                <Form.Select
                    name='numberofsessionsId'
                    value={courseFilter.numberofsessionsId}
                    onChange={e => dispatch(updateNumberOfSessionsId(e.target.value))}
                    title='Number Of Sessions Id'>
                    <option value=''>-- Chọn số buổi --</option>
                    {filter.numb.map((item, index) =>
                        <option key={index} value={item.value}>{item.text}</option>)}
                </Form.Select>
            </Form.Group> */}
            <Form.Group className='col-auto'>
                <Form.Label className='visually-hidden'>
                    Year
                </Form.Label>
                <Form.Control
                    type='number'
                    placeholder='Nhập năm ...'
                    name='year'
                    value={courseFilter.year}
                    max={courseFilter.year}
                    onChange={e => dispatch(updateYear(e.target.value))} />
            </Form.Group>
            <Form.Group className='col-auto'>
                <Form.Label className='visually-hidden'>
                    Month
                </Form.Label>
                <Form.Select
                    name='month'
                    value={courseFilter.month}
                    onChange={e => dispatch(updateMonth(e.target.value))}
                    title='Month'>
                    <option value=''>-- Chọn tháng --</option>
                    {filter.monthList.length > 0 && filter.monthList.map((item, index) =>
                        <option key={index} value={item.value}>{item.text}</option>)}
                </Form.Select>
            </Form.Group>
            <Form.Group className='col-auto'>
                <Button variant='danger' type='reset'>
                    Xóa lọc
                </Button>
                <Link to={`/admin/posts/edit`} className='btn btn-success ms-2'>Thêm mới</Link>
            </Form.Group>
        </Form>
    )
}

export default CourseFilterPane;