import { useEffect } from "react";
import SignalRService from "../../services/signalRService";
import { useNotificationStore } from "../../notifications/notificationStore";
import { NotificationContract } from "./Notifications.types";
import NotificationDisplay from "./Notification";

export default function NotificationsManager() {
    const publish = useNotificationStore(state => state.publish)

    useEffect(() => {
        const signalR = new SignalRService('notifications')
        
        signalR.startConnection()
        
        signalR.on('GetNotification', (notification: NotificationContract) => {
            publish('toast', <NotificationDisplay notification={notification}/>)
        })
        
        return () => signalR.stopConnection()
    }, [])
}