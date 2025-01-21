import { useEffect } from "react";
import SignalRService from "../../services/signalRService";
import { useNotificationStore } from "../../state/notifications/notificationStore";
import { NotificationContract } from "./Notifications.types";
import NotificationDisplay from "./Notification";
import { ToastType } from "../Toasts/Toast.types";
import { useAuth } from "../../state/authentication/authenticationStore";

export default function NotificationsManager() {
    const { user, authentication } = useAuth()
    const publish = useNotificationStore(state => state.publish)

    useEffect(() => {
        if (authentication === 'None' || !user)
        {
            console.log('Aborting SignalR connection attempt. Not authenticated.')
            return;
        }

        console.log('Beginning SignalR connection.')
        const signalR = new SignalRService('notifications', user)
        
        signalR.startConnection()
        
        signalR.on('GetNotification', (notification: NotificationContract) => {
            const toast: ToastType = {content: <NotificationDisplay notification={notification}/>} 
            publish('toast', toast)
        })
        
        return () => signalR.stopConnection()
    }, [user])

    return <></>
}