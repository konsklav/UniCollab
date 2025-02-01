import { UseQueryResult } from "@tanstack/react-query"
import Loading from "./Loading"
import DisplayError from "./DisplayError"

interface WaitForApiProps<TData> {
    children: React.ReactNode
    query: UseQueryResult<TData, Error>
}

export default function WaitForQuery<T>({children, query}: WaitForApiProps<T>) {
    if (query.isPending) return <Loading/>
    if (query.isError) return <DisplayError error={query.error}/>
    if (query.isSuccess) return <>{children}</>

    return <>Invalid State!</>
}