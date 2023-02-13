import logo from './logo.svg';
import './App.css';
import {Home} from './Home';
import {Imdb} from './Imdb';
import {Komedi} from './Komedi';
import {BrowserRouter, Route, Routes ,NavLink} from 'react-router-dom';

function App() {
  return (
    <BrowserRouter>
    <div className="App container">
      <h3 className="d-flex justify-content-center m-3">
        React JS Frontend
      </h3>
        
      <nav className="navbar navbar-expand-sm bg-light navbar-dark">
        <ul className="navbar-nav">
          <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/Home">
              Home
            </NavLink>
          </li>
          <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/Imdb">
              Imdb
            </NavLink>
          </li>
          <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/Komedi">
              Komedi
            </NavLink>
          </li>
        </ul>
      </nav>

      <Routes>
       <Route exact path='/Home' element={<Home/>}/>
        <Route exact path='/Imdb' element={<Imdb/>}/>
        <Route exact path='/Komedi' element={<Komedi/>}/>
      </Routes>
    </div>
    </BrowserRouter>
  );
}

export default App;