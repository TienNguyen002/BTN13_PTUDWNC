import React, {useEffect} from "react";
import CourseFind from "../../Components/Shared/CourseFind"
import DailyPost from "../../Components/Post/DailyPost";

const DailyMenu = () => {
    useEffect(() => {
        document.title = 'Thực đơn mỗi ngày';
    }, []);

    return(
        <div className="daily-menu">
            <div className="daily-menu-content">
                <h1 className="daily-menu-content-title text-center">THỰC ĐƠN MỖI NGÀY</h1>
                <CourseFind/>
                <p>Với thực đơn 7 ngày trong tuần vừa ngon vừa rẻ dưới đây, bạn không phải suy nghĩ lựa chọn món mỗi khi vào bếp mà chỉ cần tham khảo và áp dụng. Các món ăn phong phú, đa dạng, bao gồm các loại thịt, cá, rau củ được chế biến khác nhau sẽ đem đến cho bạn những bữa ăn ngon miệng.</p>
                <p>Trong cuộc sống hiện đại, công việc và học hành khiến chúng ta không có nhiều thời gian vào bếp, vì vậy hầu hết đều chọn cách ăn uống bên ngoài. Tuy nhiên, đứng trước thực trạng mất an toàn vệ sinh thực phẩm như hiện nay, bạn nên tự nấu ăn tại nhà để bảo vệ sức khỏe cho mình và cả gia đình. Hiểu được điều đó <b className="p-color">Nghề Bếp Á Âu</b> sẽ gợi ý cho bạn <b className="p-color">thực đơn trong tuần</b> vừa ngon bổ lại vừa rẻ mà lại không quá cầu kỳ hay tốn thời gian, công sức, bạn hãy vào bếp thường xuyên để có những bữa ăn ngon miệng nhé!</p>
                <h4 className="daily-menu-content-title text-center">BẠN ĐANG CẦN LOẠI THỰC ĐƠN NÀO?</h4>
                <DailyPost/>
            </div>
        </div>
    )
}

export default DailyMenu;