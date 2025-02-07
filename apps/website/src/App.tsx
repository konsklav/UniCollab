import { BrowserRouter, Route, Routes } from "react-router-dom";
import Layout from "./layout/Layout";
import HomePage from "./pages/HomePage";
import ChatPage from "./pages/ChatPage";
import LoginPage from "./pages/Auth/LoginPage";
import GroupsPage from "./pages/GroupsPage";
import AuthenticatedRoute from "./components/Routing/AuthenticatedRoute";
import GuestLayout from "./layout/GuestLayout";
import RegisterPage from "./pages/Auth/RegisterPage";
import MyPostsPage from "./pages/MyPostsPage";
import ReadPostPage from "./pages/ReadPostPage";
import { ToastContainer } from "./components/Toasts/ToastContainer";
import NotificationsManager from "./components/Notifications/NotificationsManager";
import UsersPage from "./pages/UsersPage";
import UsersGroups from "./features/Groups/UsersGroups";
import CreateGroupForm from "./features/Groups/CreateGroupForm";
import BrowseGroups from "./features/Groups/BrowseGroups";
import GroupDisplay from "./features/Groups/GroupInformation";

export default function App() {
  return (
    <>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout/>}>
          <Route path="/" element={<AuthenticatedRoute/>}>
            <Route index element={<HomePage/>}/>
            <Route path="chat/:state?" element={<ChatPage/>}/>
            <Route path="users" element={<UsersPage/>}/>
            <Route path="groups" element={<GroupsPage/>}>
              <Route index element={<UsersGroups/>}/>
              <Route path=":state" element={<GroupDisplay/>}/>
              <Route path="create" element={<CreateGroupForm/>}/>
              <Route path="browse" element={<BrowseGroups/>}/>
            </Route>
            <Route path="user/posts/:state?" element={<MyPostsPage/>}/>
            <Route path="posts/:slug" element={<ReadPostPage/>}/>
          </Route>
        </Route>
        <Route path="/" element={<GuestLayout/>}>
          <Route path="login" element={<LoginPage/>}/>
          <Route path="register" element={<RegisterPage/>}/>
        </Route>
      </Routes>
    </BrowserRouter>
    
    <NotificationsManager/>
    <ToastContainer/>
    </>
  )
}