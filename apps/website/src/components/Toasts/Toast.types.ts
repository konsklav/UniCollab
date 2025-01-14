export interface ReadToastContext {
    onToastShown: (event: ToastShowEvent) => void
}

export interface WriteToastContext {
    showToast: (toast: Toast) => void
}

export interface ToastShowEvent {
    toast: Toast
}

export interface Toast {
    content: React.ReactNode
}