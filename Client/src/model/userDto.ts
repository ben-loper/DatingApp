export class UserDto {
    id: number;
    userName: string;

    constructor(id: number, userName: string){
        this.id = id;
        this.userName = userName;
    }
}