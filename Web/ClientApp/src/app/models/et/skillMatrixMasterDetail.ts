import { EntityBase } from "../entityBase";

export class SkillMatrixMasterDetail extends EntityBase {
    id: string;
    skillMatrixMasterId: string;
    subject: string;
    subjectGroup: string;
    descriptions: string;
    maxScore: number;
}