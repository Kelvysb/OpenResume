export interface User {
    id?: number;
    name?: string;
    description?: string;
    itemOrder?: number;
    login?: string;
    email?: string;
    lastName?: string;
    passwordHash?: string;
    emailConfirmed?: boolean;
    resetPassword?: boolean;
    resetToken?: string;
    confirmationToken?: string;
    createdDate?: Date;
    updatedDate?: Date;
    lastActivity?: Date;
    token?: string;
}


