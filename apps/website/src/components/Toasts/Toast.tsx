import { ChildrenProps } from "../../common/common.types";

export default function Toast({children}: ChildrenProps) {
    return (
        <div className="toast">
            {children}
        </div>
    )
}