import { useNavigate } from "react-router-dom"
import { basicLogin } from "../endpoints/authEndpoints"
import { UserCredentials } from "../features/Users/Users.types"
import { useAuth } from "../state/authentication/authenticationStore"

export const useLogin = (onUnauthorized?: () => void) => {
    const {authenticate} = useAuth()
    const navigate = useNavigate()

    return async (user: UserCredentials) => {
        await basicLogin(user)
            .then(authUser => {
                authenticate(authUser)
                navigate('/')
            })
            .catch(() => {
                onUnauthorized?.()
            })
    }
}