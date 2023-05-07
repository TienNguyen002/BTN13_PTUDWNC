import { Button } from "react-bootstrap";
import { Link } from "react-router-dom"

const Login = () => {
    return(
        <>
            <Link  to={"/admin"}>
                <Button className="btn btn-success">Đăng nhập</Button>
            </Link>
        </>
    )
}

export default Login;