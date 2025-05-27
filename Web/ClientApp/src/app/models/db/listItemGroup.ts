import { EntityBase } from "../entityBase";
import { ListItem } from "./listItem";

export class ListItemGroup extends EntityBase {
    listItemGroupCode: string
    listItemGroupName: string
    listItem: ListItem[] = []
}