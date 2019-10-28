import { Block } from './block.model';

export class Resume {
    constructor(public Id: number,
                public Name: string,
                public Description: string,
                public ItemOrder: number,
                public UserId: string,
                public Link: string,
                public Token: string,
                public Language: string,
                public Template: string,
                public CreatedDate: string,
                public UpdatedDate: string,
                public AccessLevel: string,
                public Blocks: Block[]
    ) { }
}


