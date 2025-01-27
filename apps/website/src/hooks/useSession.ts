import { useAuth } from "../state/authentication/authenticationStore"

export const useSession = () => {
    const user = useAuth(state => state.user)

    if (!user) {
        throw new Error('Cannot useSession() when not authenticated.')
    }

    return {
        user
    }
}