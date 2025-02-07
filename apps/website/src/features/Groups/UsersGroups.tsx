import { useQuery, useQueryClient } from "@tanstack/react-query";
import { useSession } from "../../hooks/useSession";
import { getUserGroups, leaveGroup } from "../../endpoints/groupEndpoints";
import { DetailedGroupInformationDto } from "./Group.types";
import WaitForQuery from "../../components/WaitForQuery";
import GroupCard from "./GroupCard";
import { Button } from "../../components/Button";
import { Link } from "react-router-dom";

export default function UsersGroups() {
    const {user} = useSession()

    const queryKey = ['groups', 'participate', user.id]
    const queryClient = useQueryClient()
    const query = useQuery({
        queryKey: queryKey,
        queryFn: () => getUserGroups(user.id) 
    })

    const handleLeave = async (group: DetailedGroupInformationDto) => {
        const response = await leaveGroup(user.id, group.info.id)
        if (response.status === 200) {
            queryClient.invalidateQueries({queryKey: queryKey})
        }
    }

    return (
        <div className="container">
            <div className="row">
                <WaitForQuery query={query}>
                    {query.data?.map(group => (
                        <div className="col-auto">
                        <GroupCard key={group.info.id} group={group}>
                            <div className="d-flex gap-2 align-items-center">
                                <Button color={'danger'} onClick={() => handleLeave(group)} loadingText={`Leaving ${group.info.name}...`}>
                                    Leave
                                </Button>
                                <Link to={`/groups/${group.info.id}`} className="btn btn-primary">
                                    Open
                                </Link>
                            </div>
                        </GroupCard>
                        </div>
                    ))}
                </WaitForQuery>
            </div>
        </div>
    )
}