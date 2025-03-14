import ReactDOM from 'react-dom';
import ShowHomes from './ShowHomes';
import { it } from 'mocha';


it('It should mount', () => {
  const div = document.createElement('div');
  ReactDOM.render(<ShowHomes />, div);
  ReactDOM.unmountComponentAtNode(div);
});