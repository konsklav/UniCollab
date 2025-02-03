import { NotificationDto } from "./Notifications.types";

export default function NotificationDisplay({notification}: {notification: NotificationDto}) {
    return (
        <div className="d-flex flex-column">
            <div className="fw-bold p-1 border-bottom">
                {notification.header}
            </div>
            <div className="p-2 flex-grow-1">
                {notification.message}
            </div>
        </div>
    )
}