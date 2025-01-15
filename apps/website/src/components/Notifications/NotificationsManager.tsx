import { useEffect } from "react";
import SignalRService from "../../services/signalRService";
import { useNotificationStore } from "../../notifications/notificationStore";
import { NotificationContract } from "./Notifications.types";
import NotificationDisplay from "./Notification";
import { ToastType } from "../Toasts/Toast.types";

export default function NotificationsManager() {
    const publish = useNotificationStore(state => state.publish)

    useEffect(() => {
        const signalR = new SignalRService('notifications')
        
        signalR.startConnection()
        
        signalR.on('GetNotification', (notification: NotificationContract) => {
            const toast: ToastType = {content: <NotificationDisplay notification={notification}/>} 
            publish('toast', toast)
        })
        
        return () => signalR.stopConnection()
    }, [])

    return <></>
}