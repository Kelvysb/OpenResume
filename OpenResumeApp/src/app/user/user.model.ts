export class User {
    constructor(public Id: number,
                public Name: string,
                public Description: string,
                public ItemOrder: number,
                public Login: string,
                public Email: string,
                public LastName: string,
                public PasswordHash: string,
                public EmailConfirmed: boolean,
                public ResetPassword: boolean,
                public ResetToken: string,
                public ConfirmationToken: string,
                public CreatedDate: Date,
                public UpdatedDate: Date,
                public LastActivity: Date,
                public Token: string
    ) { }
}


