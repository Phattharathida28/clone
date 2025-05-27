import { EntityBase } from "../entityBase";

export class Content extends EntityBase {
    id: number;
    name: string;
    path: string | ArrayBuffer;
    reference?: boolean;
    validatePath: boolean;
    container: string;
    size: number;
}