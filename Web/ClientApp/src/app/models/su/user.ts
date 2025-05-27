import { EntityBase } from "../entityBase";
import { UserProfile } from "./userProfile";

export class User extends EntityBase {
    userId: number;
    userName: string;
    passwordPolicyCode: string;
    passwordHash: string;
    defaultLang?: string;
    lockoutEnabled?: boolean;
    forceChangePassword?: boolean;
    startEffectiveDate?: Date;
    endEffectiveDate?: Date;
    lastChangePassword?: Date;
    accessFailedCount?: number;
    concurrencyStamp: string;
    securityStamp: string;
    email: string;
    phoneNumber: string;
    firstname: string;
    lastname: string;
    normalizedUserName: string;
    lockoutEnd: Date;
    active?: boolean;
    firstnameTh: string;
    lastnameTh: string;
    ripple?: boolean;
    inputstyle: string;
    menumode: string;
    colorscheme: string;
    color: string;
    scale: string;
    userProfiles: UserProfile[]

    constructor(){
        super()
        this.userProfiles = []
    }
}