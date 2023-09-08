import { FormGroup } from '@angular/forms';

export class FormsErrorHandler {

    constructor(private readonly _form: FormGroup) {}

    public showOn(controlName: string, validatorName: string) {
        const path = `controls.${controlName}.errors.${validatorName}`;
        return !this._form.valid && this._deepGet(this._form, path) ? true : false;
    }

    private _deepGet(from: any, path: any) {
        try {
            for (var i = 0, path = path.split('.'), len = path.length; i < len; i++) {
                from = from[path[i]];
            }
            return from;
        } catch (e) {
            return null;
        }
    }
}
