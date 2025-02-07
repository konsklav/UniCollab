import Select from "react-select";
import { UserInformation } from "../../features/Users/Users.types";

interface UserSelectProps {
    onChange: (users: readonly UserInformation[]) => void
    options?: readonly UserInformation[]
}

export default function UserSelect({onChange, options}: UserSelectProps) {
    return (
        <Select 
            isMulti
            defaultValue={new Array<UserInformation>()}
            getOptionLabel={u => u.username}
            getOptionValue={u => u.id}
            onChange={onChange}
            options={options}/>
    )
}