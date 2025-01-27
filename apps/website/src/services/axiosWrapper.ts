import axios, { AxiosInstance, AxiosRequestConfig, AxiosResponse } from "axios"
import { commonUrls } from "../common/common.urls"
import { useAuth } from "../state/authentication/authenticationStore"

export class AxiosWrapper {
    public get: (route: string) => Promise<AxiosResponse<any, any>>
    public post: (route: string, data?: any) => Promise<AxiosResponse<any, any>>
    public put: (route: string, data?: any) => Promise<AxiosResponse<any, any>>
    public delete: (route: string) => Promise<AxiosResponse<any, any>>

    private baseAxios: AxiosInstance

    constructor() {
        this.baseAxios = axios.create({
            baseURL: commonUrls.api,
            headers: {
                'Content-Type': 'application/json'
            }
        })

        this.get = (route: string) => {
            return this.baseAxios.get(route, this.getConfig())
        }

        this.post = (route: string, data?: any) => {
            return this.baseAxios.post(route, data, this.getConfig())
        }

        this.put = (route: string, data?: any) => {
            return this.baseAxios.put(route, data, this.getConfig())
        }

        this.delete = (route: string) => {
            return this.baseAxios.delete(route, this.getConfig())
        }
    }

    private getConfig(): AxiosRequestConfig<any> | undefined  {
        const { token } = useAuth.getState()
        if (!token)
            return { }

        return { headers: { 'Authorization': token }}
    }
}