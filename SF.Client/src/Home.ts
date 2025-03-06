import { formatCurrency } from './helpers/Formatters';
export class Home {
    id: number;
    address: string;
    city: string;
    state: string;
    zipcode: number;
    price: string;
    comments: string;
    url: string;

    constructor(id: number, address: string, city: string, state: string, zipcode: number, comments: string, url: string, price: number) {
        this.id = id;
        this.address = address;
        this.city = city;
        this.state = state;
        this.zipcode = zipcode;
        this.comments = comments;
        this.url = url;
        this.price = formatCurrency(price)
    }
}