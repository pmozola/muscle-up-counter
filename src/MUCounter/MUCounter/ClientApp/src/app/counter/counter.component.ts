import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent {
  public currentCount = 0;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.http.get<number>(baseUrl + 'api/Repetition/total').subscribe(result => {
      this.currentCount = result;
    }, error => console.error(error));
  }

  public incrementCounter() {
    this.http.post(this.baseUrl + 'api/Repetition/','').subscribe(
      () => this.currentCount++);
  }

  public decrementCounter() {
    this.http.delete(this.baseUrl + 'api/Repetition/').subscribe(
      () => this.currentCount--);
  }
}
