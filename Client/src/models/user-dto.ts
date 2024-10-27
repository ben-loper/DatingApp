export class UserDto {
    jwt: string;
    nameId: string;

    constructor(jwt: string, nameId: string){
        this.jwt = jwt;
        this.nameId = nameId;
    }
}
