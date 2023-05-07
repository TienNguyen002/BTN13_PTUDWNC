import React, {useEffect} from "react";
import CourseList from "../../Components/Course/Courses/CourseList"
import Table from "react-bootstrap/Table"

const Cooking = () => {
    useEffect(() => {
        document.title = 'Học Nấu Ăn';
    }, []);

    return(
        <div className="cooking">
            <div className="cooking-content">
                <h1 className="cooking-content-title text-center">HỌC NẤU ĂN</h1>
                <img src="https://nghebep.com/wp-content/uploads/2019/05/hoc-nghe-nau-an.jpg" className="cooking-content-image" alt="HỌC NẤU ĂN"/>
                <p>Bạn muốn <b>học nấu ăn</b> để thể trở thành đầu bếp chuyên nghiệp hay học để biết nấu nướng các món ăn ngon. Bạn cần tìm kiếm các khóa học nấu ăn cơ bản hay các khóa nấu ăn nâng cao. Nghề Bếp Á Âu sẽ là trung tâm đào tạo kiến thức nấu ăn giúp bạn trở thành bếp trưởng trong tương lai.</p>
                <p>Phương pháp giảng dạy chú trọng thực hành lên đến 90% thời gian học. Học viên được rèn luyện. Kiến thức – Kỹ năng – Thái độ để có thể phát huy năng lực bản thân; trở thành đầu bếp chuyên nghiệp. Nội dung bài học không chỉ cung cấp những công thức nấu ăn ngon mà còn mở rộng kỹ thuật làm bánh, pha chế, làm kem,… giúp học viên có được khối kiến thức về ẩm thực toàn diện.</p>
                <p>Nghề Bếp Á Âu (NBAAu) xây dựng đa dạng các khóa học đi từ những kiến thức cơ bản nhất cho đến nâng cao và chuyên sâu giúp bạn có thể chọn được lớp học phù hợp với sở thích nấu ăn, hợp mục đích cá nhân một cách khoa học. Đến với <b className="p-color">Nghề Bếp Á Âu</b>, bạn không phải đi tìm lời giải đáp cho câu hỏi học nấu ăn ở đâu tốt nhất. Có mặt ở nhiều tỉnh thành trên toàn quốc như: TP. HCM, Hà Nội, Đà Nẵng, Hội An, Nha Trang, Buôn Ma Thuột, Phan Thiết, Biên Hòa, Bình Dương, Cần Thơ, Rạch Giá…, Nghề Bếp Á Âu (NBAAU) đầu tư dụng cụ nấu ăn chất lượng và hiện đại mang đến nhiều hơn cơ hội trở thành đầu bếp chuyên nghiệp cho bạn.</p>
                <p>Ngoài ra, để học viên có thể tiến xa hơn đến các cấp bậc quản lý trong gian bếp chuyên nghiệp. Nghề Bếp Á Âu (NBAAu) còn tích hợp các bài học về kỹ năng mềm như: làm việc nhóm, lập dự án kinh doanh ẩm thực, đào tạo nghề bếp, vận hành quản lý bếp,… trong các khóa học.</p>
            </div>
            <div className="cooking-table">
                <h4 className="cooking-table-header text-center">NỘI DUNG KHÓA HỌC NẤU ĂN CHUYÊN NGHIỆP</h4>
                <p className="cooking-table-content">Chương trình HỌC do NGHỀ BẾP Á ÂU giới thiệu dành cho học viên muốn hướng đến một Đầu bếp chuyên nghiệp và được làm việc tại những nhà hàng khách sạn lớn với thu nhập hấp dẫn hay muốn mở nhà hàng để kinh doanh.</p>
                <CourseList/>
            </div>
            <div className="cooking-table">
                <h4 className="cooking-table-header text-center">LỊCH HỌC</h4>
                <Table>
                    <thead>
                        <tr className="table-header text-center">
                            <td className="table-header-content">STT</td>
                            <td className="table-header-content">Ngày học</td>
                            <td className="table-header-content">Sáng</td>
                            <td className="table-header-content">Chiều</td>
                            <td className="table-header-content">Tối</td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr className="text-center">
                            <td>1</td>
                            <td>Thứ 2 – 4 – 6</td>
                            <td>08h30 – 11h30</td>
                            <td>13h30 – 16h30</td>
                            <td>18h00 – 21h00</td>
                        </tr>   
                        <tr className="text-center">
                            <td>2</td>
                            <td>Thứ 3 – 5 – 7</td>
                            <td>09h30 – 12h30</td>
                            <td>14h30 – 15h30</td>
                            <td>17h00 – 22h00</td>
                        </tr>
                    </tbody>
                </Table>              
            </div>
            <div className="cooking-content">
                <h4 className="cooking-content-title">HÌNH ẢNH LỚP HỌC</h4>
                <div className="col imgClass">
                    <img className="imgClass" alt="Anh1" src="https://nghebep.com/wp-content/uploads/2018/12/hoc-vien-thuc-hanh.jpg"/>
                    <img className="imgClass" alt="Anh2" src="https://nghebep.com/wp-content/uploads/2018/12/hoc-vien-khoa-thanh-thao.jpg"/>   
                </div> 
            </div>
            {/* <div className="cooking-relative">
                <h4 className="cooking-relative-title">BÀI VIẾT LIÊN QUAN</h4>
            </div> */}
        </div>
    )
}

export default Cooking;