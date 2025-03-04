import { useEffect, useState } from 'react';
import { Log } from './logger';
import configData from "./logConfig.json";

//import DataTable from 'datatables.net-react';
//import DT from 'datatables.net-dt';
//import 'datatables.net-select-dt';
//import 'datatables.net-buttons-dt';
//import 'datatables.net-buttons/js/buttons.html5';
//import jszip from 'jszip';
//import pdfmake from 'pdfmake';

import './App.css';


//import { json } from './';
interface Home {
    Id: number;
    address: string;
    city: string;
    state: string;
    zipcode: number,
    comments: string;
    imageUrl: string;
    price: number;
}

function App() {

    const [homes, setHomes] = useState<Home[]>([]);

    useEffect( () => {
        const getHomes = async () => {
            try {
                const homesData = await populateHomeData();
                setHomes(homesData);
                const logData = JSON.stringify(homesData);
                const log = new Log(logData);
                log.info();
            }
            catch (error) {
                const logData = JSON.stringify(error);
                const log = new Log(`Error ${error}: ${logData}`);
                log.error();
            }

        };
        getHomes();
    }, []);

    const contents = homes === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Address</th>
                    <th>City</th>
                    <th>State</th>
                    <th>Price</th>
                    <th>Url</th>
                </tr>
            </thead>
            <tbody>
                {homes.map(home =>
                    <tr key={home.Id}>
                        <td>{home.address}</td>
                        <td>{home.city}</td>
                        <td>{home.state}</td>
                        <td>{home.price}</td>
                        <td>{home.imageUrl}</td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tabelLabel">Santa Fe Homes</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
    );

    async function populateHomeData(): Promise<Home[]>{

        const baseUrl = configData.development.apiUrl;

        let log = new Log(`This is the baseUrl ${baseUrl}`);
        log.info();

        const response = await fetch(`${baseUrl}/api/home/GetAllAsync`);
 
        if (!response.ok) {
            const logData = JSON.stringify(response);
            const log = new Log(`This is the error ${logData}`);
            log.error();
            throw new Error('Network response was not ok');
        }

        const logData = JSON.stringify(response);

        log = new Log(logData);
        log.info();
        return response.json();

    }
}

export default App;