//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v14.0.8.0 (NJsonSchema v11.0.1.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming

import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable()
export class Client {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl ?? "";
    }

    /**
     * @return OK
     */
    astronautDutiesGET(username: string): Observable<AstronautDutiesDto> {
        let url_ = this.baseUrl + "/AstronautDuties/{username}";
        if (username === undefined || username === null)
            throw new Error("The parameter 'username' must be defined.");
        url_ = url_.replace("{username}", encodeURIComponent("" + username));
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "text/plain"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processAstronautDutiesGET(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processAstronautDutiesGET(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<AstronautDutiesDto>;
                }
            } else
                return _observableThrow(response_) as any as Observable<AstronautDutiesDto>;
        }));
    }

    protected processAstronautDutiesGET(response: HttpResponseBase): Observable<AstronautDutiesDto> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = AstronautDutiesDto.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null as any);
    }

    /**
     * @param body (optional) 
     * @return OK
     */
    astronautDutiesPOST(body: CreateAstronautDutyRequest | undefined): Observable<void> {
        let url_ = this.baseUrl + "/AstronautDuties";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(body);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processAstronautDutiesPOST(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processAstronautDutiesPOST(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<void>;
                }
            } else
                return _observableThrow(response_) as any as Observable<void>;
        }));
    }

    protected processAstronautDutiesPOST(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return _observableOf(null as any);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null as any);
    }

    /**
     * @return OK
     */
    peopleGET(): Observable<void> {
        let url_ = this.baseUrl + "/People";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processPeopleGET(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processPeopleGET(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<void>;
                }
            } else
                return _observableThrow(response_) as any as Observable<void>;
        }));
    }

    protected processPeopleGET(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return _observableOf(null as any);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null as any);
    }

    /**
     * @param body (optional) 
     * @return OK
     */
    peoplePOST(body: CreatePersonDto | undefined): Observable<void> {
        let url_ = this.baseUrl + "/People";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(body);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processPeoplePOST(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processPeoplePOST(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<void>;
                }
            } else
                return _observableThrow(response_) as any as Observable<void>;
        }));
    }

    protected processPeoplePOST(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return _observableOf(null as any);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null as any);
    }

    /**
     * @return OK
     */
    peopleGET2(username: string): Observable<void> {
        let url_ = this.baseUrl + "/People/{username}";
        if (username === undefined || username === null)
            throw new Error("The parameter 'username' must be defined.");
        url_ = url_.replace("{username}", encodeURIComponent("" + username));
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processPeopleGET2(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processPeopleGET2(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<void>;
                }
            } else
                return _observableThrow(response_) as any as Observable<void>;
        }));
    }

    protected processPeopleGET2(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return _observableOf(null as any);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap((_responseText: string) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf(null as any);
    }
}

export class AstronautDutiesDto implements IAstronautDutiesDto {
    person?: PersonAstronaut;
    duties?: AstronautDutyDto[] | undefined;

    constructor(data?: IAstronautDutiesDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.person = _data["person"] ? PersonAstronaut.fromJS(_data["person"]) : <any>undefined;
            if (Array.isArray(_data["duties"])) {
                this.duties = [] as any;
                for (let item of _data["duties"])
                    this.duties!.push(AstronautDutyDto.fromJS(item));
            }
        }
    }

    static fromJS(data: any): AstronautDutiesDto {
        data = typeof data === 'object' ? data : {};
        let result = new AstronautDutiesDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["person"] = this.person ? this.person.toJSON() : <any>undefined;
        if (Array.isArray(this.duties)) {
            data["duties"] = [];
            for (let item of this.duties)
                data["duties"].push(item.toJSON());
        }
        return data;
    }
}

export interface IAstronautDutiesDto {
    person?: PersonAstronaut;
    duties?: AstronautDutyDto[] | undefined;
}

export class AstronautDutyDto implements IAstronautDutyDto {
    rank?: Rank;
    title?: string | undefined;
    startDate?: Date;
    endDate?: Date | undefined;

    constructor(data?: IAstronautDutyDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.rank = _data["rank"];
            this.title = _data["title"];
            this.startDate = _data["startDate"] ? new Date(_data["startDate"].toString()) : <any>undefined;
            this.endDate = _data["endDate"] ? new Date(_data["endDate"].toString()) : <any>undefined;
        }
    }

    static fromJS(data: any): AstronautDutyDto {
        data = typeof data === 'object' ? data : {};
        let result = new AstronautDutyDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["rank"] = this.rank;
        data["title"] = this.title;
        data["startDate"] = this.startDate ? this.startDate.toISOString() : <any>undefined;
        data["endDate"] = this.endDate ? this.endDate.toISOString() : <any>undefined;
        return data;
    }
}

export interface IAstronautDutyDto {
    rank?: Rank;
    title?: string | undefined;
    startDate?: Date;
    endDate?: Date | undefined;
}

