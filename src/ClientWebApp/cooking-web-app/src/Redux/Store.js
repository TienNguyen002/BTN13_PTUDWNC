import { configureStore } from "@reduxjs/toolkit";
import { reducer } from './Reducer'

const store = configureStore({
    reducer: {
        courseFilter: reducer,
    },
});

export default store;