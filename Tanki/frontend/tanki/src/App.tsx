import './App.css';
import { Header } from './components/Header';
import { IndexPage } from './pages/IndexPage';
import { SessionPage } from './pages/SessionPage';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { RegisterForm } from './components/RegisterForm';
import { PrivateRoute } from './components/PrivateRoute';


function App() {

  return (


    <div className='bg-[url("../public/img/background.jpg")]
      bg-no-repeat h-screen bg-cover'>

      <div className='flex h-[8%]'>
        <Header />
      </div>

      <div className='flex h-[90%]'>
        <BrowserRouter>
          <Routes>
            <Route path='/' element={<PrivateRoute><IndexPage/></PrivateRoute>}/>
            <Route path='/session' element={<PrivateRoute><SessionPage/></PrivateRoute>}/>

            <Route path='/register' element={<RegisterForm />} />
          </Routes>
        </BrowserRouter>
      </div>
    </div>
  );
}

export default App;
