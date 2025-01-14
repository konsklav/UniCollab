export interface NotificationEventCallback {
    callback: (event: NotificationEvent) => void
}

export interface NotificationEvent {
    payload: any
}

export interface NotificationContextType {
    subscribe: (notificationType: string, callback: NotificationEventCallback) => void
    publish: (notificationType: string, payload: any) => void
}

