import { create } from "zustand";
import { UserInformation } from "../../features/Users/Users.types";
import { AuthenticatedUser } from "../../common/common.types";

export type UniCollabAuthenticationMethod = 'Basic' | 'Google'

interface AuthenticationStore {
    user: UserInformation | undefined
    token: string | undefined
    authenticate: (user: AuthenticatedUser) => void
    invalidate: () => void
    isAuthenticated: () => boolean
}
export const useAuth = create<AuthenticationStore>(
        (set, get) => ({
            user: undefined,
            token: undefined,
            authenticate: (authUser: AuthenticatedUser) => {
                return set({
                    token: authUser.token,
                    user: authUser.user
                });
            },
            invalidate: () => set({
                token: undefined,
                user: undefined
            }),
            isAuthenticated: () => get().token !== undefined && get().user !== undefined})
)