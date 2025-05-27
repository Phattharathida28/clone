import { EntityBase } from "../entityBase";
import { MenuLabel } from "./menuLabel";

export class Menu extends EntityBase {
    menuCode: string;
    programCode: string;
    mainMenu: string;
    systemCode: string;
    icon: string;
    active: boolean;
    menuLabels: MenuLabel[]

    constructor() {
        super();
        this.menuLabels = []
    }
}