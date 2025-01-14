import { createContext, useContext, useState } from "react";
import { ChildrenProps } from "../common/common.types";
import { NotificationContextType, NotificationEventCallback } from "./Notifications.types";

type NotificationListeners = Map<string, Set<NotificationEventCallback>>

const NotificationContext = createContext<NotificationContextType | undefined>(undefined);

export function NotificationProvider({children}: ChildrenProps) {
    const [listeners, setListeners] = useState<NotificationListeners>(new Map())

    const publish = (notificationType: string, payload: any) => {
        const eventListeners = listeners.get(notificationType)
        if (!eventListeners)
        {
            console.log(`No '${notificationType}' event listeners.`)
            return;
        }

        eventListeners.forEach(listener => listener.callback({payload}))
    }

    const subscribe = (notifactionType: string, callback: NotificationEventCallback) => {
        setListeners(listeners => {
            const newListeners = new Map(listeners)
            if (!newListeners.has(notifactionType)) {
                newListeners.set(notifactionType, new Set())
            }
            newListeners.get(notifactionType)!.add(callback)
            return newListeners
        })
    }

    return (
        <NotificationContext.Provider value={{publish, subscribe}}>
            {children}
        </NotificationContext.Provider>
    )
}

export const useNotifications = () => {
    const context = useContext(NotificationContext)
    if (!context) {
        throw new Error('A NotificationProvider component is required to wrap any consumer of useNotifications().')
    }

    return context;
}