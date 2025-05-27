import { EntityBase } from "../entityBase";
import { ProfileMenu } from "./profileMenu";

export class Profile extends EntityBase {
    profileCode: string;
    description: string;
    active: boolean;
    profileType: string;
    profileMenus: ProfileMenu[];

    constructor(){
        super()
        this.profileMenus = []
    }
}