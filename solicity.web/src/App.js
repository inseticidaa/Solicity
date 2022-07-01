import { Routes, Route } from "react-router-dom";
import { ProtectedLayout } from "./components/ProtectedLayout";

// Components
import { AuthProvider } from "./components/AuthProvider";

// Pages
import LoginPage from "./pages/Login";
import HomePage from "./pages/Home";
import IssuesPage from "./pages/Issues";
import IssueCreatePage from "./pages/IssueCreate";
import IssuePage from "./pages/Issue";

import 'bootstrap/dist/css/bootstrap.min.css';
import 'react-toastify/dist/ReactToastify.css';
import 'react-virtualized/styles.css';
import './assets/styles.css';

export default function App() {
  return (
    <AuthProvider>
      <Routes>
        <Route path="/login" element={<LoginPage />} />

        <Route path="/" element={<ProtectedLayout />}>
          <Route path="" element={<HomePage />} />
        </Route>

        <Route path="/issues" element={<ProtectedLayout />}>
          <Route path="" element={<IssuesPage />} />
          <Route path="create" element={<IssueCreatePage />} />
          <Route path=":issueId" element={<IssuePage />} />
        </Route>
      </Routes>
    </AuthProvider>
  );
}