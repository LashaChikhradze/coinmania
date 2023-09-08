import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
    selector: 'app-root',
    templateUrl: './bootstrap.component.html',
    styleUrls: ['./bootstrap.component.scss'],
})
export class BootstrapComponent implements OnInit{
    title = 'CoinMania';

    constructor(private _authService: AuthService) {}

    ngOnInit(): void {
        this._authService.autoLogin();
    }
}
