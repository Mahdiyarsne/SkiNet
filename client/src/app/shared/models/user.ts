export type User ={
    firstName: string;
    lastName : string;
    email: string;
    address : Address;
}

export type Address ={
    link1:string;
    link2?:string;
    city:string;
    state:string;
    country: string;
    postalCode:string;
}