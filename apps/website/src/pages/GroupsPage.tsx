import { Link, useParams } from "react-router-dom"
import BrowseGroups from "../features/Groups/BrowseGroups"
import UsersGroups from "../features/Groups/UsersGroups";
import CreateGroupForm from "../features/Groups/CreateGroupForm";
import NavigationButtons from "../components/NavigationButtons";

export default function GroupsPage() {
    const {state} = useParams()

    const setPage = () => {
        switch (state) {
            case 'create': return <CreateGroupForm/>;
            case 'browse': return <BrowseGroups/>;
            default: return <UsersGroups/>;
        }
    }

    return (
        <>
        <NavigationButtons>
            <Link to={'/groups'} className="btn btn-primary">My Groups</Link>
            <Link to={'/groups/create'} className="btn btn-success">Create</Link>
            <Link to={'/groups/browse'} className="btn btn-secondary">Browse</Link>
        </NavigationButtons>
        {setPage()}
        </>
    )
}