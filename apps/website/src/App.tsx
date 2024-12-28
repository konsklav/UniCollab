import { useState } from 'react'
import './fun.css'


function App() {
  const [count, setCount] = useState(0)

  return (
    <>
    <div className='container'>
      <h1 className='spin'>SaaS Project, Oh Shiiiit!</h1>
      <button className='btn btn-primary' onClick={() => setCount(ct => ct + 1)}>
        Count = {count}, nice!
      </button>
      </div>
    </>
  )
}

export default App
