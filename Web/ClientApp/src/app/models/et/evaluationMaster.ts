import { EntityBase } from "../entityBase";
import { EvaluationMasterDetail } from "./evaluationMasterDetail";

export class EvaluationMaster extends EntityBase {
    id: string;
    positionCode: string;
    active?: boolean;
    evaluationMasterDetails: EvaluationMasterDetail[];

    constructor() {
        super();
        this.evaluationMasterDetails = [] as EvaluationMasterDetail[]
    }
}