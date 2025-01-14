import { ChangeEvent } from "react";
import { InputProps } from "./inputs.types";
import { generateUniqueId } from "../../utils/uniqueId";

import './inputs.css'

export default function InputText({value, onChange, placeholder, label, id}: InputProps<string>) {
    const inputId: string = id ?? generateUniqueId()

    function handleInput(event: ChangeEvent<HTMLInputElement>): void {
        onChange(event.target.value)
    }

    return (
        <div className="form-input-group">
            {label !== undefined && <label className="form-label" htmlFor={inputId}>{label}</label>}
            <input  type="text" 
                    placeholder={placeholder}
                    id={inputId} 
                    value={value} 
                    onChange={handleInput}/>
        </div>
    )
}