import { createSlice } from "@reduxjs/toolkit";

const initialState = {
    keyword: '',
    demandId: '',
    priceId: '',
    numberofsessionsId: '',
    year: '',
    month: '',
};

const courseFilterReducer = createSlice({
    name: 'courseFilter',
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
        updateDemandId: (state, action) => {
            return {
                ...state,
                demandId: action.payload
            }
        },
        updatePriceId: (state, action) => {
            return {
                ...state,
                priceId: action.payload
            }
        },
        updateNumberOfSessionsId: (state, action) => {
            return {
                ...state,
                numberofsessionsId: action.payload
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
    updateDemandId,
    updatePriceId,
    updateNumberOfSessionsId,
    updateMonth,
    updateYear,
} = courseFilterReducer.actions;

export const reducer = courseFilterReducer.reducer;