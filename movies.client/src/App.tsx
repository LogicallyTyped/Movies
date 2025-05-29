import { Outlet, useLocation } from 'react-router-dom';
import './App.css';
import MovieTable from './components/movies/MovieTable';
import { Container } from '@mui/material';

function App() {
    const location = useLocation();

    return (
        <>
            {location.pathname === '/' ? <MovieTable /> : (
                <Container className="container-style">
                    <Outlet />
                </Container>
            )}
        </>
    )
}

export default App;