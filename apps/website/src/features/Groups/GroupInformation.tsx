import { useQuery } from "@tanstack/react-query";
import SimpleUserCard from "../Friends/SimpleUserCard";
import { useParams } from "react-router-dom";
import { validate } from "uuid";
import { getGroupById } from "../../endpoints/groupEndpoints";
import WaitForQuery from "../../components/WaitForQuery";

export default function GroupDisplay() {
    const {state} = useParams()

    if (state === undefined || !validate(state))
        return (
            <div className="text-danger p-4">
                <h1>Invalid Group ID!</h1>
            </div>
        )

    const query = useQuery({
        queryKey: ['group', state],
        queryFn: () => getGroupById(state)
    })

    return (
        <WaitForQuery query={query}>
            <h2>{query.data?.name}</h2>
            <h5 className="text-black-50">Created by: {query.data?.creator?.username ?? '???'}</h5>
            <div className="container mt-3">
                <div className="row">
                    {query.data?.members.map(user => (
                        <div className="col-auto">
                            <SimpleUserCard key={user.id} user={user}/>
                        </div>
                    ))}
                </div>
            </div>
        </WaitForQuery>
    )
}