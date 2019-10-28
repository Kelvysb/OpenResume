export class Field {
    constructor(public Id: number,
                public Name: string,
                public Description: string,
                public ItemOrder: number,
                public UserId: number,
                public ResumeId: number,
                public BlockId: number,
                public FieldType: string,
                public Content: string,
                public InitialDate: Date,
                public FinalDate: Date,
                public Present: boolean
    ) { }
}
