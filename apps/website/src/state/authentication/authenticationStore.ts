import { create } from "zustand";
import { UserCredentials } from "../../features/Users/Users.types";
import { persist } from "zustand/middleware";


interface AuthenticationStore {
    user: UserCredentials | undefined
    authentication: UniCollabAuthenticationMethod | 'None'
    login: (credentials: UserCredentials, method: UniCollabAuthenticationMethod) => void
    logout: () => void
}
export type UniCollabAuthenticationMethod = 'Basic' | 'Google'
export const useAuth = create<AuthenticationStore>()(
    persist(
        (set) => ({
            user: undefined,
            authentication: 'None',
            login: (credentials: UserCredentials, method: UniCollabAuthenticationMethod) => set({ 
                user: credentials,
                authentication: method
            }),
            logout: () => set({
                user: undefined,
                authentication: 'None'
            })}),
        {
            name: 'auth-storage'
        }
    )
)