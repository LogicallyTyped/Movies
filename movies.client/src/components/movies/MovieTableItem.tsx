import { Button } from "@mui/material";
import { NavLink } from "react-router-dom";
import apiConnector from "../../api/apiConnector";
import type { MovieDto } from "../../models/movieDto";

interface Props {
    movie: MovieDto;
}

export default function MovieTableItem({movie}: Props) {
    return (
        <>
            <tr className="center aligned">
                <td data-label="Id">{movie.id}</td>
                <td data-label="Title">{movie.title}</td>
                <td data-label="Description">{movie.description}</td>
                <td data-label="CreatedDate">{movie.createdDate}</td>
                <td data-label="Category">{movie.category}</td>
                <td data-label="Action">
                    <NavLink to={`/editMovie/${movie.id}`}>
                        <Button color="warning" type="submit">Edit</Button>
                    </NavLink>
                    <NavLink to={`/deleteMovie/${movie.id}`}>
                        <Button color="error" type="button" onClick={async () => {
                            await apiConnector.deleteMovie(movie.id!);
                            window.location.reload();
                        }}>Delete</Button>
                    </NavLink>
                </td>
            </tr>
        </>
    );
}