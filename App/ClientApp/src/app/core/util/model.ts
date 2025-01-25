export class User {
    userName: string = '';
    password: string = '';
    token: string;
    dateExpireted: Date;
}

export interface ReturnDTO{
    isSuccess: boolean;
    deMessage: string
    resultObject: any;
}

export class LogIntegrationEntity{
    idLogIntegration: number;
    idProject: number;
    idMailing: number;
    idLogIntegrationType: number;
    dtRegister: Date;
    nuVersion: string;
    deMethod: string;
    deUrl: string;
    deContent: string;
    deResult: string;
    deMessage: string;
    deExceptionMessage: string;
    deStackTrace: string;
    isSuccess: boolean;
    isActive: boolean;
    nmLogIntegrationType: string;
}