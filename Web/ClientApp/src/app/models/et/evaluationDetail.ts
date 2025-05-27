import { Guid } from "guid-typescript";
import { EntityBase } from "../entityBase";

export class EvaluationDetail extends EntityBase {
    id: Guid;
    evaluationId: Guid;
    subject: string;
    subjectId: string;
    subjectGroup: string;
    descriptions: string;
    maxScore: number;
    score?: number;
}