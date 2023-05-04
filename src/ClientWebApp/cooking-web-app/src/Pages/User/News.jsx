import React, {useEffect} from "react";
import CourseFind from "../../Components/Shared/CourseFind"

const News = () => {
    useEffect(() => {
        document.title = 'Tin tức';
    }, []);

    return(
        <div className="news">
            <div className="news-content">
                <h1 className="news-content-title text-center">TIN TỨC</h1>
                <CourseFind/>
                <h4 className="news-content-title text-center">TIN TỨC HAY VỀ NGHỀ BẾP</h4>
                <p>Nghề đầu bếp là một nghề mang tính đặc biệt, đòi hỏi người làm vừa phải có chuyên môn vừa có tâm với nghề. Nếu là một người đầu bếp thực sự có tâm với nghề thì hầu như sẽ không còn cảm nhận thấy nỗi buồn mà chỉ có niềm vui sướng khi món ăn mình làm ra được khách hàng đón nhận.</p>
                <p>Để trở thành đầu bếp là cả một quá trình học nghề khá vất vả và phải trải qua nhiều khó khăn và thử thách mới có thể thành công được. Quá trình đó cần thời gian, không đơn giản chỉ là 1 hay 2 năm. Mà còn là một quá trình dài bắt buộc bạn phải biết rõ về <b className="p-color">các mẹo vặt ở nhà bếp</b> và những kỹ thuật chế biến món ăn làm cho thực khách hay các thành viên trong gia đình thích thú hơn.</p>
                <p>Chuyên trang này được <b className="p-color">Nghề Bếp Á Âu</b> lập ra với mục đích giới thiệu một trong những kỹ năng, những khám phá nghề nghiệp mà bạn cần phải học hỏi, tiếp thu rèn luyện những bài học quý giá từ những đầu bếp nổi tiếng ở Việt Nam và trên thế giới, từ đó giúp bạn có được cái nhìn tổng quan hơn về nghề bếp và có những chuẩn bị cần thiết để thành công trong nghề.</p>
            </div>
        </div>
    )
}

export default News;