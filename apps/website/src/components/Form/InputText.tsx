import { ChangeEvent, useMemo, useRef } from "react";
import { InputProps } from "./inputs.types";
import { generateUniqueId } from "../../utils/uniqueId";

import './inputs.css'

export default function InputText({value, onChange, placeholder, label, id}: InputProps<string>) {
    const inputId = useRef(id ?? generateUniqueId())

    function handleInput(event: ChangeEvent<HTMLInputElement>): void {
        onChange(event.target.value)
    }

    return (
        <div className="form-input-group">
            {label !== undefined && <label className="form-label" htmlFor={inputId.current}>{label}</label>}
            <input  type="text" 
                    placeholder={placeholder}
                    id={inputId.current} 
                    value={value} 
                    onChange={handleInput}/>
        </div>
    )
}