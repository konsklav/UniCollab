type CommonUrls = {
    api: string
}

const url = import.meta.env.VITE_API_ROUTE;
if (!url) {
    throw new Error('App requires the API_ROUTE environment variable to be set!')
}

export const commonUrls: CommonUrls = {
    api: url
}