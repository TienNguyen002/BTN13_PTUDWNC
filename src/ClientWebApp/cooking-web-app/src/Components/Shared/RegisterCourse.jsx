import React from "react";
import "./style/index.scss"
import { Form } from "react-bootstrap";
import Button from 'react-bootstrap/Button'

const RegisterCourse = () => {
    return(
        <div className="container register-course mb-4">
            <h2 class="h1-responsive font-weight-bold text-center my-4">Đăng ký khóa học</h2>
            <div className="row">
                <div id="Form" class="col-md-9 mb-md-0 mb-5">
                    <Form method="get">
                        <div className="row input-type">
                            <div className="col-6">
                                <Form.Group>
                                    <Form.Control
                                        type="text"
                                        aria-label='Nhập Họ và tên của bạn*'
                                        aria-describedby="btnSend"
                                        required
                                        placeholder="Nhập Họ và tên của bạn*" />
                                </Form.Group>
                            </div>
                            <div className="col-6">
                                <Form.Group>
                                    <Form.Control
                                        type="number"
                                        aria-label='Nhập Số điện thoại của bạn*'
                                        aria-describedby="btnSend"
                                        required
                                        placeholder="Nhập Số điện thoại của bạn*" />
                                </Form.Group>
                            </div>
                        </div>
                        <div className="row input-type">
                            <div className="col-6">
                                <Form.Group>
                                    <Form.Control
                                        type="text"
                                        aria-label='Nhập Email của bạn*'
                                        aria-describedby="btnSend"
                                        required
                                        placeholder="Nhập Email của bạn*" />
                                </Form.Group>
                            </div>
                            <div className="col-6">
                                <Form.Group className='col-auto'>
                                    <Form.Label className='visually-hidden'>
                                        DemandId
                                    </Form.Label>
                                    <Form.Select
                                        name='demandId'
                                        title='Demand Id'>
                                        <option value=''>-- Chọn Nhu cầu --</option>
                                    </Form.Select>
                                </Form.Group>
                            </div>
                        </div>
                    </Form>
                </div>
                <div className="button-register">
                    <Button variant='primary' type='submit'>
                        GỬI
                    </Button>
                </div>
            </div>
        </div>
    )
}

export default RegisterCourse;