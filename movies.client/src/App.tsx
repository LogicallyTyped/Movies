import { Container } from '@mui/material';
import { useEffect } from 'react';
import { Outlet, useLocation } from 'react-router-dom';
import './App.css';
import MovieTable from './components/movies/MovieTable';
import { setupErrorHandlingInterceptor } from './interceptors/axiosinterceptor';

function App() {
    const location = useLocation();

    useEffect(() => {
        setupErrorHandlingInterceptor();
    }, []);

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