// Represents the response model for fetching movies

import type { MovieDto } from "./movieDto";

export interface GetMoviesResponse {
  movieDtos: MovieDto[];
}