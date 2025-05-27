import { SelectItem } from "primeng/api";
import { ModalResolve } from "../components/modal/modal.resolver";

export type NotifyPosition = "tl" | "tc" | "tr" | "bl" | "bc" | "br"

export type ModalConfig = {
    content: any;
    data?: any;
    header?: string;
    width?: string;
    size?: number;
    baseZIndex?: number;
    maximizable?: boolean;
    closable?: boolean;
    resolver?: ModalResolve<any>;
    resolverParam?: any;
    position?: string;
}

export enum RowState {
    Normal, Add, Edit, Delete
}

export enum AttachmentType {
    Image = "Image",
    File = "File"
}

export enum Category {
    Defalt = "default"
}

export enum FileType {
    ALL_IMAGE = 'image/*',
    PNG = 'image/png',
    JPEG = 'image/jpeg',
    GIF = 'image/gif',
    SVG = 'image/svg+xml',
    ALL_APPLICATION = 'application/*',
    PDF = 'application/pdf',
    JSON = 'application/json',
    XML = 'application/xml',
    XLSX = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
    XLS = 'application/vnd.ms-excel',
    DOCX = 'application/vnd.openxmlformats-officedocument.wordprocessingml.document',
    DOC = 'application/msword',
    PPTX = 'application/vnd.openxmlformats-officedocument.presentationml.presentation',
    ZIP = 'application/zip',
    PLAIN = 'text/plain',
    HTML = 'text/html',
    MP3 = 'audio/mp3',
    WAV = 'audio/wav',
    MP4 = 'video/mp4',
    WEBM = 'video/webm',
}

export type Columns = {
    field: string;
    header: string;
}

export type Lang = SelectItem & { pattern: string }