import { useEffect, useState } from 'react'
import Form from 'react-bootstrap/Form'
import Button from 'react-bootstrap/Button'
import { Link } from 'react-router-dom'
import { getFilter } from '../../../Services/CourseRepository'

const CourseFilterPane = () => {
    const current = new Date(),
        [keyword, setKeyword] = useState(''),
        [demandId, setDemandId] = useState(''),
        [priceId, setPriceId] = useState(''),
        [numberOfSessionsId, setNumberOfSessionsId] = useState(''),
        [year, setYear] = useState(current.getFullYear()),
        [month, setMonth] = useState(current.getMonth()),
        [filter, setFilter] = useState({
            demandList: [],
            priceList: [],
            numberOfSessionsList: [],
            monthList: []
        });

    // const handleReset = (e) => {
    //     dispatch(reset());
    // };

    const handleSubmit = (e) => {
        e.preventDefault();
    };

    useEffect(() => {
        getFilter().then(data => {
            if (data) {
                setFilter({
                    demandList: data.demandList,
                    priceList: data.priceList,
                    numberOfSessionsList: data.numberOfSessionsList,
                    monthList: data.monthList
                });
            } else {
                setFilter({
                    demandList: [],
                    priceList: [],
                    numberOfSessionsList: [],
                    monthList: []
                });
            }
        })
    }, [])

    return (
        <Form method='get'
            onSubmit={handleSubmit}
            className='row gy-2 gx-3 align-items-center p-2'>
            <Form.Group className='col-auto'>
                <Form.Label className='visually-hidden'>
                    Keyword
                </Form.Label>
                <Form.Control
                    type='text'
                    placeholder='Nhập từ khóa ...'
                    name='keyword'
                    value={keyword}
                    onChange={e => setKeyword(e.target.value)} />
            </Form.Group>
            <Form.Group className='col-auto'>
                <Form.Label className='visually-hidden'>
                    DemandId
                </Form.Label>
                <Form.Select
                    name='demandId'
                    value={demandId}
                    onChange={e => setDemandId(e.target.value)}
                    title='Demand Id'>
                    <option value=''>-- Chọn nhu cầu --</option>
                    {filter.demandList && filter.demandList.map((item, index) =>
                        <option key={index} value={item.value}>{item.text}</option>)}
                </Form.Select>
            </Form.Group>
            <Form.Group className='col-auto'>
                <Form.Label className='visually-hidden'>
                    PriceId
                </Form.Label>
                <Form.Select
                    name='priceId'
                    value={priceId}
                    onChange={e => setPriceId(e.target.value)}
                    title='Price Id'>
                    <option value=''>-- Chọn giá --</option>
                    {filter.priceList && filter.priceList.map((item, index) =>
                        <option key={index} value={item.value}>{item.text}</option>)}
                </Form.Select>
            </Form.Group>
            <Form.Group className='col-auto'>
                <Form.Label className='visually-hidden'>
                    NumberOfSessionsId
                </Form.Label>
                <Form.Select
                    name='numberofsessionsId'
                    value={numberOfSessionsId}
                    onChange={e => setNumberOfSessionsId(e.target.value)}
                    title='Number Of Sessions Id'>
                    <option value=''>-- Chọn số buổi --</option>
                    {filter.numberOfSessionsList && filter.numberOfSessionsList.map((item, index) =>
                        <option key={index} value={item.value}>{item.text}</option>)}
                </Form.Select>
            </Form.Group>
            <Form.Group className='col-auto'>
                <Form.Label className='visually-hidden'>
                    Year
                </Form.Label>
                <Form.Control
                    type='number'
                    placeholder='Nhập năm ...'
                    name='year'
                    value={year}
                    max={year}
                    onChange={e => setYear(e.target.value)} />
            </Form.Group>
            <Form.Group className='col-auto'>
                <Form.Label className='visually-hidden'>
                    Month
                </Form.Label>
                <Form.Select
                    name='month'
                    value={month}
                    onChange={e => setMonth(e.target.value)}
                    title='Month'>
                    <option value=''>-- Chọn tháng --</option>
                    {filter.monthList.length > 0 && filter.monthList.map((item, index) =>
                        <option key={index} value={item.value}>{item.text}</option>)}
                </Form.Select>
            </Form.Group>
            <Form.Group className='col-auto'>
                <Button variant='primary' type='submit'>
                    Tìm/Lọc
                </Button>
                <Link to={`/admin/posts/edit`} className='btn btn-success ms-2'>Thêm mới</Link>
            </Form.Group>
        </Form>
    )
}

export default CourseFilterPane;