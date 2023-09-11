import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  apiKey = '4b286d1a';
  searchTerm = '';
  movies: any[] = [];

  constructor(private http: HttpClient) {}

  searchMovies() {
    if (this.searchTerm === '') {
      this.movies = [];
      return;
    }

    this.http
      .get(`https://www.omdbapi.com/?apikey=${this.apiKey}&s=${this.searchTerm}`)
      .pipe(
        debounceTime(300),
        distinctUntilChanged(),
        switchMap((data: any) => {
          if (data.Response === 'True') {
            return Promise.all(data.Search.slice(0, 3).map((movie: any) => this.getMovieDetails(movie.imdbID)));
          } else {
            return [];
          }
        })
      )
      .subscribe((movies: any[]) => {
        this.movies = movies;
      });
  }

  getMovieDetails(imdbID: string) {
    return this.http.get(`https://www.omdbapi.com/?apikey=${this.apiKey}&i=${imdbID}`).toPromise();
  }

  openMovieDetails(imdbID: string) {
    window.open(`https://www.imdb.com/title/${imdbID}`);
  }
}
