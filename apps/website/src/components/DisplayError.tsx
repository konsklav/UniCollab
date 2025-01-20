export default function DisplayError({error}: {error: Error}) {
    return (
        <div className="p-2 text-danger">
            <div className="fs-5">{error.name}</div>
            <div className="fs-6">{error.message}</div>
        </div>
    )
}