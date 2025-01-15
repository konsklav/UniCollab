import { NotificationContract } from "./Notifications.types";

export default function NotificationDisplay({notification}: {notification: NotificationContract}) {
    return (
        <div className="p-2">
            {notification.message}
        </div>
    )
}