import { BrowserRouter, Route, Routes } from "react-router-dom";
import Layout from "./layout/Layout";
import HomePage from "./pages/HomePage";
import ChatPage from "./pages/ChatPage";
import LoginPage from "./pages/LoginPage";
import GroupsPage from "./pages/GroupsPage";
import AuthenticatedRoute from "./components/Routing/AuthenticatedRoute";

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout/>}>
          <AuthenticatedRoute index element={<HomePage/>}/>
          <AuthenticatedRoute path="chat" element={<ChatPage/>}/>
          <AuthenticatedRoute path="groups" element={<GroupsPage/>}/>
        </Route>
        <Route path="/login" element={<LoginPage/>}/>
      </Routes>
    </BrowserRouter>
  )
}