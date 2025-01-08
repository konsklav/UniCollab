import { BrowserRouter, Route, Routes } from "react-router-dom";
import Layout from "./layout/Layout";
import HomePage from "./pages/HomePage";
import ChatPage from "./pages/ChatPage";
import LoginPage from "./pages/LoginPage";

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout/>}>
          <Route index element={<HomePage/>}/>
          <Route path="chat" element={<ChatPage/>}/>
        </Route>
        <Route path="/login" element={<LoginPage/>}/>
      </Routes>
    </BrowserRouter>
  )
}