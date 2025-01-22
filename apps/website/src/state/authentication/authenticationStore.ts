import { create } from "zustand";
import { UserCredentials, UserInformation } from "../../features/Users/Users.types";
import { persist } from "zustand/middleware";

export type UniCollabAuthenticationMethod = 'Basic' | 'Google'

interface AuthenticationStore {
    user: UserInformation | undefined
    credentials: UserCredentials | undefined
    authentication: UniCollabAuthenticationMethod | 'None'
    login: (credentials: UserCredentials, method: UniCollabAuthenticationMethod) => void
    logout: () => void,
    isAuthenticated: () => boolean
}
export const useAuth = create<AuthenticationStore>()(
    persist(
        (set, get) => ({
            user: undefined,
            credentials: undefined,
            authentication: 'None',
            login: (credentials: UserCredentials, method: UniCollabAuthenticationMethod) => set({ 
                credentials: credentials,
                authentication: method
            }),
            logout: () => set({
                credentials: undefined,
                authentication: 'None'
            }),
            isAuthenticated: () => get().authentication !== 'None' && get().credentials !== undefined}),
        {
            name: 'auth-storage'
        }
    )
)