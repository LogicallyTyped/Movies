import { Button } from "@mui/material";
import type { MovieDto } from "../../models/movieDto";
import apiConnector from "../../api/apiConnector";

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
                    <Button color="warning" type="submit">Edit</Button>
                    <Button color="error" type="button" onClick={async () => {
                        await apiConnector.deleteMovie(movie.id!);
                        window.location.reload();
                    }}>Delete</Button>
                </td>
            </tr>
        </>
    );
}