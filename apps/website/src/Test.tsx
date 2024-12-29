import { useEffect, useState } from "react"
import './fun.css'

export default function Test() {
    const [text, setText] = useState<string>()
    const [number, setNumber] = useState<number>(-1)

    useEffect(() => {
        fetch('http://localhost:4000/test')
            .then(res => res.text())
            .then(data => setText(_ => data))
    }, [])

    function getNumber() {
        fetch('http://localhost:4000/number')
        .then(res => res.text())
        .then(data => setNumber(_ => parseInt(data, 10)))
    }

    return <>
        <button className="btn btn-success" onClick={getNumber}>
            Get Random Number
        </button>
        <div className="showcase">
            <strong>{text}</strong>
            <div className="number">{number}</div>
        </div>
    </>
}