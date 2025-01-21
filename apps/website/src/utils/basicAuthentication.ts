import { UserCredentials } from "../features/Users/Users.types"
import { Buffer } from "buffer"

export const createBasicAuthToken = ({username, password}: UserCredentials) => {
    const tokenBytes = Buffer.from(`${username}:${password}`)
    const b64Token = tokenBytes.toString('base64')

    return `Basic ${b64Token}`
}