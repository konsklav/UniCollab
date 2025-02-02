import { ChangeEvent, useRef } from "react";
import { InputProps } from "./inputs.types";
import { generateUniqueId } from "../../utils/uniqueId";

import styles from './inputs.module.css'

export default function InputText({value, onChange, placeholder, label, id, className}: InputProps<string>) {
    const inputId = useRef(id ?? generateUniqueId())

    function handleInput(event: ChangeEvent<HTMLInputElement>): void {
        onChange(event.target.value)
    }

    return (
        <div className={`${styles["form-input-group"]} ${className}`}>
            {label !== undefined && <label className={styles["form-label"]} htmlFor={inputId.current}>{label}</label>}
            <input  type="text" 
                    className="form-control"
                    placeholder={placeholder}
                    id={inputId.current} 
                    value={value} 
                    onChange={handleInput}/>
        </div>
    )
}