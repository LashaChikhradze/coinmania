export class User {
    constructor(public phone: string, public firstName: string, public lastName: string, public id: string, private _token: string) {}

    get token() {
        return this._token;
    }
}