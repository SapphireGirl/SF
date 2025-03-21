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
        const [selectedHomeId, setSelectedHomeId] = useState<number | null>(null);

		useEffect(() => {
            const getHomes = async () => {
            const log = new Log();

            try {
                const homesData = await populateHomeData();
                if (homesData.length !== 0) {

                    const homesWithPrices = homesData.map(home => ({
                        ...home,
                        price: formatCurrency(parseInt(home.price)),
                        // assets directory will not work here
                        image: `../../Images/${home.image}` // works
                    }));
                    setHomes(homesWithPrices);
                    const logData = JSON.stringify(homesWithPrices);
                    log.info('getHomes \n' + logData);
                }
            } catch (error) {
                const logData = JSON.stringify(error);
                log.error(`Error ${error}: ${logData}`);
            }
        };
        getHomes();
    }, []);
    async function populateHomeData(): Promise<Home[]> {
        const baseUrl = configData.development.apiUrl;
        const response = await fetch(`${baseUrl}/api/home/GetAllAsync`);
        const log = new Log();

        if (!response.ok) {
            const logData = JSON.stringify(response);
            log.error(`This is the error ${logData}`);
            throw new Error('Network response was not ok');
        } else {
            const logData = JSON.stringify(response);
            log.info('populateHomeData ' + logData);
            return response.json();
        }
    }

    function handleCardClick(homeId: number) {
        const log = new Log();
        setSelectedHomeId(homeId === selectedHomeId ? null : homeId);
        const logData = "Clicking on card with ID: " + homeId;
        
        log.info(logData);
    }

    function handleCloseClick() {
        setSelectedHomeId(null);
    }

    return (
        <div>
            <h1 className="row text-center my-4 center">Santa Fe Homes</h1>
            <div className="container center">
                <div className="row justify-content-center">
                    {homes.map(home => (
                        <React.Fragment key={home.id}>
                            <div className="col-md-4">
                                <div className="card mb-4">
                                    <img src={home.image} className="card-img-top img-fluid w-100" alt={home.address} onClick={() => handleCardClick(home.id)} />
                                    
                                </div>
                            </div>
                            {selectedHomeId === home.id && (
                                <div className="col-12">
                                    <div className="card mb-4">
                                        <div className="card-body d-flex justify-content-between align-items-center">
                                            <div>
                                                <p className="card-text text-start"><strong>Address:</strong> {home.address}</p>
                                                <p className="card-text text-start"><strong>City:</strong> {home.city}</p>
                                                <p className="card-text text-start"><strong>State:</strong> {home.state}</p>
                                                <p className="card-text text-start"><strong>Zipcode:</strong> {home.zipcode}</p>
                                                <p className="card-text text-start"><strong>Comments:</strong> {home.comments}</p>
                                                <p className="card-text text-start"><strong>Price:</strong> {home.price}</p>
                                            </div>
                                            <button
                                                type="button"
                                                className="btn-close position-absolute top-0 end-0"
                                                aria-label="Close"
                                                onClick={handleCloseClick}
                                            ></button>
                                        </div>
                                    </div>
                                </div>
                            )}
                        </React.Fragment>
                    ))}
                </div>
            </div>
        </div>
    );
};

export default HomeComponent;