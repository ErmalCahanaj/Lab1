import logo from './logo.svg';
import './App.css';
import {Home} from './Home';
import {Imdb} from './Imdb';
import {Komedi} from './Komedi';
import { Drama } from "./Drama";
import { Hindi } from "./Hindi";
import {Mafia} from './Mafia';
import {Shqip} from './Shqip';
import {Dokumentar} from './Dokumentar';
import {Kategoria} from './Kategoria';
import {Biografia} from './Biografia';
import { Viti } from "./Viti";
import { Nordik } from "./Nordik";
import {Krim} from './Krim';
import {Netflix} from './Netflix';
import {Francez} from './Francez';
import {Romance} from './Romance';


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
          <li className="nav-item- m-1">
              <NavLink
                className="btn btn-light btn-outline-primary"
                to="/Drama"
              >
                Drama
              </NavLink>
            </li>
            <li className="nav-item- m-1">
              <NavLink
                className="btn btn-light btn-outline-primary"
                to="/Hindi"
              >
                Hindi
              </NavLink>
            </li>
      <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/Netflix">
              Netflix
            </NavLink>
          </li>
        
          <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/Krim">
              Krim
            </NavLink>
          </li>
          <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/Francez">
              Francez
            </NavLink>
          </li>
              <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/Mafia">
              Mafia
            </NavLink>
          </li>
          <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/Shqip">
              Shqip
            </NavLink>
          </li>
          <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/Dokumentar">
            Dokumentar
            </NavLink>
          </li>
          <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/Kategoria">
            Kategoria
            </NavLink>
          </li>
          <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/Biografia">
            Biografia
            </NavLink>
          </li>
    <li className="nav-item- m-1">
              <NavLink className="btn btn-light btn-outline-primary" to="/Viti">
                Viti
              </NavLink>
            </li>
            <li className="nav-item- m-1">
              <NavLink
                className="btn btn-light btn-outline-primary"
                to="/Nordik"
              >
                Nordik
              </NavLink>
            </li>
    </li>
            <li className="nav-item- m-1">
              <NavLink
                className="btn btn-light btn-outline-primary"
                to="/Romance"
              >
                Romance
              </NavLink>
            </li>

        </ul>
      </nav>

      <Routes>
       <Route exact path='/Home' element={<Home/>}/>
        <Route exact path='/Imdb' element={<Imdb/>}/>
        <Route exact path='/Komedi' element={<Komedi/>}/>
       <Route exact path='/Netflix' element={<Netflix/>}/>
        <Route exact path='/Krim' element={<Krim/>}/>
        <Route exact path='/Francez' element={<Francez/>}/>
           <Route exact path="/Drama" element={<Drama />} />
          <Route exact path="/Hindi" element={<Hindi />} />
         <Route exact path='/Mafia' element={<Mafia/>}/>
        <Route exact path='/Shqip' element={<Shqip/>}/>
        <Route exact path='/Dokumentar' element={<Dokumentar/>}/>
        <Route exact path='/Kategoria' element={<Kategoria/>}/>
        <Route exact path='/Biografia' element={<Biografia/>}/>
          <Route exact path="/Viti" element={<Viti />} />
          <Route exact path="/Nordik" element={<Nordik />} />
            <Route exact path="/Romance" element={<Romance />}
            

      </Routes>
    </div>
    </BrowserRouter>
  );
}

export default App;
