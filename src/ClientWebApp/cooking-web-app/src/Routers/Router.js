import { BrowserRouter, Route, Routes } from "react-router-dom";
import Home from "../Pages/User/Home";
import Layout from "../Pages/User/Layout";
import Cooking from "../Pages/User/Cooking";
import Food from "../Pages/User/Food";
import DailyMenu from "../Pages/User/DailyMenu";
import KitchenTips from "../Pages/User/KitchenTips"
import News from "../Pages/User/News";
import RSS from "../Pages/User/RSS";

const Router = () => {
    return(
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Layout/>}>
                    <Route path="/" element={<Home/>}/>
                    <Route path="/hoc-nau-an" element={<Cooking/>}/>
                    <Route path="/mon-an-ngon" element={<Food/>}/>
                    <Route path="/thuc-don-moi-ngay" element={<DailyMenu/>}/>
                    <Route path="/meo-nha-bep" element={<KitchenTips/>}/>
                    <Route path="/tin-tuc" element={<News/>}/>
                    <Route path="/rss" element={<RSS/>}/>
                </Route>
            </Routes>
        </BrowserRouter>
    )
}

export default Router;
