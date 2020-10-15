import { Component } from '@angular/core';
import { HttpHeaders, HttpClient, HttpParams } from '@angular/common/http';
import { NgModel } from '@angular/forms';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
    styles: []
})

export class AppComponent {
    curencies: any;
    date: any;
    cities: any;
    selectedCity1: any;

    constructor(private http: HttpClient) {
    }

    ngOnInit() {
        this.http.get('https://localhost:44346/list-in-year?date=2020').subscribe((res) => {
            this.curencies = res;
        });

        this.http.get('https://localhost:44346/currency').subscribe((result) => {

            var currenciesName = [];
            for (let item in result)
                currenciesName.push({ name: result[item] });

            this.cities = currenciesName;
        });
    }

    /**
     * 
     * @param date11 
     * @param selectedCity1 
     */
    getData(date11: NgModel, selectedCity1: NgModel,) {

        let Params = new HttpParams();
        let revertDate = this.revertDate(this.date);
        if (this.date != undefined) {
            if (this.selectedCity1 != undefined || this.selectedCity1 != null) {
                Params = Params.append('date', revertDate);
                Params = Params.append('currency', this.selectedCity1.name);
            } else
                Params = Params.append('date', revertDate);

            let header: HttpHeaders = new HttpHeaders();
            header = header.append('Content-Type', 'text/plain');

            this.http.get('https://localhost:44346/api/ExchangeRates', { params: Params, headers: header }).subscribe((res) => {
                if (Object.keys(res).length > 0)
                    this.curencies = res;
                else
                    alert("На эту дату данных нет! Выберите другую дату")
            });
        }
        else {
            this.http.get('https://localhost:44346/list-in-year?date=2020').subscribe((res) => {
                this.curencies = res;
            });
        }
    }

    /**
     * Переворачиваем дату.
     * @param date Дата
     */
    revertDate(date) {
        let arrDate = new Date(date).toLocaleDateString().split('.');
        let revertDate = arrDate[1] + "." + arrDate[0] + "." + arrDate[2];
        return revertDate;
    }
}