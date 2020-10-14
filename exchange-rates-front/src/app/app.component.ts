import { Component } from '@angular/core';
import {MenuItem} from 'primeng/api';
import {SelectItem} from 'primeng/api';
import {SelectItemGroup} from 'primeng/api';
import {trigger,state,style,transition,animate} from '@angular/animations';
import { HttpHeaders, HttpClient, HttpParams } from '@angular/common/http';
import { NgModel} from '@angular/forms';

interface City {
    name: string,
    code: string
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  styles:[],
    animations: [
        trigger('animation', [
            state('visible', style({
                transform: 'translateX(0)',
                opacity: 1
            })),
            transition('void => *', [
                style({transform: 'translateX(50%)', opacity: 0}),
                animate('300ms ease-out')
            ]),
            transition('* => void', [
                animate(('250ms ease-in'), style({
                    height: 0,
                    opacity: 0,
                    transform: 'translateX(50%)'
                }))
            ])
        ])
    ]
})
export class AppComponent { 
    curencies: any;
    date11: any;
    cities: any;
    selectedCity1: any;
    items: SelectItem[];
    item: string;

    constructor(private http: HttpClient) {
        this.items = [];
        for (let i = 0; i < 10000; i++) {
            this.items.push({label: 'Item ' + i, value: 'Item ' + i});
        }

        
       
    }
    ngOnInit() {     
         this.http.get('https://localhost:44346/list-in-year?date=2020').subscribe((res)=>{
            this.curencies = res;
        });

        this.http.get('https://localhost:44346/currency').subscribe((result)=>{
           
            var currenciesName = [] ;            
            for (let item in result) 
                currenciesName.push({name:result[item]});   

            this.cities = currenciesName;
        });

    }
    getData(date11:NgModel, selectedCity1: NgModel, ){
        
        var arrDate =  new Date(this.date11).toLocaleDateString().split('.');
        var revertDate = arrDate[1]+"."+arrDate[0]+"."+arrDate[2];   
         
        let Params = new HttpParams();
        if (this.selectedCity1 != undefined || this.selectedCity1 != null){           
            Params = Params.append('date', revertDate);
            Params = Params.append('currency', this.selectedCity1.name);
        }else
            Params = Params.append('date', revertDate);

        let header: HttpHeaders = new HttpHeaders();
        header = header.append('Content-Type', 'text/plain');

        this.http.get('https://localhost:44346/api/ExchangeRates',{ params: Params, headers: header }).subscribe((res)=>{
            this.curencies = res;
        });
    }
    

}