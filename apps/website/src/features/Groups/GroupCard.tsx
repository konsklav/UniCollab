import { DetailedGroupInformationDto } from "./Group.types";

interface GroupCardProps {
    children: React.ReactNode,
    group: DetailedGroupInformationDto
}

export default function GroupCard({group, children}: GroupCardProps) {

    const friends = group.friendsInGroup

    return (
        <div className="card">
            <div className="card-header">
                <h2 className="fw-bold">{group.info.name}</h2>
            </div>
            <div className="card-body d-flex flex-column">
                <div>Created By: <strong>{group.info.creator 
                    ? group.info.creator.username
                    : '???'}</strong>
                </div>
                <div> Members: <strong>{group.info.memberCount}</strong> ({friends.length} friends)</div>
            </div>
            <div className="card-footer"> {children} </div>
        </div>
    )
}