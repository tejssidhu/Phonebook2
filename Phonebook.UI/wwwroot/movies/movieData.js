import {inject} from "aurelia-framework";
import {HttpClient} from "aurelia-http-client";

//let baseUrl = "api/movies";
let baseUrl = "movies.json";
let singleUrl = "movie.json";

@inject(HttpClient)
export class MovieData {

    constructor(httpClient) {
        this.http = httpClient;
    }

    getById(id) {
        return this.http.get(singleUrl)
                        .then(response => {
                            return response.content;
                        });
    }

    getAll() {
        return this.http.get(baseUrl)
                        .then(response => {
                            return response.content;
                        });
    }

    save(movie) {
        //aurelia had bug at time of video
        //return this.http.put(baseUrl, movie);

        //this is the work around
        //var request = this.http.createRequery();
        //request.asPut()
        //       .withUrl(baseUrl)
        //       .withHeader("Accept", "application/json")
        //       .withHeader("Content-Type", "application/json")
        //       .withContent(movie)

        //return request.send().then(response => response.content);

        console.log("movie saved");

        return this.http.get(singleUrl)
                        .then(response => {
                            return response.content;
                        });
        
    }
}