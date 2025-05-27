import { EntityBase } from "../entityBase";
import { ActivityLog } from "./activityLog";

export class ActivityType extends EntityBase {
    activityTypeCode: string;
    activityTypeName: string;
    activityGroupCode: string;
    logTemplate: string;
    active: boolean;
    activityLogs: ActivityLog[]

    constructor(){
        super();
        this.activityLogs = []
    }
}