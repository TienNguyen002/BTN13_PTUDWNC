import React, {useRef, useState, useEffect} from "react";
import {Link, Navigate, useParams} from "react-router-dom"
import {
    addCourse,
    getCourseById,
    updateCourse,
    updateImage
} from "../../../Services/CourseRepository"
import { isEmptyOrSpaces, isInteger } from "../../../Utils/Utils"
import { Button, Form } from "react-bootstrap";
import { getDemands, getPrices, getSessions} from "../../../Services/Other"

const initialState = {
    title: "",
    shortDescription: "",
    description: "",
    urlSlug: "",
    published: false,
    demand: {
        id: ""
    },
    price: {
        id: ""
    },
    numberOfSessions: {
        id: ""
    },
    chef: {
        id: ""
    },
    demandId: "",
    priceId: "",
    numberOfSessionsId: "",
    chefId: ""
};

export default function CourseEdit(){
    const params = useParams();

    const [course, setCourse] = useState(initialState);
    const [demands, setDemands] = useState([]);
    const [prices, setPrices] = useState([]);
    const [numberOfSessions, setNumberOfSessions] = useState([]);
    const [chefs, setChefs] = useState([]);
    const imageFile = useRef();

    useEffect(() => {
        fetchCourses();
        async function fetchCourses(){
            const data = await getCourseById(params.id);
            if(data){
                setCourse(data);
            } else setCourse([]);
            const demands = await getDemands();
            if(demands) setDemands(demands);
            const prices = await getPrices();
            if(prices) setPrices(prices);
            const numberOfSessions = await getSessions();
            if(numberOfSessions) setNumberOfSessions(numberOfSessions);
            const chefs = await getSessions();
            if(chefs) setChefs(chefs);
        }
    }, []);

    if(params.id && !isInteger(params.id)){
        return <Navigate to={`/400`}/>;
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        const data = {
            title: course.title,
            shortDescription: course.shortDescription,
            description: course.description,
            urlSlug: course.urlSlug,
            demandId: course.demandId ? course.demandId : course.demand.id,
            priceId: course.priceId ? course.priceId : course.price.id,
            numberOfSessionsId: course.numberOfSessionsId ? course.numberOfSessionsId : course.numberOfSessions.id,
            chefId: course.chefId ? course.chefId : course.chef.id,
            published: course.published
        }

        const file = imageFile.current.files[0];
        if(params.id){
            UpdateCourse(data);
        }
        else{
            AddCourse(data);
        }

        async function UpdateCourse(data) {
            const response = await updateCourse(params.id, data);
            console.log(response);
            if (response) {
              alert("Cập nhập thành công");
            } else {
              console.log(response);
              alert("Cập nhập thất bại");
            }
            const responseImage = await updateImage(response.id, file)
            if (responseImage) {
              alert("Cập nhập hình ảnh thành công");
            } else {
              console.log(responseImage);
              alert("Cập nhập hình ảnh thất bại");
            }
        }

        async function AddCourse(data) {
            const response = await addCourse(data);
            if (response) {
              alert("Thêm thành công");
            } else {
              alert("Thêm thất bại");
            }
        }
    };

    return(
        <>
            <Form
                method={isInteger(params.id) ? "put" : "post"}
                encType="multipart/form-data"
                onSubmit={handleSubmit}>
                <div className="row mb-3">
                    <Form.Label className="col-sm-2 col-form-label">Tiêu đề</Form.Label>
                    <div className="col-sm-10">
                        <Form.Control
                            type="text"
                            name="title"
                            title="Title"
                            required
                            value={course.title || ""}
                            onChange={(e) =>
                            setCourse({
                                ...course,
                                title: e.target.value
                            })
                        }
                        />
                    </div>
                </div>
                <div className="row mb-3">
                    <Form.Label className="col-sm-2 col-form-label">UrlSlug</Form.Label>
                    <div className="col-sm-10">
                        <Form.Control
                            type="text"
                            name="slug"
                            title="Slug"
                            required
                            value={course.urlSlug || ""}
                            onChange={(e) =>
                            setCourse({
                                ...course,
                                urlSlug: e.target.value
                            })
                        }
                        />
                    </div>
                </div>
                <div className="row mb-3">
                    <Form.Label className="col-sm-2 col-form-label">Giới thiệu</Form.Label>
                    <div className="col-sm-10">
                        <Form.Control
                            type="text"
                            name="shortDescription"
                            title="Short Description"
                            required
                            value={course.shortDescription || ""}
                            onChange={(e) =>
                            setCourse({
                                ...course,
                                shortDescription: e.target.value
                            })
                        }
                        />
                    </div>
                </div>
                <div className="row mb-3">
                    <Form.Label className="col-sm-2 col-form-label">Nội dung</Form.Label>
                    <div className="col-sm-10">
                        <Form.Control
                            type="text"
                            name="description"
                            title="Description"
                            required
                            value={course.description || ""}
                            onChange={(e) =>
                            setCourse({
                                ...course,
                                description: e.target.value
                            })
                        }
                        />
                    </div>
                </div>
                <div className="row mb-3">
                    <Form.Label className="col-sm-2 col-form-label">Loại nhu cầu</Form.Label>
                    <div className="col-sm-10">
                        <Form.Select
                            name="demandId"
                            title="DemandId"
                            value={course.demandId ? course.demandId : course.demand?.id}
                            required
                            onChange={(e) =>
                                setCourse({
                                    ...course,
                                    demandId: e.target.value
                                })
                            }>
                            <option value="">-- Chọn loại nhu cầu --</option>
                            {demands.map((demand) => (
                                <option key={demand.id} value={demand.id}>
                                    {demand.name}
                                </option>
                            ))}
                        </Form.Select>
                    </div>
                </div>
                <div className="row mb-3">
                    <Form.Label className="col-sm-2 col-form-label">Giá</Form.Label>
                    <div className="col-sm-10">
                        <Form.Select
                            name="priceId"
                            title="PriceId"
                            value={course.priceId ? course.priceId : course.price?.id}
                            required
                            onChange={(e) =>
                                setCourse({
                                    ...course,
                                    priceId: e.target.value
                                })
                            }>
                            <option value="">-- Chọn giá --</option>
                            {prices.map((price) => (
                                <option key={price.id} value={price.id}>
                                    {price.name}
                                </option>
                            ))}
                        </Form.Select>
                    </div>
                </div>
                <div className="row mb-3">
                    <Form.Label className="col-sm-2 col-form-label">Số buổi</Form.Label>
                    <div className="col-sm-10">
                        <Form.Select
                            name="numberOfSessionsId"
                            title="NumberOfSessionsId"
                            value={course.numberOfSessionsId ? course.numberOfSessionsId : course.numberOfSessions?.id}
                            required
                            onChange={(e) =>
                                setCourse({
                                    ...course,
                                    numberOfSessionsId: e.target.value
                                })
                            }>
                            <option value="">-- Chọn số buổi --</option>
                            {numberOfSessions.map((numberOfSessions) => (
                                <option key={numberOfSessions.id} value={numberOfSessions.id}>
                                    {numberOfSessions.name}
                                </option>
                            ))}
                        </Form.Select>
                    </div>
                </div>
                <div className="row mb-3">
                    <Form.Label className="col-sm-2 col-form-label">Đầu bếp</Form.Label>
                    <div className="col-sm-10">
                        <Form.Select
                            name="chefId"
                            title="ChefId"
                            value={course.chefId ? course.chefId : course.chef?.id}
                            required
                            onChange={(e) =>
                                setCourse({
                                    ...course,
                                    chefId: e.target.value
                                })
                            }>
                            <option value="">-- Chọn đầu bếp --</option>
                            {chefs.map((chef) => (
                                <option key={chef.id} value={chef.id}>
                                    {chef.name}
                                </option>
                            ))}
                        </Form.Select>
                    </div>
                </div>
                {!isEmptyOrSpaces(course.imageUrl) && (
                    <div className="row mb-3">
                        <Form.Label className="col-sm-2 col-form-label">Hình hiện tại</Form.Label>
                        <div className="col-sm-10">
                            <img src={`https://localhost:7029/${course.imageUrl}`} alt=""/>
                        </div>
                    </div>
                )}
                <div className="row mb-3">
                    <Form.Label className="col.sm-2 col-form-label">
                        Chọn hình ảnh
                    </Form.Label>
                    <div className="col-sm-10">
                        <Form.Control
                        type="file"
                        name="imageFile"
                        accept="image/*"
                        title="Image File"
                        ref={imageFile}
                        onChange={(e) =>
                            setCourse({ ...course, imageFile: e.target.files[0] })
                        }
                        />
                    </div>
                </div>
                <div className="row mb-3">
                    <div className="col-sm-10 offset-sm-2">
                        <div className="form-check">
                            <input
                                className="form-check-input"
                                type="checkbox"
                                name="published"
                                checked={course.published}
                                onChange={(e) =>
                                setCourse({
                                    ...course,
                                    published: e.target.checked
                                })}
                            />
                            <Form.Label className="form-check-label">Đã xuất bản</Form.Label>
                        </div>
                    </div>
                </div>
                <div className="text-center">
                    <Button variant="primary" type="submit">
                        Lưu các thay đổi
                    </Button>
                    <Link to="/admin/courses" className="btn btn-danger ms-2">
                        Hủy và quay lại
                    </Link>
                </div>
            </Form>
        </>
    )
};



