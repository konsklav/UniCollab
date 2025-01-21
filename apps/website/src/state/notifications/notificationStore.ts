import { create } from "zustand";
import { NotificationEventCallback } from "./Notifications.types";

type UnsubscribeFunction = () => void

type NotificationType = string
type NotificationListeners = Record<NotificationType, Array<NotificationEventCallback>>
type NotificationStore = {
    listeners: NotificationListeners
    publish: (type: NotificationType, payload: any) => void
    subscribe: (type: NotificationType, callback: NotificationEventCallback) => UnsubscribeFunction
}

export const useNotificationStore = create<NotificationStore>((set, get) => ({
    listeners: { },
    publish: (type: NotificationType, payload: any) => {
        const eventListeners = get().listeners[type] || new Map()
        eventListeners.forEach(listener => listener.callback({payload}))
    },
    subscribe: (type: NotificationType, callback: NotificationEventCallback) => {
        set(state => {
            const eventListeners = state.listeners[type] || new Map();
            return {
                listeners: {
                    ...state.listeners,
                    [type]: [...eventListeners, callback]
                }
            }
        })
        
        return () => {
            set(state => {
                const eventListeners = state.listeners[type] 
                if (!eventListeners || eventListeners.length === 0)
                    return state

                return {
                    listeners: {
                        ...state.listeners,
                        [type]: eventListeners.filter(listener => listener !== callback)
                    }
                }
            })
        }
    }
}))