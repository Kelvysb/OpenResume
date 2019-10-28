import { Field } from './field.model';

export class Block {
    constructor(public Id: number,
                public Name: string,
                public Description: string,
                public ItemOrder: number,
                public UserId: number,
                public ResumeId: number,
                public BlockType: string,
                public Title: string,
                public Content: string,
                public Fields: Field[]
    ) { }
}
