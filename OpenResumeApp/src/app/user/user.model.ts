export interface User {
    Id?: number;
    Name?: string;
    Description?: string;
    ItemOrder?: number;
    Login?: string;
    Email?: string;
    LastName?: string;
    PasswordHash?: string;
    EmailConfirmed?: boolean;
    ResetPassword?: boolean;
    ResetToken?: string;
    ConfirmationToken?: string;
    CreatedDate?: Date;
    UpdatedDate?: Date;
    LastActivity?: Date;
    Token?: string;
}


