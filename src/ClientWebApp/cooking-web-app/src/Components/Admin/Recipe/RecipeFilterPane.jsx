import { useEffect, useState } from 'react'
import Form from 'react-bootstrap/Form'
import Button from 'react-bootstrap/Button'
import { Link } from 'react-router-dom'
import { getFilter } from '../../../Services/RecipeRepository'
import {
    reset,
    updateKeyword,
    updateAuthorId,
    updateCourseId,
    updateYear,
    updateMonth,
} from "../../../Redux/Recipe/Reducer"
import { useSelector, useDispatch } from 'react-redux'

const RecipeFilterPane = () => {
    const recipeFilter = useSelector(state => state.recipeFilter),
        dispatch = useDispatch(),
        [filter, setFilter] = useState({
            authorList: [],
            courseList: [],
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
                    authorList: data.authorList,
                    courseList: data.courseList,
                    monthList: data.monthList
                });
            } else {
                setFilter({
                    authorList: [],
                    courseList: [],
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
                    value={recipeFilter.keyword}
                    onChange={e => dispatch(updateKeyword(e.target.value))} />
            </Form.Group>
            <Form.Group className='col-auto'>
                <Form.Label className='visually-hidden'>
                    AuthorId
                </Form.Label>
                <Form.Select
                    name='authorId'
                    value={recipeFilter.authorId}
                    onChange={e => dispatch(updateAuthorId(e.target.value))}
                    title='Author Id'>
                    <option value=''>-- Chọn tác giả --</option>
                    {filter.authorList && filter.authorList.map((item, index) =>
                        <option key={index} value={item.value}>{item.text}</option>)}
                </Form.Select>
            </Form.Group>
            <Form.Group className='col-auto'>
                <Form.Label className='visually-hidden'>
                    CourseId
                </Form.Label>
                <Form.Select
                    name='courseId'
                    value={recipeFilter.courseId}
                    onChange={e => dispatch(updateCourseId(e.target.value))}
                    title='Course Id'>
                    <option value=''>-- Chọn khóa học --</option>
                    {filter.courseList && filter.courseList.map((item, index) =>
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
                    value={recipeFilter.year}
                    max={recipeFilter.year}
                    onChange={e => dispatch(updateYear(e.target.value))} />
            </Form.Group>
            <Form.Group className='col-auto'>
                <Form.Label className='visually-hidden'>
                    Month
                </Form.Label>
                <Form.Select
                    name='month'
                    value={recipeFilter.month}
                    onChange={e => dispatch(updateMonth(e.target.value))}
                    title='Month'>
                    <option value=''>-- Chọn tháng --</option>
                    {filter.monthList.length > 0 && filter.monthList.map((item, index) =>
                        <option key={index} value={item.value}>{item.text}</option>)}
                </Form.Select>
            </Form.Group>
            <Form.Group className='col-auto'>
                <Button variant='danger' type='reset'>
                    <Link to={`/`}/>Xoá lọc
                </Button>
                <Link to={`/admin/posts/edit`} className='btn btn-success ms-2'>Thêm mới</Link>
            </Form.Group>
        </Form>
    )
}

export default RecipeFilterPane;