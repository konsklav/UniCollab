import { useNavigate } from "react-router-dom"
import { basicLogin, googleLogin } from "../endpoints/authEndpoints"
import { UserCredentials } from "../features/Users/Users.types"
import { useAuth } from "../state/authentication/authenticationStore"
import { GoogleCredentialResponse } from "@react-oauth/google"
import { AuthenticatedUser } from "../common/common.types"

interface LoginCallbacks {
    basicLogin: (user: UserCredentials) => Promise<void>
    googleLogin: (token: GoogleCredentialResponse) => Promise<void>
}

export const useLogin = (onUnauthorized?: () => void): LoginCallbacks => {
    const {authenticate} = useAuth()
    const navigate = useNavigate()

    const succeedAuth = (authUser: AuthenticatedUser) => {
        authenticate(authUser)
        navigate('/')
    }

    return {
        basicLogin: async user => {
            return basicLogin(user)
            .then(succeedAuth)
            .catch(() => onUnauthorized?.())
        },
        googleLogin: async token => {
            return googleLogin(token)
            .then(succeedAuth)
            .catch(() => onUnauthorized?.())
        }
    }
}