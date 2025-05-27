import { EntityBase } from "../entityBase";

export class ActivityLog extends EntityBase {
    id: number;
    activityTypeCode: string;
    logMessage: string;
    loggedBy: string;
    loggedDate: Date;
    active: boolean;
}