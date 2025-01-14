import { useEffect, useState } from "react"
import { useNotifications } from "../notifications/NotificationProvider"
import { ToastType } from "../components/Toasts/Toast.types"

export default function ChatPage() {
    const [count, setCount] = useState(0)
    const {publish} = useNotifications()

    useEffect(() => {
        const toast: ToastType = {content: <Cool count={count}/>}

        publish('toast', toast)
    }, [count, publish])

    const handleClick = () => {
        setCount(c => c += 1)
    }

    return (
        <div>
            Chat Page
            <button onClick={handleClick} className="btn btn-primary">Count: {count}</button>
        </div>
    )
}

interface CoolProps { count: number }
const Cool = ({count}: CoolProps) => {
    return (
        <h1>{count}</h1>
    )
}