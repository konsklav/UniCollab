import { useEffect, useState } from "react"
import { ToastType } from "./Toast.types"

import './toasts.css'
import Toast from "./Toast"
import { useNotificationStore } from "../../state/notifications/notificationStore"
import { generateUniqueId } from "../../utils/uniqueId"

interface ToastInternal {
    id: string
    toast: ToastType
    timeShown: number
}

const toastDuration = 5000
// const toastDisappearDuration = 500

export function ToastContainer() {
    const [toasts, setToasts] = useState<readonly ToastInternal[]>([])
    const subscribe = useNotificationStore(state => state.subscribe)

    useEffect(() => {
        const unsubscribe = subscribe('toast', {
            callback: e => handleShowToast(e.payload)
        })        

        return () => unsubscribe()
    }, [])

    const handleShowToast = (toast: ToastType) => {
        const toastInternal: ToastInternal = {
            id: generateUniqueId(),
            toast: toast,
            timeShown: new Date().getTime() 
        } 

        setTimeout(() => handleRemoveToast(toastInternal), toastDuration)

        setToasts(toasts => [...toasts, toastInternal])
    }

    const handleRemoveToast = (toast: ToastInternal) => {
        setToasts(toasts => toasts.filter(t => t !== toast))
    }

    return (
        <div className="my-toast-container">
            {toasts.map(toast => <Toast key={toast.id}>{toast.toast.content}</Toast>)}
        </div>
    )
}