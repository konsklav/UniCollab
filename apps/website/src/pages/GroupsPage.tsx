import { Link, Outlet } from "react-router-dom"
import NavigationButtons from "../components/NavigationButtons";

export default function GroupsPage() {
    return (
        <>
        <NavigationButtons>
            <Link to={'/groups'} className="btn btn-primary">My Groups</Link>
            <Link to={'/groups/create'} className="btn btn-success">Create</Link>
            <Link to={'/groups/browse'} className="btn btn-secondary">Browse</Link>
        </NavigationButtons>
        <div className="container p-2">
            <Outlet/>
        </div>
        </>
    )
}