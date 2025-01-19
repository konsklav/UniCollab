import { commonUrls } from "../common/common.urls";

export const uniFetch = (route: string, init?: RequestInit) => {
    const fixedRoute = route.endsWith('/') ? route : route + '/'
    return fetch(`${commonUrls.api}${fixedRoute}`, init);
} 