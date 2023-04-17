using CookingWeb.Core.Entities;
using CookingWeb.Data.Contexts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Data.Seeders
{
    public class DataSeeder : IDataSeeder
    {
        private readonly WebDbContext _dbContext;

        public DataSeeder(WebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Initialize()
        {
            _dbContext.Database.EnsureCreated();
            if (_dbContext.Courses.Any()) return;

            var authors = AddAuthors();
            var topics = AddTopics();
            var chefs = AddChefs();
            var demands = AddDemands();
            var prices = AddPrices();
            var numberofsessions = AddNumberOfSessions();

            var categories = AddCategories(topics);
            var courses = AddCourses(demands, prices, numberofsessions, chefs);
            var students = AddStudents(courses);
            var recipes = AddRecipes(courses, authors);
            var posts = AddPosts(categories, authors);
        }

        private IList<Author> AddAuthors()
        {
            var authors = new List<Author>()
            {
                new()
                {
                    FullName = "Makiro Nata",
                    Description = "Từ nhỏ, tôi thường vào phụ mỗi khi Mẹ nấu ăn. Tôi thích những món ăn mà Mẹ tôi nấu, vì trong đó có tuổi thơ, có tình yêu thương mà Mẹ dành cho tôi. Lớn lên tôi mang cái tâm để đi theo Nghề Bếp.",
                    UrlSlug = "makiro-nata",
                    JoinedDate = new DateTime(2017, 07, 18)
                },
            };
            var authorAdd = new List<Author>();
            foreach(var author in authors)
            {
                if(!_dbContext.Authors.Any(a => a.UrlSlug == author.UrlSlug))
                {
                    authorAdd.Add(author);
                }
            }
            _dbContext.AddRange(authorAdd);
            _dbContext.SaveChanges();
            return authors;
        }

        private IList<Topic> AddTopics()
        {
            var topics = new List<Topic>()
            {
                new()
                {
                    Name = "Thực đơn mỗi ngày",
                    UrlSlug = "thuc-don-moi-ngay",
                },
                new()
                {
                    Name = "Mẹo nhà bếp",
                    UrlSlug = "meo-nha-bep",
                },
                new()
                {
                    Name = "Tin tức",
                    UrlSlug = "tin-tuc"
                }
            };
            var topicAdd = new List<Topic>();
            foreach (var topic in topics)
            {
                if (!_dbContext.Topics.Any(t => t.UrlSlug.Equals(topic.UrlSlug)))
                {
                    topicAdd.Add(topic);
                }
            }
            _dbContext.AddRange(topicAdd);
            _dbContext.SaveChanges();
            return topics;
        }
        private IList<Category> AddCategories(
            IList<Topic> topics)
        {
            var categories = new List<Category>()
            {
                new()
                {
                    Name = "Giảm cân",
                    Description = "Béo phì ngày càng gia tăng tại Việt Nam và đang ở mức báo động, đặc biệt là ở khu vực thành phố. Các con số không ngừng tăng lên. Có nhiều nguyên nhân gây nên tình trạng này. Cho nên, dinh dưỡng là yếu tố quyế t định hơn 70% sự thành công. Bên cạnh việc luyện tập thì bạn cũng cần phải có được một bữa ăn hợp lý và đầy đủ dinh dưỡng. Nghề Bếp Á Âu chia sẻ các thực đơn mỗi ngày nói chung và người muốn giảm cân nói riêng. Bạn có thể tham khảo thêm các thực đơn dưới đây nhé\r\n\r\nTổng hợp những thực đơn giảm cân trong 49 ngày được nghebep.com sưu tầm lại giúp bạn tham khảo thêm các món ăn giảm cân nhưng lại cung cấp đầy đủ năng lượng và chất dinh dưỡng",
                    UrlSlug = "giam-can",
                    Topic = topics[0],
                    ShowOnMenu = true,
                },
                new()
                {
                    Name = "Tập Gym",
                    Description = "",
                    UrlSlug = "tap-gym",
                    Topic = topics[0],
                    ShowOnMenu = true,
                },
                new()
                {
                    Name = "Bà Bầu",
                    Description = "",
                    UrlSlug = "ba-bau",
                    Topic = topics[0],
                    ShowOnMenu = true,
                },
                new()
                {
                    Name = "Người bệnh",
                    Description = "",
                    UrlSlug = "nguoi-benh",
                    Topic = topics[0],
                    ShowOnMenu = true,
                },
                new()
                {
                    Name = "Thực phẩm",
                    Description = "Thực phẩm sạch luôn là một trong những chủ đề quan tâm của khá nhiều người. Với những mẹo nhà bếp sẽ giúp bạn có những chọn lựa, bảo quản, chế biến thực phẩm một cách tốt nhất để giúp các chị em nội trợ có được một bữa cơm ngon và bổ dưỡng.",
                    UrlSlug = "thuc-pham",
                    Topic = topics[1],
                    ShowOnMenu = true,
                },
                new()
                {
                    Name = "Dụng cụ",
                    Description = "Tổng hợp các mẹo vặt sử dụng các dụng cụ nhà bếp đúng cách, hợp vệ sinh, đúng kỹ thuật như những đầu bếp chuyên nghiệp",
                    UrlSlug = "dung-cu",
                    Topic = topics[1],
                    ShowOnMenu = true,
                },
                new()
                {
                    Name = "Chế biến",
                    Description = "Tổng hợp và chia sẽ các Mẹo hay, kỹ thuật, bí quyết các cách Chế biến thực phẩm mà vẫn giữ được chất dinh dưỡng, tao ra một món ăn ngon.\r\n\r\nNgoài ra bạn có thể tham khảo nhiều mẹo vặt hay nhà bếp để có thêm nhiều kiến thức, kỹ năng, mẹo vặt giúp bạn yêu bếp hơn.",
                    UrlSlug = "che-bien",
                    Topic = topics[1],
                    ShowOnMenu = true,
                },
                new()
                {
                    Name = "Trang trí",
                    Description = "Một trong những cách để tạo ra sự hấp dẫn cho món ăn đó chính là cách trang trí món ăn. Nó đóng một vai trò quan trọng, góp phần và việc tạo ra được một món ăn thơm ngon và hấp dẫn. Nhưng không phải ai cũng có thể khéo tay để thực hiện được điều đó, món ăn được trang trí đẹp thì phải có bí quyết.\r\n\r\nChuyên mục dưới đây tổng hợp và trình bày các bí quyết trang trí món ăn hấp dẫn và bắt mắt từ các đầu bếp chuyên nghiệp.\r\n\r\nSự hấp dẫn của món ăn phụ thuộc và cách trang trí nhưng một phần, nó còn phụ thuộc và nhiều yếu tốkhác nữa. Bạn có thể tham khảo đầy đủ các yếu tố đó ở mẹo nhà bếp. Tại đây bạn có thể biết đươc những kiến thức hay về mẹo chọn lựa, chế biến nguyên liệu, món ăn để góp phần làm ngon bữa ăn của gia đình bạn\r\n\r\n",
                    UrlSlug = "trang-tri",
                    Topic = topics[1],
                    ShowOnMenu = true,
                },
                new()
                {
                    Name = "Khám phá",
                    Description = "Với nghề đầu bếp, đây là một nghề rất vất vả nhưng rất thú vị. Mỗi nơi, mỗi người, mỗi món ăn đều có những đặc trưng riêng. Đặc biệt là những nét riêng biệt ẩm thực vùng miền.\r\n\r\nChuyên trang tin hay nghề bếp sẽ giúp bạn khám phá thêm nhiều điều thú vị về ẩm thức của các vùng miền khác nhau, không những ở Việt nam mà còn các nước trên thế giới.\r\n\r\nĐiều này giúp bạn có được cái nhìn tổng quan hơn với nghề bếp. Từ đó giúp bạn yêu nghề và đam mê với nghề hơn.",
                    UrlSlug = "kham-pha",
                    Topic = topics[2],
                    ShowOnMenu = true,
                },
                new()
                {
                    Name = "Đầu bếp nổi tiếng",
                    Description = "Nếu mỗi món ăn là một tác phẩm nghệ thuật thì người đầu bếp chính là một nghệ sĩ. Thế giới đã và đang vinh danh những “nghệ sĩ ẩm thực” góp phần làm thay đổi bộ mặt của nền ẩm thực đương đại, là những tấm gương sáng cho các thế hệ đầu bếp trẻ trên hành trình nỗ lực để theo đuổi đam mê. Tại đây, có thể bạn sẽ tìm ra niềm cảm hứng cho chính mình qua các bài viết về những đầu bếp nổi tiếng thế giới cũng như tại Việt Nam.\r\n\r\nCon đường vươn đến đỉnh cao nghề bếp không hề dễ dàng. Để chạm tay vào thành công, các đầu bếp nổi tiếng đã phải vượt qua vô vàn khó khăn, thử thách và cả sự hoài nghi của xã hội. Nhưng họ đã dũng cảm vượt lên chính mình, từng bước chứng minh cho cả thế giới thấy rằng chỉ cần nỗ lực không ngừng, thành công sẽ tìm đến bạn vào một ngày nào đó. Cùng Nghề Bếp Á Âu lắng nghe câu chuyện, chia sẻ, bí quyết và kinh nghiệm để đời của những đầu bếp nổi tiếng thế giới và những đầu bếp Việt lừng danh để tìm ra lời giải đáp cho những thành công vượt trội của họ.\r\n\r\nKhông chỉ vượt trội về khả năng chuyên môn, tiếng vang của những tên tuổi này còn vươn xa khỏi quốc gia của họ nhờ tài năng ở các lĩnh vực khác như truyền hình, làm phim, viết sách, kinh doanh hay hoạt động xã hội. Hy vọng loạt bài viết về các đầu bếp lừng danh trong nước và quốc tế sẽ đem đến niềm vui và nguồn động lực lớn cho bạn thêm tin tưởng vào lựa chọn của mình.\r\n\r\nNgoài ra bạn có thể xem thêm chuyên mục Tin hay Nghề bếp để cập những kiến thức, kỹ năng để bổ sung cho bản thân mình",
                    UrlSlug = "dau-bep-noi-tieng",
                    Topic = topics[2],
                    ShowOnMenu = true,
                },
                new()
                {
                    Name = "Định hướng",
                    Description = "Với nghề đầu bếp, một nghề mang tính đặc biệt đòi hỏi người làm phải vừa có chuyên môn vừa có tâm với nghề. Nếu thật sự người đầu bếp có nhiều kỹ năng, có tâm thì hầu như tất cả những nổi buồn sẽ không còn.\r\n\r\nKhi thực khách thưởng thức một món ăn ngon, người ta hầu như sẽ nhớ ngay đến nhà hàng mà ít ai nhớ đến tên người nấu. Còn khi thức ăn không ngon miệng thì người đầu bếp là người chịu trách nhiệm đầu tiên. Tuy nhiên, niềm vui lớn nhất là được thỏa đam mê với nghề, được làm ra những món ăn ngon cho thực khách, cho những người thân yêu. Vui nhất là khi thực khách xuống bếp bắt tay vì được ăn ngon. Khi ấy những mệt mỏi, áp lực hầu như tan biến. Và còn gì hạnh phúc hơn khi chính người đầu bếp được gắn với một thương hiệu món riêng như: “Văn ba ba”, “Hiếu gà hành tăm”, “Đoan lẩu canh củ”, “Hoàng rươi Tứ Kỳ”…\r\n\r\nVà chuyên mục này sẽ nói những tâm sự của những người trong nghề bếp “lem luốc” mà đáng quý này! Giúp những bạn chưa có định hướng nên học nghề gì tìm được một nghề theo đam mê và thu nhập ổn định cho mình.",
                    UrlSlug = "dinh-huong",
                    Topic = topics[2],
                    ShowOnMenu = true,
                },
                new()
                {
                    Name = "Kinh doanh",
                    Description = "Nếu bạn đang có dự định kinh doanh khởi nghiệp với nghề nấu ăn như mở quán cafe, mở tiệm bánh, kinh doanh ẩm thực nhưng bạn không biết bắt đầu từ đầu. Đây là chuyên mục chia sẽ những kiến thức mà bạn cần học hỏi. Chia sẻ cho bạn những kinh nghiệm kinh doanh quán ăn hấp dẫn bắt kịp xu hướng hiện đại.\r\n\r\nNgoài ra bạn có thể xem thêm chuyên mục Tin hay Nghề Đầu bếp để cập những kiến thức, kỹ năng để bổ sung kinh nghiệm nghề bếp cho bản thân mình. Từ đó áp dụng thực tế kinh doanh để đạt kết quả thành công.",
                    UrlSlug = "kinh-doanh",
                    Topic = topics[2],
                    ShowOnMenu = true,
                },
                new()
                {
                    Name = "Kỹ thuật nấu ăn",
                    Description = "Tổng hợp các bài viết về kỹ thuật nấu ăn từ cơ bản đến nâng cao dành cho các đầu bếp chuyên nghiệp hoặc những ai muốn cải thiện kỹ năng nghề bếp. Dù là một người nội trợ gia đình hay đầu bếp chuyên nghiệp, việc hiểu rõ các phương pháp, kỹ thuật nấu nướng sẽ giúp bạn tạo ra các món ăn chất lượng cả về hương vị và tính thẩm mỹ.\r\n\r\nCác nội dung được đầu tư đa dạng, cung cấp đầy đủ chi tiết kiến thức từ các kỹ thuật nấu ăn cơ bản: áp chảo, chiên, hấp… cho đến các kỹ thuật nấu nướng nâng cao dành cho đầu bếp chuyên nghiệp: kỹ thuật confit, kỹ thuật đút lò, phi lê cá… giúp bạn từng bước hoàn thiện kỹ năng chuyên môn, trau dồi những kiến thức ẩm thực quan trọng. Nhuần nhuyễn các kỹ thuật chế biến món ăn cũng là nền tảng để bạn chinh phục các vị trí làm việc đòi hỏi kỹ năng tay nghề cao hoặc thỏa sức sáng tạo với các món ăn mang dấu ấn riêng, thỏa mãn mọi khách hàng khó tính nhất.\r\n\r\nTrong mỗi chia sẻ, chúng tôi luôn chú trọng đề cập đến các khái niệm, định nghĩa, hướng dẫn thực hiện chi tiết từng phương pháp chế biến xen kẽ các ví dụ và hình ảnh rõ ràng, bám sát thực tế. Hy vọng những kiến thức mà chúng tôi chia sẻ sẽ là cẩm nang gối đầu của các bạn đang học nghề làm đầu bếp để tạo ra những món ăn ngon.",
                    UrlSlug = "ky-thuat-nau-an",
                    Topic = topics[2],
                    ShowOnMenu = true,
                },
                new()
                {
                    Name = "Kiến thức ẩm thực",
                    Description = "Một đầu bếp giỏi không chỉ là người biết nấu ăn ngon mà còn có nền tảng kiến thức cơ bản về ẩm thực vững vàng được học từ những đầu bếp có nhiều kinh nghiệm hoặc được học tại các khóa học nấu ăn nữa đấy. Việc am hiểu cách bảo quản, sử dụng các dụng cụ và nguyên vật liệu nấu ăn giúp bạn áp dụng vào chế biến, sáng tạo món ăn một cách nhanh chóng, hiệu quả, giảm thiểu tối đa các rủi ro không đáng có trong khi làm bếp. Hiểu rõ tính chất của từng nguyên liệu, thực phẩm cũng giúp bạn nắm bắt khẩu vị của khách hàng đến từ các nền văn hóa, quốc gia khác nhau. Từ đó, việc phục vụ thực khách quốc tế trở nên dễ dàng hơn bao giờ hết.\r\n\r\nNhận thấy được tầm quan trọng của việc trau dồi kiến thức về nấu ăn, Nghề Bếp Á Âu (NBAAu) xây dựng chuyên mục Kiến thức ẩm thực nhằm đem đến cho các đầu bếp các bài viết với nội dung đa dạng, bổ ích cho quá trình theo đuổi đam mê nấu ăn của bạn.\r\n\r\nBên cạnh kiến thức về chuyên môn, rất nhiều mẹo hay trong nhà bếp được chia sẻ trong chuyên mục không chỉ hữu ích dành cho đầu bếp chuyên nghiệp mà còn thích hợp cho các đầu bếp gia đình, người yêu nấu ăn tham khảo. Những thắc mắc, hiểu lầm thường gặp đến từ các vấn đề trong nhà bếp được giải đáp cặn kẽ và chi tiết giúp người đọc dễ hình dung, dễ dàng áp dụng vào thực tế. Cùng đón đọc ngay những bài viết về kiến thức ẩm thực của Nghề Bếp Á Âu để khám phá những điều thú vị từ thế giới ẩm thực đa dạng, đầy màu sắc.",
                    UrlSlug = "kien-thuc-am-thuc",
                    Topic = topics[2],
                    ShowOnMenu = true,
                }
            };
            var categoryAdd = new List<Category>();
            foreach(var category in categories)
            {
                if(!_dbContext.Categories.Any(c => c.UrlSlug.Equals(category.UrlSlug)))
                {
                    categoryAdd.Add(category);
                }
            }
            _dbContext.AddRange(categoryAdd);
            _dbContext.SaveChanges();
            return categories;
        }

        private IList<Chef> AddChefs()
        {
            var chefs = new List<Chef>()
            {
                new()
                {
                    FullName = "Thầy Huỳnh Công Tạo",
                    Description = "Bếp trưởng Kubara Japanese Cuisine",
                    JoinedDate = new DateTime(2017, 07, 18),
                    UrlSlug = "huynh-cong-tao",
                }
            };
            var chefAdd = new List<Chef>();
            foreach(var chef in chefs)
            {
                if(!_dbContext.Chefs.Any(c => c.UrlSlug.Equals(chef.UrlSlug)))
                {
                    chefAdd.Add(chef);
                }
            }
            _dbContext.AddRange(chefAdd);
            _dbContext.SaveChanges();
            return chefs;
        }

        private IList<Demand> AddDemands()
        {
            var demands = new List<Demand>()
            {
                new()
                {
                    Name = "Học dài hạn",
                    UrlSlug = "hoc-dai-han"
                },
                new()
                {
                    Name = "Chăm sóc gia đình",
                    UrlSlug = "cham-soc-gia-dinh"
                },
                new()
                {
                    Name = "Mở cửa hàng kinh doanh",
                    UrlSlug = "mo-cua-hang-kinh-doanh"
                },
                new()
                {
                    Name = "Tìm việc làm",
                    UrlSlug = "tim-viec-lam"
                },
                new()
                {
                    Name = "Tìm hiếu ẩm thực mới",
                    UrlSlug = "tim-hieu-am-thuc-moi"
                },
                new()
                {
                    Name = "Trước khi lấy chồng",
                    UrlSlug = "truoc-khi-lay-chong"
                },
                new()
                {
                    Name = "Kỹ năng sống cho trẻ",
                    UrlSlug = "ky-nang-song-cho-tre"
                },
                new()
                {
                    Name = "Đi du học",
                    UrlSlug = "di-du-hoc"
                },
                new()
                {
                    Name = "Học theo yêu cầu",
                    UrlSlug = "hoc-theo-yeu-cau"
                }
            };
            var demandAdd = new List<Demand>();
            foreach(var demand in demands)
            {
                if(!_dbContext.Demands.Any(d => d.UrlSlug.Equals(demand.UrlSlug)))
                {
                    demandAdd.Add(demand);
                }
            }
            _dbContext.AddRange(demandAdd);
            _dbContext.SaveChanges();
            return demands;
        }

        private IList<Price> AddPrices()
        {
            var prices = new List<Price>()
            {
                new()
                {
                    Name = "Dưới 3 triệu",
                    UrlSlug = "duoi-3-trieu"
                },
                new()
                {
                    Name = "3 - 7 triệu",
                    UrlSlug = "3-7-trieu"
                },
                new()
                {
                    Name = "Trên 7 triệu",
                    UrlSlug = "tren-7-trieu"
                },
            };
            _dbContext.AddRange(prices);
            _dbContext.SaveChanges();
            return prices;
        }

        private IList<NumberOfSessions> AddNumberOfSessions()
        {
            var numberofsessions = new List<NumberOfSessions>()
            {
                new()
                {
                    Name = "Dưới 10 buổi",
                    UrlSlug = "duoi-10-buoi"
                },
                new()
                {
                    Name = "10 - 30 buổi",
                    UrlSlug = "10-30-buoi"
                },
                new()
                {
                    Name = "Trên 30 buổi",
                    UrlSlug = "tren-30-buoi"
                },
            };
            _dbContext.AddRange(numberofsessions);
            _dbContext.SaveChanges();
            return numberofsessions;
        }

        private IList<Student> AddStudents(
            IList<Course> courses)
        {
            var students = new List<Student>()
            {
                new()
                {
                    FullName = "Nguyễn Văn Kiệt",
                    Mobile = "0913411264",
                    Email = "nguyenvankiet@gmail.com",
                    UrlSlug = "nguyen-van-kiet",
                    RegisterDate = new DateTime(2022, 12, 1),
                    Notes = "",
                    Courses = new List<Course>()
                    {
                        courses[0],
                    }
                }
            };
            var studentAdd = new List<Student>();
            foreach (var student in students)
            {
                if (!_dbContext.Students.Any(s => s.UrlSlug.Equals(student.UrlSlug)))
                {
                    studentAdd.Add(student);
                }
            }
            _dbContext.AddRange(studentAdd);
            _dbContext.SaveChanges();
            return students;
        }

        private IList<Course> AddCourses(
            IList<Demand> demands,
            IList<Price> prices,
            IList<NumberOfSessions> numberOfSessions,
            IList<Chef> chefs)
        {
            var courses = new List<Course>()
            {
                new()
                {
                    Title = "Học nấu món Á",
                    ShortDescription = "Các món ăn châu Á hấp dẫn du khách khắp thế giới với các nguyên liệu tươi ngon, tinh khiết cùng cách chế biến tinh tế giữ lại hương vị thuần túy nhất của thực phẩm. Nhận thấy được tiềm năng phát triển mạnh mẽ của ẩm thực châu Á, Nghề Bếp Á Âu xây dựng và thiết kếchương trình học nấu món Á chuyên biệt dành cho các đối tượng có niềm đam mê nấu ăn hàng ngày hoặc muốn phát triển nghề nghiệp lâu dài trong lĩnh vực bếp Á.\r\n\r\nThị trường ẩm thực Việt những năm gần đây đánh dấu sự bùng nổ của các nhà hàng, quán ăn chuyên món Á. Các món lẩu nướng Hàn, sushi Nhật, cà-ri Ấn Độ, dimsum Hongkong… luôn nằm trong danh sách những món ăn được yêu thích nhất của giới trẻ. Tuy nhiên, số lượng đầu bếp và nhà hàng chuyên món Á chất lượng dường như chưa đáp ứng đủ nhu cầu của thị trường. Do đó, các khóa học nấu món Á của chúng tôi ra đời với tiêu chí đào tạo nên những đầu bếp món Á chuyên nghiệp, có khả năng tạo ra những món ăn ngon chuẩn vị và tự tin làm chủ những nhà hàng món Á đắt khách.",
                    Description = "Tham gia vào chương trình dạy nấu món Á của Nghề Bếp Á Âu (NBAAu), học viên được cung cấp tổng quan và chi tiết kiến thức về các nền ẩm thực đặc sắc thuộc khu vực châu Á như Việt, Hoa, Nhật, Hàn, Ấn Độ và Singapore; được học tất cả kỹ năng cần thiết từ khâu lựa chọn, sơ chế, bảo quản nguyên liệu cho đến cách chế biến món ăn, nêm nếm chuẩn vị, kỹ năng trang trí và trình bày món ăn sao cho bắt mắt, hấp dẫn người dùng. Đặc biệt, nội dung học chuyên sâu vào kỹ thuật chế biến các món Á trứ danh và đặc trưng của mỗi nền ẩm thực.\r\n\r\nNội dung khóa học được xây dựng bài bản, đặc biệt chú trọng vào yếu tố thực hành (chiếm đến 90%) và liên tục đổi mới theo xu hướng ẩm thực thế giới; đội ngũ giảng viên giàu kinh nghiệm, không giấu nghề; thời gian học linh động, đa dạng về hình thức đào tạo: lớp chuyên đề, lớp yêu cầu, lớp học nấu ăn chuyên nghiệp đáp ứng mọi sở thích, nhu cầu và điều kiện của nhiều đối tượng học viên khác nhau. Tham gia các lớp học nấu ăn mở quán, học vì đam mê hay học để trở thành đầu bếp món Á chuyên nghiệp? Mọi nhu cầu của bạn đều được thỏa mãn với các khóa học phù hợp.",
                    UrlSlug = "hoc-nau-mon-a",
                    CreateDate = new DateTime(2017, 1, 1),
                    UpdateDate = null,
                    Demand = demands[1],
                    Price = prices[1],
                    NumberOfSessions = numberOfSessions[1],
                    Published = true,
                    Chefs = new List<Chef>()
                    {
                        chefs[0]
                    }
                }
            };
            var courseAdd = new List<Course>();
            foreach(var course in courses)
            {
                if(!_dbContext.Courses.Any(c => c.UrlSlug.Equals(course.UrlSlug)))
                {
                    courseAdd.Add(course);
                }
            }
            _dbContext.AddRange(courseAdd);
            _dbContext.SaveChanges();
            return courses;
        }

        private IList<Recipe> AddRecipes(
            IList<Course> courses,
            IList<Author> authors)
        {
            var recipes = new List<Recipe>()
            {
                new()
                {
                    Title = "Xôi xoài Thái Lan",
                    ShortDesciption = "Nếu là tín đồ của ẩm thực Thái Lan, bạn đừng vội bỏ qua cách nấu xôi xoài thơm ngon, chuẩn vị từ hướng dẫn của Nghề Bếp Á Âu. Được biết đến là một món ăn đường phố, xôi xoài cuốn hút thực khách bốn phương nhờ hương vị dẻo thơm của nếp kết hợp cùng xoài tươi ngọt mát và đặc biệt là nước dừa béo ngậy.",
                    Description = "Ẩm thực của người Thái Lan luôn khiến chúng ta bất ngờ và ngạc nhiên không chỉ bởi sự thơm ngon hấp dẫn mà còn rất độc đáo, lạ miệng. Xôi xoài là một trong những món ăn đường phố nổi tiếng của người Thái với cách chế biến khá đơn giản. Cơm nếp tưới nước cốt dừa đặt bên cạnh vài miếng xoài tươi là bạn có ngay món xôi đặc sản ngọt thơm, béo ngậy nổi tiếng của xứ chùa vàng. Hôm nay, hãy cùng Nghề Bếp Á Âu xắn tay áo lên và trổ tài làm bếp với món xôi xoài kiểu Thái siêu độc đáo này.",
                    Metadata = "Ẩm thực của người Thái Lan luôn khiến chúng ta bất ngờ và ngạc nhiên không chỉ bởi sự thơm ngon hấp dẫn mà còn rất độc đáo, lạ miệng. Xôi xoài là một trong những món ăn đường phố nổi tiếng của người Thái với cách chế biến khá đơn giản. Cơm nếp tưới nước cốt dừa đặt bên cạnh vài miếng xoài tươi là bạn có ngay món xôi đặc sản ngọt thơm, béo ngậy nổi tiếng của xứ chùa vàng. Hôm nay, hãy cùng Nghề Bếp Á Âu xắn tay áo lên và trổ tài làm bếp với món xôi xoài kiểu Thái siêu độc đáo này.",
                    UrlSlug = "xoi-xoai-thai-lan",
                    Ingredient = "250g gạo nếp\n1 quả xoài chín\n200ml nước cốt dừa\n20g đậu phộng rang đập dập\n2 muỗng canh mè rang\n1/3 muỗng cà phê muối\n60g đường\n1 muỗng cà phê bột năng\n20g dừa tươi bào sợi",
                    Step = "Bước 1: Hấp xôi\r\nGạo nếp mang đi vo sạch, sau đó ngâm qua đêm, rửa lại thêm 1 lần nữa cho sạch, để ráo. Tiếp theo, bạn trộn đều vào gạo nếp 1/3 muỗng cà phê muối.\nBắc xửng hấp lên bếp đun với lửa lớn cho nước sôi rồi cho gạo nếp vào hấp chín, thời gian hấp là khoảng 15 phút.\r\n\r\nGạo nếp sau khi hấp khoảng 15 phút, bạn giở nắp cho 50ml nước cốt dừa và 30gr đường vào trộn đều lên và tiếp tục hấp trong 10 phút nữa.\r\n\r\n",
                    CreateDate = new DateTime(2020, 12, 1),
                    UpdateDate = null,
                    Published = true,
                    Author = authors[0],
                    Course = courses[0],
                    ViewCount = 1,
                }
            };
            var recipeAdd = new List<Recipe>();
            foreach (var recipe in recipes)
            {
                if(!_dbContext.Recipes.Any(r => r.UrlSlug.Equals(recipe.UrlSlug)))
                {
                    recipeAdd.Add(recipe);
                }
            }
            _dbContext.AddRange(recipeAdd);
            _dbContext.SaveChanges();
            return recipes;
        }

        private IList<Post> AddPosts(
            IList<Category> categories,
            IList<Author> authors)
        {
            var posts = new List<Post>()
            {
                new()
                {
                    Title = "Thực Đơn Giảm Cân Cho Nữ",
                    ShortDescription = "Tổng hợp thực đơn giảm cân cho nữ theo ngày dưới đây sẽ giúp bạn có được vóc dáng thon gọn như mong muốn để tự tin diện những bộ cánh thật đẹp. Hiện nay, có rất nhiều thực đơn giảm cân nhưng để lựa chọn một chế độ phù hợp với cơ thể, an toàn, hiệu quả lại không dễ dàng.",
                    Description = "Vậy làm sao để có thể có một thực đơn giảm cân khoa học lại hiệu quả, hãy cùng Nghề Bếp Á Âu tham khảo thực đơn giảm cân tăng cơ 7 ngày cho nữ dưới đây ngay nhé!",
                    UrlSlug = "thuc-don-giam-can-cho-nu",
                    Metadata = "Vậy làm sao để có thể có một thực đơn giảm cân khoa học lại hiệu quả, hãy cùng Nghề Bếp Á Âu tham khảo thực đơn giảm cân tăng cơ 7 ngày cho nữ dưới đây ngay nhé!",
                    CreateDate = new DateTime(2020,10 ,31),
                    UpdateDate = null,
                    Published = true,
                    Author = authors[0],
                    Category = categories[0],
                }
            };
            var postAdd = new List<Post>();
            foreach(var post in posts)
            {
                if(!_dbContext.Posts.Any(p => p.UrlSlug.Equals(post.UrlSlug)))
                {
                    postAdd.Add(post);
                }
            }
            _dbContext.AddRange(postAdd);
            _dbContext.SaveChanges();
            return posts;
        }
    }
}
