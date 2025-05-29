import type { AxiosResponse } from "axios";
import axios from "axios";
import { API_BASE_URL } from "../../config.ts";
import type { GetMovieByIdResponse } from "../models/getMovieByIdResponse";
import type { GetMoviesResponse } from "../models/getMoviesResponse";
import type { MovieDto } from "../models/movieDto";

const apiConnector = {
    getMovies: async (): Promise<MovieDto[]> => {
        try {
            const response: AxiosResponse<GetMoviesResponse> =
                await axios.get(`${API_BASE_URL}/movies`);
            const movies = response.data.movieDtos.map(movie => ({
                ...movie,
                createdDate: movie.createdDate?.slice(0, 10) ?? ""
            }));
            return movies;
        } catch (error) {
            console.log("Error fetching movies:", error);
            throw error;
        }
    },

    createMovie: async (movie: MovieDto): Promise<void> => {
        try {
            await axios.post<number>(`${API_BASE_URL}/movies`, movie);
        } catch (error) {
            console.log("Error creating movie:", error);
            throw error;
        }
    },

    editMovie: async (movie: MovieDto): Promise<void> => {
        try {
            await axios.put<number>(`${API_BASE_URL}/movies`, movie);
        } catch (error) {
            console.log("Error updating movie:", error);
            throw error;
        }
    },

    deleteMovie: async (movieId: number): Promise<void> => {
        try {
            await axios.delete(`${API_BASE_URL}/movies/${movieId}`);
        } catch (error) {
            console.log("Error deleting movie:", error);
            throw error;
        }
    },

    getMovieById: async (movieId: string): Promise<MovieDto | undefined> => {
        try {
            const response =
                await axios.get<GetMovieByIdResponse>(`${API_BASE_URL}/movies/${movieId}`);
            return response.data.movieDto;
        } catch (error) {
            console.log("Error fetching movie by ID:", error);
            throw error;
        }
    }
}

export default apiConnector;