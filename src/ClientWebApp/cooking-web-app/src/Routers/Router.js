import { BrowserRouter, Route, Routes } from "react-router-dom";
import Home from "../Pages/User/Home";
import Layout from "../Pages/User/Layout";
import Cooking from "../Pages/User/Cooking";
import Food from "../Pages/User/Food";
import DailyMenu from "../Pages/User/DailyMenu";
import KitchenTips from "../Pages/User/KitchenTips"
import News from "../Pages/User/News";
import CourseDetail from "../Components/Course/CourseDetail/CourseDetail"
import RecipeDetail from "../Components/Recipe/RecipeDetail/RecipeDetail";
import PostDetail from "../Components/Post/PostDetail/PostDetail"
import AdminLayout from "../Pages/Admin/AdminLayout";
import AdminCourse from "../Pages/Admin/Course/AdminCourses";
import CourseEdit from "../Pages/Admin/Course/CourseEdit"
import BadRequest from "../Pages/Shared/BadRequest";
import NotFound from "../Pages/Shared/NotFound"
import AdminRecipe from "../Pages/Admin/Recipe/AdminRecipes";
import RecipeEdit from "../Pages/Admin/Recipe/RecipeEdit";
import AdminPost from "../Pages/Admin/Post/AdminPosts";
import PostEdit from "../Pages/Admin/Post/PostEdit"
import About from "../Pages/User/About";

const Router = () => {
    return(
        <BrowserRouter>
            <Routes>
                <Route path="/400" element={<BadRequest/>}/>
                <Route path="/*" element={<NotFound/>}/>
                <Route path="/" element={<Layout/>}>
                    <Route path="/" element={<Home/>}/>
                    <Route path="/hoc-nau-an" element={<Cooking/>}/>
                    <Route path="/khoa-hoc/:slug" element={<CourseDetail/>}/>
                    <Route path="/mon-an-ngon" element={<Food/>}/>
                    <Route path="/cong-thuc/:slug" element={<RecipeDetail/>}/>
                    <Route path="/thuc-don-moi-ngay" element={<DailyMenu/>}/>
                    <Route path="/thuc-don/:slug" element={<PostDetail/>}/>
                    <Route path="/meo-nha-bep" element={<KitchenTips/>}/>
                    <Route path="/tin-tuc" element={<News/>}/>
                    <Route path="/gioi-thieu" element={<About/>}/>
                </Route>
                <Route path="/admin" element={<AdminLayout/>}>
                    <Route path="/admin/courses" element={<AdminCourse/>}/> 
                    <Route path="/admin/courses/edit" element={<CourseEdit/>}/>  
                    <Route path="/admin/courses/edit/:id" element={<CourseEdit/>}/>  
                    <Route path="/admin/recipes" element={<AdminRecipe/>}/>  
                    <Route path="/admin/recipes/edit" element={<RecipeEdit/>}/>  
                    <Route path="/admin/recipes/edit/:id" element={<RecipeEdit/>}/>  
                    <Route path="/admin/posts" element={<AdminPost/>}/> 
                    <Route path="/admin/posts/edit" element={<PostEdit/>}/>  
                    <Route path="/admin/posts/edit/:id" element={<PostEdit/>}/>   
                </Route>          
            </Routes>
        </BrowserRouter>
    )
}

export default Router;
