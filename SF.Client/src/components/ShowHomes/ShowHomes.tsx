import { FC } from 'react';
import { ShowHomesWrapper } from './ShowHomes.styled';

interface Home {
    id: number;
    address: string;
    city: string;
    state: string;
    zipcode: number;
    comments: string;
    url: string;
    price: string;
}

const ShowHomes: FC<Home[]> = () => (
 <ShowHomesWrapper>
    ShowHomes Component
 </ShowHomesWrapper>
);

export default ShowHomes;
