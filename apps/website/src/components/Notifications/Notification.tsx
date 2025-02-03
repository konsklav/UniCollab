import { NotificationDto } from "./Notifications.types";

export default function NotificationDisplay({notification}: {notification: NotificationDto}) {
    return (
        <div className="p-2">
            {notification.message}
        </div>
    )
}