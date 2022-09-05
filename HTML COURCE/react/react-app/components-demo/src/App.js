import React from 'react';
import logo from './logo.svg';
import { Header } from './components/Header';
import { Booklist } from './components/Booklist';
import { Timer } from './components/Timer';
import './App.css';

function App() {
    const books =[
        {"title": "Northanger Abbey","author": "Austen, Jane", "year": 1983}
    ];
    return (
        <div className="App">
            <header className="App-header">

                <Header>
                    <span className='fancy-font'>Book</span>
                </Header>
                <Booklist books={books}></Booklist>
                <Timer/>
                <img src={logo} className="App-logo" alt="logo" />
            </header>
        </div>
    );
}

export default App;
