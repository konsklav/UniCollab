import { HubConnection, HubConnectionBuilder, HubConnectionState } from "@microsoft/signalr";
import { commonUrls } from "../common/common.urls";
import { createBasicAuthToken } from "../utils/basicAuthentication";
import { UserCredentials } from "../features/Users/Users.types";

export default class SignalRService {
    private connection: HubConnection

    constructor(hubName: string, user: UserCredentials) {
        this.connection = new HubConnectionBuilder()
            .withUrl(`${commonUrls.api}/hubs/${hubName}`, {
                headers: {
                    'Authorization': createBasicAuthToken(user)
                }
            })
            .withAutomaticReconnect()
            .build()
    }

    async startConnection() {
        await this.startConnectionCore()
    }

    stopConnection() {
        this.connection.stop()
    }

    private async startConnectionCore() {
        if (this.connection.state === HubConnectionState.Connected || 
            this.connection.state === HubConnectionState.Connecting) {
            return;
        }

        await this.connection.start()
        console.log(`Connected to hub: ${this.connection.baseUrl}`)
    }

    on(methodName: string, callback: (...args: any[]) => void) {
        this.connection.on(methodName, callback)
    }

    off(methodName: string) {
        this.connection.off(methodName)
    }

    async send(methodName: string, ...args: any[]) {
        try { 
            await this.connection.invoke(methodName, args)
        } catch (err) {
            console.error('SignalR method invocation error: ', err)
        }
    }
}