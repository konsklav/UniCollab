export interface NotificationDto {
    type: string,
    header: string,
    message: string,
    metadata: readonly any[]
}
