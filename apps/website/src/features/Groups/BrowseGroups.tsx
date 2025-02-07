import { useQuery } from "@tanstack/react-query";
import { useSession } from "../../hooks/useSession";
import { getJoinableGroups, joinGroup } from "../../endpoints/groupEndpoints";
import WaitForQuery from "../../components/WaitForQuery";
import GroupCard from "./GroupCard";
import { DetailedGroupInformationDto } from "./Group.types";
import { useNavigate } from "react-router-dom";
import { Button } from "../../components/Button";

export default function BrowseGroups() {
    const {user} = useSession()
    const navigate = useNavigate()

    const query = useQuery({
        queryKey: ['groups', 'join', user.id],
        queryFn: () => getJoinableGroups(user.id)
    })

    const handleJoin = async (group: DetailedGroupInformationDto) => {
        const response = await joinGroup(user.id, group.info.id)
        if (response.status === 200) {
            navigate(`/groups/${group.info.id}`)
        }
    }

    return (
        <div className="container">
            <div className="row">
            <WaitForQuery query={query}>
                {query.data?.map(group => ( 
                    <div className="col-auto">
                        <GroupCard key={group.info.id} group={group}>
                            <Button color={'success'} onClick={() => handleJoin(group)} loadingText={`Joining ${group.info.name}`}>
                                Join
                            </Button>
                        </GroupCard>
                    </div>
                ))}
            </WaitForQuery>
            </div>
        </div>
    )
}