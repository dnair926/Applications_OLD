export interface IAlert {
    message?: string;
    alertType?: AlertType;
}

export enum AlertType {
    Error,
    Success,
    Invalid
}