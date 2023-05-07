import { get_api } from "./Methods"

export function getChefs(){
    return get_api(`https://localhost:7029/api/chefs`);
  }