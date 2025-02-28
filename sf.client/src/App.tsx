import { useEffect, useState } from 'react';
import { logger } from './logger';
import './App.css';

//interface Forecast {
//    date: string;
//    temperatureC: number;
//    temperatureF: number;
//    summary: string;
//}

interface Home {
    address: string;
    city: string;
    state: string;
    zipcode: number,
    comments: string;
    imageUrl: string;
    price: number;
}

function App() {
    //const [forecasts, setForecasts] = useState<Forecast[]>();
    const [homes, setHomes] = useState<Home[]>();

    const messageTemplate:string = 'Hello from the client!';

    useEffect(() => {

        logger.emit({
            timestamp: new Date(),
            level: 'Information',
            messageTemplate,
            properties: {
                source: navigator.userAgent
            }
        });

         populateHomeData();
      }, []);

    const contents = homes === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Temp. (C)</th>
                    <th>Temp. (F)</th>
                    <th>Summary</th>
                </tr>
            </thead>
            <tbody>
                {homes.map(home =>
                    <tr key={home.address}>
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

    async function populateHomeData() {
        const response = await fetch('/api/home/Get');
        const data = await response.json();
        setHomes(data);
    }
}

export default App;