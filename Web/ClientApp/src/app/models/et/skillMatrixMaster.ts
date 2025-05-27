import { EntityBase } from "../entityBase";
import { SkillMatrixMasterDetail } from "./skillMatrixMasterDetail";

export class SkillMatrixMaster extends EntityBase {
    id: string;
    positionCode: string;
    active?: boolean;
    skillMatrixMasterDetails: SkillMatrixMasterDetail[] = []
}