export class CreateAstronautDutyRequest implements ICreateAstronautDutyRequest {
    username!: string | undefined;
    rank!: Rank;
    title!: string | undefined;
    startDate?: Date;

    constructor(data?: ICreateAstronautDutyRequest) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.username = _data["username"];
            this.rank = _data["rank"];
            this.title = _data["title"];
            this.startDate = _data["startDate"] ? new Date(_data["startDate"].toString()) : <any>undefined;
        }
    }

    static fromJS(data: any): CreateAstronautDutyRequest {
        data = typeof data === 'object' ? data : {};
        let result = new CreateAstronautDutyRequest();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["username"] = this.username;
        data["rank"] = this.rank;
        data["title"] = this.title;
        data["startDate"] = this.startDate ? this.startDate.toISOString() : <any>undefined;
        return data;
    }
}

export interface ICreateAstronautDutyRequest {
    username: string | undefined;
    rank: Rank;
    title: string | undefined;
    startDate?: Date;
}

export class CreatePersonDto implements ICreatePersonDto {
    username!: string;
    displayName?: string | undefined;
    dateOfBirth?: Date | undefined;

    constructor(data?: ICreatePersonDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.username = _data["username"];
            this.displayName = _data["displayName"];
            this.dateOfBirth = _data["dateOfBirth"] ? new Date(_data["dateOfBirth"].toString()) : <any>undefined;
        }
    }

    static fromJS(data: any): CreatePersonDto {
        data = typeof data === 'object' ? data : {};
        let result = new CreatePersonDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["username"] = this.username;
        data["displayName"] = this.displayName;
        data["dateOfBirth"] = this.dateOfBirth ? this.dateOfBirth.toISOString() : <any>undefined;
        return data;
    }
}

export interface ICreatePersonDto {
    username: string;
    displayName?: string | undefined;
    dateOfBirth?: Date | undefined;
}

export class PersonAstronaut implements IPersonAstronaut {
    personId?: number;
    username?: string | undefined;
    displayName?: string | undefined;
    dateOfBirth?: Date | undefined;
    rank?: string | undefined;
    dutyTitle?: string | undefined;
    careerStartDate?: Date | undefined;
    careerEndDate?: Date | undefined;

    constructor(data?: IPersonAstronaut) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.personId = _data["personId"];
            this.username = _data["username"];
            this.displayName = _data["displayName"];
            this.dateOfBirth = _data["dateOfBirth"] ? new Date(_data["dateOfBirth"].toString()) : <any>undefined;
            this.rank = _data["rank"];
            this.dutyTitle = _data["dutyTitle"];
            this.careerStartDate = _data["careerStartDate"] ? new Date(_data["careerStartDate"].toString()) : <any>undefined;
            this.careerEndDate = _data["careerEndDate"] ? new Date(_data["careerEndDate"].toString()) : <any>undefined;
        }
    }

    static fromJS(data: any): PersonAstronaut {
        data = typeof data === 'object' ? data : {};
        let result = new PersonAstronaut();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["personId"] = this.personId;
        data["username"] = this.username;
        data["displayName"] = this.displayName;
        data["dateOfBirth"] = this.dateOfBirth ? this.dateOfBirth.toISOString() : <any>undefined;
        data["rank"] = this.rank;
        data["dutyTitle"] = this.dutyTitle;
        data["careerStartDate"] = this.careerStartDate ? this.careerStartDate.toISOString() : <any>undefined;
        data["careerEndDate"] = this.careerEndDate ? this.careerEndDate.toISOString() : <any>undefined;
        return data;
    }
}

export interface IPersonAstronaut {
    personId?: number;
    username?: string | undefined;
    displayName?: string | undefined;
    dateOfBirth?: Date | undefined;
    rank?: string | undefined;
    dutyTitle?: string | undefined;
    careerStartDate?: Date | undefined;
    careerEndDate?: Date | undefined;
}

export enum Rank {
    _0 = 0,
    _1 = 1,
    _2 = 2,
    _3 = 3,
    _4 = 4,
    _5 = 5,
    _6 = 6,
    _7 = 7,
    _8 = 8,
    _9 = 9,
    _10 = 10,
    _11 = 11,
    _12 = 12,
    _13 = 13,
    _14 = 14,
    _15 = 15,
    _16 = 16,
    _17 = 17,
    _18 = 18,
    _19 = 19,
}

export class ApiException extends Error {
    override message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isApiException = true;

    static isApiException(obj: any): obj is ApiException {
        return obj.isApiException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
    if (result !== null && result !== undefined)
        return _observableThrow(result);
    else
        return _observableThrow(new ApiException(message, status, response, headers, null));
}

function blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
        if (!blob) {
            observer.next("");
            observer.complete();
        } else {
            let reader = new FileReader();
            reader.onload = event => {
                observer.next((event.target as any).result);
                observer.complete();
            };
            reader.readAsText(blob);
        }
    });
}