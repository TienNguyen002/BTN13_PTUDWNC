import React, {useEffect} from "react";
import CourseFind from "../../Components/Shared/CourseFind"
import Recipe from "../../Components/Recipe/Recipe";

const Food = () => {
    useEffect(() => {
        document.title = 'Món ăn ngon';
    }, []);

    return(
        <div className="food">
            <div className="food-content">
                <h1 className="food-content-title text-center">MÓN ĂN NGON</h1>
                <CourseFind/>
                <p>Các bí quyết, phương pháp nấu ăn ngon luôn là vốn kiến thức ẩm thực quý báu mà tất cả những ai đam mê nấu ăntìm kiếm. Chuyên mục Công thức nấu ăn do <b className="p-color">Nghề Bếp Á Âu</b> xây dựng tổng hợp đầy đủ các công thức món ngon từ đơn giản đến cầu kỳ, từ quán ăn bình dân cho đến thực đơn nhà hàng cao cấp. Những kinh nghiệm trong chế biến món ăn, bí quyết nêm nếm chuẩn vị từ các chuyên gia dinh dưỡng, đầu bếp hàng đầu cũng sẽ được bật mí trong các bài viết của chúng tôi.</p>
                <p>Nội dung dạy nấu ăn phong phú tổng hợp <b className="p-color">các công thức nấu ăn ngon</b> bao gồm Âu, món Á, món Nhật; phong phú trong cách chế biến: món chiên, món nướng, món súp, món gỏi, sushi… dành cho tất cả những ai đam mê nấu nướng, muốn chăm sóc gia đình bằng những món ăn đặc sắc về hương vị mà vẫn đảm bảo đầy đủ chất dinh dưỡng. Bạn muốn tìm hiểu công thức các món ăn ngon hay muốn rèn luyện tay nghề để trở thành đầu bếp chuyên nghiệp bạn có thể tham gia các lớp <b className="p-color">học nấu ăn chuyên nghiệp</b> của chúng tôi hoặc theo dõi các công thức nấu ăn tại đây đều thỏa mãn nhu cầu của bạn nhé.</p>
                <h4 className="food-content-title text-center">MÓN ĂN NGON</h4>
                <Recipe/>
            </div>
        </div>
    )
}

export default Food;