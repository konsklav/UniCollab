import { create } from "zustand";
import { UserCredentials } from "../../features/Users/Users.types";
import { persist } from "zustand/middleware";

interface AuthenticationStore {
    user: UserCredentials | undefined
    isAuthenticated: () => boolean
    login: (credentials: UserCredentials) => void
    logout: () => void
}

export const useAuth = create<AuthenticationStore>()(
    persist(
        (set, get) => ({
            user: undefined,
            isAuthenticated: () => get().user !== undefined,
            login: (credentials: UserCredentials) => set({ user: credentials }),
            logout: () => set({user: undefined})}),
        {
            name: 'auth-storage'
        }
    )
)