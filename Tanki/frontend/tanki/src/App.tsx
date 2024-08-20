import './App.css';
import { Lobby } from './components/Lobby';
import { Connection } from './models/Connection';
import { Header } from './components/Header';


function App() {

  var connection = new Connection();
  //connection.Connect();

  return (
    <div className='app'>
      <Header/>
      <Lobby connection={connection}/>
    </div>
  );
}

export default App;
