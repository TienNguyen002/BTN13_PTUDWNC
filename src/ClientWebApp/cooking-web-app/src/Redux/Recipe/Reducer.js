import { createSlice } from "@reduxjs/toolkit";

const initialState = {
    keyword: '',
    authorId: '',
    courseId: '',
    year: '',
    month: '',
};

const recipeFilterReducer = createSlice({
    name: 'recipeFilter',
    initialState,
    reducers: {
        reset: (state, action) => {
            return initialState;
        },
        updateKeyword: (state, action) => {
            return {
                ...state,
                keyword: action.payload
            };
        },
        updateAuthorId: (state, action) => {
            return {
                ...state,
                authorId: action.payload
            }
        },
        updateCourseId: (state, action) => {
            return {
                ...state,
                courseId: action.payload
            }
        },
        updateMonth: (state, action) => {
            return {
                ...state,
                month: action.payload
            }
        },
        updateYear: (state, action) => {
            return {
                ...state,
                year: action.payload
            }
        }
    }
})

export const {
    reset,
    updateKeyword,
    updateAuthorId,
    updateCourseId,
    updateMonth,
    updateYear,
} = recipeFilterReducer.actions;

export const recipeReducer = recipeFilterReducer.reducer;