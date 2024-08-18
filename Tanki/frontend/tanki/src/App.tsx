import { Container } from '@chakra-ui/react';
import './App.css';
import { Lobby } from './components/Lobby';


function App() {

  return (
    <>
      <Container 
        minH="100vh" minW="100vw" padding="0" 
        className="bg-gradient-to-b from-gray-900 to-slate-900">
       
        <Lobby/>
      </Container>
    </>
  );
}

export default App;
