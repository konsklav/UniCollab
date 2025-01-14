import { useEffect, useState } from "react"
import { ToastType } from "./Toast.types"
import { useNotifications } from "../../notifications/NotificationProvider"

import './toasts.css'
import Toast from "./Toast"

interface ToastInternal {
    toast: ToastType
    timeShown: number
}

const toastDuration = 1000
const toastDisappearDuration = 500

export function ToastContainer() {
    const [toasts, setToasts] = useState<readonly ToastInternal[]>([])
    const {subscribe} = useNotifications()

    useEffect(() => {
        const unsubscribe = subscribe('toast', {
            callback: e => handleShowToast(e.payload)
        })        

        return () => unsubscribe()
    }, [])

    const handleShowToast = (toast: ToastType) => {
        const time = new Date().getTime()
        const toastInternal: ToastInternal = {
            toast: toast,
            timeShown: time 
        } 

        setTimeout(() => handleRemoveToast(toastInternal), toastDuration)

        setToasts(toasts => [...toasts, toastInternal])
    }

    const handleRemoveToast = (toast: ToastInternal) => {
        setToasts(toasts => toasts.filter(t => t !== toast))
    }

    return (
        <div className="toast-container">
            {toasts.map(toast => <Toast>{toast.toast.content}</Toast>)}
        </div>
    )
}