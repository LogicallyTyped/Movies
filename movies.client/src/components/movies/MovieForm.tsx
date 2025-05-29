import { Box, Button, Paper, Stack, TextField } from "@mui/material";
import { useEffect, useState, type ChangeEvent } from "react";
import { useNavigate, useParams } from "react-router-dom";
import apiConnector from "../../api/apiConnector";
import type { MovieDto } from "../../models/movieDto";

export default function MovieForm() {

    const { id } = useParams();
    const navigate = useNavigate();

    const [movie, setMovie] = useState<MovieDto>({
        id: undefined,
        title: '',
        description: '',
        createdDate: undefined,
        category: ''
    })

    useEffect(() => {
        if (id) {
            apiConnector.getMovieById(id).then(movie => setMovie(movie!))
        }
    }, [id]);

    function handleSubmit() {
        if (!movie.id) {
            apiConnector.createMovie(movie).then(() => navigate('/'));
        } else {
            apiConnector.editMovie(movie).then(() => navigate('/'));
        }
    }

    const handleChange = (e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target;
        setMovie(prev => ({ ...prev, [name]: value }));
    };

    return (
        <Paper
            elevation={3}
            sx={{
                p: 4,
                bgcolor: 'primary.dark',
                color: 'primary.contrastText',
            }}
        >
            <Box
                component="form"
                onSubmit={handleSubmit}
                autoComplete="off"
                sx={{ width: '100%' }}
            >
                <Stack spacing={3}>
                    <TextField
                        fullWidth
                        variant="outlined"
                        label="Title"
                        name="title"
                        value={movie.title}
                        onChange={handleChange}
                        sx={{
                            '& .MuiInputLabel-root': {
                                color: 'inherit',
                            },
                            '& .MuiOutlinedInput-input': {
                                color: 'inherit',
                            },
                            '& .MuiOutlinedInput-notchedOutline': {
                                borderColor: 'inherit',
                            },
                            '&:hover .MuiOutlinedInput-notchedOutline': {
                                borderColor: 'inherit',
                            },
                            '&.Mui-focused .MuiOutlinedInput-notchedOutline': {
                                borderColor: 'inherit',
                            },
                        }}
                    />

                    <TextField
                        fullWidth
                        variant="outlined"
                        label="Description"
                        name="description"
                        value={movie.description}
                        multiline
                        rows={4}
                        onChange={handleChange}
                        sx={{
                            '& .MuiInputLabel-root': {
                                color: 'inherit',
                            },
                            '& .MuiOutlinedInput-input': {
                                color: 'inherit',
                            },
                            '& .MuiOutlinedInput-notchedOutline': {
                                borderColor: 'inherit',
                            },
                            '&:hover .MuiOutlinedInput-notchedOutline': {
                                borderColor: 'inherit',
                            },
                            '&.Mui-focused .MuiOutlinedInput-notchedOutline': {
                                borderColor: 'inherit',
                            },
                        }}
                    />

                    <TextField
                        fullWidth
                        variant="outlined"
                        label="Category"
                        name="category"
                        value={movie.category}
                        onChange={handleChange}
                        sx={{
                            '& .MuiInputLabel-root': {
                                color: 'inherit',
                            },
                            '& .MuiOutlinedInput-input': {
                                color: 'inherit',
                            },
                            '& .MuiOutlinedInput-notchedOutline': {
                                borderColor: 'inherit',
                            },
                            '&:hover .MuiOutlinedInput-notchedOutline': {
                                borderColor: 'inherit',
                            },
                            '&.Mui-focused .MuiOutlinedInput-notchedOutline': {
                                borderColor: 'inherit',
                            },
                        }}
                    />

                    <Box display="flex" justifyContent="flex-end" gap={2}>
                        <Button
                            variant="contained"
                            color="secondary"
                            onClick={() => navigate('/')}
                        >
                            Cancel
                        </Button>

                        <Button
                            type="submit"
                            variant="contained"
                            color="success"
                        >
                            Submit
                        </Button>
                    </Box>
                </Stack>
            </Box>
        </Paper>
    );
}