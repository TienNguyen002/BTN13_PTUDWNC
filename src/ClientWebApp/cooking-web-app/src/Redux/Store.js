import { configureStore } from "@reduxjs/toolkit";
import { courseReducer } from './Course/Reducer'
import { recipeReducer } from "./Recipe/Reducer";
import { postReducer} from "./Post/Reducer"

const store = configureStore({
    reducer: {
        courseFilter: courseReducer,
        recipeFilter: recipeReducer,
        postFilter: postReducer,
    },
});

export default store;