import { BrowserRouter, Route, Routes } from "react-router-dom";
import Layout from "./layout/Layout";
import HomePage from "./pages/HomePage";
import ChatPage from "./pages/ChatPage";
import LoginPage from "./pages/Login/LoginPage";
import GroupsPage from "./pages/GroupsPage";
import AuthenticatedRoute from "./components/Routing/AuthenticatedRoute";
import { AuthenticationProvider } from "./components/Authentication/AuthenticationProvider";

export default function App() {
  return (
    <AuthenticationProvider>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout/>}>
          <Route path="/" element={<AuthenticatedRoute/>}>
            <Route index element={<HomePage/>}/>
            <Route path="chat" element={<ChatPage/>}/>
            <Route path="groups" element={<GroupsPage/>}/>
          </Route>
        </Route>
        <Route path="/login" element={<LoginPage/>}/>
      </Routes>
    </BrowserRouter>
    </AuthenticationProvider>
  )
}