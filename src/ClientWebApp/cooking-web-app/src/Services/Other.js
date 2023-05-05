import { get_api } from "./Methods"

export function getDemands(){
    return get_api(`https://localhost:7029/api/others/demands`);
}

export function getPrices(){
    return get_api(`https://localhost:7029/api/others/prices`);
}

export function getSessions(){
    return get_api(`https://localhost:7029/api/others/sessions`);
}