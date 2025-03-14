import React, {
	useEffect,
	useState,
} from 'react';

import { Log } from '../../logger';
import configData from "../../logConfig.json";
import { formatCurrency } from '../../helpers/Formatters';
import 'bootstrap/dist/css/bootstrap.min.css';
import '../../App.css';

interface Home {
    id: number;
    address: string;
    city: string;
    state: string;
    zipcode: number;
    comments: string;
    url: string;
    image: string;
    price: string;
}

	const HomeComponent: React.FC<Home> = () => {
      const[homes, setHomes] = useState<Home[] > ([]);

		useEffect(() => {
			const getHomes = async () => {
            try {
                const homesData = await populateHomeData();
                if (homesData.length !== 0) {

                    const homesWithPrices = homesData.map(home => ({
                        ...home,
                        price: formatCurrency(parseInt(home.price)),
                        // assets directory will not work here
                        image: `../../public/Images/${home.image}` // works
                    }));
                    setHomes(homesWithPrices);
                    const logData = JSON.stringify(homesWithPrices);
                    const log = new Log('getHomes \n' + logData);
                    log.info();
                }
            } catch (error) {
                const logData = JSON.stringify(error);
                const log = new Log(`Error ${error}: ${logData}`);
                log.error();
            }
        };
        getHomes();
    }, []);
    async function populateHomeData(): Promise<Home[]> {
        const baseUrl = configData.development.apiUrl;
        const response = await fetch(`${baseUrl}/api/home/GetAllAsync`);

        if (!response.ok) {
            const logData = JSON.stringify(response);
            const log = new Log(`This is the error ${logData}`);
            log.error();
            throw new Error('Network response was not ok');
        } else {
            const logData = JSON.stringify(response);
            const log = new Log('populateHomeData ' + logData);
            log.info();
            return response.json();
        }
    }


        return (
            <div>
                <h1 className=" row text-center my-4 center">Santa Fe Homes</h1>

                <div className="container center">
                    <div className="row justify-content-center">
                        {homes.map(home => (
                            <div key={home.id} className="col-md-4">
                                <div className="card mb-4">
                                    <img src={home.image} className="card-img-top" alt={home.address} />
                                    <div className="card-body">
                                        <h5 className="card-text"><strong>Address:</strong> {home.address}</h5>
                                        <p className="card-text"><strong>City:</strong> {home.city}</p>
                                        <p className="card-text"><strong>State:</strong> {home.state}</p>
                                        <p className="card-text"><strong>Zipcode:</strong> {home.zipcode}</p>
                                        <p className="card-text"><strong>Comments:</strong> {home.comments}</p>
                                        <p className="card-text"><strong>Price:</strong> {home.price}</p>
                                    </div>
                                </div>
                            </div>
                        ))}
                    </div>
                </div>
            </div>
        );

};

export default HomeComponent;