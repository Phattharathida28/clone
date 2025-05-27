import { Guid } from "guid-typescript";
import { EntityBase } from "../entityBase";
import { EvaluationDetail } from "./evaluationDetail";
import { SkillMatrix } from "./skillMatrix";

export class Evaluation extends EntityBase {
    id: Guid;
    employeeCode: string;
    startDate?: Date;
    endDate?: Date;
    status?: Guid;
    skillMatrixId: Guid;
    skillMatrixMasterId: Guid;
    evaluationDetails: EvaluationDetail[] = []
    skillMatrices: SkillMatrix[] = []
}