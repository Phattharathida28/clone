import { EntityBase } from "../entityBase";
import { ProgramLabel } from "./programLabel";

export class Program extends EntityBase {
    programCode: string
    programName: string;
    programPath: string
    systemCode: string
    moduleCode: string
    programLabels: ProgramLabel[]

    constructor() {
        super();
        this.programLabels = []
    }
}