import React, {useEffect} from "react";
import CourseFind from "../../Components/Shared/CourseFind"

const KitchenTips = () => {
    useEffect(() => {
        document.title = 'Mẹo nhà bếp';
    }, []);

    return(
        <div className="kitchen-tips">
            <div className="kitchen-tips-content">
                <h1 className="kitchen-tips-content-title text-center">MẸO NHÀ BẾP</h1>
                <CourseFind/>
                <h4 className="kitchen-tips-content-title text-center">MẸO VẶT NHÀ BẾP</h4>
                <p><b className="p-color">Nghề Bếp Á Âu</b> sẽ cung cấp, chia sẻ đến bạn những mẹo nghề bếp thật hữu dụng để công việc làm bếp của bạn trở nên thú vị hơn. </p>
                <p>Với cuộc sống hiện đại, thực phẩm sạch luôn là một trong những chủ đề quan tâm của tất cả chúng ta, đặc biệt là các chị em nội trợ muốn có được một bữa cơm ngon và bổ dưỡng mà không phải lo lắng các vấn đề an toàn thực phẩm.</p>
                <p>Chính vì điều này ngoài những tham gia các <b className="p-color">lớp học nấu ăn chuyên nghiệp</b> thì bạn cũng cần trang bị cho mình những giải pháp tốt hơn cũng như việc tạo nên những thói quen, thao tác trong khâu chế biến và sử dụng thực phẩm.</p>
                <p>Với những kỹ năng sử dụng dụng cụ, cách lựa chọn thực phẩm, chia sẻ về các loại thực phẩm giàu chất dinh dưỡng, rất tốt cho sức khỏe, cơ thể và cả làm đẹp nữa.</p>
                <p>Cùng với đó là <b className="p-color">những kỹ thuật chế biến thức ăn</b> bạn sẽ có được một bữa ăn hoàn hảo.</p>
            </div>
        </div>
    )
}

export default KitchenTips;