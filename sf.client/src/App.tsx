import HomeComponent from './components/HomesComponent/HomesComponent';
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';
function App() {
    return (
        <div>
            <HomeComponent id={0} address={''} city={''} state={''} zipcode={0} comments={''} url={''} price={''} />
        </div>
    );
}

export default App;