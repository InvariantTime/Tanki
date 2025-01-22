import './App.css';
import { IndexPage } from './pages/IndexPage';
import { SessionPage } from './pages/SessionPage';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { PrivateRoute } from './components/PrivateRoute';
import { RegisterPage } from './pages/auth/RegisterPage';
import { SignInPage } from './pages/auth/SignInPage';
import { GameField } from './components/GameField';


function App() {

  return (
    <div className='bg-[url("../public/img/background.jpg")]
      bg-no-repeat h-screen bg-cover'>

      {<BrowserRouter>
        <Routes>

          <Route element={<PrivateRoute />}>
            <Route path='/' element={<IndexPage />} />
            <Route path='/session/:sessionId' element={<SessionPage />} />
          </Route>

          <Route path='/register' element={<RegisterPage />}/>
          <Route path='/signin' element={<SignInPage />}/>

        </Routes>
      </BrowserRouter>}

    </div>
  );
}

export default App;
