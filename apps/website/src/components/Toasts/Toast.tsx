import { ChildrenProps } from "../../common/common.types";

export default function Toast({children}: ChildrenProps) {
    return (
        <div className="my-toast">
            {children}
        </div>
    )
}