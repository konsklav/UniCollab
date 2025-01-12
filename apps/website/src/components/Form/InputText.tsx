import { ChangeEvent, useState } from "react";
import { InputProps } from "./inputs.types";

export default function InputText({onChange, placeholder, label, id}: InputProps<string>) {
    const [value, setValue] = useState('')

    const inputId: string = id ?? crypto.randomUUID()

    function handleInput(event: ChangeEvent<HTMLInputElement>): void {
        const newValue = event.target.value
        setValue(_ => newValue)
        onChange(newValue)
    }

    return (
        <div>
            {label !== undefined && <label className="form-label" htmlFor={inputId}>{label}</label>}
            <input  type="text" 
                    placeholder={placeholder}
                    id={inputId} 
                    value={value} 
                    onChange={handleInput}/>
        </div>
    )
}