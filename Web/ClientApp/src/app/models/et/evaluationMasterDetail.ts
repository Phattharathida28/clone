import { EntityBase } from "../entityBase";

export class EvaluationMasterDetail extends EntityBase {
    id: string;
    evaluationMasterId: string;
    subject: string;
    subjectGroup: string;
    descriptions: string;
    maxScore: number;
}