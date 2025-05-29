import { Button, Container } from "@mui/material";
import { useEffect, useState } from "react";
import apiConnector from "../../api/apiConnector.ts";
import type { MovieDto } from "../../models/movieDto.ts";
import MovieTableItem from "./MovieTableItem.tsx";

export default function MovieTable() {

    const [movies, setMovies] = useState<MovieDto[]>([]);

    useEffect(() => {
        const fetchData = async () => {
            const fetchedMovies = await apiConnector.getMovies();
            setMovies(fetchedMovies);
        }

        fetchData();
    }, []);

    return (
        <>
            <Container className="container-style">
                <table className="ui inverted table">
                    <thead style={{ textAlign: 'center' }}>
                        <tr>
                            <th>Id</th>
                            <th>Title</th>
                            <th>Description</th>
                            <th>CreateDate</th>
                            <th>Category</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {movies.length !== 0 && (
                            movies.map((movie, index) => (
                                <MovieTableItem key={index} movie={movie} />
                            ))
                        )}
                    </tbody>
                </table>
                <Button sx={{ float: 'right' }} type="button" variant="contained" color="success">Create Movie</Button>
            </Container>
        </>
    );
}