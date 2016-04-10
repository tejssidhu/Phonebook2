import {inject} from "aurelia-framework";
import {MovieData} from "./movieData";

@inject(MovieData)
export class Details {

    constructor(movieData) {
        this.movieData = movieData;
    }

    activate(params) {
        return this.movieData
                   .getById(params.id)
                   .then(movie => this.movie = movie);
    }
}