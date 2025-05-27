import { Guid } from "guid-typescript";
import { EntityBase } from "../entityBase";

export class SkillMatrix extends EntityBase {
    id: Guid;
    evaluationId: string;
    subject: string;
    subjectId: string;
    subjectGroup: string;
    descriptions: string;
    maxScore?: number;
    score?: number;
}